import { Link, useNavigate } from "@tanstack/react-router";
import { Menu, X, User as UserIcon, LogOut, Heart } from "lucide-react";
import { useState } from "react";
import { Logo } from "./Logo";
import { Button } from "@/components/ui/button";
import { useAuth } from "@/store/auth";
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from "@/components/ui/dropdown-menu";

const nav = [
  { to: "/", label: "Início" },
  { to: "/imoveis", label: "Imóveis" },
  { to: "/sobre", label: "Sobre" },
  { to: "/contacto", label: "Contacto" },
] as const;

export function Header() {
  const [open, setOpen] = useState(false);
  const { user, logout } = useAuth();
  const navigate = useNavigate();

  const dashboardPath =
    user?.role === "admin"
      ? "/admin"
      : user?.role === "corretor"
        ? "/corretor"
        : "/cliente";

  return (
    <header className="sticky top-0 z-40 border-b border-border/60 bg-background/85 backdrop-blur-md">
      <div className="container-page flex h-16 items-center justify-between">
        <Link to="/" className="shrink-0">
          <Logo />
        </Link>

        <nav className="hidden items-center gap-1 md:flex">
          {nav.map((n) => (
            <Link
              key={n.to}
              to={n.to}
              activeOptions={{ exact: n.to === "/" }}
              activeProps={{ className: "text-primary bg-accent/60" }}
              className="rounded-full px-4 py-2 text-sm font-medium text-foreground/80 transition-colors hover:text-primary"
            >
              {n.label}
            </Link>
          ))}
        </nav>

        <div className="hidden items-center gap-2 md:flex">
          {user ? (
            <DropdownMenu>
              <DropdownMenuTrigger asChild>
                <Button variant="outline" className="rounded-full">
                  <UserIcon className="mr-2 h-4 w-4" />
                  {user.name.split(" ")[0]}
                </Button>
              </DropdownMenuTrigger>
              <DropdownMenuContent align="end" className="w-52">
                <DropdownMenuItem onClick={() => navigate({ to: dashboardPath })}>
                  <UserIcon className="mr-2 h-4 w-4" /> Painel
                </DropdownMenuItem>
                {user.role === "cliente" && (
                  <DropdownMenuItem onClick={() => navigate({ to: "/cliente/favoritos" })}>
                    <Heart className="mr-2 h-4 w-4" /> Favoritos
                  </DropdownMenuItem>
                )}
                <DropdownMenuSeparator />
                <DropdownMenuItem
                  onClick={() => {
                    logout();
                    navigate({ to: "/" });
                  }}
                >
                  <LogOut className="mr-2 h-4 w-4" /> Terminar sessão
                </DropdownMenuItem>
              </DropdownMenuContent>
            </DropdownMenu>
          ) : (
            <>
<Button variant="ghost" asChild>
                <a href="/login-cliente">Entrar</a>
              </Button>
              <Button asChild className="rounded-full">
                <Link to="/registo">Criar conta</Link>
              </Button>
            </>
          )}
        </div>

        <button
          aria-label="Abrir menu"
          className="md:hidden"
          onClick={() => setOpen((o) => !o)}
        >
          {open ? <X /> : <Menu />}
        </button>
      </div>

      {open && (
        <div className="border-t border-border bg-background md:hidden">
          <div className="container-page flex flex-col gap-1 py-3">
            {nav.map((n) => (
              <Link
                key={n.to}
                to={n.to}
                onClick={() => setOpen(false)}
                className="rounded-md px-3 py-2 text-sm font-medium hover:bg-accent"
              >
                {n.label}
              </Link>
            ))}
            <div className="mt-2 flex gap-2">
              {user ? (
                <Button asChild className="flex-1">
                  <Link to={dashboardPath}>Painel</Link>
                </Button>
              ) : (
                <>
<Button variant="outline" asChild className="flex-1">
                    <a href="/login-cliente">Entrar</a>
                  </Button>
                  <Button asChild className="flex-1">
                    <Link to="/registo">Criar conta</Link>
                  </Button>
                </>
              )}
            </div>
          </div>
        </div>
      )}
    </header>
  );
}
