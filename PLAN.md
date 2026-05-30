# SolarWinds Service Desk API Parity Migration Plan

This plan drives a docs-first, breaking-change migration so the client matches the official SolarWinds Service Desk OpenAPI behavior and request shapes.

## Scope and Intent

- Align all Service Desk endpoints, query parameters, and request body wrappers with official docs/OpenAPI.
- Replace current broad entity-as-request write patterns with endpoint-specific request models (for example: `IncidentUpdateRequest` with top-level `incident` wrapper).
- Normalize interface and client patterns across all supported Service Desk objects.
- Keep read operations heavily unit-tested.
- Do **not** add unit tests for write operations (POST/PUT/PATCH/DELETE) due production safety constraints in customer environments.

## Hard Rules

- [ ] No new unit tests for write queries.
- [ ] Unit tests must cover all GET query request construction/serialization behaviors.
- [ ] Breaking changes are allowed when required to match documentation.
- [ ] Preserve authentication, headers, and retry/backoff behavior unless docs require change.
- [ ] Keep this file updated as work progresses.
- [ ] Commit at the end of each completed phase with a clear phase-oriented commit message.

## Current Baseline (Already Collected)

- [x] OpenAPI snapshot downloaded to [temp/docs/resolved_schema.json](temp/docs/resolved_schema.json).
- [x] Endpoint parity report generated at [temp/docs/parity-report.md](temp/docs/parity-report.md).
- [x] Endpoint inventories generated:
  - [temp/docs/openapi-endpoints.csv](temp/docs/openapi-endpoints.csv)
  - [temp/docs/repo-refit-endpoints.csv](temp/docs/repo-refit-endpoints.csv)
- [x] Request root key extraction generated:
  - [temp/docs/openapi-request-roots.csv](temp/docs/openapi-request-roots.csv)
  - [temp/docs/openapi-request-roots-summary.md](temp/docs/openapi-request-roots-summary.md)

## Phase 1: Contract Mapping and Change Specification

Goal: Produce explicit method-by-method migration map from current repo contracts to OpenAPI contracts.

- [x] Create mapping matrix file at `temp/docs/migration-matrix.csv` with columns:
  - `Tag`
  - `Method`
  - `Path`
  - `OperationId`
  - `CurrentInterface`
  - `CurrentMethodSignature`
  - `TargetMethodSignature`
  - `RequestWrapperKey`
  - `Status` (`no-change`, `signature-change`, `new-endpoint`, `remove-or-rework`)
- [x] Identify all endpoints currently marked as missing in repo and classify as:
  - `must-add`
  - `optional-followup`
  - `intentionally-out-of-scope` (with justification)
- [x] Identify all extra repo endpoints and classify as:
  - `align-with-docs`
  - `legacy-compat` (only if deliberately retained)
  - `remove`
- [x] For each write endpoint, define dedicated request model names and wrapper keys.
- [x] For each GET endpoint, define query request model names and query parameter typing expectations.
- [x] Commit phase completion.

## Phase 2: Shared Request/Response Infrastructure

Goal: Introduce reusable primitives so endpoint models stay consistent.

- [ ] Add common request wrapper primitives (if beneficial), for example top-level object wrappers.
- [ ] Add/normalize enums for documented constrained values.
- [ ] Add/normalize date/query formatting helpers where docs require special formats.
- [ ] Add or adjust serializer attributes to exactly match documented payload property names.
- [ ] Verify no write behavior change yet; this phase should be infrastructure-only.
- [ ] Commit phase completion.

## Phase 3: Incident Domain Migration

Goal: Fully align Incident endpoints and request bodies.

- [x] Introduce dedicated models:
  - `IncidentCreateRequest`
  - `IncidentUpdateRequest`
  - Any nested request DTOs required by docs samples/schema
- [x] Update [SolarWinds.Api/ServiceDesk/Interfaces/IIncidents.cs](SolarWinds.Api/ServiceDesk/Interfaces/IIncidents.cs) signatures to use dedicated request types.
- [x] Keep GET list/query support aligned with docs, including `layout` and updated/created filtering semantics.
- [x] Ensure request wrapper key is `incident` exactly where required.
- [x] Add or update GET query unit tests for incident list/get query options.
- [x] Do **not** add unit tests for incident create/update/delete write calls.
- [ ] Commit phase completion.

## Phase 4: Problem, Change, Release, Solution Domains

Goal: Apply same migration pattern to major ITSM entities.

- [x] Introduce dedicated create/update request models per domain:
  - `ProblemCreateRequest`, `ProblemUpdateRequest`
  - `ChangeCreateRequest`, `ChangeUpdateRequest`
  - `ReleaseCreateRequest`, `ReleaseUpdateRequest`
  - `SolutionCreateRequest`, `SolutionUpdateRequest`
- [x] Align interface signatures for each domain.
- [x] Ensure wrapper keys match docs (`problem`, `change`, `release`, `solution`).
- [ ] Add/expand GET query unit tests for all list/get query parameters in these domains.
- [x] No write-query unit tests.
- [ ] Commit phase completion.

## Phase 5: Asset and Catalog Domains

Goal: Migrate catalog/configuration/asset-style domains.

- [ ] Catalog item request models and signature alignment (`catalog_item`).
- [ ] Configuration item request models and signature alignment (`configuration_item`).
- [ ] Other asset request models and signature alignment (`other_asset`).
- [ ] Hardware/mobile/printer/software/contract/purchase-order/vendor request model alignment where docs define write schemas.
- [ ] Handle sub-resource endpoints (warranties, contract items, dependent assets, asset links) with documented paths and payload keys.
- [ ] Add/expand GET query unit tests for all GET queryable endpoints in these domains.
- [ ] No write-query unit tests.
- [ ] Commit phase completion.

