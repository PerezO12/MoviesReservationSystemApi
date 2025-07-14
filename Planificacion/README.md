# Planificación del Sistema de Reserva de Películas

## 1. Stack Tecnológico y Versiones

- **ASP.NET Core** (>=8.0): Framework principal para la API REST.
- **Entity Framework Core** (>=8.0): ORM para acceso a datos.
- **Npgsql** (>=8.0): Provider EF Core para PostgreSQL.
- **PostgreSQL** (>=15): Motor de base de datos relacional.
- **SignalR** (>=8.0): Comunicación en tiempo real (bloqueo/actualización de asientos).
- **MediatR** (>=12): Mediador para desacoplar lógica de negocio y controladores.
- **AutoMapper** (>=12): Mapeo entre entidades y DTOs.
- **FluentValidation** (>=11): Validación de DTOs.
- **ASP.NET Core Identity** (>=8.0): Gestión de usuarios y roles.
- **Microsoft.AspNetCore.Authentication.JwtBearer**: Autenticación JWT.
- **Swashbuckle.AspNetCore**: Documentación Swagger/OpenAPI.
- **xUnit + Moq**: Pruebas unitarias y mocks.
- **Docker**: Contenerización y despliegue.
- **Serilog**: Logging estructurado.

## 2. Arquitectura Propuesta (MVC + Capas)

- **Patrón MVC clásico**:
  - **Controllers**: Reciben las peticiones HTTP, validan entrada y devuelven respuestas (API REST).
  - **Services**: Contienen la lógica de negocio principal (ej: validaciones, reglas de reserva, cálculos).
  - **Repositories**: Acceso a datos y persistencia (consultas, inserciones, actualizaciones en la base de datos usando EF Core).
  - **DTOs**: Objetos de transferencia de datos para entrada/salida.
  - **Middlewares**: Manejo global de errores, autenticación, logging, etc.
  - **AutoMapper**: Mapeo entre entidades y DTOs.
  - **FluentValidation**: Validación de DTOs antes de llegar a la lógica de negocio.
  - **SignalR Hubs**: Para comunicación en tiempo real (bloqueo/liberación de asientos).

### Ventajas de esta arquitectura
- **Simplicidad**: Más fácil de entender y seguir para quienes inician en ASP.NET Core.
- **Separación de responsabilidades**: Cada capa tiene un propósito claro.
- **Escalabilidad**: Permite crecer hacia arquitecturas más complejas en el futuro.
- **Testabilidad**: Los servicios y repositorios pueden ser testeados de forma aislada.

---

## 3. Entidades Principales y Relaciones

- **User** (IdentityUser extendido)
  - Roles: Admin, User
- **Movie**
  - Título, descripción, póster, género
- **Showtime**
  - Fecha/hora, sala, película (FK)
- **Seat**
  - Número, fila, sala (FK)
- **Reservation**
  - Usuario (FK), showtime (FK), asientos (N:M), estado, timestamps

### Diagrama Entidad-Relación (ER)

Ver archivo `Planificacion/ER_Diagram.md`.

## 4. Casos de Uso Principales

- **Registro/Login de usuario**
- **Gestión de películas (Admin)**: CRUD
- **Gestión de horarios (Admin)**: CRUD
- **Explorar películas y horarios**
- **Reserva de asientos**
- **Ver/cancelar reservas**
- **Reportes (Admin)**: reservas, capacidad, ingresos
- **Bloqueo/actualización de asientos en tiempo real**

Ver archivo `Planificacion/CasosDeUso.md`.

## 5. Infraestructura de Datos y Persistencia

- **DbContext** con EF Core, entidades y relaciones configuradas.
- **Migraciones** para versionado de esquema.
- **Configuración de conexión** vía secrets/env vars.

## 6. Seguridad y Autenticación

- **Identity + JWT** para autenticación y autorización.
- **Roles**: seed inicial de Admin.
- **Endpoints protegidos** (401/403).

## 7. Servicios y Repositorios

- **Servicios**: Lógica de negocio (IUserService, IReservationService, etc.).
- **Repositorios**: Acceso a datos (IMovieRepository, IShowtimeRepository, etc.).
- **Patrón Result<T>** para flujos de éxito/fallo.

## 8. Validaciones y Manejo de Errores

- **FluentValidation** para DTOs.
- **Middleware global** para excepciones y mapeo a HTTP.

## 9. Mapeo y DTOs

- **AutoMapper** para entidades <-> DTOs.
- **DTOs** de entrada/salida por endpoint.

## 10. Pruebas Automatizadas

- **Unit tests** para lógica de dominio.
- **Integration tests** para API.
- **Cobertura mínima**: 80% en lógica crítica.

## 11. Documentación y Swagger

- **OpenAPI** con Swashbuckle.
- **Documentar** esquemas y códigos de estado.

## 12. Funcionalidades en Tiempo Real

- **SignalR Hub** para asientos.
- **Cliente de ejemplo** para suscripción a eventos.

## 13. Reportes y Optimización

- **REST/GraphQL** para estadísticas.
- **Read models**/vistas materializadas para queries complejas.

## 14. Despliegue y Monitoreo

- **Dockerfile** y despliegue en cloud.
- **Health checks** y métricas.
- **Logging estructurado** (Serilog).

## 15. Revisión y Hand-over

- **Code review**.
- **Pruebas de aceptación**.
- **Documentación de arquitectura y contribución.**

---

### Próximos pasos
- [ ] Refinar requisitos funcionales y no funcionales
- [ ] Diagramar entidades y casos de uso
- [ ] Diseñar DbContext y entidades
- [ ] Configurar autenticación y roles
- [ ] Definir estructura de carpetas y namespaces
- [ ] Planificar endpoints y servicios

---

> **Cualquier duda, sugerencia o cambio, consultar antes de avanzar.** 