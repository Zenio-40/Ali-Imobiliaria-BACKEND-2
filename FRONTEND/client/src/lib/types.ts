export type PropertyPurpose = "venda" | "arrendamento";
export type PropertyType = "apartamento" | "moradia" | "terreno" | "escritorio" | "loja";
export type PropertyStatus = "disponivel" | "reservado" | "vendido" | "inativo";

export interface Property {
  id: string;
  title: string;
  type: PropertyType;
  purpose: PropertyPurpose;
  status: PropertyStatus;
  price: number;
  location: string;
  bedrooms: number;
  bathrooms: number;
  area: number;
  image: string;
  images?: string[];
  videoUrl?: string;
  approvalStatus?: "Pendente" | "Aprovado" | "Reprovado";
  description: string;
  brokerId: string;
  createdAt: string;
  featured?: boolean;
}

export type Role = "cliente" | "corretor" | "admin";

export interface User {
  id: string;
  name: string;
  email: string;
  phone: string;
  role: Role;
  avatar?: string;
}

export interface Visit {
  id: string;
  propertyId: string;
  clientId: string;
  brokerId: string;
  date: string;
  status: "pendente" | "confirmada" | "concluida" | "cancelada";
}
