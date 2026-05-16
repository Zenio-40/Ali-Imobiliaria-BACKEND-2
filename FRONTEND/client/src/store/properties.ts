import { useMemo } from "react";
import { create } from "zustand";
import { persist } from "zustand/middleware";
import { properties as seed } from "@/lib/mock-data";
import type { Property, PropertyStatus, PropertyType } from "@/lib/types";
import { apiGet, apiPatch, apiPost, apiPut, unwrapDados } from "@/lib/api";

export type PropertyDraft = Omit<Property, "id" | "createdAt"> & {
  id?: string;
};

type BackendImovel = {
  id?: number;
  Id?: number;
  descricao?: string;
  Descricao?: string;
  preco?: number;
  Preco?: number;
  estado?: boolean;
  Estado?: boolean;
  estadoAprovacao?: string;
  EstadoAprovacao?: string;
  idTipoImovel?: number;
  IdTipoImovel?: number;
  tipoImovel?: string;
  TipoImovel?: string;
  idTipologia?: number;
  IdTipologia?: number;
  tipologia?: string;
  Tipologia?: string;
  idFuncionario?: number;
  IdFuncionario?: number;
  funcionario?: string;
  Funcionario?: string;
  fotos?: string[];
  Fotos?: string[];
  videoUrl?: string;
  VideoUrl?: string;
  provincia?: string;
  Provincia?: string;
  municipio?: string;
  Municipio?: string;
  bairro?: string;
  Bairro?: string;
  endereco?: string;
  Endereco?: string;
};

interface PropertiesState {
  hasHydrated: boolean;
  items: Property[];
  loadFromApi: () => Promise<void>;
  create: (draft: PropertyDraft) => Promise<Property | null>;
  update: (id: string, patch: Partial<Property>) => Promise<void>;
  setStatus: (id: string, status: PropertyStatus) => Promise<void>;
  remove: (id: string) => void;
  reset: () => void;
  setHasHydrated: (value: boolean) => void;
}

function idTipoImovel(type: PropertyType) {
  if (type === "moradia") return 2;
  if (type === "terreno") return 3;
  return 1;
}

function typeFromBackend(tipo: string | undefined, idTipo: number | undefined): PropertyType {
  const t = (tipo ?? "").toLowerCase();
  if (t.includes("terreno") || idTipo === 3) return "terreno";
  if (t.includes("casa") || t.includes("moradia") || idTipo === 2) return "moradia";
  if (t.includes("loja")) return "loja";
  if (t.includes("escrit")) return "escritorio";
  return "apartamento";
}

function statusFromBackend(estado: boolean | undefined, aprovacao: string | undefined): PropertyStatus {
  if (estado === false) return "inativo";
  if ((aprovacao ?? "").toLowerCase() === "reprovado") return "inativo";
  if ((aprovacao ?? "").toLowerCase() === "pendente") return "reservado";
  return "disponivel";
}

function fromBackend(i: BackendImovel): Property {
  const id = i.id ?? i.Id ?? Date.now();
  const descricao = i.descricao ?? i.Descricao ?? "";
  const tipo = i.tipoImovel ?? i.TipoImovel;
  const idTipo = i.idTipoImovel ?? i.IdTipoImovel;
  const fotos = i.fotos ?? i.Fotos ?? [];
  const location = [i.bairro ?? i.Bairro, i.municipio ?? i.Municipio, i.provincia ?? i.Provincia]
    .filter(Boolean)
    .join(", ");

  return {
    id: String(id),
    title: descricao.split(".")[0]?.slice(0, 80) || "Imovel",
    type: typeFromBackend(tipo, idTipo),
    purpose: "venda",
    status: statusFromBackend(i.estado ?? i.Estado, i.estadoAprovacao ?? i.EstadoAprovacao),
    price: Number(i.preco ?? i.Preco ?? 0),
    location: location || i.endereco || i.Endereco || "Localizacao por definir",
    bedrooms: Math.max(0, Number(String(i.tipologia ?? i.Tipologia ?? "T0").replace(/\D/g, "")) || 0),
    bathrooms: 1,
    area: 1,
    image: fotos[0] || seed[0]?.image || "",
    images: fotos,
    videoUrl: i.videoUrl ?? i.VideoUrl,
    approvalStatus: (i.estadoAprovacao ?? i.EstadoAprovacao ?? "Pendente") as Property["approvalStatus"],
    description: descricao,
    brokerId: String(i.idFuncionario ?? i.IdFuncionario ?? ""),
    createdAt: new Date().toISOString().slice(0, 10),
  };
}

function toBackend(draft: PropertyDraft) {
  return {
    Descricao: draft.description || draft.title,
    Preco: draft.price,
    IdTipoImovel: idTipoImovel(draft.type),
    IdTipologia: Math.min(Math.max(draft.bedrooms || 1, 1), 3),
    IdFuncionario: Number(draft.brokerId || 0),
    IdBairro: 1,
    Endereco: draft.location,
    Fotos: draft.images?.length ? draft.images : [draft.image].filter(Boolean),
    VideoUrl: draft.videoUrl || null,
  };
}

export const useProperties = create<PropertiesState>()(
  persist(
    (set, get) => ({
      hasHydrated: false,
      items: seed,
      setHasHydrated: (value) => set({ hasHydrated: value }),
      loadFromApi: async () => {
        try {
          const envelope = await apiGet<BackendImovel[]>("/api/cliente/imoveis");
          const dados = unwrapDados(envelope) ?? [];
          set({ items: dados.map(fromBackend) });
        } catch {
          // Mantem os dados locais quando a API ainda nao estiver ligada.
        }
      },
      create: async (draft) => {
        const local: Property = {
          ...draft,
          id: `p${Date.now()}`,
          createdAt: new Date().toISOString().slice(0, 10),
        };

        try {
          await apiPost("/api/corretora/imoveis", toBackend(draft), true);
          await get().loadFromApi();
          return local;
        } catch {
          set((s) => ({ items: [local, ...s.items] }));
          return local;
        }
      },
      update: async (id, patch) => {
        const current = get().items.find((p) => p.id === id);
        if (!current) return;
        const next = { ...current, ...patch };
        set((s) => ({
          items: s.items.map((p) => (p.id === id ? next : p)),
        }));

        if (/^\d+$/.test(id)) {
          try {
            await apiPut("/api/admin/imoveis", { Id: Number(id), ...toBackend(next), Estado: next.status !== "inativo", EstadoAprovacao: next.approvalStatus ?? "Pendente" }, true);
          } catch {
            // Edicao local fica preservada para nao quebrar a tela.
          }
        }
      },
      setStatus: async (id, status) => {
        set((s) => ({
          items: s.items.map((p) => (p.id === id ? { ...p, status } : p)),
        }));

        if (/^\d+$/.test(id)) {
          try {
            if (status === "disponivel") await apiPatch(`/api/admin/imoveis/${id}/aprovar`, undefined, true);
            if (status === "inativo") await apiPatch(`/api/admin/imoveis/${id}/reprovar`, undefined, true);
          } catch {
            // Estado local fica preservado para manter a operacao fluida.
          }
        }
      },
      remove: (id) =>
        set((s) => ({ items: s.items.filter((p) => p.id !== id) })),
      reset: () => set({ items: seed }),
    }),
    {
      name: "ali-properties",
      version: 2,
      skipHydration: true,
      onRehydrateStorage: () => (state) => {
        state?.setHasHydrated(true);
        void state?.loadFromApi();
      },
    },
  ),
);

export const usePublicProperties = () => {
  const items = useProperties((s) => s.items);
  return useMemo(() => items.filter((p) => p.status !== "inativo"), [items]);
};
