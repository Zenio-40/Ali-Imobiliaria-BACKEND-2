import { useMemo } from "react";
import { createFileRoute, Navigate } from "@tanstack/react-router";
import { LayoutDashboard, Heart, Calendar } from "lucide-react";
import { DashboardLayout } from "@/components/DashboardLayout";
import { PropertyCard } from "@/components/PropertyCard";
import { useAuth } from "@/store/auth";
import { useProperties } from "@/store/properties";

const nav = [
  { to: "/cliente", label: "Visão geral", icon: LayoutDashboard },
  { to: "/cliente/favoritos", label: "Favoritos", icon: Heart },
  { to: "/cliente/visitas", label: "Visitas", icon: Calendar },
];

export const Route = createFileRoute("/cliente/favoritos")({
  head: () => ({ meta: [{ title: "Favoritos — Ali-Imobiliária" }] }),
  component: FavoritosPage,
});

function FavoritosPage() {
  const { user, favorites } = useAuth();
if (!user) return <Navigate to="/login" />;
  const items = useProperties((s) => s.items);
  const list = useMemo(
    () => items.filter((p) => favorites.includes(p.id) && p.status !== "inativo"),
    [items, favorites],
  );

  return (
    <DashboardLayout title="Os meus favoritos" nav={nav}>
      {list.length === 0 ? (
        <div className="rounded-2xl border border-dashed border-border bg-card p-12 text-center">
          <Heart className="mx-auto h-10 w-10 text-muted-foreground" />
          <p className="mt-4 text-muted-foreground">Ainda não tem imóveis favoritos.</p>
        </div>
      ) : (
        <div className="grid gap-5 sm:grid-cols-2 lg:grid-cols-3">
          {list.map((p) => <PropertyCard key={p.id} property={p} />)}
        </div>
      )}
    </DashboardLayout>
  );
}
