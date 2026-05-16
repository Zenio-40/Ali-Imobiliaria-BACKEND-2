import { createFileRoute, Navigate } from "@tanstack/react-router";
import { LayoutDashboard, Building2, Users as UsersIcon, BarChart3, DollarSign, TrendingUp, Home, Archive } from "lucide-react";
import { DashboardLayout, StatCard } from "@/components/DashboardLayout";
import { Badge } from "@/components/ui/badge";
import { useAuth } from "@/store/auth";
import { properties, users, formatPrice } from "@/lib/mock-data";
import { ResponsiveContainer, BarChart, Bar, XAxis, YAxis, Tooltip, CartesianGrid, PieChart, Pie, Cell, Legend } from "recharts";

const nav = [
  { to: "/admin", label: "Visão geral", icon: LayoutDashboard },
  { to: "/admin/imoveis", label: "Imóveis", icon: Building2 },
  { to: "/admin/imoveis/inativos", label: "Inativos", icon: Archive },
  { to: "/admin/utilizadores", label: "Utilizadores", icon: UsersIcon },
];

const monthly = [
  { mes: "Jan", vendas: 4, arrendamentos: 8 },
  { mes: "Fev", vendas: 6, arrendamentos: 11 },
  { mes: "Mar", vendas: 8, arrendamentos: 9 },
  { mes: "Abr", vendas: 11, arrendamentos: 14 },
  { mes: "Mai", vendas: 9, arrendamentos: 16 },
];

const COLORS = ["oklch(0.72 0.16 55)", "oklch(0.32 0.01 60)", "oklch(0.65 0.15 145)", "oklch(0.6 0.18 220)"];

export const Route = createFileRoute("/admin/")({
  head: () => ({ meta: [{ title: "Painel do Admin — Ali-Imobiliária" }] }),
  component: AdminDashboard,
});

function AdminDashboard() {
  const { user } = useAuth();
if (!user) return <Navigate to="/login" />;

  const totalProps = properties.length;
  const totalUsers = users.length;
  const corretores = users.filter((u) => u.role === "corretor").length;
  const totalVal = properties.filter((p) => p.purpose === "venda").reduce((s, p) => s + p.price, 0);

  const byType = ["apartamento", "moradia", "escritorio", "terreno", "loja"].map((t) => ({
    name: t,
    value: properties.filter((p) => p.type === t).length,
  })).filter((d) => d.value > 0);

  return (
    <DashboardLayout title="Painel Administrativo" nav={nav}>
      <div className="grid gap-4 md:grid-cols-2 lg:grid-cols-4">
        <StatCard label="Imóveis" value={totalProps.toString()} icon={Home} hint="No catálogo" />
        <StatCard label="Utilizadores" value={totalUsers.toString()} icon={UsersIcon} hint={`${corretores} corretores`} />
        <StatCard label="Volume de venda" value={`${(totalVal / 1_000_000).toFixed(0)}M Kz`} icon={DollarSign} accent />
        <StatCard label="Conversão" value="32%" icon={TrendingUp} hint="Visitas → contratos" />
      </div>

      <div className="mt-6 grid gap-6 lg:grid-cols-[2fr_1fr]">
        <div className="rounded-2xl border border-border bg-card p-6 shadow-[var(--shadow-card)]">
          <div className="flex items-center justify-between">
            <h2 className="font-display text-lg font-bold">Transações por mês</h2>
            <BarChart3 className="h-5 w-5 text-muted-foreground" />
          </div>
          <div className="mt-4 h-72">
            <ResponsiveContainer width="100%" height="100%">
              <BarChart data={monthly}>
                <CartesianGrid strokeDasharray="3 3" stroke="oklch(0.9 0.01 70)" />
                <XAxis dataKey="mes" stroke="oklch(0.5 0.015 60)" fontSize={12} />
                <YAxis stroke="oklch(0.5 0.015 60)" fontSize={12} />
                <Tooltip contentStyle={{ borderRadius: 12 }} />
                <Bar dataKey="vendas" fill="oklch(0.72 0.16 55)" radius={[8, 8, 0, 0]} />
                <Bar dataKey="arrendamentos" fill="oklch(0.32 0.01 60)" radius={[8, 8, 0, 0]} />
              </BarChart>
            </ResponsiveContainer>
          </div>
        </div>

        <div className="rounded-2xl border border-border bg-card p-6">
          <h2 className="font-display text-lg font-bold">Distribuição por tipo</h2>
          <div className="mt-4 h-72">
            <ResponsiveContainer width="100%" height="100%">
              <PieChart>
                <Pie data={byType} cx="50%" cy="50%" innerRadius={50} outerRadius={90} dataKey="value">
                  {byType.map((_, i) => <Cell key={i} fill={COLORS[i % COLORS.length]} />)}
                </Pie>
                <Tooltip />
                <Legend wrapperStyle={{ fontSize: 12 }} />
              </PieChart>
            </ResponsiveContainer>
          </div>
        </div>
      </div>

      <section className="mt-8 rounded-2xl border border-border bg-card p-6">
        <h2 className="mb-4 font-display text-lg font-bold">Imóveis recentes</h2>
        <div className="overflow-x-auto">
          <table className="w-full text-sm">
            <thead className="text-left text-xs uppercase tracking-wider text-muted-foreground">
              <tr><th className="p-3">Imóvel</th><th className="p-3">Corretor</th><th className="p-3">Preço</th><th className="p-3">Finalidade</th><th className="p-3">Estado</th></tr>
            </thead>
            <tbody>
              {properties.slice(0, 6).map((p) => {
                const broker = users.find((u) => u.id === p.brokerId);
                return (
                  <tr key={p.id} className="border-t border-border">
                    <td className="p-3 font-medium">{p.title}</td>
                    <td className="p-3 text-muted-foreground">{broker?.name ?? "—"}</td>
                    <td className="p-3">{formatPrice(p.price, p.purpose)}</td>
                    <td className="p-3 capitalize">{p.purpose}</td>
                    <td className="p-3"><Badge variant={p.status === "disponivel" ? "default" : "secondary"} className="capitalize">{p.status}</Badge></td>
                  </tr>
                );
              })}
            </tbody>
          </table>
        </div>
      </section>
    </DashboardLayout>
  );
}
