import { createFileRoute } from "@tanstack/react-router";
import { Award, Users, Building2, Target } from "lucide-react";
import { PublicLayout } from "@/components/PublicLayout";

export const Route = createFileRoute("/sobre")({
  head: () => ({
    meta: [
      { title: "Sobre — Ali-Imobiliária" },
      { name: "description", content: "Conheça a Ali-Imobiliária, a sua parceira em gestão e mediação de vendas de imóveis." },
    ],
  }),
  component: () => (
    <PublicLayout>
      <section className="container-page py-16 md:py-24">
        <span className="text-xs font-semibold uppercase tracking-widest text-primary">Sobre nós</span>
        <h1 className="mt-2 max-w-2xl font-display text-4xl font-extrabold md:text-5xl">A sua parceira de confiança no mercado imobiliário angolano.</h1>
        <p className="mt-5 max-w-3xl text-lg text-muted-foreground">
          A Ali-Imobiliária nasceu com o propósito de simplificar a compra, venda e arrendamento
          de imóveis em Angola. Combinamos conhecimento profundo do mercado local com tecnologia
          e processos transparentes para entregar a melhor experiência possível.
        </p>

        <div className="mt-14 grid gap-6 md:grid-cols-2">
          <div className="rounded-2xl border border-border bg-card p-8">
            <Target className="h-8 w-8 text-primary" />
            <h2 className="mt-4 font-display text-xl font-bold">A nossa missão</h2>
            <p className="mt-2 text-muted-foreground">Tornar o acesso à habitação mais simples, justo e transparente para todos os angolanos.</p>
          </div>
          <div className="rounded-2xl border border-border bg-card p-8">
            <Award className="h-8 w-8 text-primary" />
            <h2 className="mt-4 font-display text-xl font-bold">A nossa visão</h2>
            <p className="mt-2 text-muted-foreground">Ser a referência nacional em mediação imobiliária digital até 2030.</p>
          </div>
        </div>

        <div className="mt-14 grid gap-6 md:grid-cols-3">
          {[
            { icon: Building2, t: "+500 imóveis", d: "geridos com sucesso" },
            { icon: Users, t: "+1200 clientes", d: "satisfeitos em todo o país" },
            { icon: Award, t: "10+ anos", d: "de experiência no mercado" },
          ].map((s) => (
            <div key={s.t} className="rounded-2xl bg-secondary p-7 text-secondary-foreground">
              <s.icon className="h-6 w-6 text-primary" />
              <div className="mt-3 font-display text-2xl font-extrabold">{s.t}</div>
              <div className="text-sm text-secondary-foreground/70">{s.d}</div>
            </div>
          ))}
        </div>
      </section>
    </PublicLayout>
  ),
});
