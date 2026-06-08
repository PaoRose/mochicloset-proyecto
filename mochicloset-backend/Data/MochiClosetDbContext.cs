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
}