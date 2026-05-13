import { createFileRoute } from "@tanstack/react-router";
import { useState } from "react";
import { Mail, Phone, MapPin, Send } from "lucide-react";
import { PublicLayout } from "@/components/PublicLayout";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Textarea } from "@/components/ui/textarea";
import { toast } from "sonner";

export const Route = createFileRoute("/contacto")({
  head: () => ({
    meta: [
      { title: "Contacto — Ali-Imobiliária" },
      { name: "description", content: "Entre em contacto com a equipa Ali-Imobiliária." },
    ],
  }),
  component: ContactPage,
});

function ContactPage() {
  const [sent, setSent] = useState(false);
  return (
    <PublicLayout>
      <section className="container-page py-16 md:py-20">
        <div className="grid gap-10 lg:grid-cols-[1fr_1.2fr]">
          <div>
            <span className="text-xs font-semibold uppercase tracking-widest text-primary">Contacto</span>
            <h1 className="mt-2 font-display text-4xl font-extrabold">Vamos conversar.</h1>
            <p className="mt-3 text-muted-foreground">Estamos disponíveis para tirar todas as suas dúvidas e marcar visitas.</p>
            <div className="mt-8 space-y-4">
              <Info icon={MapPin} title="Endereço" value="Av. 4 de Fevereiro, Luanda" />
              <Info icon={Phone} title="Telefone" value="+244 923 000 000" />
              <Info icon={Mail} title="Email" value="geral@ali-imobiliaria.ao" />
            </div>
          </div>

          <form
            onSubmit={(e) => {
              e.preventDefault();
              setSent(true);
              toast.success("Mensagem enviada! Entraremos em contacto em breve.");
            }}
            className="rounded-3xl border border-border bg-card p-7 shadow-[var(--shadow-card)]"
          >
            <h2 className="font-display text-xl font-bold">Envie-nos uma mensagem</h2>
            <div className="mt-5 grid gap-4">
              <div className="grid gap-4 md:grid-cols-2">
                <Input placeholder="Nome completo" required />
                <Input placeholder="Telefone" required />
              </div>
              <Input type="email" placeholder="Email" required />
              <Input placeholder="Assunto" />
              <Textarea rows={5} placeholder="A sua mensagem..." required />
              <Button type="submit" className="rounded-full" size="lg">
                <Send className="mr-2 h-4 w-4" /> {sent ? "Enviada" : "Enviar mensagem"}
              </Button>
            </div>
          </form>
        </div>
      </section>
    </PublicLayout>
  );
}

function Info({ icon: Icon, title, value }: { icon: typeof Mail; title: string; value: string }) {
  return (
    <div className="flex items-start gap-3 rounded-xl border border-border bg-card p-4">
      <div className="grid h-10 w-10 place-items-center rounded-lg bg-accent text-primary"><Icon className="h-5 w-5" /></div>
      <div>
        <div className="text-xs text-muted-foreground">{title}</div>
        <div className="font-display font-semibold">{value}</div>
      </div>
    </div>
  );
}
