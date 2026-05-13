import { createFileRoute, Link } from "@tanstack/react-router";
import { Bed, Bath, Maximize, MapPin, Calendar, Phone, Mail, ArrowLeft, Heart } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { PublicLayout } from "@/components/PublicLayout";
import { users, formatPrice, properties as seedProperties } from "@/lib/mock-data";
import { useProperties } from "@/store/properties";
import { useAuth } from "@/store/auth";
import { cn } from "@/lib/utils";

export const Route = createFileRoute("/imoveis/$id")({
  head: ({ params }) => {
    const property = seedProperties.find((p) => p.id === params.id);
    return {
      meta: property
        ? [
            { title: `${property.title} — Ali-Imobiliária` },
            { name: "description", content: property.description.slice(0, 150) },
            { property: "og:image", content: property.image },
          ]
        : [{ title: "Imóvel — Ali-Imobiliária" }],
    };
  },
  component: DetailPage,
});

function DetailPage() {
  const { id } = Route.useParams();
  const property = useProperties((s) => s.items.find((p) => p.id === id));
  const { favorites, toggleFavorite, user } = useAuth();

  if (!property || property.status === "inativo") {
    return (
      <PublicLayout>
        <div className="container-page py-24 text-center">
          <h1 className="font-display text-3xl font-bold">Imóvel não encontrado</h1>
          <Button asChild className="mt-6"><Link to="/imoveis">Ver todos os imóveis</Link></Button>
        </div>
      </PublicLayout>
    );
  }

  const broker = users.find((u) => u.id === property.brokerId);
  const isFav = favorites.includes(property.id);

  return (
    <PublicLayout>
      <div className="container-page py-8">
        <Button asChild variant="ghost" size="sm" className="mb-4">
          <Link to="/imoveis"><ArrowLeft className="mr-2 h-4 w-4" /> Voltar</Link>
        </Button>

        <div className="grid gap-8 lg:grid-cols-[1.5fr_1fr]">
          <div>
            <div className="overflow-hidden rounded-3xl">
              <img src={property.image} alt={property.title} className="h-[460px] w-full object-cover" />
            </div>

            <div className="mt-6 flex flex-wrap items-center gap-2">
              <Badge className="rounded-full capitalize bg-primary text-primary-foreground border-0">{property.purpose}</Badge>
              <Badge variant="outline" className="rounded-full capitalize">{property.type}</Badge>
              <Badge variant="secondary" className="rounded-full capitalize">{property.status}</Badge>
            </div>

            <h1 className="mt-3 font-display text-3xl font-extrabold md:text-4xl">{property.title}</h1>
            <div className="mt-2 flex items-center gap-1 text-muted-foreground"><MapPin className="h-4 w-4" /> {property.location}</div>

            <div className="mt-6 grid grid-cols-3 gap-3 rounded-2xl border border-border bg-card p-5">
              <Stat icon={Bed} label="Quartos" value={property.bedrooms.toString()} />
              <Stat icon={Bath} label="WC" value={property.bathrooms.toString()} />
              <Stat icon={Maximize} label="Área" value={`${property.area} m²`} />
            </div>

            <div className="mt-8">
              <h2 className="font-display text-xl font-bold">Descrição</h2>
              <p className="mt-3 text-muted-foreground leading-relaxed">{property.description}</p>
            </div>
          </div>

          {/* SIDEBAR */}
          <aside className="space-y-4">
            <div className="rounded-2xl border border-border bg-card p-6 shadow-[var(--shadow-card)]">
              <div className="text-xs uppercase tracking-widest text-muted-foreground">Preço</div>
              <div className="mt-1 font-display text-3xl font-extrabold text-secondary">
                {formatPrice(property.price, property.purpose)}
              </div>
              <div className="mt-5 grid gap-2">
                <Button asChild className="rounded-full"><Link to="/contacto"><Calendar className="mr-2 h-4 w-4" /> Agendar visita</Link></Button>
                {user?.role === "cliente" ? (
                  <Button onClick={() => toggleFavorite(property.id)} variant="outline" className="rounded-full">
                    <Heart className={cn("mr-2 h-4 w-4", isFav && "fill-primary text-primary")} />
                    {isFav ? "Nos favoritos" : "Guardar nos favoritos"}
                  </Button>
                ) : (
                  <Button asChild variant="outline" className="rounded-full"><Link to="/login">Entrar para guardar</Link></Button>
                )}
              </div>
            </div>

            {broker && (
              <div className="rounded-2xl border border-border bg-card p-6">
                <div className="text-xs uppercase tracking-widest text-muted-foreground">Corretor responsável</div>
                <div className="mt-3 flex items-center gap-3">
                  <div className="grid h-12 w-12 place-items-center rounded-full bg-primary text-primary-foreground font-display font-bold">
                    {broker.name.charAt(0)}
                  </div>
                  <div>
                    <div className="font-display font-semibold">{broker.name}</div>
                    <div className="text-xs text-muted-foreground">Corretor certificado</div>
                  </div>
                </div>
                <div className="mt-4 space-y-2 text-sm">
                  <a href={`tel:${broker.phone}`} className="flex items-center gap-2 text-foreground hover:text-primary"><Phone className="h-4 w-4" /> +244 {broker.phone}</a>
                  <a href={`mailto:${broker.email}`} className="flex items-center gap-2 text-foreground hover:text-primary"><Mail className="h-4 w-4" /> {broker.email}</a>
                </div>
              </div>
            )}
          </aside>
        </div>
      </div>
    </PublicLayout>
  );
}

function Stat({ icon: Icon, label, value }: { icon: typeof Bed; label: string; value: string }) {
  return (
    <div className="text-center">
      <div className="mx-auto inline-flex h-10 w-10 items-center justify-center rounded-xl bg-accent text-primary"><Icon className="h-5 w-5" /></div>
      <div className="mt-2 font-display font-bold">{value}</div>
      <div className="text-xs text-muted-foreground">{label}</div>
    </div>
  );
}
