import { createFileRoute, Navigate, Link } from "@tanstack/react-router";
import { useMemo, useState } from "react";
import {
  LayoutDashboard,
  Building2,
  Users as UsersIcon,
  Archive,
  Plus,
  Pencil,
  Power,
  PowerOff,
  Search,
  RotateCcw,
} from "lucide-react";
import { DashboardLayout } from "@/components/DashboardLayout";
import { PropertyFormDialog } from "@/components/PropertyFormDialog";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { Input } from "@/components/ui/input";
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

export const Route = createFileRoute("/admin/imoveis")({
  head: () => ({ meta: [{ title: "Gestão de imóveis — Ali-Imobiliária" }] }),
  component: AdminPropertiesPage,
});

function AdminPropertiesPage() {
  const { user } = useAuth();
  const items = useProperties((s) => s.items);
  const setStatus = useProperties((s) => s.setStatus);
  const reset = useProperties((s) => s.reset);

  const [open, setOpen] = useState(false);
  const [editing, setEditing] = useState<Property | null>(null);
  const [confirm, setConfirm] = useState<Property | null>(null);
  const [q, setQ] = useState("");

  const brokers = useMemo(
    () => users.filter((u) => u.role === "corretor" || u.role === "admin").map(({ id, name }) => ({ id, name })),
    [],
  );

  const filtered = useMemo(
    () =>
      items.filter(
        (p) =>
          q === "" ||
          p.title.toLowerCase().includes(q.toLowerCase()) ||
          p.location.toLowerCase().includes(q.toLowerCase()),
      ),
    [items, q],
  );

if (!user) return <Navigate to="/login" />;

  const handleNew = () => {
    setEditing(null);
    setOpen(true);
  };

  const handleEdit = (p: Property) => {
    setEditing(p);
    setOpen(true);
  };

  const handleToggle = (p: Property) => {
    if (p.status === "inativo") {
      setStatus(p.id, "disponivel");
      toast.success("Imóvel reativado.");
    } else {
      setConfirm(p);
    }
  };

  const confirmDeactivate = () => {
    if (!confirm) return;
    setStatus(confirm.id, "inativo");
    toast.success(`"${confirm.title}" foi desativado.`);
    setConfirm(null);
  };

  return (
    <DashboardLayout title="Gestão de imóveis" nav={nav}>
      <div className="mb-6 flex flex-wrap items-center justify-between gap-3">
        <div className="flex flex-1 items-center gap-2 min-w-[240px] max-w-md rounded-xl border border-border bg-card px-3">
          <Search className="h-4 w-4 text-muted-foreground" />
          <Input
            value={q}
            onChange={(e) => setQ(e.target.value)}
            placeholder="Pesquisar por título ou localização"
            className="border-0 bg-transparent shadow-none focus-visible:ring-0"
          />
        </div>
        <div className="flex gap-2">
          <Button variant="ghost" size="sm" onClick={() => { reset(); toast.success("Lista reposta."); }}>
            <RotateCcw className="mr-1 h-4 w-4" /> Repor
          </Button>
          <Button onClick={handleNew} className="rounded-full">
            <Plus className="mr-1 h-4 w-4" /> Novo imóvel
          </Button>
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
              {filtered.map((p) => {
                const broker = users.find((u) => u.id === p.brokerId);
                const inativo = p.status === "inativo";
                return (
                  <tr key={p.id} className={`border-t border-border ${inativo ? "opacity-60" : ""}`}>
                    <td className="p-4">
                      <div className="flex items-center gap-3">
                        <img src={p.image} alt="" className="h-12 w-16 rounded-md object-cover" />
                        <div>
                          <Link to="/imoveis/$id" params={{ id: p.id }} className="font-medium hover:text-primary line-clamp-1">
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
                      <Badge
                        variant={p.status === "disponivel" ? "default" : inativo ? "outline" : "secondary"}
                        className="capitalize"
                      >
                        {p.status}
                      </Badge>
                    </td>
                    <td className="p-4">
                      <div className="flex justify-end gap-1">
                        <Button size="sm" variant="ghost" onClick={() => handleEdit(p)}>
                          <Pencil className="h-4 w-4" />
                        </Button>
                        <Button
                          size="sm"
                          variant="ghost"
                          onClick={() => handleToggle(p)}
                          title={inativo ? "Reativar" : "Desativar"}
                        >
                          {inativo ? <Power className="h-4 w-4 text-success" /> : <PowerOff className="h-4 w-4 text-destructive" />}
                        </Button>
                      </div>
                    </td>
                  </tr>
                );
              })}
              {filtered.length === 0 && (
                <tr>
                  <td colSpan={6} className="p-12 text-center text-muted-foreground">
                    Nenhum imóvel encontrado.
                  </td>
                </tr>
              )}
            </tbody>
          </table>
        </div>
      </div>

      <PropertyFormDialog
        open={open}
        onOpenChange={setOpen}
        property={editing}
        brokers={brokers}
      />

      <AlertDialog open={!!confirm} onOpenChange={(v) => !v && setConfirm(null)}>
        <AlertDialogContent>
          <AlertDialogHeader>
            <AlertDialogTitle>Desativar imóvel?</AlertDialogTitle>
            <AlertDialogDescription>
              "{confirm?.title}" deixará de aparecer no catálogo público. Pode reativar a qualquer momento.
            </AlertDialogDescription>
          </AlertDialogHeader>
          <AlertDialogFooter>
            <AlertDialogCancel>Cancelar</AlertDialogCancel>
            <AlertDialogAction onClick={confirmDeactivate}>Desativar</AlertDialogAction>
          </AlertDialogFooter>
        </AlertDialogContent>
      </AlertDialog>
    </DashboardLayout>
  );
}
