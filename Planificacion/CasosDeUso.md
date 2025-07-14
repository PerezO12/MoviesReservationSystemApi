# Casos de Uso Principales

## 1. Registro de Usuario
- El usuario proporciona email, nombre y contraseña.
- El sistema valida y crea el usuario con rol "User" por defecto.

## 2. Login de Usuario
- El usuario envía email y contraseña.
- El sistema valida credenciales y retorna JWT.

## 3. Gestión de Películas (Admin)
- CRUD de películas (título, descripción, póster, género).
- Solo accesible para usuarios con rol Admin.

## 4. Gestión de Horarios (Showtimes) (Admin)
- CRUD de horarios (fecha/hora, sala, película).
- Solo accesible para Admin.

## 5. Explorar Películas y Horarios
- Cualquier usuario puede consultar películas y horarios disponibles por fecha.

## 6. Reserva de Asientos
- El usuario selecciona película, horario y asientos disponibles.
- El sistema bloquea temporalmente los asientos (SignalR).
- El usuario confirma la reserva.
- El sistema crea la reserva y asocia los asientos.

## 7. Ver/Cancelar Reservas
- El usuario puede ver sus reservas activas y cancelarlas si son futuras.

## 8. Reportes (Admin)
- El admin puede consultar:
  - Reservas por película/fecha.
  - Capacidad ocupada por sala.
  - Ingresos generados.

## 9. Bloqueo/Actualización de Asientos en Tiempo Real
- Cuando un usuario selecciona un asiento, se notifica a otros clientes en tiempo real (SignalR).
- Al liberar/cancelar, se actualiza el estado en todos los clientes.

---

> Para cada caso de uso, se definirán DTOs, validaciones y respuestas estándar. 