type ApiEnvelope<T> = {
  dados?: T;
  mensagem?: string;
  codigo?: number;
  token?: string;
  sucesso?: boolean;
};

const API_BASE_FALLBACK = "http://localhost:5000";

function getBaseUrl() {
  const envBase = import.meta.env.VITE_API_BASE_URL as string | undefined;
  const raw = envBase && envBase.trim().length > 0 ? envBase : API_BASE_FALLBACK;
  return raw.replace(/\/+$/, "");
}

const TOKEN_KEY = "__ALIMOBI_TOKEN__";

export function setBearerToken(token: string | null) {
  if (!token) {
    delete (window as unknown as Record<string, unknown>)[TOKEN_KEY];
    return;
  }
  (window as unknown as Record<string, unknown>)[TOKEN_KEY] = token;
}

function getBearerToken() {
  const v = (window as unknown as Record<string, unknown>)[TOKEN_KEY];
  return typeof v === "string" ? v : null;
}

async function apiRequest<T>(
  path: string,
  options: RequestInit & { auth?: boolean } = {},
): Promise<ApiEnvelope<T>> {
  const baseUrl = getBaseUrl();
  const url = `${baseUrl}${path.startsWith("/") ? "" : "/"}${path}`;

  const authEnabled = options.auth ?? true;
  const token = authEnabled ? getBearerToken() : null;

  const headers: HeadersInit = {
    "Content-Type": "application/json",
    ...(options.headers ?? {}),
  };

  if (token) {
    (headers as Record<string, string>).Authorization = `Bearer ${token}`;
  }

  const res = await fetch(url, {
    ...options,
    headers,
  });

  const text = await res.text();
  let json: unknown = null;
  try {
    json = text ? JSON.parse(text) : null;
  } catch {
    json = null;
  }

  const envelope = (json ?? {}) as ApiEnvelope<T>;

  if (!res.ok) {
    if (envelope.mensagem == null) envelope.mensagem = `HTTP ${res.status}`;
    if (envelope.codigo == null) envelope.codigo = res.status;
    throw envelope;
  }

  return envelope;
}

function withQuery(path: string, query?: Record<string, unknown>) {
  if (!query) return path;

  const params = new URLSearchParams(
    Object.entries(query)
      .filter(([, v]) => v !== undefined && v !== null)
      .map(([k, v]) => [k, String(v)]),
  );

  if (!params.toString()) return path;
  return `${path}${path.includes("?") ? "&" : "?"}${params.toString()}`;
}

export async function apiGet<T>(path: string, query?: Record<string, unknown>) {
  const fullPath = withQuery(path, query);
  return apiRequest<T>(fullPath, { method: "GET" });
}

export async function apiPost<T>(path: string, body?: unknown, auth: boolean = true) {
  return apiRequest<T>(path, {
    method: "POST",
    body: body == null ? undefined : JSON.stringify(body),
    auth,
  });
}

export async function apiPut<T>(path: string, body?: unknown, auth: boolean = true) {
  return apiRequest<T>(path, {
    method: "PUT",
    body: body == null ? undefined : JSON.stringify(body),
    auth,
  });
}

export async function apiPatch<T>(path: string, body?: unknown, auth: boolean = true) {
  return apiRequest<T>(path, {
    method: "PATCH",
    body: body == null ? undefined : JSON.stringify(body),
    auth,
  });
}

export async function apiDelete<T>(path: string, auth: boolean = true) {
  return apiRequest<T>(path, { method: "DELETE", auth });
}

export function unwrapDados<T>(envelope: ApiEnvelope<T>): T {
  return envelope.dados as T;
}

export function unwrapMensagem(envelope: ApiEnvelope<unknown>): string {
  return envelope.mensagem ?? "";
}

