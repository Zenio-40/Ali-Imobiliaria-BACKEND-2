import { createFileRoute, Navigate, Link } from "@tanstack/react-router";
import { LayoutDashboard, Heart, Calendar } from "lucide-react";
import { DashboardLayout } from "@/components/DashboardLayout";
import { Badge } from "@/components/ui/badge";
import { useAuth } from "@/store/auth";
import { properties, visits, users } from "@/lib/mock-data";

const nav = [
  { to: "/cliente", label: "Visão geral", icon: LayoutDashboard },
  { to: "/cliente/favoritos", label: "Favoritos", icon: Heart },
  { to: "/cliente/visitas", label: "Visitas", icon: Calendar },
];

export const Route = createFileRoute("/cliente/visitas")({
  head: () => ({ meta: [{ title: "Visitas — Ali-Imobiliária" }] }),
  component: VisitasPage,
});

function VisitasPage() {
  const { user } = useAuth();
if (!user) return <Navigate to="/login" />;
  const myVisits = visits.filter((v) => v.clientId === user.id);

  return (
    <DashboardLayout title="As minhas visitas" nav={nav}>
      <div className="overflow-hidden rounded-2xl border border-border bg-card">
        <table className="w-full text-sm">
          <thead className="bg-muted text-left text-xs uppercase tracking-wider text-muted-foreground">
            <tr>
              <th className="p-4">Imóvel</th>
              <th className="p-4">Corretor</th>
              <th className="p-4">Data</th>
              <th className="p-4">Estado</th>
            </tr>
          </thead>
          <tbody>
            {myVisits.map((v) => {
              const prop = properties.find((p) => p.id === v.propertyId);
              const broker = users.find((u) => u.id === v.brokerId);
              return (
                <tr key={v.id} className="border-t border-border">
                  <td className="p-4 font-medium">
                    <Link to="/imoveis/$id" params={{ id: v.propertyId }} className="hover:text-primary">{prop?.title}</Link>
                  </td>
                  <td className="p-4 text-muted-foreground">{broker?.name}</td>
                  <td className="p-4">{new Date(v.date).toLocaleString("pt-PT")}</td>
                  <td className="p-4"><Badge variant={v.status === "confirmada" ? "default" : "secondary"} className="capitalize">{v.status}</Badge></td>
                </tr>
              );
            })}
            {myVisits.length === 0 && (
              <tr><td colSpan={4} className="p-12 text-center text-muted-foreground">Sem visitas agendadas.</td></tr>
            )}
          </tbody>
        </table>
      </div>
    </DashboardLayout>
  );
}
