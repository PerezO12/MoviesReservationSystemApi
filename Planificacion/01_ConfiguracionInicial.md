# Configuración Inicial del Proyecto

## Paso 1: Configuración de la base de datos PostgreSQL
- **Host:** 127.0.0.1
- **Puerto:** 5432
- **Usuario:** postgres
- **Password:** 123455
- **Base de datos:** (la crearemos, nombre sugerido: `movie_reservation`)

### Acciones:
1. Crear la base de datos en PostgreSQL (puedes usar pgAdmin, DBeaver, o línea de comandos):
   ```sql
   CREATE DATABASE movie_reservation;
   ```
2. Verificar que puedes conectarte con estos datos.

---

## Paso 2: Configuración de la cadena de conexión en ASP.NET Core
- Editar `appsettings.json` para agregar la cadena de conexión:
  ```json
  "ConnectionStrings": {
    "DefaultConnection": "Host=127.0.0.1;Port=5432;Database=movie_reservation;Username=postgres;Password=123455"
  }
  ```
- También puedes usar variables de entorno para mayor seguridad en producción.

---

## Paso 3: Configuración de Entity Framework Core
- Crear la clase `ApplicationDbContext` en la carpeta `Models/` o `Data/`.
- Registrar el DbContext en `Program.cs` usando la cadena de conexión.
- Probar la conexión ejecutando una migración vacía.

---

## Paso 4: Configuración de Identity y JWT
- Integrar ASP.NET Core Identity para gestión de usuarios y roles.
- Configurar autenticación JWT Bearer.

---

## Paso 5: Configuración de Swagger y Middlewares básicos
- Agregar y configurar Swagger para documentación de la API.
- Configurar middlewares globales de manejo de errores y logging.

---

## Notas y Preguntas
- ¿Quieres que la base de datos se cree automáticamente desde la migración inicial, o prefieres crearla manualmente?
- ¿Prefieres que la cadena de conexión esté solo en `appsettings.json` o también en variables de entorno?
- ¿Quieres que configuremos el seed de un usuario Admin desde el principio?

---

> A medida que avancemos, iré documentando cada paso aquí y te iré preguntando cuando sea necesario tomar decisiones. 