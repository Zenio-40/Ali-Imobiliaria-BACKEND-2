import logo from "@/assets/logo.png";

export function Logo({ size = 40, withText = true }: { size?: number; withText?: boolean }) {
  return (
    <div className="flex items-center gap-2.5">
      <img src={logo} alt="Ali-Imobiliária" width={size} height={size} className="rounded-md object-cover" style={{ width: size, height: size }} />
      {withText && (
        <div className="leading-tight">
          <div className="font-display font-extrabold text-[15px] tracking-tight text-secondary">
            ALI <span className="text-primary">·</span> Imobiliária
          </div>
          <div className="text-[10px] text-muted-foreground">Gestão e mediação de imóveis</div>
        </div>
      )}
    </div>
  );
}
