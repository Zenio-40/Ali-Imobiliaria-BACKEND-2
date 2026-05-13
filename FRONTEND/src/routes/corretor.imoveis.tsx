import { useMemo } from "react";
import { createFileRoute, Navigate, Link } from "@tanstack/react-router";
import { LayoutDashboard, Building2 } from "lucide-react";
import { DashboardLayout } from "@/components/DashboardLayout";
import { PropertyCard } from "@/components/PropertyCard";
import { useAuth } from "@/store/auth";
import { useProperties } from "@/store/properties";

const nav = [
  { to: "/corretor", label: "Visão geral", icon: LayoutDashboard },
  { to: "/corretor/imoveis", label: "Os meus imóveis", icon: Building2 },
];

export const Route = createFileRoute("/corretor/imoveis")({
  head: () => ({ meta: [{ title: "Os meus imóveis — Ali-Imobiliária" }] }),
  component: BrokerProperties,
});

function BrokerProperties() {
  const { user } = useAuth();
if (!user) return <Navigate to="/login" />;
  const items = useProperties((s) => s.items);
  const list = useMemo(
    () => items.filter((p) => p.brokerId === user.id || user.role === "admin"),
    [items, user.id, user.role],
  );

  return (
    <DashboardLayout title="Os meus imóveis" nav={nav}>
      {list.length === 0 ? (
        <div className="rounded-2xl border border-dashed border-border bg-card p-12 text-center text-muted-foreground">
          Ainda não tem imóveis associados. <Link to="/imoveis" className="text-primary">Ver catálogo →</Link>
        </div>
      ) : (
        <div className="grid gap-5 sm:grid-cols-2 lg:grid-cols-3">
          {list.map((p) => <PropertyCard key={p.id} property={p} />)}
        </div>
      )}
    </DashboardLayout>
  );
}
