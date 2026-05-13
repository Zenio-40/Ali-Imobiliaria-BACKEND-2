import { Link } from "@tanstack/react-router";
import { Mail, Phone, MapPin, Facebook, Instagram } from "lucide-react";
import { Logo } from "./Logo";

export function Footer() {
  return (
    <footer className="mt-24 border-t border-border bg-secondary text-secondary-foreground">
      <div className="container-page grid gap-10 py-14 md:grid-cols-4">
        <div className="md:col-span-2">
          <div className="rounded-xl bg-background/10 p-3 inline-block">
            <Logo />
          </div>
          <p className="mt-4 max-w-md text-sm text-secondary-foreground/70">
            A Ali-Imobiliária acompanha-o em cada etapa da compra, venda ou arrendamento do
            seu imóvel, com transparência e profissionalismo.
          </p>
          <div className="mt-5 flex gap-3">
            <a className="rounded-full bg-background/10 p-2 hover:bg-primary" href="#"><Facebook className="h-4 w-4" /></a>
            <a className="rounded-full bg-background/10 p-2 hover:bg-primary" href="#"><Instagram className="h-4 w-4" /></a>
          </div>
        </div>

        <div>
          <h4 className="font-display text-sm font-semibold uppercase tracking-wider text-primary">Navegação</h4>
          <ul className="mt-4 space-y-2 text-sm text-secondary-foreground/80">
            <li><Link to="/imoveis">Imóveis</Link></li>
            <li><Link to="/sobre">Sobre</Link></li>
            <li><Link to="/contacto">Contacto</Link></li>
<li><a href="/login-cliente">Área pessoal</a></li>
          </ul>
        </div>

        <div>
          <h4 className="font-display text-sm font-semibold uppercase tracking-wider text-primary">Contactos</h4>
          <ul className="mt-4 space-y-3 text-sm text-secondary-foreground/80">
            <li className="flex items-start gap-2"><MapPin className="h-4 w-4 mt-0.5 text-primary" /><span>Luanda, Angola</span></li>
            <li className="flex items-start gap-2"><Phone className="h-4 w-4 mt-0.5 text-primary" /><span>+244 923 000 000</span></li>
            <li className="flex items-start gap-2"><Mail className="h-4 w-4 mt-0.5 text-primary" /><span>geral@ali-imobiliaria.ao</span></li>
          </ul>
        </div>
      </div>
      <div className="border-t border-background/10 py-5 text-center text-xs text-secondary-foreground/60">
        © {new Date().getFullYear()} Ali-Imobiliária. Todos os direitos reservados.
      </div>
    </footer>
  );
}
