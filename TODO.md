# TODO - Separação Frontends (Cliente x Funcionários)

## Passo 1
- [ ] Criar estrutura de pastas para 2 frontends: `FRONTEND/client` e `FRONTEND/staff`.

## Passo 2
- [ ] Copiar/compartilhar código base: components comuns, UI, hooks e lib/store.
- [ ] Definir como compartilhar: via imports (sem duplicar em excesso) usando um “package” local ou via cópia controlada.

## Passo 3
- [ ] Criar entrypoints próprios de router para cada frontend.
- [ ] Ajustar rotas: cliente só com `cliente.*` + `login-cliente` (+ páginas públicas necessárias).
- [ ] Ajustar rotas: staff/admin/corretor só com `admin.*` `corretor.*` + `login-funcionarios` (+ páginas públicas necessárias).

## Passo 4
- [ ] Criar/ajustar configs Vite e scripts npm para rodar os 2 frontends.

## Passo 5
- [ ] Garantir que ambos usam a MESMA API (`VITE_API_BASE_URL` e `api.ts`).

## Passo 6
- [ ] Testar localmente:
  - [ ] Frontend Cliente: login cliente, páginas de favoritos/visitas.
  - [ ] Frontend Staff: login funcionário, dashboards/imóveis/admin.

## Passo 7
- [ ] Ajustar build/deploy se houver.

