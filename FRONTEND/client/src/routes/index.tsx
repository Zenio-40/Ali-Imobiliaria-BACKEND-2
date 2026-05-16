import { createFileRoute, Link } from "@tanstack/react-router";
import { Search, Building2, Key, Award, ArrowRight, MapPin, ShieldCheck, Sparkles } from "lucide-react";
import { Button } from "@/components/ui/button";
import { PublicLayout } from "@/components/PublicLayout";
import { PropertyCard } from "@/components/PropertyCard";
import { usePublicProperties } from "@/store/properties";
import heroHouse from "@/assets/hero-house.jpg";

export const Route = createFileRoute("/")({
  head: () => ({
    meta: [
      { title: "Ali-Imobiliária — Compra, venda e arrendamento de imóveis em Angola" },
      { name: "description", content: "Encontre o seu próximo imóvel com a Ali-Imobiliária. Apartamentos, moradias e escritórios para compra e arrendamento em Luanda." },
      { property: "og:title", content: "Ali-Imobiliária" },
      { property: "og:description", content: "Gestão e mediação de vendas e arrendamento de imóveis." },
    ],
  }),
  component: HomePage,
});

function HomePage() {
  const properties = usePublicProperties();
  const featured = properties.filter((p) => p.featured);

  return (
    <PublicLayout>
      {/* HERO */}
      <section className="relative overflow-hidden">
        <div className="absolute inset-0">
          <img src={heroHouse} alt="" className="h-full w-full object-cover" />
          <div className="absolute inset-0" style={{ background: "var(--gradient-hero)" }} />
        </div>
        <div className="container-page relative grid gap-10 py-24 md:py-32 lg:grid-cols-[1.1fr_1fr] lg:items-center">
          <div className="text-background">
            <span className="inline-flex items-center gap-2 rounded-full bg-background/15 px-4 py-1.5 text-xs font-medium backdrop-blur">
              <Sparkles className="h-3.5 w-3.5 text-primary" />
              Mais de 500 imóveis disponíveis em Angola
            </span>
            <h1 className="mt-5 font-display text-4xl font-extrabold leading-[1.05] md:text-6xl">
              O lar dos seus sonhos<br />começa <span className="text-primary">aqui</span>.
            </h1>
            <p className="mt-5 max-w-xl text-base text-background/85">
              Compra, venda e arrendamento de imóveis com gestão profissional, transparência
              e o apoio dedicado dos nossos corretores.
            </p>
            <div className="mt-8 flex flex-wrap gap-3">
              <Button asChild size="lg" className="rounded-full">
                <Link to="/imoveis"><Search className="mr-2 h-4 w-4" /> Explorar imóveis</Link>
              </Button>
              <Button asChild size="lg" variant="outline" className="rounded-full bg-background/10 text-background border-background/30 hover:bg-background/20 hover:text-background">
                <Link to="/contacto">Falar connosco</Link>
              </Button>
            </div>
          </div>

          {/* Search Card */}
          <div className="rounded-2xl border border-background/15 bg-background/95 p-6 shadow-[var(--shadow-elegant)] backdrop-blur">
            <h3 className="font-display text-lg font-semibold">Pesquisa rápida</h3>
            <div className="mt-4 grid gap-3">
              <div className="grid grid-cols-2 gap-2">
                <Button variant="default" className="rounded-full">Comprar</Button>
                <Button variant="outline" className="rounded-full">Arrendar</Button>
              </div>
              <div className="rounded-lg border border-input px-3 py-2.5 flex items-center gap-2">
                <MapPin className="h-4 w-4 text-muted-foreground" />
                <input className="w-full bg-transparent text-sm outline-none" placeholder="Localização (ex: Talatona)" />
              </div>
              <div className="grid grid-cols-2 gap-2">
                <select className="rounded-lg border border-input bg-background px-3 py-2.5 text-sm">
                  <option>Tipo de imóvel</option>
                  <option>Apartamento</option>
                  <option>Moradia</option>
                  <option>Escritório</option>
                </select>
                <select className="rounded-lg border border-input bg-background px-3 py-2.5 text-sm">
                  <option>Quartos</option>
                  <option>1+</option><option>2+</option><option>3+</option><option>4+</option>
                </select>
              </div>
              <Button asChild className="rounded-full">
                <Link to="/imoveis"><Search className="mr-2 h-4 w-4" /> Procurar</Link>
              </Button>
            </div>
          </div>
        </div>
      </section>

      {/* STATS */}
      <section className="container-page py-16">
        <div className="grid grid-cols-2 gap-4 rounded-2xl border border-border bg-card p-8 md:grid-cols-4">
          {[
            { n: "500+", l: "Imóveis listados" },
            { n: "1.2k+", l: "Clientes felizes" },
            { n: "25+", l: "Corretores certificados" },
            { n: "10 anos", l: "De experiência" },
          ].map((s) => (
            <div key={s.l} className="text-center">
              <div className="font-display text-3xl font-extrabold text-primary">{s.n}</div>
              <div className="mt-1 text-xs text-muted-foreground">{s.l}</div>
            </div>
          ))}
        </div>
      </section>

      {/* SERVICES */}
      <section className="container-page py-16">
        <div className="text-center">
          <span className="text-xs font-semibold uppercase tracking-widest text-primary">O que fazemos</span>
          <h2 className="mt-2 font-display text-3xl font-bold md:text-4xl">Serviços completos de mediação imobiliária</h2>
        </div>
        <div className="mt-10 grid gap-5 md:grid-cols-3">
          {[
            { icon: Building2, t: "Venda de imóveis", d: "Avaliamos, divulgamos e acompanhamos cada negócio até à escritura." },
            { icon: Key, t: "Arrendamento", d: "Gestão de contratos e acompanhamento da relação senhorio-inquilino." },
            { icon: Award, t: "Consultoria", d: "Apoio estratégico para investidores e compradores exigentes." },
          ].map((s) => (
            <div key={s.t} className="group rounded-2xl border border-border bg-card p-7 transition hover:border-primary hover:shadow-[var(--shadow-card)]">
              <div className="inline-flex h-12 w-12 items-center justify-center rounded-xl bg-accent text-primary group-hover:bg-primary group-hover:text-primary-foreground transition">
                <s.icon className="h-6 w-6" />
              </div>
              <h3 className="mt-4 font-display text-lg font-semibold">{s.t}</h3>
              <p className="mt-2 text-sm text-muted-foreground">{s.d}</p>
            </div>
          ))}
        </div>
      </section>

      {/* FEATURED */}
      <section className="container-page py-16">
        <div className="flex items-end justify-between gap-4">
          <div>
            <span className="text-xs font-semibold uppercase tracking-widest text-primary">Destaques</span>
            <h2 className="mt-2 font-display text-3xl font-bold md:text-4xl">Imóveis em destaque</h2>
          </div>
          <Button asChild variant="ghost" className="rounded-full">
            <Link to="/imoveis">Ver todos <ArrowRight className="ml-1 h-4 w-4" /></Link>
          </Button>
        </div>
        <div className="mt-8 grid gap-6 sm:grid-cols-2 lg:grid-cols-3">
          {featured.map((p) => <PropertyCard key={p.id} property={p} />)}
        </div>
      </section>

      {/* CTA */}
      <section className="container-page py-16">
        <div className="overflow-hidden rounded-3xl bg-secondary p-10 text-secondary-foreground md:p-14">
          <div className="grid gap-6 md:grid-cols-[1.5fr_1fr] md:items-center">
            <div>
              <ShieldCheck className="h-10 w-10 text-primary" />
              <h2 className="mt-4 font-display text-3xl font-bold md:text-4xl">
                Pronto para vender ou arrendar o seu imóvel?
              </h2>
              <p className="mt-3 max-w-2xl text-secondary-foreground/80">
                Os nossos corretores estão disponíveis para uma avaliação gratuita do seu imóvel.
              </p>
            </div>
            <div className="flex md:justify-end">
              <Button asChild size="lg" className="rounded-full">
                <Link to="/contacto">Pedir avaliação grátis</Link>
              </Button>
            </div>
          </div>
        </div>
      </section>
    </PublicLayout>
  );
}
