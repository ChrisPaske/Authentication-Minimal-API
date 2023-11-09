# Authentication-Minimal-API
JWT Authentication using .NET Core 6 Minimal API with PostgreSQL.  

Demonstrates a vertical slice architecture approach for web api projects that groups implementation by apis using modules instead of the traditional approach of slicing the project by technical concerns.  

### Structure Overview
**Core** - Classes providing value to multiple modules.  These can include base classes, utilities, and configuration.

**Modules** - Module folders containing all implementation required to service each module, where a module is a collection of services provided through an api.  A module can contain an additional directory structure (Dtos, Models, Repositories, Services) if needed.

### Resources
https://timdeschryver.dev/blog/maybe-its-time-to-rethink-our-project-structure-with-dot-net-6#a-domain-driven-api
