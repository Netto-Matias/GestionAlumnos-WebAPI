using Alumnos.API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Registramos el contexto para la inyeccion de Dependencias
builder.Services.AddDbContext<GestionAlumnosContext>(opciones => 
    opciones.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=GestionAlumnosDB;Trusted_connection=True;TrustServerCertificate=True;"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
