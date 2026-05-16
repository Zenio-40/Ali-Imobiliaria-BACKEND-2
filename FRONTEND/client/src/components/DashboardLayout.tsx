import { Link, useNavigate, useLocation } from "@tanstack/react-router";
import type { ComponentType } from "react";
import { LogOut, Bell } from "lucide-react";
import { Logo } from "./Logo";
import { Button } from "@/components/ui/button";
import { useAuth } from "@/store/auth";
import { cn } from "@/lib/utils";

export interface NavItem {
  to: string;
  label: string;
  icon: ComponentType<{ className?: string }>;
}

export function DashboardLayout({
  title,
  nav,
  children,
}: {
  title: string;
  nav: NavItem[];
  children: React.ReactNode;
}) {
  const { user, logout } = useAuth();
  const navigate = useNavigate();
  const location = useLocation();

  return (
    <div className="grid min-h-screen md:grid-cols-[260px_1fr] bg-muted/40">
      {/* Sidebar */}
      <aside className="hidden border-r border-border bg-card md:flex md:flex-col">
        <div className="border-b border-border p-5">
          <Link to="/"><Logo /></Link>
        </div>
        <nav className="flex-1 space-y-1 p-3">
          {nav.map((n) => {
            const active = location.pathname === n.to || (n.to !== "/" && location.pathname.startsWith(n.to));
            return (
              <Link
                key={n.to}
                to={n.to}
                className={cn(
                  "flex items-center gap-3 rounded-xl px-3 py-2.5 text-sm font-medium transition",
                  active ? "bg-primary text-primary-foreground shadow-[var(--shadow-card)]" : "text-foreground/80 hover:bg-accent",
                )}
              >
                <n.icon className="h-4 w-4" />
                {n.label}
              </Link>
            );
          })}
        </nav>
        <div className="border-t border-border p-3">
          <Button
            variant="ghost"
            className="w-full justify-start"
            onClick={() => {
              logout();
              navigate({ to: "/" });
            }}
          >
            <LogOut className="mr-2 h-4 w-4" /> Sair
          </Button>
        </div>
      </aside>

      <div className="flex flex-col">
        {/* Topbar */}
        <header className="flex h-16 items-center justify-between border-b border-border bg-card px-6">
          <div>
            <h1 className="font-display text-lg font-bold">{title}</h1>
            <div className="text-xs text-muted-foreground">Bem-vindo, {user?.name}</div>
          </div>
          <div className="flex items-center gap-3">
            <button className="rounded-full p-2 hover:bg-accent" aria-label="Notificações"><Bell className="h-4 w-4" /></button>
            <div className="grid h-9 w-9 place-items-center rounded-full bg-primary text-primary-foreground font-display font-bold text-sm">
              {user?.name?.charAt(0)}
            </div>
          </div>
        </header>

        {/* Mobile nav */}
        <div className="flex gap-2 overflow-x-auto border-b border-border bg-card p-2 md:hidden">
          {nav.map((n) => (
            <Link key={n.to} to={n.to} className="rounded-full bg-muted px-4 py-2 text-xs font-medium whitespace-nowrap">{n.label}</Link>
          ))}
        </div>

        <main className="flex-1 p-6 md:p-8">{children}</main>
      </div>
    </div>
  );
}

export function StatCard({ label, value, hint, icon: Icon, accent }: { label: string; value: string; hint?: string; icon: ComponentType<{ className?: string }>; accent?: boolean }) {
  return (
    <div className={cn("rounded-2xl border border-border p-5 shadow-[var(--shadow-card)]", accent ? "bg-secondary text-secondary-foreground" : "bg-card")}>
      <div className="flex items-start justify-between">
        <div>
          <div className={cn("text-xs uppercase tracking-wider", accent ? "text-secondary-foreground/70" : "text-muted-foreground")}>{label}</div>
          <div className="mt-2 font-display text-2xl font-extrabold">{value}</div>
          {hint && <div className={cn("mt-1 text-xs", accent ? "text-secondary-foreground/60" : "text-muted-foreground")}>{hint}</div>}
        </div>
        <div className={cn("grid h-10 w-10 place-items-center rounded-xl", accent ? "bg-primary text-primary-foreground" : "bg-accent text-primary")}>
          <Icon className="h-5 w-5" />
        </div>
      </div>
    </div>
  );
}
