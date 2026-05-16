import { createFileRoute } from "@tanstack/react-router";
import { useState, useMemo } from "react";
import { Search, SlidersHorizontal } from "lucide-react";
import { PublicLayout } from "@/components/PublicLayout";
import { PropertyCard } from "@/components/PropertyCard";
import { Button } from "@/components/ui/button";
import { usePublicProperties } from "@/store/properties";
import type { PropertyPurpose, PropertyType } from "@/lib/types";

export const Route = createFileRoute("/imoveis/")({
  head: () => ({
    meta: [
      { title: "Imóveis disponíveis — Ali-Imobiliária" },
      { name: "description", content: "Catálogo de imóveis para compra e arrendamento em Angola." },
    ],
  }),
  component: ListPage,
});

function ListPage() {
  const [purpose, setPurpose] = useState<"todos" | PropertyPurpose>("todos");
  const [type, setType] = useState<"todos" | PropertyType>("todos");
  const [q, setQ] = useState("");
  const properties = usePublicProperties();

  const list = useMemo(
    () =>
      properties.filter(
        (p) =>
          (purpose === "todos" || p.purpose === purpose) &&
          (type === "todos" || p.type === type) &&
          (q === "" || p.title.toLowerCase().includes(q.toLowerCase()) || p.location.toLowerCase().includes(q.toLowerCase())),
      ),
    [purpose, type, q, properties],
  );

  return (
    <PublicLayout>
      <section className="border-b border-border bg-[var(--gradient-soft)]">
        <div className="container-page py-12">
          <h1 className="font-display text-3xl font-extrabold md:text-4xl">Encontre o seu imóvel</h1>
          <p className="mt-2 text-muted-foreground">Filtre entre venda e arrendamento e descubra o que combina consigo.</p>

          <div className="mt-6 flex flex-wrap gap-3 rounded-2xl border border-border bg-card p-3 shadow-[var(--shadow-card)]">
            <div className="flex flex-1 items-center gap-2 rounded-xl bg-background px-3 min-w-[220px]">
              <Search className="h-4 w-4 text-muted-foreground" />
              <input value={q} onChange={(e) => setQ(e.target.value)} placeholder="Pesquisar por localização ou título" className="w-full bg-transparent py-2.5 text-sm outline-none" />
            </div>
            <div className="flex gap-1 rounded-xl bg-muted p-1">
              {(["todos", "venda", "arrendamento"] as const).map((v) => (
                <button key={v} onClick={() => setPurpose(v)} className={`rounded-lg px-4 py-1.5 text-sm font-medium capitalize transition ${purpose === v ? "bg-primary text-primary-foreground" : "hover:bg-background"}`}>{v}</button>
              ))}
            </div>
            <select value={type} onChange={(e) => setType(e.target.value as typeof type)} className="rounded-xl border border-input bg-background px-3 text-sm">
              <option value="todos">Todos os tipos</option>
              <option value="apartamento">Apartamento</option>
              <option value="moradia">Moradia</option>
              <option value="escritorio">Escritório</option>
              <option value="loja">Loja</option>
              <option value="terreno">Terreno</option>
            </select>
            <Button variant="outline" className="rounded-xl"><SlidersHorizontal className="mr-2 h-4 w-4" /> Mais filtros</Button>
          </div>
        </div>
      </section>

      <section className="container-page py-10">
        <div className="mb-6 text-sm text-muted-foreground">{list.length} imóvel(eis) encontrado(s)</div>
        {list.length === 0 ? (
          <div className="rounded-2xl border border-dashed border-border p-16 text-center text-muted-foreground">Sem resultados para os filtros aplicados.</div>
        ) : (
          <div className="grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
            {list.map((p) => <PropertyCard key={p.id} property={p} />)}
          </div>
        )}
      </section>
    </PublicLayout>
  );
}
