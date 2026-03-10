# Equipment Checkout (A3 Starter) - .NET 8 MVC

This starter is intentionally **plumbing only**:
- MVC is wired up.
- SQLite DbContext is registered.
- `AppData/` is included and copied to the output directory on build.

You are responsible for implementing Phase 1 features:
- controllers/actions/views for the required screens
- UI read models + UI read query interfaces
- Domain entities, DTOs, services, DAOs (interfaces)
- Persistence implementations (EF + DAO + read-model gateways)
- domain rules and tests

## AppData
- `AppData/equipment-checkout.db` : starter SQLite database
- `AppData/seed.sql` : schema + seed data used to build the database

The connection string is built at runtime from:
`<ContentRoot>/AppData/equipment-checkout.db`

## View locations
Views are configured to live under:
- `/Ui/Views/{Controller}/{View}.cshtml`
- `/Ui/Views/Shared/{View}.cshtml`

(So you do not need a top-level `/Views` folder.)

## Next steps (students)
- Add your entities under `Domain/Entities`
- Add your DbSet<T> properties in `Persistence/Ef/AppDbContext.cs`
- Create DAO interfaces in `Domain/Daos` and implement them in `Persistence/Daos`
- Create read query interfaces in `Ui/Queries` and implement them in `Persistence/Queries`
- Keep controllers thin: call Domain services for POST/commands; use query gateways for GET screens.
