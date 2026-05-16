import { createFileRoute, Link, useNavigate } from "@tanstack/react-router";
import { useState } from "react";
import { Logo } from "@/components/Logo";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { useAuth } from "@/store/auth";
import { toast } from "sonner";
import heroHouse from "@/assets/hero-house.jpg";

export const Route = createFileRoute("/login")({
  head: () => ({ meta: [{ title: "Entrar — Ali-Imobiliária" }] }),
  component: LoginPage,
});

function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const login = useAuth((s) => s.login);
  const navigate = useNavigate();

  const submit = async (e: React.FormEvent) => {
    e.preventDefault();

    const u = await login(email, password);
    if (!u) {
      toast.error("Credenciais inválidas.");
      return;
    }

    toast.success(`Bem-vindo, ${u.name}!`);

    // Mantemos a rota antiga apenas como "ponto de entrada".
    // Após login, mandamos sempre para a área correta.
    navigate({
      to: u.role === "admin" ? "/admin" : u.role === "corretor" ? "/corretor" : "/cliente",
    });
  };

  return (
    <div className="grid min-h-screen lg:grid-cols-2">
      <div className="flex flex-col justify-center px-6 py-10 sm:px-12">
        <div className="mx-auto w-full max-w-md">
          <Link to="/" className="contents">
            <h1 className="mt-10 font-display text-3xl font-extrabold">Área pessoal</h1>
          </Link>
          <p className="mt-2 text-sm text-muted-foreground">Escolha o tipo de conta e entre no seu painel.</p>

          <div className="mt-6 grid grid-cols-1 gap-3">
            <Button asChild variant="outline" className="rounded-full" size="lg">
              <a href="/login-cliente">Entrar como Cliente</a>
            </Button>
            <Button asChild className="rounded-full" size="lg">
              <a href="/login-funcionarios">Entrar como Funcionário</a>
            </Button>
          </div>

          <div className="mt-8 rounded-2xl border border-border bg-card p-5">
            <div className="text-sm font-medium">Login rápido (compatível)</div>
            <p className="mt-1 text-sm text-muted-foreground">Para não quebrar links antigos, esta página continua funcional.</p>

            <form onSubmit={submit} className="mt-5 space-y-4">
              <div>
                <Label htmlFor="email">Email</Label>
                <Input id="email" type="email" required value={email} onChange={(e) => setEmail(e.target.value)} placeholder="seu@email.ao" />
              </div>
              <div>
                <Label htmlFor="password">Palavra-passe</Label>
                <Input id="password" type="password" required value={password} onChange={(e) => setPassword(e.target.value)} placeholder="••••••••" />
              </div>
              <Button type="submit" className="w-full rounded-full" size="lg">Entrar</Button>
            </form>

            <p className="mt-4 text-center text-xs text-muted-foreground">
              Para melhor experiência, use as telas específicas: <a href="/login-cliente" className="text-primary font-medium">Cliente</a> ou <a href="/login-funcionarios" className="text-primary font-medium">Funcionários</a>.
            </p>
          </div>
        </div>
      </div>

      <div className="relative hidden lg:block">
        <img src={heroHouse} alt="" className="h-full w-full object-cover" />
        <div className="absolute inset-0" style={{ background: "var(--gradient-hero)" }} />
        <div className="absolute inset-0 flex items-end p-12 text-background">
          <div>
            <h2 className="font-display text-3xl font-bold">Bem-vindo à Ali-Imobiliária</h2>
            <p className="mt-2 max-w-md text-background/80">Gestão e acompanhamento do seu processo, com segurança.</p>
          </div>
        </div>
      </div>
    </div>
  );
}
