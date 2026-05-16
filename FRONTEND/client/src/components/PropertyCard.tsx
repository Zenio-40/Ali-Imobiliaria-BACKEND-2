import { Link } from "@tanstack/react-router";
import { Bed, Bath, Maximize, MapPin, Heart } from "lucide-react";
import { Badge } from "@/components/ui/badge";
import { formatPrice } from "@/lib/mock-data";
import type { Property } from "@/lib/types";
import { useAuth } from "@/store/auth";
import { cn } from "@/lib/utils";

export function PropertyCard({ property }: { property: Property }) {
  const { favorites, toggleFavorite, user } = useAuth();
  const isFav = favorites.includes(property.id);

  return (
    <article className="group overflow-hidden rounded-2xl border border-border bg-card shadow-[var(--shadow-card)] transition-all hover:-translate-y-1 hover:shadow-[var(--shadow-elegant)]">
      <Link to="/imoveis/$id" params={{ id: property.id }} className="block">
        <div className="relative aspect-[4/3] overflow-hidden">
          <img
            src={property.image}
            alt={property.title}
            loading="lazy"
            className="h-full w-full object-cover transition-transform duration-700 group-hover:scale-110"
          />
          <div className="absolute left-3 top-3 flex gap-2">
            <Badge className="rounded-full bg-primary text-primary-foreground border-0 capitalize">
              {property.purpose}
            </Badge>
            {property.status !== "disponivel" && (
              <Badge variant="secondary" className="rounded-full capitalize">
                {property.status}
              </Badge>
            )}
          </div>
          {user?.role === "cliente" && (
            <button
              onClick={(e) => {
                e.preventDefault();
                toggleFavorite(property.id);
              }}
              className="absolute right-3 top-3 rounded-full bg-background/95 p-2 shadow-md transition hover:scale-110"
              aria-label="Favoritar"
            >
              <Heart className={cn("h-4 w-4", isFav ? "fill-primary text-primary" : "text-foreground")} />
            </button>
          )}
        </div>
      </Link>

      <div className="p-5">
        <div className="flex items-start justify-between gap-3">
          <Link to="/imoveis/$id" params={{ id: property.id }} className="line-clamp-1 font-display text-base font-semibold hover:text-primary">
            {property.title}
          </Link>
        </div>
        <div className="mt-1 flex items-center gap-1 text-xs text-muted-foreground">
          <MapPin className="h-3.5 w-3.5" /> {property.location}
        </div>

        <div className="mt-4 flex items-center justify-between">
          <div className="text-lg font-display font-bold text-secondary">
            {formatPrice(property.price, property.purpose)}
          </div>
        </div>

        <div className="mt-4 flex items-center gap-4 border-t border-border pt-3 text-xs text-muted-foreground">
          {property.bedrooms > 0 && <span className="flex items-center gap-1"><Bed className="h-3.5 w-3.5" /> {property.bedrooms}</span>}
          <span className="flex items-center gap-1"><Bath className="h-3.5 w-3.5" /> {property.bathrooms}</span>
          <span className="flex items-center gap-1"><Maximize className="h-3.5 w-3.5" /> {property.area}m²</span>
        </div>
      </div>
    </article>
  );
}
