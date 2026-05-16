import { createFileRoute, Link, useNavigate } from "@tanstack/react-router";
import { useState } from "react";
import { Logo } from "@/components/Logo";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useAuth } from "@/store/auth";
import { toast } from "sonner";
import heroHouse from "@/assets/hero-house.jpg";

export const Route = createFileRoute("/login-cliente")({
  head: () => ({ meta: [{ title: "Entrar (Cliente) — Ali-Imobiliária" }] }),
  component: LoginClientePage,
});

function LoginClientePage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const loginCliente = useAuth((s) => s.loginCliente);
  const navigate = useNavigate();

  const submit = async (e: React.FormEvent) => {
    e.preventDefault();
    const u = await loginCliente(email, password);
    if (!u) {
      toast.error("Email/telefone ou palavra-passe invalido.");
      return;
    }
    toast.success("Entrada realizada.");
    navigate({ to: "/cliente" });
  };


  return (
    <div className="grid min-h-screen lg:grid-cols-2">
      <div className="flex flex-col justify-center px-6 py-10 sm:px-12">
        <div className="mx-auto w-full max-w-md">
          <Link to="/"><Logo /></Link>
          <h1 className="mt-10 font-display text-3xl font-extrabold">Entrar como cliente</h1>
          <p className="mt-2 text-sm text-muted-foreground">Guarde favoritos, agende visitas e acompanhe os seus pedidos.</p>

          <form onSubmit={submit} className="mt-8 space-y-4">
            <div>
              <Label htmlFor="email">Email ou telefone</Label>
              <Input id="email" required value={email} onChange={(e) => setEmail(e.target.value)} placeholder="seu@email.ao ou 9XX XXX XXX" />
            </div>
            <div>
              <Label htmlFor="password">Palavra-passe</Label>
              <Input id="password" type="password" required value={password} onChange={(e) => setPassword(e.target.value)} placeholder="••••••••" />
            </div>
            <Button type="submit" className="w-full rounded-full" size="lg">Entrar</Button>
          </form>

          <p className="mt-6 text-center text-sm text-muted-foreground">
            Ainda não tem conta? <Link to="/registo" className="font-medium text-primary">Criar conta</Link>
          </p>


        </div>
      </div>

      <div className="relative hidden lg:block">
        <img src={heroHouse} alt="" className="h-full w-full object-cover" />
        <div className="absolute inset-0" style={{ background: "var(--gradient-hero)" }} />
        <div className="absolute inset-0 flex items-end p-12 text-background">
          <div>
            <h2 className="font-display text-3xl font-bold">Bem-vindo ao seu painel</h2>
            <p className="mt-2 max-w-md text-background/80">Acesse a sua área pessoal e encontre imóveis com confiança.</p>
          </div>
        </div>
      </div>
    </div>
  );
}

