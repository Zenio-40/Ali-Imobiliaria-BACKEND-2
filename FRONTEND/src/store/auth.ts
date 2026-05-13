import { create } from "zustand";
import { persist } from "zustand/middleware";
import type { Role, User } from "@/lib/types";
import { apiPost, apiPut, setBearerToken } from "@/lib/api";

interface AuthState {
  hasHydrated: boolean;
  user: User | null;
  token: string | null;
  favorites: string[];
  login: (telefone: string, senha: string) => Promise<User | null>;
  loginCliente: (emailOuTelefone: string, senha: string) => Promise<User | null>;
  register: (data: { name: string; email: string; phone: string; password: string }) => Promise<User | null>;
  changePassword: (data: { emailOrPhone: string; currentPassword: string; newPassword: string }) => Promise<boolean>;
  logout: () => void;
  toggleFavorite: (id: string) => void;
  setHasHydrated: (value: boolean) => void;
}

type LoginDados = {
  usuarioId?: number | string;
  UsuarioId?: number | string;
  nome?: string;
  Nome?: string;
  email?: string;
  Email?: string;
  telefone?: string;
  Telefone?: string;
  perfil?: string;
  Perfil?: string;
  token?: string;
  Token?: string;
};

type LoginEnvelope = {
  dados?: LoginDados;
  mensagem?: string;
  codigo?: number;
  token?: string;
  Token?: string;
};

function roleFromPerfil(perfil: string | undefined): Role {
  const p = (perfil ?? "").toLowerCase();
  if (p.includes("admin") || p.includes("director") || p.includes("diretor")) return "admin";
  if (p.includes("cliente")) return "cliente";
  return "corretor";
}

function userFromEnvelope(envelope: LoginEnvelope, fallbackPhone: string, fallbackRole: Role): { user: User; token: string } | null {
  const dados = envelope.dados ?? {};
  const token = envelope.token ?? envelope.Token ?? dados.token ?? dados.Token ?? null;
  if (!token) return null;

  const perfil = dados.perfil ?? dados.Perfil;
  const role = perfil ? roleFromPerfil(String(perfil)) : fallbackRole;

  const id = dados.usuarioId ?? dados.UsuarioId ?? Date.now();
  const name = dados.nome ?? dados.Nome ?? "Utilizador";
  const email = dados.email ?? dados.Email ?? "";
  const phone = dados.telefone ?? dados.Telefone ?? fallbackPhone;

  return {
    token,
    user: {
      id: String(id),
      name: String(name),
      email: String(email),
      phone: String(phone),
      role,
    },
  };
}

export const useAuth = create<AuthState>()(
  persist(
    (set, get) => ({
      hasHydrated: false,
      user: null,
      token: null,
      favorites: [],
      setHasHydrated: (value) => set({ hasHydrated: value }),

      login: async (telefone, senha) => {
        try {
          const envelope = await apiPost<LoginEnvelope>(
            "/api/auth/login/funcionario",
            { Telefone: telefone, Senha: senha },
            false,
          );
          console.log("login funcionario envelope:", envelope);


          const auth = userFromEnvelope(envelope, telefone, "corretor");
          if (!auth) {
            set({ user: null, token: null });
            return null;
          }

          set({ user: auth.user, token: auth.token });
          setBearerToken(auth.token);
          return auth.user;
        } catch {
          set({ user: null, token: null });
          setBearerToken(null);
          return null;
        }
      },

      loginCliente: async (emailOuTelefone, senha) => {
        try {
          const envelope = await apiPost<LoginEnvelope>(
            "/api/auth/login/cliente",
            { EmailOuTelefone: emailOuTelefone, Senha: senha },
            false,
          );

          const auth = userFromEnvelope(envelope, emailOuTelefone, "cliente");
          if (!auth) {
            set({ user: null, token: null });
            return null;
          }

          set({ user: auth.user, token: auth.token });
          setBearerToken(auth.token);
          return auth.user;
        } catch {
          set({ user: null, token: null });
          setBearerToken(null);
          return null;
        }
      },

      register: async (data) => {
        try {
          await apiPost(
            "/api/cliente",
            {
              ClienteNome: data.name,
              ClienteEmail: data.email,
              ClienteTelefone: data.phone,
              ClienteSenha: data.password,
              ClienteIdPerfil: 3,
            },
            false,
          );

          return await get().loginCliente(data.email, data.password);
        } catch {
          return null;
        }
      },

      changePassword: async (data) => {
        try {
          await apiPut(
            "/api/auth/cliente/repor-senha",
            {
              EmailOuTelefone: data.emailOrPhone,
              SenhaAtual: data.currentPassword,
              NovaSenha: data.newPassword,
            },
            true,
          );
          return true;
        } catch {
          return false;
        }
      },

      logout: () => {
        set({ user: null, token: null });
        setBearerToken(null);
      },

      toggleFavorite: (id) => {
        const favs = get().favorites;
        set({
          favorites: favs.includes(id) ? favs.filter((f) => f !== id) : [...favs, id],
        });
      },
    }),
    {
      name: "ali-auth",
      skipHydration: true,
      onRehydrateStorage: () => (state) => {
        state?.setHasHydrated(true);
        setBearerToken(state?.token ?? null);
      },
    },
  ),
);
