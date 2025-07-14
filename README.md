# Movie Reservation System

## Descripción
Backend robusto para un sistema de reserva de películas, implementado en ASP.NET Core, siguiendo patrones profesionales (Repository, Service, CQRS, FluentValidation, Identity, JWT, Result<T>, etc.). Permite registro/login de usuarios, gestión de películas, salas, asientos, horarios, reservas, roles y reportes.

---

## Tecnologías y Stack
- **ASP.NET Core** 8+
- **Entity Framework Core** + Npgsql (PostgreSQL)
- **ASP.NET Core Identity** (gestión de usuarios y roles)
- **JWT Bearer** (autenticación)
- **FluentValidation** (validaciones de DTOs)
- **AutoMapper** (mapeo entidades/DTOs)
- **MediatR** (CQRS, si se desea)
- **Swagger** (documentación de API)
- **Serilog** (logging estructurado)
- **Docker** (despliegue)

---

## Arquitectura y Estructura
- **Repository + Service Pattern**: Separación de acceso a datos y lógica de negocio.
- **DTOs y Validadores**: Todos los endpoints validan entrada con FluentValidation.
- **Patrón Result<T>**: Todas las respuestas siguen un patrón uniforme de éxito/error/código HTTP.
- **Identity + JWT**: Seguridad robusta, roles, claims y autenticación basada en tokens.
- **Carpetas**:
  - `Controllers/` - Endpoints RESTful
  - `Services/` - Lógica de negocio
  - `Repositories/` - Acceso a datos
  - `DTOs/` - Objetos de transferencia de datos
  - `Validators/` - Validaciones de DTOs
  - `Query/` - DTOs de consulta, paginación, filtros
  - `Helpers/` - Result<T>, helpers
  - `Models/` - Entidades de dominio
  - `Middlewares/`, `Extensions/` - Utilidades

---

## Endpoints principales
### Autenticación y usuarios
- `POST /api/auth/register` - Registro de usuario (solo admin puede crear admin)
- `POST /api/auth/login` - Login y obtención de JWT
- `GET /api/auth/me` - Info del usuario autenticado
- `PUT /api/auth/update` - Actualizar email, nombre, contraseña
- `POST /api/auth/forgot-password` - Solicitar reseteo de contraseña
- `POST /api/auth/reset-password` - Resetear contraseña con token
- `POST /api/auth/change-role` - Cambiar rol de usuario (solo admin)
- `GET /api/auth/all` - Listar todos los usuarios y roles (solo admin)

### Películas, salas, asientos, horarios, reservas
- `GET/POST/PUT/DELETE /api/movies` - CRUD de películas
- `GET/POST/PUT/DELETE /api/rooms` - CRUD de salas
- `GET/POST/PUT/DELETE /api/seats` - CRUD de asientos
- `GET/POST/PUT/DELETE /api/showtimes` - CRUD de horarios
- `GET/POST/PUT/DELETE /api/reservations` - CRUD de reservas (con validación de asientos disponibles)
- `POST /api/movies/upload-poster` - Subida de póster de película

---

## Seguridad y roles
- **Roles**: `Admin`, `User` (solo admin puede crear/cambiar admin)
- **JWT**: Todos los endpoints protegidos requieren token Bearer
- **Validaciones**: Todos los DTOs validados con FluentValidation
- **Manejo de errores**: Result<T> con código HTTP y mensajes claros

---

## Cómo correr el proyecto
1. Clona el repo y navega a la carpeta del proyecto
2. Configura la cadena de conexión a PostgreSQL en `appsettings.json`
3. Ejecuta las migraciones:
   ```bash
   dotnet ef database update
   ```
4. Corre el proyecto:
   ```bash
   dotnet run
   ```
5. Accede a Swagger en `/swagger` para probar la API

---

## Recomendaciones para producción
- Usa un proveedor de email real para recuperación de contraseña
- Configura HTTPS y variables de entorno seguras
- Usa almacenamiento externo para archivos (pósters)
- Configura logging y monitoreo
- Limita los endpoints de administración solo a usuarios con rol Admin

---
