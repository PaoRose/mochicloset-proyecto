using Microsoft.EntityFrameworkCore;
using mochi_closet.Datos;

namespace mochi_closet.Data;

public class MochiClosetDbContext : DbContext
{
    public MochiClosetDbContext(
        DbContextOptions<MochiClosetDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Publicacion> Publicaciones { get; set; }
    public DbSet<Compra> Compras { get; set; }
    public DbSet<Favorito> Favoritos { get; set; }
    public DbSet<Conversacion> Conversaciones { get; set; }
    public DbSet<Mensaje> Mensajes { get; set; }
    public DbSet<Notificacion> Notificaciones { get; set; }
    public DbSet<Reporte> Reportes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Publicacion>()
            .Property(p => p.Precio)
            .HasPrecision(10, 2);

        modelBuilder.Entity<Compra>()
            .Property(c => c.MontoTotal)
            .HasPrecision(10, 2);
    }
}