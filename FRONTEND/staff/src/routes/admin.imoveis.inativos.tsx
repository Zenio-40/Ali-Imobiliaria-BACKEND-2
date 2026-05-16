import { createFileRoute, Navigate, Link } from "@tanstack/react-router";
import { useMemo, useState } from "react";
import {
  LayoutDashboard,
  Building2,
  Users as UsersIcon,
  Archive,
  Power,
  Trash2,
  Search,
  ChevronLeft,
  ChevronRight,
  ArrowUpDown,
} from "lucide-react";
import { DashboardLayout } from "@/components/DashboardLayout";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Input } from "@/components/ui/input";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import {
  AlertDialog,
  AlertDialogAction,
  AlertDialogCancel,
  AlertDialogContent,
  AlertDialogDescription,
  AlertDialogFooter,
  AlertDialogHeader,
  AlertDialogTitle,
} from "@/components/ui/alert-dialog";
import { useAuth } from "@/store/auth";
import { useProperties } from "@/store/properties";
import { users, formatPrice } from "@/lib/mock-data";
import type { Property } from "@/lib/types";
import { toast } from "sonner";

const nav = [
  { to: "/admin", label: "Visão geral", icon: LayoutDashboard },
  { to: "/admin/imoveis", label: "Imóveis", icon: Building2 },
  { to: "/admin/imoveis/inativos", label: "Inativos", icon: Archive },
  { to: "/admin/utilizadores", label: "Utilizadores", icon: UsersIcon },
];

const PAGE_SIZE_OPTIONS = [8, 16, 24] as const;
type PageSize = (typeof PAGE_SIZE_OPTIONS)[number];

const SORT_OPTIONS = [
  { value: "recent", label: "Mais recentes" },
  { value: "oldest", label: "Mais antigos" },
  { value: "price_asc", label: "Preço: menor para maior" },
  { value: "price_desc", label: "Preço: maior para menor" },
  { value: "location_asc", label: "Localização (A–Z)" },
  { value: "location_desc", label: "Localização (Z–A)" },
] as const;
type SortKey = (typeof SORT_OPTIONS)[number]["value"];

export const Route = createFileRoute("/admin/imoveis/inativos")({
  head: () => ({ meta: [{ title: "Imóveis inativos — Ali-Imobiliária" }] }),
  component: AdminInactivePropertiesPage,
});