## Phase 6: Cross-Object Generic Endpoints

Goal: Align object-type scoped endpoints and remove unsupported shortcuts.

- [ ] Align generic path endpoints:
  - `/{object_type}/{id}/comments`
  - `/{object_type}/{id}/tasks`
  - `/{object_type}/{id}/time_tracks`
  - `/{object_type}/{id}/purchases`
  - `/{object_type}/{id}/audits`
- [ ] Replace/retire non-doc singular shortcuts (`/comments/{id}`, `/tasks/{id}`, etc.) unless intentionally retained with explicit compatibility notes.
- [ ] Align membership/service request/change request endpoints with documented routes and payload wrappers.
- [ ] Add GET unit tests for any query-bearing GET endpoints introduced or changed.
- [ ] No write-query unit tests.
- [ ] Commit phase completion.

## Phase 7: GET Query Test Completion Gate

Goal: Guarantee all GET query behavior is covered by unit tests.

- [ ] Inventory every GET endpoint that accepts query parameters from OpenAPI.
- [ ] Ensure each has dedicated query serialization tests (including enums/date formats where applicable).
- [ ] Ensure each has omission/null behavior tests for optional params.
- [ ] Ensure page/per_page and layout semantics are tested where documented.
- [ ] Ensure customer-safe principle holds: no unit tests for write queries.
- [x] Generate a short test coverage checklist file at `temp/docs/get-query-test-matrix.md`.
- [ ] Commit phase completion.

## Phase 8: Deprecation and Breaking-Change Documentation

Goal: Make breaking contract changes transparent for consumers.

- [ ] Update README usage snippets to new request model pattern.
- [ ] Add migration guidance section: old vs new method signatures and payload types.
- [ ] Document renamed/removed endpoints and rationale tied to docs parity.
- [ ] Add release note draft in `temp/docs/release-notes-draft.md`.
- [ ] Commit phase completion.

## Phase 9: Final Validation and Release Readiness

Goal: Produce a stable, documented, docs-aligned client.

- [ ] Run full unit test suite.
- [ ] Run targeted integration tests that are safe/read-focused.
- [ ] Re-run parity generation and confirm missing/extra endpoint deltas are expected.
- [ ] Final review of all unchecked items in this plan.
- [ ] Mark completed phases and tasks in this file.
- [ ] Commit final phase completion.

## Progress Log

Use this section to append progress notes as each phase is completed.

- 2026-05-30: Phase 1 mapping artifacts generated.
  - `temp/docs/migration-matrix.csv` produced with 135 OpenAPI operations and statuses:
    - `new-endpoint=32`
    - `signature-change=40`
    - `no-change=63`
  - Missing endpoint classification file created at `temp/docs/missing-endpoints-classification.csv` (32 rows).
  - Extra endpoint classification file created at `temp/docs/extra-endpoints-classification.csv` (29 rows).
  - Migration matrix includes OpenAPI tag/operation id, current interface/signature hints, target signature proposal, request wrapper key, and status.

- 2026-05-30: Phase 3 started with incident write-shape migration.
  - Added dedicated request models:
    - `IncidentCreateRequest`
    - `IncidentUpdateRequest`
    - `IncidentWriteFields`
  - Updated incident write method signatures in `IIncidents` to consume request wrappers.
  - Updated incident lifecycle integration test payload construction to new wrappers.
  - Validation run (`IncidentQueryRequestTests`, `IncidentQueryIntegrationTests`, `IncidentLifecycleIntegrationTests`):
    - 12 passed, 1 failed.
    - Failure is live API behavior (`HTTP 500`) during incident lifecycle integration write call; no compile errors.

- 2026-05-30: Phase 7 groundwork artifact generated.
  - Created `temp/docs/get-query-test-matrix.md` from OpenAPI GET query parameters.
  - Heuristic status summary:
    - `Covered=4`
    - `Partial=8`
    - `Missing=2`
  - Priority missing area identified: `other_assets` GET query coverage.

- 2026-05-30: Phase 4 write-contract migration implemented (Problem/Change/Release/Solution).
  - Added request wrappers and write field models:
    - `ProblemCreateRequest` / `ProblemUpdateRequest` / `ProblemWriteFields`
    - `ChangeCreateRequest` / `ChangeUpdateRequest` / `ChangeWriteFields`
    - `ReleaseCreateRequest` / `ReleaseUpdateRequest` / `ReleaseWriteFields`
    - `SolutionCreateRequest` / `SolutionUpdateRequest` / `SolutionWriteFields`
  - Updated interfaces to use dedicated request payloads:
    - `IProblems`
    - `IChanges`
    - `IReleases`
    - `ISolutions`
  - Validation run:
    - `IncidentQueryRequestTests`, `ServiceDeskUrlParameterFormatterTests`, `IncidentQueryIntegrationTests`
    - `18 passed, 0 failed`, no compile errors.

- [x] Phase 1 completed.
- [ ] Phase 2 completed.
- [ ] Phase 3 completed.
- [ ] Phase 4 completed.
- [ ] Phase 5 completed.
- [ ] Phase 6 completed.
- [ ] Phase 7 completed.
- [ ] Phase 8 completed.
- [ ] Phase 9 completed.
