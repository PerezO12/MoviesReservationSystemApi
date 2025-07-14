using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Movie_Reservation_System.Data;
using Movie_Reservation_System.Services;
using Movie_Reservation_System.DTOs.Auth;
using Movie_Reservation_System.DTOs.Movie;
using Movie_Reservation_System.DTOs.Room;
using Movie_Reservation_System.DTOs.Seat;
using Movie_Reservation_System.DTOs.Showtime;
using Movie_Reservation_System.DTOs.Reservation;
using Movie_Reservation_System.Repositories;
using Movie_Reservation_System.Validators.Auth;
using Movie_Reservation_System.Validators.Movie;
using Movie_Reservation_System.Validators.Room;
using Movie_Reservation_System.Validators.Seat;
using Movie_Reservation_System.Validators.Showtime;
using Movie_Reservation_System.Validators.Reservation;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configurar DbContext con PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configurar Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IMovieRepository, MovieRepository>();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<ISeatRepository, SeatRepository>();
builder.Services.AddScoped<ISeatService, SeatService>();
builder.Services.AddScoped<IShowtimeRepository, ShowtimeRepository>();
builder.Services.AddScoped<IShowtimeService, ShowtimeService>();
builder.Services.AddScoped<IValidator<RoomCreateDto>, RoomCreateDtoValidator>();
builder.Services.AddScoped<IValidator<RoomUpdateDto>, RoomUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<SeatCreateDto>, SeatCreateDtoValidator>();
builder.Services.AddScoped<IValidator<SeatUpdateDto>, SeatUpdateDtoValidator>();
builder.Services.AddScoped<IValidator<ShowtimeCreateDto>, ShowtimeCreateDtoValidator>();
builder.Services.AddScoped<IValidator<ShowtimeUpdateDto>, ShowtimeUpdateDtoValidator>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IValidator<ReservationCreateDto>, ReservationCreateDtoValidator>();
builder.Services.AddScoped<IValidator<ReservationUpdateDto>, ReservationUpdateDtoValidator>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IValidator<RegisterDto>, RegisterDtoValidator>();
builder.Services.AddScoped<IValidator<LoginDto>, LoginDtoValidator>();
builder.Services.AddScoped<IValidator<ForgotPasswordDto>, ForgotPasswordDtoValidator>();
builder.Services.AddScoped<IValidator<ResetPasswordDto>, ResetPasswordDtoValidator>();
builder.Services.AddScoped<IValidator<UpdateUserDto>, UpdateUserDtoValidator>();

var app = builder.Build();

// Sembrar roles y usuario Admin al iniciar
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await DbInitializer.SeedRolesAndAdminAsync(services);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.MapControllers();

app.Run();
