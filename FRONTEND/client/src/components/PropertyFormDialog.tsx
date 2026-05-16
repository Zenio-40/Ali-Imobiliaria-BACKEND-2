import { useEffect, useRef, useState } from "react";
import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import { z } from "zod";
import { Upload, X, Loader2 } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Textarea } from "@/components/ui/textarea";
import {
  Dialog,
  DialogContent,
  DialogHeader,
  DialogTitle,
  DialogFooter,
} from "@/components/ui/dialog";
import {
  Select,
  SelectContent,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { useProperties, type PropertyDraft } from "@/store/properties";
import type { Property } from "@/lib/types";
import { toast } from "sonner";
import { useAuth } from "@/store/auth";

const schema = z.object({
  title: z.string().trim().min(5, "Título deve ter pelo menos 5 caracteres").max(120),
  type: z.enum(["apartamento", "moradia", "terreno", "escritorio", "loja"]),
  purpose: z.enum(["venda", "arrendamento"]),
  status: z.enum(["disponivel", "reservado", "vendido", "inativo"]),
  price: z.coerce.number().positive("Preço deve ser maior que zero"),
  location: z.string().trim().min(3, "Localização obrigatória").max(120),
  bedrooms: z.coerce.number().int().min(0).max(20),
  bathrooms: z.coerce.number().int().min(0).max(20),
  area: z.coerce.number().positive("Área deve ser maior que zero").max(100000),
  description: z.string().trim().min(10, "Descrição deve ter pelo menos 10 caracteres").max(2000),
  brokerId: z.string().min(1, "Selecione um corretor"),
  image: z.string().min(1, "Adicione uma foto do imóvel"),
  videoUrl: z.string().optional(),
});

type FormValues = z.infer<typeof schema>;

const MAX_FILE_BYTES = 4 * 1024 * 1024; // 4MB

export function PropertyFormDialog({
  open,
  onOpenChange,
  property,
  brokers,
}: {
  open: boolean;
  onOpenChange: (v: boolean) => void;
  property?: Property | null;
  brokers: { id: string; name: string }[];
}) {
  const create = useProperties((s) => s.create);
  const update = useProperties((s) => s.update);
  const { user } = useAuth();
  const fileRef = useRef<HTMLInputElement>(null);
  const [uploading, setUploading] = useState(false);

  const form = useForm<FormValues>({
    resolver: zodResolver(schema),
    defaultValues: {
      title: "",
      type: "apartamento",
      purpose: "venda",
      status: "disponivel",
      price: 0,
      location: "",
      bedrooms: 0,
      bathrooms: 0,
      area: 0,
      description: "",
      brokerId: brokers[0]?.id ?? "",
      image: "",
      videoUrl: "",
    },
  });

  // Reset quando muda o item
  useEffect(() => {
    if (open) {
      if (property) {
        form.reset({
          title: property.title,
          type: property.type,
          purpose: property.purpose,
          status: property.status,
          price: property.price,
          location: property.location,
          bedrooms: property.bedrooms,
          bathrooms: property.bathrooms,
          area: property.area,
          description: property.description,
          brokerId: property.brokerId,
          image: property.image,
          videoUrl: property.videoUrl ?? "",
        });
      } else {
        form.reset({
          title: "",
          type: "apartamento",
          purpose: "venda",
          status: "disponivel",
          price: 0,
          location: "",
          bedrooms: 0,
          bathrooms: 0,
          area: 0,
          description: "",
          brokerId: user?.role === "corretor" ? user.id : brokers[0]?.id ?? "",
          image: "",
          videoUrl: "",
        });
      }
    }
  }, [open, property, brokers, form, user]);

  const image = form.watch("image");

  const handleFile = (file: File) => {
    if (!file.type.startsWith("image/")) {
      toast.error("Selecione um ficheiro de imagem.");
      return;
    }
    if (file.size > MAX_FILE_BYTES) {
      toast.error("Imagem muito grande. Máximo 4MB.");
      return;
    }
    setUploading(true);
    const reader = new FileReader();
    reader.onload = () => {
      form.setValue("image", reader.result as string, { shouldValidate: true });
      setUploading(false);
    };
    reader.onerror = () => {
      toast.error("Erro ao ler a imagem.");
      setUploading(false);
    };
    reader.readAsDataURL(file);
  };

  const onSubmit = async (values: FormValues) => {
    const draft: PropertyDraft = {
      ...values,
      images: [values.image],
      videoUrl: values.videoUrl,
    };
    if (property) {
      await update(property.id, draft);
      toast.success("Imóvel atualizado.");
    } else {
      await create(draft);
      toast.success("Imóvel criado com sucesso.");
    }
    onOpenChange(false);
  };

  return (
    <Dialog open={open} onOpenChange={onOpenChange}>
      <DialogContent className="max-w-3xl max-h-[90vh] overflow-y-auto">
        <DialogHeader>
          <DialogTitle className="font-display text-xl">
            {property ? "Editar imóvel" : "Novo imóvel"}
          </DialogTitle>
        </DialogHeader>

        <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-5">
          {/* Foto */}
          <div>
            <Label>Foto principal</Label>
            <div className="mt-2">
              {image ? (
                <div className="relative overflow-hidden rounded-xl border border-border">
                  <img src={image} alt="Pré-visualização" className="h-56 w-full object-cover" />
                  <button
                    type="button"
                    onClick={() => form.setValue("image", "", { shouldValidate: true })}
                    className="absolute right-2 top-2 rounded-full bg-background/95 p-2 shadow"
                    aria-label="Remover foto"
                  >
                    <X className="h-4 w-4" />
                  </button>
                </div>
              ) : (
                <button
                  type="button"
                  onClick={() => fileRef.current?.click()}
                  className="flex h-44 w-full flex-col items-center justify-center gap-2 rounded-xl border-2 border-dashed border-border bg-muted/40 text-muted-foreground transition hover:border-primary hover:text-primary"
                >
                  {uploading ? <Loader2 className="h-6 w-6 animate-spin" /> : <Upload className="h-6 w-6" />}
                  <span className="text-sm">Clique para enviar uma foto (máx. 4MB)</span>
                </button>
              )}
              <input
                ref={fileRef}
                type="file"
                accept="image/*"
                className="hidden"
                onChange={(e) => {
                  const f = e.target.files?.[0];
                  if (f) handleFile(f);
                  e.target.value = "";
                }}
              />
              {form.formState.errors.image && (
                <p className="mt-1 text-xs text-destructive">{form.formState.errors.image.message}</p>
              )}
            </div>
          </div>

          {/* Title + Location */}
          <div className="grid gap-4 md:grid-cols-2">
            <Field label="Título" error={form.formState.errors.title?.message}>
              <Input {...form.register("title")} placeholder="Apartamento T3 com vista..." />
            </Field>
            <Field label="Localização" error={form.formState.errors.location?.message}>
              <Input {...form.register("location")} placeholder="Talatona, Luanda" />
            </Field>
          </div>

          {/* Tipo / Finalidade / Estado */}
          <div className="grid gap-4 md:grid-cols-3">
            <Field label="Tipo" error={form.formState.errors.type?.message}>
              <Select value={form.watch("type")} onValueChange={(v) => form.setValue("type", v as FormValues["type"])}>
                <SelectTrigger><SelectValue /></SelectTrigger>
                <SelectContent>
                  <SelectItem value="apartamento">Apartamento</SelectItem>
                  <SelectItem value="moradia">Moradia</SelectItem>
                  <SelectItem value="escritorio">Escritório</SelectItem>
                  <SelectItem value="loja">Loja</SelectItem>
                  <SelectItem value="terreno">Terreno</SelectItem>
                </SelectContent>
              </Select>
            </Field>
            <Field label="Finalidade" error={form.formState.errors.purpose?.message}>
              <Select value={form.watch("purpose")} onValueChange={(v) => form.setValue("purpose", v as FormValues["purpose"])}>
                <SelectTrigger><SelectValue /></SelectTrigger>
                <SelectContent>
                  <SelectItem value="venda">Venda</SelectItem>
                  <SelectItem value="arrendamento">Arrendamento</SelectItem>
                </SelectContent>
              </Select>
            </Field>
            <Field label="Estado" error={form.formState.errors.status?.message}>
              <Select value={form.watch("status")} onValueChange={(v) => form.setValue("status", v as FormValues["status"])}>
                <SelectTrigger><SelectValue /></SelectTrigger>
                <SelectContent>
                  <SelectItem value="disponivel">Disponível</SelectItem>
                  <SelectItem value="reservado">Reservado</SelectItem>
                  <SelectItem value="vendido">Vendido</SelectItem>
                  <SelectItem value="inativo">Inativo</SelectItem>
                </SelectContent>
              </Select>
            </Field>
          </div>

          {/* Numéricos */}
          <div className="grid gap-4 md:grid-cols-4">
            <Field label="Preço (Kz)" error={form.formState.errors.price?.message}>
              <Input type="number" min={0} step={1000} {...form.register("price")} />
            </Field>
            <Field label="Quartos" error={form.formState.errors.bedrooms?.message}>
              <Input type="number" min={0} {...form.register("bedrooms")} />
            </Field>
            <Field label="WC" error={form.formState.errors.bathrooms?.message}>
              <Input type="number" min={0} {...form.register("bathrooms")} />
            </Field>
            <Field label="Área (m²)" error={form.formState.errors.area?.message}>
              <Input type="number" min={0} {...form.register("area")} />
            </Field>
          </div>

          {/* Corretor */}
          <Field label="Corretor responsável" error={form.formState.errors.brokerId?.message}>
            <Select value={form.watch("brokerId")} onValueChange={(v) => form.setValue("brokerId", v, { shouldValidate: true })}>
              <SelectTrigger><SelectValue placeholder="Selecionar corretor" /></SelectTrigger>
              <SelectContent>
                {brokers.map((b) => (
                  <SelectItem key={b.id} value={b.id}>{b.name}</SelectItem>
                ))}
              </SelectContent>
            </Select>
          </Field>

          <Field label="Descrição" error={form.formState.errors.description?.message}>
            <Textarea rows={4} {...form.register("description")} placeholder="Descrição detalhada do imóvel..." />
          </Field>

          <Field label="URL do vídeo" error={form.formState.errors.videoUrl?.message}>
            <Input {...form.register("videoUrl")} placeholder="https://..." />
          </Field>

          <DialogFooter>
            <Button type="button" variant="outline" onClick={() => onOpenChange(false)}>Cancelar</Button>
            <Button type="submit" disabled={form.formState.isSubmitting}>
              {property ? "Guardar alterações" : "Criar imóvel"}
            </Button>
          </DialogFooter>
        </form>
      </DialogContent>
    </Dialog>
  );
}

function Field({ label, error, children }: { label: string; error?: string; children: React.ReactNode }) {
  return (
    <div>
      <Label>{label}</Label>
      <div className="mt-1.5">{children}</div>
      {error && <p className="mt-1 text-xs text-destructive">{error}</p>}
    </div>
  );
}
