using Microsoft.EntityFrameworkCore;
using mochi_closet.Data;
using mochi_closet.Negocio;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<GestionCategorias>();
builder.Services.AddScoped<GestionUsuarios>();
builder.Services.AddScoped<GestionCompras>();
builder.Services.AddScoped<GestionPublicaciones>();
builder.Services.AddScoped<GestionFavoritos>();
builder.Services.AddScoped<GestionMensajes>();
builder.Services.AddScoped<GestionNotificaciones>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddDbContext<MochiClosetDbContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("PermitirAngular");
app.UseAuthorization();
app.MapControllers();
app.Run();