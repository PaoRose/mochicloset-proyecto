using Microsoft.EntityFrameworkCore;
using mochi_closet.Data;
using mochi_closet.Negocio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<GestionCategorias>();
builder.Services.AddScoped<GestionUsuarios>();
builder.Services.AddScoped<GestionCompras>();
builder.Services.AddScoped<GestionPublicaciones>();


builder.Services.AddDbContext<MochiClosetDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();