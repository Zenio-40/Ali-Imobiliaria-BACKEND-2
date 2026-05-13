# TODO - Modelar Frontend com base no Backend

## Passo 1 — Planejamento e contrato
- [x] Levantar rotas do backend (Auth/Cliente/Admin/Corretora)
- [x] Analisar frontend (rotas/estado atual com mock-data)
- [x] Aprovar plano para substituir mock-data por API

## Passo 2 — Camada API
- [x] Criar `FRONTEND/src/lib/api.ts` com wrapper de fetch (baseURL, Authorization Bearer, parse de respostas)




## Passo 3 — Autenticação real
- [x] Atualizar `FRONTEND/src/store/auth.ts`:

  - [ ] Implementar login via `POST /api/auth/login/funcionario`
  - [ ] Guardar token JWT no Zustand (persist)
  - [ ] Fazer logout limpar token

## Passo 4 — Dados públicos (imóveis)
- [ ] Substituir `useProperties`/mock-data no catálogo:
  - [ ] Criar/atualizar store para buscar `GET /api/cliente/imoveis?pagina=&quantidade=`
  - [ ] Ajustar rota `FRONTEND/src/routes/imoveis.index.tsx` e `FRONTEND/src/routes/imoveis.$id.tsx`

## Passo 5 — Cliente (favoritos e visitas/solicitações)
- [ ] Atualizar rotas:
  - [ ] `cliente.favoritos.tsx`: listar via `GET /api/cliente/favoritos/{clienteId}`
  - [ ] `cliente.visitas.tsx`: listar via `GET /api/cliente/solicitacoes/cliente/{clienteId}`
  - [ ] Ajustar add/remove favorito para usar `POST /api/cliente/favorito` e `DELETE /api/cliente/favorito/{id}`

## Passo 6 — Painéis Admin/Corretora
- [ ] Atualizar rotas:
  - [ ] `admin.index.tsx`: usar `GET /api/admin/dashboard`
  - [ ] `admin.imoveis.tsx` e `admin.imoveis.inativos.tsx`: usar endpoints reais para listagem/ativar/desativar
  - [ ] `admin.utilizadores.tsx`: usar endpoints reais para listagem de funcionários/proprietários
  - [ ] `corretor.index.tsx`: usar `GET /api/corretora/dashboard`
  - [ ] `corretor.imoveis.tsx`: usar endpoints reais para listagem dos imóveis do corretor

## Passo 7 — Ajustes de tipos/DTO
- [ ] Alinhar `FRONTEND/src/lib/types.ts` com shape real retornado pelo backend (campos e nomenclatura)

## Passo 8 — Testes
- [ ] Rodar `npm run dev`
- [ ] Testar: login funcionarios -> dashboard admin/corretor
- [ ] Testar: catálogo de imóveis e detalhes
- [ ] Testar: favoritos e visitas do cliente

