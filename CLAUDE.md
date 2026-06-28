# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Workflow
Always start every task by entering plan mode behavior: analyze, design steps, and wait for approval
before any file changes.

## Overview
DemoCore2026 is an ASP.NET Core (`net10.0`) Web API for indexing and searching `Product` and
`Category` records in Elasticsearch. Key packages: `Elastic.Clients.Elasticsearch` (9.x),
`Microsoft.AspNetCore.OpenApi`, and `Swashbuckle.AspNetCore` (Swagger).

## Commands
- Build: `dotnet build`
- Run (HTTP profile, no browser): `dotnet run --launch-profile http` → http://localhost:5256
- Run (HTTPS profile, opens Swagger): `dotnet run --launch-profile https` → https://localhost:7178
- Swagger UI (Development only): `/swagger`
- No test project exists yet.

## Prerequisites
The API requires a running Elasticsearch instance at `http://localhost:9200` (hardcoded in
`Elasticsearch/ElasticsearchClientFactory.cs`). Two indices are used: `products` and `categories`.
Their mappings and seed data are in `Installation/elasticsearch.txt`. Without Elasticsearch, all
endpoints fail at the repository layer.

## Architecture
Each resource is a self-contained vertical slice following a strict 3-layer pipeline. Two slices
exist today — Products and Categories — built from the same pattern:

`{Resource}Controller` → `I{Resource}Service` → `I{Resource}Repository` → Elasticsearch

- **Controllers/** — thin HTTP layer; wraps the service's `ResponseObj` in the appropriate
  `IActionResult` (`Ok`/`Conflict`/`NotFound`/`BadRequest`). Endpoints: `add`, `all`, `{id}`
  (Products also has `search`).
- **Services/** — business rules and input validation (e.g. name required, id range checks,
  duplicate/`NotFound` handling). Put validation/logic here, not in controllers or repositories.
  Validation can differ per resource — e.g. `ProductsService` rejects `id <= 0`, but
  `CategoriesService` rejects only `id < 0` because category id `0` ("Default") is valid.
- **Repositories/** — all Elasticsearch access. Each repository constructs its client directly via
  the static `ElasticsearchClientFactory.Create()` (not via DI) and hardcodes its own index name
  (`products` / `categories`).
- **Entities/** — domain models (`Product`, `Category`).
- **Models/** — `ResponseObj<T>` and `ErrorType` are the standard response wrapper used across all
  endpoints.

DI: each slice's `I{Resource}Service` and `I{Resource}Repository` are registered `Scoped` in
`Program.cs`. `ErrorType` is serialized as a string via a `JsonStringEnumConverter` configured in
`Program.cs`.

## Conventions
- Project-namespace `using`s are centralized in `GlobalUsings.cs` — new files generally don't need
  explicit `using DemoCore2026.*` imports.
- Nullable reference types and implicit usings are enabled.
- API routes are under `api/{resource}` (e.g. `api/products`, `api/categories`).
- New resources should mirror the existing slice: add `Entities/`, `Interfaces/I{Resource}Service`
  + `I{Resource}Repository`, `Services/`, `Repositories/`, a `Controllers/{Resource}Controller`,
  and register both services `Scoped` in `Program.cs`.
- Controller actions must always return a `ResponseObj<T>` (`Models/ResponseObj.cs`) as the standard
  API response shape — never return raw data or inconsistent response types.
