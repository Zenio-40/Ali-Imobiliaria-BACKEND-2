import { createFileRoute, Navigate } from "@tanstack/react-router";
import { LayoutDashboard, Building2, Users as UsersIcon, Archive } from "lucide-react";
import { DashboardLayout } from "@/components/DashboardLayout";
import { Badge } from "@/components/ui/badge";
import { useAuth } from "@/store/auth";
import { users } from "@/lib/mock-data";

const nav = [
  { to: "/admin", label: "Visão geral", icon: LayoutDashboard },
  { to: "/admin/imoveis", label: "Imóveis", icon: Building2 },
  { to: "/admin/imoveis/inativos", label: "Inativos", icon: Archive },
  { to: "/admin/utilizadores", label: "Utilizadores", icon: UsersIcon },
];

export const Route = createFileRoute("/admin/utilizadores")({
  head: () => ({ meta: [{ title: "Utilizadores — Ali-Imobiliária" }] }),
  component: () => {
    const { user } = useAuth();
    if (!user) return <Navigate to="/login" />;
    return (
      <DashboardLayout title="Utilizadores" nav={nav}>
        <div className="overflow-hidden rounded-2xl border border-border bg-card">
          <table className="w-full text-sm">
            <thead className="bg-muted text-left text-xs uppercase tracking-wider text-muted-foreground">
              <tr><th className="p-4">Nome</th><th className="p-4">Email</th><th className="p-4">Telefone</th><th className="p-4">Função</th></tr>
            </thead>
            <tbody>
              {users.map((u) => (
                <tr key={u.id} className="border-t border-border">
                  <td className="p-4 flex items-center gap-3">
                    <div className="grid h-9 w-9 place-items-center rounded-full bg-primary text-primary-foreground font-display font-bold text-sm">{u.name.charAt(0)}</div>
                    {u.name}
                  </td>
                  <td className="p-4 text-muted-foreground">{u.email}</td>
                  <td className="p-4 text-muted-foreground">+244 {u.phone}</td>
                  <td className="p-4"><Badge variant={u.role === "admin" ? "default" : "secondary"} className="capitalize">{u.role}</Badge></td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </DashboardLayout>
    );
  },
});
