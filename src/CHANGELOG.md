# v0.2.2
## Changed
* Awaited all database operations for `EfRepository<>`.

# v0.2.1
## Changed
* Switched to synchronous entity add in `EfRepository<>` because asynchronous version threw concurrency exception in some cases.

# v0.2.0
## Added
* Mongo infrastructure implementation.
* Service registration extensions.
## Changed
* Splitted implementation files according to the framework they relies on.
* `EFRepository` isn't more abstract.
* `IRepository<>`'s public methods not do depend on `IHaveId` interface and work with any type of entity.
* Updated Npgsql package to `3.1.0-preview2` version.
## Removed
* Search in `IRepository<>` by expression method.

# v0.1.2
## Added
* NuGet configuration with Nexus link.
* External Validation package dependency.

# v0.1.1
## Added
* Integration tests.
## Removed
* Unit Of Work implementation.

# v0.1.0
## Added
* Basic implementation of data access layer.

# vTemplate
## Added
## Changed
## Removed
