# Diagrama Entidad-Relación (ER)

```mermaid
erDiagram
    User {
        string Id PK
        string UserName
        string Email
        string PasswordHash
        string Role
    }
    Movie {
        int Id PK
        string Title
        string Description
        string PosterUrl
        string Genre
    }
    Showtime {
        int Id PK
        datetime StartTime
        int MovieId FK
        int RoomId FK
    }
    Room {
        int Id PK
        string Name
    }
    Seat {
        int Id PK
        string Row
        int Number
        int RoomId FK
    }
    Reservation {
        int Id PK
        string UserId FK
        int ShowtimeId FK
        datetime CreatedAt
        string Status
    }
    ReservationSeat {
        int ReservationId FK
        int SeatId FK
    }

    User ||--o{ Reservation : has
    Movie ||--o{ Showtime : has
    Room ||--o{ Showtime : hosts
    Room ||--o{ Seat : contains
    Showtime ||--o{ Reservation : includes
    Reservation ||--o{ ReservationSeat : books
    Seat ||--o{ ReservationSeat : reserved_in
```

---

- **Notas:**
  - `ReservationSeat` es una tabla de unión para la relación N:M entre Reservation y Seat.
  - `Room` permite modelar múltiples salas y asientos por sala.
  - `Role` en User puede ser Admin o User. 