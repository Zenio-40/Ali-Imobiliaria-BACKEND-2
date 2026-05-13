import { createFileRoute, Link, useNavigate } from "@tanstack/react-router";
import { useState } from "react";
import { Logo } from "@/components/Logo";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useAuth } from "@/store/auth";
import { toast } from "sonner";
import heroHouse from "@/assets/hero-house.jpg";

export const Route = createFileRoute("/login-funcionarios")({
  head: () => ({ meta: [{ title: "Entrar (Funcionários) — Ali-Imobiliária" }] }),
  component: LoginFuncionariosPage,
});

function LoginFuncionariosPage() {
  const [telefone, setTelefone] = useState("");
  const [password, setPassword] = useState("");

  const login = useAuth((s) => s.login);
  const navigate = useNavigate();

  const submit = async (e: React.FormEvent) => {
    e.preventDefault();

    // Backend: LoginDTO usa Telefone e Senha
    const u = await login(telefone, password);

    if (!u) {
      toast.error("Credenciais inválidas.");
      return;
    }

    if (u.role === "cliente") {
      toast.error("Este login é apenas para funcionários.");
      return;
    }

    toast.success(`Bem-vindo, ${u.name}!`);

    navigate({
      to: u.role === "admin" ? "/admin" : "/corretor",
    });
  };

  return (
    <div className="grid min-h-screen lg:grid-cols-2">
      <div className="flex flex-col justify-center px-6 py-10 sm:px-12">
        <div className="mx-auto w-full max-w-md">
          <Link to="/" ><Logo /></Link>
          <h1 className="mt-10 font-display text-3xl font-extrabold">Entrar (Funcionários)</h1>
          <p className="mt-2 text-sm text-muted-foreground">Administre imóveis, leads e visitas.</p>

          <form onSubmit={submit} className="mt-8 space-y-4">
            <div>
              <Label htmlFor="telefone">Telefone</Label>
              <Input id="telefone" type="tel" required value={telefone} onChange={(e) => setTelefone(e.target.value)} placeholder="seu telefone" />
            </div>
            <div>
              <Label htmlFor="password">Palavra-passe</Label>
              <Input id="password" type="password" required value={password} onChange={(e) => setPassword(e.target.value)} placeholder="••••••••" />
            </div>
            <Button type="submit" className="w-full rounded-full" size="lg">Aceder</Button>
          </form>

          <p className="mt-6 text-center text-sm text-muted-foreground">
<a href="/login-cliente" className="font-medium text-primary">Entrar como cliente</a>
          </p>
        </div>
      </div>

      <div className="relative hidden lg:block">
        <img src={heroHouse} alt="" className="h-full w-full object-cover" />
        <div className="absolute inset-0" style={{ background: "var(--gradient-hero)" }} />
        <div className="absolute inset-0 flex items-end p-12 text-background">
          <div>
            <h2 className="font-display text-3xl font-bold">Painel interno</h2>
            <p className="mt-2 max-w-md text-background/80">Gestão profissional para o seu trabalho diário.</p>
          </div>
        </div>
      </div>
    </div>
  );
}

