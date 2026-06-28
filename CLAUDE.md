# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview
DemoCore2026 is an ASP.NET Core (`net10.0`) Web API for indexing and searching `Product` records
in Elasticsearch.

## Commands
- Build: `dotnet build`
- Run (HTTP profile, no browser): `dotnet run --launch-profile http` → http://localhost:5256
- Run (HTTPS profile, opens Swagger): `dotnet run --launch-profile https` → https://localhost:7178
- Swagger UI (Development only): `/swagger`
- No test project exists yet.

## Prerequisites
The API requires a running Elasticsearch instance at `http://localhost:9200` (hardcoded in
`Elasticsearch/ElasticsearchClientFactory.cs`). Index name is `products`. Without it, all
endpoints fail at the repository layer.

## Architecture
Request flow is a strict 3-layer pipeline:

`ProductsController` → `IProductsService` → `IProductsRepository` → Elasticsearch

- **Controllers/** — thin HTTP layer; returns raw `Ok(...)`.
- **Services/** — business rules and input validation (e.g. name required, id > 0, empty query
  short-circuits). Put validation/logic here, not in controllers or repositories.
- **Repositories/** — all Elasticsearch access. Note: `ProductsRepository` constructs its client
  directly via the static `ElasticsearchClientFactory.Create()` rather than via DI.
- **Entities/** — domain models (`Product`).
- **Models/** — `ResponseObj<T>` and `ErrorType` exist but are not yet wired into responses.

DI: `IProductsService` and `IProductsRepository` are registered `Scoped` in `Program.cs`.

## Conventions
- Project-namespace `using`s are centralized in `GlobalUsings.cs` — new files generally don't need
  explicit `using DemoCore2026.*` imports.
- Nullable reference types and implicit usings are enabled.
- API routes are under `api/products`.
