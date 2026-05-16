import { createFileRoute, Navigate, Link } from "@tanstack/react-router";
import { LayoutDashboard, Building2, Calendar, Users as UsersIcon, Plus, TrendingUp } from "lucide-react";
import { DashboardLayout, StatCard } from "@/components/DashboardLayout";
import { Badge } from "@/components/ui/badge";
import { Button } from "@/components/ui/button";
import { useAuth } from "@/store/auth";
import { properties, visits, formatPrice } from "@/lib/mock-data";
import { ResponsiveContainer, LineChart, Line, XAxis, YAxis, Tooltip, CartesianGrid } from "recharts";

const nav = [
  { to: "/corretor", label: "Visão geral", icon: LayoutDashboard },
  { to: "/corretor/imoveis", label: "Os meus imóveis", icon: Building2 },
];

const chartData = [
  { mes: "Jan", visitas: 12, leads: 5 },
  { mes: "Fev", visitas: 18, leads: 8 },
  { mes: "Mar", visitas: 22, leads: 11 },
  { mes: "Abr", visitas: 30, leads: 14 },
  { mes: "Mai", visitas: 26, leads: 12 },
];

export const Route = createFileRoute("/corretor/")({
  head: () => ({ meta: [{ title: "Painel do Corretor — Ali-Imobiliária" }] }),
  component: BrokerDashboard,
});

function BrokerDashboard() {
  const { user } = useAuth();
if (!user) return <Navigate to="/login" />;

  const myProps = properties.filter((p) => p.brokerId === user.id || user.role === "admin");
  const myVisits = visits.filter((v) => v.brokerId === user.id);
  const totalValue = myProps.reduce((sum, p) => sum + (p.purpose === "venda" ? p.price : 0), 0);

  return (
    <DashboardLayout title="Painel do Corretor" nav={nav}>
      <div className="grid gap-4 md:grid-cols-4">
        <StatCard label="Imóveis ativos" value={myProps.length.toString()} icon={Building2} />
        <StatCard label="Visitas" value={myVisits.length.toString()} icon={Calendar} />
        <StatCard label="Leads do mês" value="14" icon={UsersIcon} />
        <StatCard label="Carteira" value={`${(totalValue / 1_000_000).toFixed(0)}M Kz`} icon={TrendingUp} accent />
      </div>

      <div className="mt-6 grid gap-6 lg:grid-cols-[2fr_1fr]">
        <div className="rounded-2xl border border-border bg-card p-6 shadow-[var(--shadow-card)]">
          <h2 className="font-display text-lg font-bold">Performance mensal</h2>
          <div className="mt-4 h-72">
            <ResponsiveContainer width="100%" height="100%">
              <LineChart data={chartData}>
                <CartesianGrid strokeDasharray="3 3" stroke="oklch(0.9 0.01 70)" />
                <XAxis dataKey="mes" stroke="oklch(0.5 0.015 60)" fontSize={12} />
                <YAxis stroke="oklch(0.5 0.015 60)" fontSize={12} />
                <Tooltip contentStyle={{ borderRadius: 12, border: "1px solid oklch(0.9 0.01 70)" }} />
                <Line type="monotone" dataKey="visitas" stroke="oklch(0.72 0.16 55)" strokeWidth={3} dot={{ r: 4 }} />
                <Line type="monotone" dataKey="leads" stroke="oklch(0.32 0.01 60)" strokeWidth={3} dot={{ r: 4 }} />
              </LineChart>
            </ResponsiveContainer>
          </div>
        </div>

        <div className="rounded-2xl border border-border bg-card p-6">
          <h2 className="font-display text-lg font-bold">Próximas visitas</h2>
          <div className="mt-4 space-y-3">
            {myVisits.length === 0 && <div className="text-sm text-muted-foreground">Sem visitas agendadas.</div>}
            {myVisits.map((v) => {
              const p = properties.find((pp) => pp.id === v.propertyId);
              return (
                <div key={v.id} className="rounded-xl border border-border p-3">
                  <div className="text-sm font-medium line-clamp-1">{p?.title}</div>
                  <div className="mt-1 flex items-center justify-between text-xs text-muted-foreground">
                    <span>{new Date(v.date).toLocaleString("pt-PT")}</span>
                    <Badge variant="outline" className="capitalize">{v.status}</Badge>
                  </div>
                </div>
              );
            })}
          </div>
        </div>
      </div>

      <section className="mt-8 rounded-2xl border border-border bg-card p-6">
        <div className="mb-4 flex items-center justify-between">
          <h2 className="font-display text-lg font-bold">Os meus imóveis</h2>
          <Button size="sm" className="rounded-full"><Plus className="mr-1 h-4 w-4" /> Novo imóvel</Button>
        </div>
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead className="text-left text-xs uppercase tracking-wider text-muted-foreground">
              <tr><th className="p-3">Imóvel</th><th className="p-3">Tipo</th><th className="p-3">Preço</th><th className="p-3">Estado</th></tr>
            </thead>
            <tbody>
              {myProps.map((p) => (
                <tr key={p.id} className="border-t border-border">
                  <td className="p-3 font-medium"><Link to="/imoveis/$id" params={{ id: p.id }} className="hover:text-primary">{p.title}</Link></td>
                  <td className="p-3 capitalize text-muted-foreground">{p.type}</td>
                  <td className="p-3">{formatPrice(p.price, p.purpose)}</td>
                  <td className="p-3"><Badge variant={p.status === "disponivel" ? "default" : "secondary"} className="capitalize">{p.status}</Badge></td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </section>
    </DashboardLayout>
  );
}