function AdminInactivePropertiesPage() {
  const { user } = useAuth();
  const items = useProperties((s) => s.items);
  const setStatus = useProperties((s) => s.setStatus);
  const remove = useProperties((s) => s.remove);

  const [q, setQ] = useState("");
  const [page, setPage] = useState(1);
  const [pageSize, setPageSize] = useState<PageSize>(8);
  const [sort, setSort] = useState<SortKey>("recent");
  const [confirmDelete, setConfirmDelete] = useState<Property | null>(null);

  const inactive = useMemo(
    () => items.filter((p) => p.status === "inativo"),
    [items],
  );

  const filtered = useMemo(() => {
    const term = q.trim().toLowerCase();
    const base = term
      ? inactive.filter(
          (p) =>
            p.title.toLowerCase().includes(term) ||
            p.location.toLowerCase().includes(term),
        )
      : inactive;

    const sorted = [...base];
    switch (sort) {
      case "recent":
        sorted.sort((a, b) => b.createdAt.localeCompare(a.createdAt));
        break;
      case "oldest":
        sorted.sort((a, b) => a.createdAt.localeCompare(b.createdAt));
        break;
      case "price_asc":
        sorted.sort((a, b) => a.price - b.price);
        break;
      case "price_desc":
        sorted.sort((a, b) => b.price - a.price);
        break;
      case "location_asc":
        sorted.sort((a, b) => a.location.localeCompare(b.location, "pt"));
        break;
      case "location_desc":
        sorted.sort((a, b) => b.location.localeCompare(a.location, "pt"));
        break;
    }
    return sorted;
  }, [inactive, q, sort]);

  const totalPages = Math.max(1, Math.ceil(filtered.length / pageSize));
  const currentPage = Math.min(page, totalPages);
  const paged = filtered.slice(
    (currentPage - 1) * pageSize,
    currentPage * pageSize,
  );

  const pageNumbers = useMemo(() => {
    // Compact pagination: always show first, last, current ±1, with ellipses
    const pages: (number | "…")[] = [];
    const add = (n: number | "…") => pages.push(n);
    if (totalPages <= 7) {
      for (let i = 1; i <= totalPages; i++) add(i);
      return pages;
    }
    add(1);
    const start = Math.max(2, currentPage - 1);
    const end = Math.min(totalPages - 1, currentPage + 1);
    if (start > 2) add("…");
    for (let i = start; i <= end; i++) add(i);
    if (end < totalPages - 1) add("…");
    add(totalPages);
    return pages;
  }, [totalPages, currentPage]);

if (!user) return <Navigate to="/login" />;

  const handleReactivate = (p: Property) => {
    setStatus(p.id, "disponivel");
    toast.success(`"${p.title}" foi reativado.`);
  };

  const handleDelete = () => {
    if (!confirmDelete) return;
    remove(confirmDelete.id);
    toast.success(`"${confirmDelete.title}" foi excluído.`);
    setConfirmDelete(null);
  };

  return (
    <DashboardLayout title="Imóveis inativos" nav={nav}>
      <div className="mb-6 flex flex-wrap items-center justify-between gap-3">
        <div>
          <p className="text-sm text-muted-foreground">
            {inactive.length} imóvel(is) desativado(s) — não aparecem no catálogo público.
          </p>
        </div>
        <div className="flex flex-1 flex-wrap items-center justify-end gap-2">
          <div className="flex flex-1 items-center gap-2 min-w-[220px] max-w-md rounded-xl border border-border bg-card px-3">
            <Search className="h-4 w-4 text-muted-foreground" />
            <Input
              value={q}
              onChange={(e) => {
                setQ(e.target.value);
                setPage(1);
              }}
              placeholder="Pesquisar por título ou localização"
              className="border-0 bg-transparent shadow-none focus-visible:ring-0"
            />
          </div>
          <div className="flex items-center gap-2">
            <ArrowUpDown className="h-4 w-4 text-muted-foreground" />
            <Select
              value={sort}
              onValueChange={(v) => {
                setSort(v as SortKey);
                setPage(1);
              }}
            >
              <SelectTrigger className="h-10 w-[220px]">
                <SelectValue placeholder="Ordenar por" />
              </SelectTrigger>
              <SelectContent>
                {SORT_OPTIONS.map((o) => (
                  <SelectItem key={o.value} value={o.value}>{o.label}</SelectItem>
                ))}
              </SelectContent>
            </Select>
          </div>
        </div>
      </div>

      <div className="overflow-hidden rounded-2xl border border-border bg-card shadow-[var(--shadow-card)]">
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead className="bg-muted text-left text-xs uppercase tracking-wider text-muted-foreground">
              <tr>
                <th className="p-4">Imóvel</th>
                <th className="p-4">Tipo</th>
                <th className="p-4">Preço</th>
                <th className="p-4">Corretor</th>
                <th className="p-4">Estado</th>
                <th className="p-4 text-right">Ações</th>
              </tr>
            </thead>
            <tbody>
              {paged.map((p) => {
                const broker = users.find((u) => u.id === p.brokerId);
                return (
                  <tr key={p.id} className="border-t border-border opacity-80">
                    <td className="p-4">
                      <div className="flex items-center gap-3">
                        <img src={p.image} alt="" className="h-12 w-16 rounded-md object-cover grayscale" />
                        <div>
                          <Link
                            to="/imoveis/$id"
                            params={{ id: p.id }}
                            className="font-medium hover:text-primary line-clamp-1"
                          >
                            {p.title}
                          </Link>
                          <div className="text-xs text-muted-foreground">{p.location}</div>
                        </div>
                      </div>
                    </td>
                    <td className="p-4 capitalize text-muted-foreground">{p.type}</td>
                    <td className="p-4 font-medium">{formatPrice(p.price, p.purpose)}</td>
                    <td className="p-4 text-muted-foreground">{broker?.name ?? "—"}</td>
                    <td className="p-4">
                      <Badge variant="outline" className="capitalize">inativo</Badge>
                    </td>
                    <td className="p-4">
                      <div className="flex justify-end gap-1">
                        <Button
                          size="sm"
                          variant="ghost"
                          onClick={() => handleReactivate(p)}
                          title="Reativar"
                        >
                          <Power className="h-4 w-4 text-success" />
                        </Button>
                        <Button
                          size="sm"
                          variant="ghost"
                          onClick={() => setConfirmDelete(p)}
                          title="Excluir definitivamente"
                        >
                          <Trash2 className="h-4 w-4 text-destructive" />
                        </Button>
                      </div>
                    </td>
                  </tr>
                );
              })}
              {paged.length === 0 && (
                <tr>
                  <td colSpan={6} className="p-12 text-center text-muted-foreground">
                    {inactive.length === 0
                      ? "Nenhum imóvel inativo de momento."
                      : "Nenhum resultado para a pesquisa."}
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>

        {filtered.length > 0 && (
          <div className="flex flex-wrap items-center justify-between gap-3 border-t border-border px-4 py-3 text-sm">
            <div className="flex items-center gap-3 text-muted-foreground">
              <span>
                Página {currentPage} de {totalPages} · {filtered.length} resultado(s)
              </span>
              <div className="flex items-center gap-2">
                <span className="text-xs">Por página</span>
                <Select
                  value={String(pageSize)}
                  onValueChange={(v) => {
                    setPageSize(Number(v) as PageSize);
                    setPage(1);
                  }}
                >
                  <SelectTrigger className="h-8 w-[72px]">
                    <SelectValue />
                  </SelectTrigger>
                  <SelectContent>
                    {PAGE_SIZE_OPTIONS.map((n) => (
                      <SelectItem key={n} value={String(n)}>{n}</SelectItem>
                    ))}
                  </SelectContent>
                </Select>
              </div>
            </div>
            <div className="flex flex-wrap items-center gap-1">
              <Button
                variant="outline"
                size="sm"
                onClick={() => setPage((p) => Math.max(1, p - 1))}
                disabled={currentPage === 1}
                aria-label="Página anterior"
              >
                <ChevronLeft className="h-4 w-4" />
              </Button>
              {pageNumbers.map((n, i) =>
                n === "…" ? (
                  <span key={`e${i}`} className="px-2 text-muted-foreground">…</span>
                ) : (
                  <Button
                    key={n}
                    size="sm"
                    variant={n === currentPage ? "default" : "outline"}
                    onClick={() => setPage(n)}
                    className="min-w-9"
                    aria-current={n === currentPage ? "page" : undefined}
                  >
                    {n}
                  </Button>
                ),
              )}
              <Button
                variant="outline"
                size="sm"
                onClick={() => setPage((p) => Math.min(totalPages, p + 1))}
                disabled={currentPage === totalPages}
                aria-label="Página seguinte"
              >
                <ChevronRight className="h-4 w-4" />
              </Button>
            </div>
          </div>
        )}
      </div>

      <AlertDialog open={!!confirmDelete} onOpenChange={(v) => !v && setConfirmDelete(null)}>
        <AlertDialogContent>
          <AlertDialogHeader>
            <AlertDialogTitle>Excluir imóvel definitivamente?</AlertDialogTitle>
            <AlertDialogDescription>
              "{confirmDelete?.title}" será removido permanentemente. Esta ação não pode ser desfeita.
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogCancel>Cancelar</AlertDialogCancel>
            <AlertDialogAction onClick={handleDelete} className="bg-destructive text-destructive-foreground hover:bg-destructive/90">
              Excluir
            </AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </DashboardLayout>
  );
}
