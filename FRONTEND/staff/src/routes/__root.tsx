import { Outlet, createRootRoute } from "@tanstack/react-router";
import { Toaster } from "@/components/ui/sonner";
import { useAuth } from "@/store/auth";
import { useProperties } from "@/store/properties";
import { useEffect } from "react";

export const Route = createRootRoute({
  component: RootComponent,
});

function RootComponent() {
  const authHydrated = useAuth((s) => s.hasHydrated);
  const propertiesHydrated = useProperties((s) => s.hasHydrated);

  useEffect(() => {
    void useAuth.persist.rehydrate();
    void useProperties.persist.rehydrate();
  }, []);

  if (!authHydrated || !propertiesHydrated) {
    return (
      <div className="min-h-screen flex items-center justify-center">
        <div className="text-sm text-muted-foreground">Carregando...</div>
      </div>
    );
  }

  return (
    <>
      <Outlet />
      <Toaster richColors position="top-right" />
    </>
  );
}

