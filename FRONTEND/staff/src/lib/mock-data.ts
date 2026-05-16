import p1 from "@/assets/property-1.jpg";
import p2 from "@/assets/property-2.jpg";
import p3 from "@/assets/property-3.jpg";
import p4 from "@/assets/property-4.jpg";
import type { Property, User, Visit } from "./types";

export const properties: Property[] = [
  {
    id: "1",
    title: "Apartamento moderno T3 com vista para a cidade",
    type: "apartamento",
    purpose: "venda",
    status: "disponivel",
    price: 85000000,
    location: "Talatona, Luanda",
    bedrooms: 3,
    bathrooms: 2,
    area: 145,
    image: p1,
    description:
      "Apartamento totalmente renovado, acabamentos premium, cozinha equipada, varanda ampla com vista panorâmica. Condomínio fechado com piscina e ginásio.",
    brokerId: "b1",
    createdAt: "2025-03-12",
    featured: true,
  },
  {
    id: "2",
    title: "Moradia V4 com piscina e jardim",
    type: "moradia",
    purpose: "venda",
    status: "disponivel",
    price: 220000000,
    location: "Benfica, Luanda",
    bedrooms: 4,
    bathrooms: 3,
    area: 380,
    image: p2,
    description:
      "Moradia exclusiva em condomínio fechado, jardim privativo, piscina, suite master e garagem para 3 viaturas.",
    brokerId: "b1",
    createdAt: "2025-04-02",
    featured: true,
  },
  {
    id: "3",
    title: "Apartamento T2 mobilado para arrendamento",
    type: "apartamento",
    purpose: "arrendamento",
    status: "disponivel",
    price: 450000,
    location: "Maianga, Luanda",
    bedrooms: 2,
    bathrooms: 1,
    area: 95,
    image: p3,
    description:
      "Apartamento totalmente mobilado e equipado, pronto para habitar. Excelente localização com fácil acesso a transportes.",
    brokerId: "b2",
    createdAt: "2025-04-15",
    featured: true,
  },
  {
    id: "4",
    title: "Townhouse moderno em condomínio premium",
    type: "moradia",
    purpose: "venda",
    status: "disponivel",
    price: 165000000,
    location: "Vila Alice, Luanda",
    bedrooms: 4,
    bathrooms: 3,
    area: 290,
    image: p4,
    description:
      "Townhouse de arquitetura contemporânea, condomínio fechado com segurança 24h, áreas comuns e zona infantil.",
    brokerId: "b2",
    createdAt: "2025-04-20",
  },
  {
    id: "5",
    title: "Apartamento T1 vista mar para arrendamento",
    type: "apartamento",
    purpose: "arrendamento",
    status: "disponivel",
    price: 380000,
    location: "Ilha do Cabo, Luanda",
    bedrooms: 1,
    bathrooms: 1,
    area: 65,
    image: p3,
    description:
      "Apartamento com vista mar deslumbrante, totalmente mobilado. Ideal para profissionais ou casais.",
    brokerId: "b1",
    createdAt: "2025-04-22",
  },
  {
    id: "6",
    title: "Escritório premium no centro empresarial",
    type: "escritorio",
    purpose: "arrendamento",
    status: "reservado",
    price: 1200000,
    location: "Kinaxixi, Luanda",
    bedrooms: 0,
    bathrooms: 2,
    area: 220,
    image: p1,
    description: "Espaço de escritórios open-space em torre empresarial com estacionamento.",
    brokerId: "b2",
    createdAt: "2025-04-10",
  },
];

export const users: User[] = [
  { id: "c1", name: "Maria Silva", email: "cliente@ali.ao", phone: "923000001", role: "cliente" },
  { id: "b1", name: "João Pereira", email: "corretor@ali.ao", phone: "923000002", role: "corretor" },
  { id: "b2", name: "Ana Costa", email: "ana@ali.ao", phone: "923000003", role: "corretor" },
  { id: "a1", name: "Admin Ali", email: "admin@ali.ao", phone: "923000000", role: "admin" },
];

export const visits: Visit[] = [
  { id: "v1", propertyId: "1", clientId: "c1", brokerId: "b1", date: "2025-05-02T15:00", status: "confirmada" },
  { id: "v2", propertyId: "3", clientId: "c1", brokerId: "b2", date: "2025-05-05T10:00", status: "pendente" },
];

export function formatPrice(value: number, purpose: "venda" | "arrendamento") {
  const formatted = new Intl.NumberFormat("pt-AO", { maximumFractionDigits: 0 }).format(value);
  return `${formatted} Kz${purpose === "arrendamento" ? "/mês" : ""}`;
}
