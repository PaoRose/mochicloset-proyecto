using Microsoft.EntityFrameworkCore;
using mochi_closet.Data;
using mochi_closet.Datos;

namespace mochi_closet.Seed;
public static class DbSeeder
{
    public static void Seed(MochiClosetDbContext context)
    {
        AplicarCorreccionesFK(context);

        if (context.Usuarios.Any())
            return;

        var usuarios = new List<Usuario>
        {
            new Usuaria { Nombre = "Maria Lopez", Username = "maria_closet", Email = "maria@gmail.com", Telefono = "76543210", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/1.jpg" },
            new Usuaria { Nombre = "Sofia Mendez", Username = "sofiamendez", Email = "sofia@gmail.com", Telefono = "75598765", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/2.jpg" },
            new Usuaria { Nombre = "Valentina Cruz", Username = "valecruz", Email = "valentina@gmail.com", Telefono = "71234567", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/3.jpg" },
            new Usuaria { Nombre = "Camila Rojas", Username = "camilarojas", Email = "camila@gmail.com", Telefono = "78887766", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/4.jpg" },
            new Usuaria { Nombre = "Gabriela Torres", Username = "gabitorres", Email = "gabriela@gmail.com", Telefono = "79991234", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/5.jpg" },
            new Usuaria { Nombre = "Daniela Vaca", Username = "danyvaca", Email = "daniela@gmail.com", Telefono = "72223344", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/6.jpg" },
            new Usuaria { Nombre = "Fernanda Suarez", Username = "fersuarez", Email = "fernanda@gmail.com", Telefono = "73334455", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/7.jpg" },
            new Usuaria { Nombre = "Valeria Ardaya", Username = "vale_closet", Email = "valeria@gmail.com", Telefono = "79846513", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/8.jpg" },
            new Usuaria { Nombre = "Dafne Rojas", Username = "dafne_closet", Email = "dafne@gmail.com", Telefono = "79846514", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/9.jpg" },
            new Usuaria { Nombre = "Karen Reyes", Username = "karen_reyes", Email = "karen@gmail.com", Telefono = "67894512", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/10.jpg" },
            new Usuaria { Nombre = "Laura Perez", Username = "laura_closet", Email = "laura@gmail.com", Telefono = "67845129", Password = "12345678", Rol = "Usuaria", Estado = "Suspendido", FotoPerfil = "https://randomuser.me/api/portraits/women/11.jpg" },
            new Usuaria { Nombre = "Ana Garcia", Username = "ana_closet", Email = "ana@gmail.com", Telefono = "76123456", Password = "12345678", Rol = "Usuaria", Estado = "Activo", FotoPerfil = "https://randomuser.me/api/portraits/women/12.jpg" },
            new Administradora { Nombre = "Admin Mochi", Username = "admin", Email = "admin@mochicloset.com", Telefono = "00000000", Password = "admin1234", Rol = "Admin", Estado = "Activo", FotoPerfil = null }
        };

        context.Usuarios.AddRange(usuarios);
        context.SaveChanges();

        var idPorUsername = context.Usuarios.ToDictionary(u => u.Username!, u => u.Id);

        var categorias = new List<Categoria>
        {
            new() { Nombre = "Vestidos", Descripcion = "Vestidos de todo tipo y ocasion" },
            new() { Nombre = "Tops y Blusas", Descripcion = "Tops, blusas y camisas" },
            new() { Nombre = "Pantalones", Descripcion = "Pantalones y jeans" },
            new() { Nombre = "Faldas", Descripcion = "Faldas de todo estilo" },
            new() { Nombre = "Abrigos", Descripcion = "Abrigos y chaquetas" },
            new() { Nombre = "Accesorios", Descripcion = "Bolsos, cinturones y mas" }
        };
        context.Categorias.AddRange(categorias);
        context.SaveChanges();

        var catId = categorias.ToDictionary(c => c.Nombre!, c => c.Id);

        var publicaciones = new List<Publicacion>
        {
            Pub("Vestido floral rosa", "Vestido floral en muy buen estado, usado pocas veces", 80.00m, "S", "Como nuevo", "Disponible", "2026-06-01 10:00:00", idPorUsername["maria_closet"], catId["Vestidos"], "https://images.unsplash.com/photo-1572804013309-59a88b7e92f1"),
            Pub("Vestido negro elegante", "Vestido negro para ocasiones especiales, talla M", 120.00m, "M", "Usado", "Disponible", "2026-06-01 10:10:00", idPorUsername["sofiamendez"], catId["Vestidos"], "https://images.unsplash.com/photo-1595777457583-95e059d581b8"),
            Pub("Vestido blanco verano", "Vestido blanco ideal para verano", 95.00m, "S", "Como nuevo", "Disponible", "2026-06-01 10:20:00", idPorUsername["valecruz"], catId["Vestidos"], "https://images.unsplash.com/photo-1566174053879-31528523f8ae"),
            Pub("Top blanco basico", "Top blanco clasico, talla S", 35.00m, "S", "Como nuevo", "Vendido", "2026-06-01 10:30:00", idPorUsername["camilarojas"], catId["Tops y Blusas"], "https://images.unsplash.com/photo-1523381210434-271e8be1f52b"),
            Pub("Blusa estampada colorida", "Blusa con estampado tropical, muy linda", 55.00m, "M", "Usado", "Disponible", "2026-06-01 10:40:00", idPorUsername["gabitorres"], catId["Tops y Blusas"], "https://images.unsplash.com/photo-1564257631407-4deb1f99d992"),
            Pub("Blusa rosa pastel", "Blusa rosa muy delicada y femenina", 45.00m, "S", "Como nuevo", "Disponible", "2026-06-01 10:50:00", idPorUsername["danyvaca"], catId["Tops y Blusas"], "https://images.unsplash.com/photo-1485462537746-965f33f7f6a7"),
            Pub("Jean azul clasico", "Jean azul en buen estado, talla 28", 90.00m, "28", "Usado", "Disponible", "2026-06-01 11:00:00", idPorUsername["fersuarez"], catId["Pantalones"], "https://images.unsplash.com/photo-1542272604-787c3835535d"),
            Pub("Pantalon negro formal", "Pantalon negro formal casi sin uso", 110.00m, "30", "Como nuevo", "Disponible", "2026-06-01 11:10:00", idPorUsername["vale_closet"], catId["Pantalones"], "https://images.unsplash.com/photo-1594938298603-c8148c4b4c3b"),
            Pub("Falda midi beige", "Falda midi color beige muy elegante", 70.00m, "S", "Como nuevo", "Disponible", "2026-06-01 11:20:00", idPorUsername["dafne_closet"], catId["Faldas"], "https://images.unsplash.com/photo-1592301933927-35b597393c0a"),
            Pub("Falda floral corta", "Falda corta con flores ideal para verano", 45.00m, "M", "Usado", "Disponible", "2026-06-01 11:30:00", idPorUsername["karen_reyes"], catId["Faldas"], "https://images.unsplash.com/photo-1583496661160-fb5218ees23b"),
            Pub("Abrigo gris largo", "Abrigo gris largo para el frio", 150.00m, "M", "Como nuevo", "Vendido", "2026-06-01 11:40:00", idPorUsername["maria_closet"], catId["Abrigos"], "https://images.unsplash.com/photo-1544022613-e87ca75a784a"),
            Pub("Chaqueta de cuero negra", "Chaqueta de cuero sintetico muy cool", 200.00m, "S", "Usado", "Disponible", "2026-06-01 11:50:00", idPorUsername["sofiamendez"], catId["Abrigos"], "https://images.unsplash.com/photo-1551028719-00167b16eac5"),
            Pub("Vestido boho largo", "Vestido estilo boho largo floreado", 85.00m, "M", "Como nuevo", "Disponible", "2026-06-02 09:00:00", idPorUsername["valecruz"], catId["Vestidos"], "https://images.unsplash.com/photo-1572804013309-59a88b7e92f1"),
            Pub("Top crop negro", "Top crop negro basico, talla XS", 30.00m, "XS", "Como nuevo", "Disponible", "2026-06-02 09:10:00", idPorUsername["camilarojas"], catId["Tops y Blusas"], "https://images.unsplash.com/photo-1503342217505-b0a15ec3261c"),
            Pub("Jean mom fit claro", "Jean mom fit celeste, talla 30", 75.00m, "30", "Buen estado", "Disponible", "2026-06-02 09:20:00", idPorUsername["gabitorres"], catId["Pantalones"], "https://images.unsplash.com/photo-1542272604-787c3835535d"),
            Pub("Falda midi negra", "Falda midi negra elegante, talla M", 60.00m, "M", "Nuevo", "Disponible", "2026-06-02 09:30:00", idPorUsername["danyvaca"], catId["Faldas"], "https://images.unsplash.com/photo-1583496661160-fb5218ees23b"),
            Pub("Abrigo camel largo", "Abrigo camel largo de invierno, talla L", 180.00m, "L", "Como nuevo", "Disponible", "2026-06-02 09:40:00", idPorUsername["fersuarez"], catId["Abrigos"], "https://images.unsplash.com/photo-1544022613-e87ca75a784a"),
            Pub("Vestido rojo elegante", "Vestido rojo largo para ocasiones especiales", 150.00m, "M", "Como nuevo", "Disponible", "2026-06-02 09:50:00", idPorUsername["vale_closet"], catId["Vestidos"], "https://images.unsplash.com/photo-1595777457583-95e059d581b8"),
            Pub("Blusa de seda beige", "Blusa de seda color beige, talla M, impecable", 55.00m, "M", "Nuevo", "Disponible", "2026-06-02 10:00:00", idPorUsername["dafne_closet"], catId["Tops y Blusas"], "https://images.unsplash.com/photo-1598554747436-c9293d6a588f"),
            Pub("Pantalon beige formal", "Pantalon beige de tela formal, talla S", 70.00m, "S", "Nuevo", "Disponible", "2026-06-02 10:10:00", idPorUsername["karen_reyes"], catId["Pantalones"], "https://images.unsplash.com/photo-1594938298603-c8148c4b4c3b"),
        };

        context.Publicaciones.AddRange(publicaciones);
        context.SaveChanges();

        var pubId = publicaciones.ToDictionary(p => p.Titulo!, p => p.Id);

        var compras = new List<Compra>
        {
            CompraNueva("2026-06-03 14:00:00", "Completada", idPorUsername["maria_closet"], pubId["Top blanco basico"], 35.00m),
            CompraNueva("2026-06-03 15:00:00", "Completada", idPorUsername["sofiamendez"], pubId["Abrigo gris largo"], 150.00m),
            CompraNueva("2026-06-04 10:00:00", "Completada", idPorUsername["valecruz"], pubId["Top blanco basico"], 35.00m),
        };
        context.Compras.AddRange(compras);
        context.SaveChanges();

        var conversaciones = new List<Conversacion>
        {
            ConvNueva(idPorUsername["maria_closet"], idPorUsername["camilarojas"], pubId["Top blanco basico"], "2026-06-03 13:00:00"),
            ConvNueva(idPorUsername["sofiamendez"], idPorUsername["maria_closet"], pubId["Abrigo gris largo"], "2026-06-03 14:30:00"),
            ConvNueva(idPorUsername["valecruz"], idPorUsername["sofiamendez"], pubId["Vestido negro elegante"], "2026-06-04 09:00:00"),
            ConvNueva(idPorUsername["camilarojas"], idPorUsername["valecruz"], pubId["Vestido blanco verano"], "2026-06-04 10:00:00"),
            ConvNueva(idPorUsername["gabitorres"], idPorUsername["danyvaca"], pubId["Blusa estampada colorida"], "2026-06-05 11:00:00"),
        };
        context.Conversaciones.AddRange(conversaciones);
        context.SaveChanges();

        var mensajes = new List<Mensaje>
        {
            MsgNuevo(conversaciones[0].Id, idPorUsername["maria_closet"], "Hola! Me interesa el top blanco, sigue disponible?", "2026-06-03 13:01:00", true),
            MsgNuevo(conversaciones[0].Id, idPorUsername["camilarojas"], "Hola! Si, sigue disponible. En que talla lo necesitas?", "2026-06-03 13:05:00", true),
            MsgNuevo(conversaciones[0].Id, idPorUsername["maria_closet"], "Perfecto, talla S. Donde podemos encontrarnos?", "2026-06-03 13:10:00", true),
            MsgNuevo(conversaciones[0].Id, idPorUsername["camilarojas"], "Podemos en el centro, plaza 24 de Septiembre", "2026-06-03 13:15:00", false),
            MsgNuevo(conversaciones[1].Id, idPorUsername["sofiamendez"], "Hola! Me interesa el abrigo gris", "2026-06-03 14:31:00", true),
            MsgNuevo(conversaciones[1].Id, idPorUsername["maria_closet"], "Hola! Esta en excelente estado, solo usado 2 veces", "2026-06-03 14:35:00", true),
            MsgNuevo(conversaciones[2].Id, idPorUsername["valecruz"], "Buenos dias! El vestido negro sigue disponible?", "2026-06-04 09:01:00", true),
            MsgNuevo(conversaciones[2].Id, idPorUsername["sofiamendez"], "Si! Te lo puedo mostrar hoy si quieres", "2026-06-04 09:10:00", false),
            MsgNuevo(conversaciones[3].Id, idPorUsername["camilarojas"], "Me encanta el vestido blanco, que talla es?", "2026-06-04 10:01:00", true),
            MsgNuevo(conversaciones[3].Id, idPorUsername["valecruz"], "Es talla S, esta casi nuevo!", "2026-06-04 10:05:00", false),
        };
        context.Mensajes.AddRange(mensajes);
        context.SaveChanges();

        var favoritos = new List<Favorito>
        {
            FavNuevo(idPorUsername["maria_closet"], pubId["Vestido negro elegante"], "2026-06-02 12:00:00"),
            FavNuevo(idPorUsername["maria_closet"], pubId["Blusa estampada colorida"], "2026-06-02 12:05:00"),
            FavNuevo(idPorUsername["sofiamendez"], pubId["Vestido floral rosa"], "2026-06-02 12:10:00"),
            FavNuevo(idPorUsername["sofiamendez"], pubId["Vestido boho largo"], "2026-06-02 12:15:00"),
            FavNuevo(idPorUsername["valecruz"], pubId["Jean azul clasico"], "2026-06-03 09:00:00"),
            FavNuevo(idPorUsername["camilarojas"], pubId["Vestido rojo elegante"], "2026-06-03 09:05:00"),
        };
        context.Favoritos.AddRange(favoritos);
        context.SaveChanges();

        var notificaciones = new List<Notificacion>
        {
            new NotificacionMensaje { UsuarioId = idPorUsername["maria_closet"], Tipo = "Mensaje", Mensaje = "sofiamendez te envio un mensaje", Leida = false, FechaCreacion = DateTime.Parse("2026-06-03 14:31:00") },
            new NotificacionMensaje { UsuarioId = idPorUsername["camilarojas"], Tipo = "Mensaje", Mensaje = "maria_closet te envio un mensaje", Leida = false, FechaCreacion = DateTime.Parse("2026-06-03 13:10:00") },
            new NotificacionMensaje { UsuarioId = idPorUsername["sofiamendez"], Tipo = "Mensaje", Mensaje = "valecruz te envio un mensaje", Leida = false, FechaCreacion = DateTime.Parse("2026-06-04 09:01:00") },
            new NotificacionVenta { UsuarioId = idPorUsername["maria_closet"], Tipo = "Venta", Mensaje = "Tu compra fue confirmada! La vendedora confirmo la venta.", Leida = false, FechaCreacion = DateTime.Parse("2026-06-03 14:00:00") },
            new NotificacionVenta { UsuarioId = idPorUsername["sofiamendez"], Tipo = "Venta", Mensaje = "Tu compra fue confirmada! La vendedora confirmo la venta.", Leida = true, FechaCreacion = DateTime.Parse("2026-06-03 15:00:00") },
        };
        context.Notificaciones.AddRange(notificaciones);
        context.SaveChanges();

        var reportes = new List<Reporte>
        {
            new ReportePrenda { ReportanteId = idPorUsername["maria_closet"], PublicacionId = pubId["Chaqueta de cuero negra"], ReportadaId = null, Razon = "Las fotos no corresponden a la prenda real", Estado = "Pendiente", FechaReporte = DateTime.Parse("2026-06-05 10:00:00"), TituloPublicacion = "Chaqueta de cuero negra" },
            new ReporteUsuaria { ReportanteId = idPorUsername["valecruz"], PublicacionId = null, ReportadaId = idPorUsername["laura_closet"], Razon = "Usuaria no responde los mensajes", Estado = "Pendiente", FechaReporte = DateTime.Parse("2026-06-05 11:00:00"), TituloPublicacion = null },
            new ReportePrenda { ReportanteId = idPorUsername["sofiamendez"], PublicacionId = pubId["Blusa estampada colorida"], ReportadaId = null, Razon = "Descripcion enganosa del producto", Estado = "Resuelto", FechaReporte = DateTime.Parse("2026-06-04 15:00:00"), TituloPublicacion = "Blusa estampada colorida" },
        };
        context.Reportes.AddRange(reportes);
        context.SaveChanges();

        Console.WriteLine("Base de datos poblada con datos de prueba: " +
            $"{context.Usuarios.Count()} usuarios, {context.Categorias.Count()} categorias, " +
            $"{context.Publicaciones.Count()} publicaciones, {context.Compras.Count()} compras, " +
            $"{context.Conversaciones.Count()} conversaciones, {context.Mensajes.Count()} mensajes, " +
            $"{context.Favoritos.Count()} favoritos, {context.Notificaciones.Count()} notificaciones, " +
            $"{context.Reportes.Count()} reportes.");
    }

    private static Publicacion Pub(string titulo, string descripcion, decimal precio, string talla,
        string condicion, string estado, string fecha, int usuarioId, int categoriaId, string imagenUrl)
    {
        return new Publicacion
        {
            Titulo = titulo,
            Descripcion = descripcion,
            Precio = precio,
            Talla = talla,
            Condicion = condicion,
            Estado = estado,
            FechaPublicacion = DateTime.Parse(fecha),
            UsuarioId = usuarioId,
            CategoriaId = categoriaId,
            ImagenUrl = imagenUrl
        };
    }

    private static Compra CompraNueva(string fecha, string estado, int usuarioId, int publicacionId, decimal monto)
    {
        return new Compra
        {
            FechaCompra = DateTime.Parse(fecha),
            Estado = estado,
            UsuarioId = usuarioId,
            PublicacionId = publicacionId,
            MontoTotal = monto
        };
    }

    private static Conversacion ConvNueva(int compradoraId, int vendedoraId, int publicacionId, string fecha)
    {
        return new Conversacion
        {
            CompradoraId = compradoraId,
            VendedoraId = vendedoraId,
            PublicacionId = publicacionId,
            FechaCreacion = DateTime.Parse(fecha)
        };
    }

    private static Mensaje MsgNuevo(int conversacionId, int remitenteId, string texto, string fecha, bool leido)
    {
        return new Mensaje
        {
            ConversacionId = conversacionId,
            RemitenteId = remitenteId,
            Texto = texto,
            FechaEnvio = DateTime.Parse(fecha),
            Leido = leido
        };
    }

    private static Favorito FavNuevo(int usuarioId, int publicacionId, string fecha)
    {
        return new Favorito
        {
            UsuarioId = usuarioId,
            PublicacionId = publicacionId,
            FechaAgregado = DateTime.Parse(fecha)
        };
    }

    private static void AplicarCorreccionesFK(MochiClosetDbContext context)
    {
        var comandos = new[]
        {
            "ALTER TABLE Compras ALTER COLUMN PublicacionId INT NULL",
            @"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Compras_Pub_PubId')
              ALTER TABLE Compras DROP CONSTRAINT FK_Compras_Pub_PubId",
            @"ALTER TABLE Compras ADD CONSTRAINT FK_Compras_Pub_PubId
              FOREIGN KEY (PublicacionId) REFERENCES Publicaciones(Id) ON DELETE SET NULL",

            "ALTER TABLE Favoritos ALTER COLUMN PublicacionId INT NULL",
            @"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Favoritos_Pub_PubId')
              ALTER TABLE Favoritos DROP CONSTRAINT FK_Favoritos_Pub_PubId",
            @"ALTER TABLE Favoritos ADD CONSTRAINT FK_Favoritos_Pub_PubId
              FOREIGN KEY (PublicacionId) REFERENCES Publicaciones(Id) ON DELETE SET NULL",

            "ALTER TABLE Conversaciones ALTER COLUMN PublicacionId INT NULL",
            @"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Conversaciones_Pub_PubId')
              ALTER TABLE Conversaciones DROP CONSTRAINT FK_Conversaciones_Pub_PubId",
            @"ALTER TABLE Conversaciones ADD CONSTRAINT FK_Conversaciones_Pub_PubId
              FOREIGN KEY (PublicacionId) REFERENCES Publicaciones(Id) ON DELETE SET NULL",

            @"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Reportes_Pub_PubId')
              ALTER TABLE Reportes DROP CONSTRAINT FK_Reportes_Pub_PubId",
            @"ALTER TABLE Reportes ADD CONSTRAINT FK_Reportes_Pub_PubId
              FOREIGN KEY (PublicacionId) REFERENCES Publicaciones(Id) ON DELETE SET NULL",

            @"IF EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_Mensajes_Conv_ConvId')
              ALTER TABLE Mensajes DROP CONSTRAINT FK_Mensajes_Conv_ConvId",
            @"ALTER TABLE Mensajes ADD CONSTRAINT FK_Mensajes_Conv_ConvId
              FOREIGN KEY (ConversacionId) REFERENCES Conversaciones(Id) ON DELETE CASCADE",
        };

        foreach (var sql in comandos)
        {
            try
            {
                context.Database.ExecuteSqlRaw(sql);
            }
            catch
            {
            }
        }

        Console.WriteLine("Correcciones de Foreign Keys verificadas/aplicadas.");
    }
}