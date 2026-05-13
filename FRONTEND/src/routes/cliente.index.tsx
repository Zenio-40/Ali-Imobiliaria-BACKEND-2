import { createFileRoute, Navigate } from "@tanstack/react-router";
import { LayoutDashboard, Heart, Calendar, Search } from "lucide-react";
import { DashboardLayout, StatCard } from "@/components/DashboardLayout";
import { PropertyCard } from "@/components/PropertyCard";
import { Button } from "@/components/ui/button";
import { useAuth } from "@/store/auth";
import { properties, visits } from "@/lib/mock-data";

const nav = [
  { to: "/cliente", label: "Visão geral", icon: LayoutDashboard },
  { to: "/cliente/favoritos", label: "Favoritos", icon: Heart },
  { to: "/cliente/visitas", label: "Visitas", icon: Calendar },
];

export const Route = createFileRoute("/cliente/")({
  head: () => ({ meta: [{ title: "Painel do Cliente — Ali-Imobiliária" }] }),
  component: ClientDashboard,
});

function ClientDashboard() {
  const { user, favorites } = useAuth();
if (!user) return <Navigate to="/login" />;

  const myFavs = properties.filter((p) => favorites.includes(p.id));
  const myVisits = visits.filter((v) => v.clientId === user.id);
  const recommended = properties.slice(0, 3);

  return (
    <DashboardLayout title="Painel do Cliente" nav={nav}>
      <div className="grid gap-4 md:grid-cols-3">
        <StatCard label="Favoritos" value={favorites.length.toString()} icon={Heart} hint="Imóveis guardados" />
        <StatCard label="Visitas agendadas" value={myVisits.length.toString()} icon={Calendar} hint="Próximas visitas" />
        <StatCard label="Recomendações" value={recommended.length.toString()} icon={Search} accent />
      </div>

      <section className="mt-10">
        <div className="mb-4 flex items-center justify-between">
          <h2 className="font-display text-xl font-bold">Os seus favoritos</h2>
          <Button variant="ghost" size="sm" asChild><a href="/cliente/favoritos">Ver todos</a></Button>
        </div>
        {myFavs.length === 0 ? (
          <div className="rounded-2xl border border-dashed border-border bg-card p-10 text-center text-muted-foreground">
            Ainda não guardou imóveis. <a href="/imoveis" className="text-primary font-medium">Explorar agora →</a>
          </div>
        ) : (
          <div className="grid gap-5 sm:grid-cols-2 lg:grid-cols-3">
            {myFavs.slice(0, 3).map((p) => <PropertyCard key={p.id} property={p} />)}
          </div>
        )}
      </section>

      <section className="mt-12">
        <h2 className="font-display text-xl font-bold mb-4">Recomendações para si</h2>
        <div className="grid gap-5 sm:grid-cols-2 lg:grid-cols-3">
          {recommended.map((p) => <PropertyCard key={p.id} property={p} />)}
        </div>
      </section>
    </DashboardLayout>
  );
}
