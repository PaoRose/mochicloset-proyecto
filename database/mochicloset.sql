-- MOCHI CLOSET

USE master;
GO

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MochiClosetDB')
    DROP DATABASE MochiClosetDB;
GO

CREATE DATABASE MochiClosetDB;
GO

USE MochiClosetDB;
GO

-- TABLAS

CREATE TABLE Usuarios (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(200)   NULL,
    Username    NVARCHAR(100)   NULL,
    Email       NVARCHAR(200)   NULL,
    Telefono    NVARCHAR(50)    NULL,
    Password    NVARCHAR(200)   NULL,
    Rol         NVARCHAR(50)    NULL,
    Estado      NVARCHAR(50)    NULL,
    FotoPerfil  NVARCHAR(500)   NULL
);

CREATE TABLE Categorias (
    Id          INT IDENTITY(1,1) PRIMARY KEY,
    Nombre      NVARCHAR(100)   NULL,
    Descripcion NVARCHAR(500)   NULL
);

CREATE TABLE Publicaciones (
    Id               INT IDENTITY(1,1) PRIMARY KEY,
    Titulo           NVARCHAR(200)   NULL,
    Descripcion      NVARCHAR(MAX)   NULL,
    Precio           DECIMAL(10,2)   NOT NULL DEFAULT 0,
    Talla            NVARCHAR(20)    NULL,
    Condicion        NVARCHAR(50)    NULL,
    Estado           NVARCHAR(50)    NULL,
    FechaPublicacion DATETIME2       NOT NULL DEFAULT GETDATE(),
    UsuarioId        INT             NOT NULL,
    CategoriaId      INT             NOT NULL,
    ImagenUrl        NVARCHAR(500)   NULL,
    CONSTRAINT FK_Publicaciones_Usuarios   FOREIGN KEY (UsuarioId)   REFERENCES Usuarios(Id)   ON DELETE NO ACTION,
    CONSTRAINT FK_Publicaciones_Categorias FOREIGN KEY (CategoriaId) REFERENCES Categorias(Id) ON DELETE NO ACTION
);

CREATE TABLE Compras (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    FechaCompra   DATETIME2       NOT NULL DEFAULT GETDATE(),
    Estado        NVARCHAR(50)    NULL,
    UsuarioId     INT             NOT NULL,
    PublicacionId INT             NULL,
    MontoTotal    DECIMAL(10,2)   NOT NULL DEFAULT 0,
    CONSTRAINT FK_Compras_Usuarios      FOREIGN KEY (UsuarioId)     REFERENCES Usuarios(Id)      ON DELETE NO ACTION,
    CONSTRAINT FK_Compras_Publicaciones FOREIGN KEY (PublicacionId) REFERENCES Publicaciones(Id) ON DELETE SET NULL
);

CREATE TABLE Conversaciones (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    CompradoraId  INT       NOT NULL,
    VendedoraId   INT       NOT NULL,
    PublicacionId INT       NULL,
    FechaCreacion DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Conversaciones_Compradora  FOREIGN KEY (CompradoraId)  REFERENCES Usuarios(Id)      ON DELETE NO ACTION,
    CONSTRAINT FK_Conversaciones_Vendedora   FOREIGN KEY (VendedoraId)   REFERENCES Usuarios(Id)      ON DELETE NO ACTION,
    CONSTRAINT FK_Conversaciones_Publicacion FOREIGN KEY (PublicacionId) REFERENCES Publicaciones(Id) ON DELETE SET NULL
);

CREATE TABLE Favoritos (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId     INT       NOT NULL,
    PublicacionId INT       NULL,
    FechaAgregado DATETIME2 NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Favoritos_Usuarios      FOREIGN KEY (UsuarioId)     REFERENCES Usuarios(Id)      ON DELETE NO ACTION,
    CONSTRAINT FK_Favoritos_Publicaciones FOREIGN KEY (PublicacionId) REFERENCES Publicaciones(Id) ON DELETE SET NULL
);

CREATE TABLE Mensajes (
    Id             INT IDENTITY(1,1) PRIMARY KEY,
    ConversacionId INT           NOT NULL,
    RemitenteId    INT           NOT NULL,
    Texto          NVARCHAR(MAX) NULL,
    FechaEnvio     DATETIME2     NOT NULL DEFAULT GETDATE(),
    Leido          BIT           NOT NULL DEFAULT 0,
    CONSTRAINT FK_Mensajes_Conversaciones FOREIGN KEY (ConversacionId) REFERENCES Conversaciones(Id) ON DELETE NO ACTION,
    CONSTRAINT FK_Mensajes_Usuarios       FOREIGN KEY (RemitenteId)    REFERENCES Usuarios(Id)       ON DELETE NO ACTION
);

CREATE TABLE Notificaciones (
    Id            INT IDENTITY(1,1) PRIMARY KEY,
    UsuarioId     INT           NOT NULL,
    Tipo          NVARCHAR(50)  NULL,
    Mensaje       NVARCHAR(MAX) NULL,
    Leida         BIT           NOT NULL DEFAULT 0,
    FechaCreacion DATETIME2     NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Notificaciones_Usuarios FOREIGN KEY (UsuarioId) REFERENCES Usuarios(Id) ON DELETE NO ACTION
);

CREATE TABLE Reportes (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    ReportanteId    INT           NOT NULL,
    PublicacionId   INT           NULL,
    ReportadaId     INT           NULL,
    Razon           NVARCHAR(MAX) NULL,
    Estado          NVARCHAR(50)  NULL,
    FechaReporte    DATETIME2     NOT NULL DEFAULT GETDATE(),
    Discriminator   NVARCHAR(50)  NOT NULL DEFAULT 'Reporte',
    TituloPublicacion NVARCHAR(200) NULL,
    CONSTRAINT FK_Reportes_Reportante  FOREIGN KEY (ReportanteId)  REFERENCES Usuarios(Id)      ON DELETE NO ACTION,
    CONSTRAINT FK_Reportes_Publicacion FOREIGN KEY (PublicacionId) REFERENCES Publicaciones(Id) ON DELETE SET NULL,
    CONSTRAINT FK_Reportes_Reportada   FOREIGN KEY (ReportadaId)   REFERENCES Usuarios(Id)      ON DELETE NO ACTION
);

CREATE TABLE __EFMigrationsHistory (
    MigrationId  NVARCHAR(150) NOT NULL PRIMARY KEY,
    ProductVersion NVARCHAR(32) NOT NULL
);

-- MIGRACIONES

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20260611002918_EstadoActual', '10.0.8');
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20260613005825_AgregarFotoPerfilUsuario', '10.0.8');
INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES ('20260613033902_AgregarFavoritos', '10.0.8');

-- USUARIOS

SET IDENTITY_INSERT Usuarios ON;

INSERT INTO Usuarios (Id, Nombre, Username, Email, Telefono, Password, Rol, Estado, FotoPerfil) VALUES
(1,  'Maria Lopez',     'maria_closet',   'maria@gmail.com',     '76543210', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/1.jpg'),
(2,  'Sofia Mendez',    'sofiamendez',    'sofia@gmail.com',     '75598765', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/2.jpg'),
(3,  'Valentina Cruz',  'valecruz',       'valentina@gmail.com', '71234567', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/3.jpg'),
(4,  'Camila Rojas',    'camilarojas',    'camila@gmail.com',    '78887766', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/4.jpg'),
(5,  'Gabriela Torres', 'gabitorres',     'gabriela@gmail.com',  '79991234', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/5.jpg'),
(6,  'Daniela Vaca',    'danyvaca',       'daniela@gmail.com',   '72223344', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/6.jpg'),
(7,  'Fernanda Suarez', 'fersuarez',      'fernanda@gmail.com',  '73334455', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/7.jpg'),
(8,  'Valeria Ardaya',  'vale_closet',    'valeria@gmail.com',   '79846513', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/8.jpg'),
(9,  'Dafne Rojas',     'dafne_closet',   'dafne@gmail.com',     '79846514', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/9.jpg'),
(10, 'Karen Reyes',     'karen_reyes',    'karen@gmail.com',     '67894512', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/10.jpg'),
(11, 'Laura Perez',     'laura_closet',   'laura@gmail.com',     '67845129', '12345678', 'Usuaria', 'Suspendido','https://randomuser.me/api/portraits/women/11.jpg'),
(12, 'Ana Garcia',      'ana_closet',     'ana@gmail.com',       '76123456', '12345678', 'Usuaria', 'Activo',    'https://randomuser.me/api/portraits/women/12.jpg'),
(13, 'Admin Mochi',     'admin',          'admin@mochicloset.com','00000000', 'admin1234','Admin',   'Activo',    NULL);

SET IDENTITY_INSERT Usuarios OFF;

-- CATEGORIAS

SET IDENTITY_INSERT Categorias ON;

INSERT INTO Categorias (Id, Nombre, Descripcion) VALUES
(1, 'Vestidos',    'Vestidos de todo tipo y ocasion'),
(2, 'Tops y Blusas', 'Tops, blusas y camisas'),
(3, 'Pantalones',  'Pantalones y jeans'),
(4, 'Faldas',      'Faldas de todo estilo'),
(5, 'Abrigos',     'Abrigos y chaquetas'),
(6, 'Accesorios',  'Bolsos, cinturones y mas');

SET IDENTITY_INSERT Categorias OFF;

-- PUBLICACIONES

SET IDENTITY_INSERT Publicaciones ON;

INSERT INTO Publicaciones (Id, Titulo, Descripcion, Precio, Talla, Condicion, Estado, FechaPublicacion, UsuarioId, CategoriaId, ImagenUrl) VALUES
(1,  'Vestido floral rosa',      'Vestido floral en muy buen estado, usado pocas veces',         80.00,  'S',  'Como nuevo', 'Disponible', '2026-06-01 10:00:00', 1,  1, 'https://images.unsplash.com/photo-1572804013309-59a88b7e92f1'),
(2,  'Vestido negro elegante',   'Vestido negro para ocasiones especiales, talla M',             120.00, 'M',  'Usado',      'Disponible', '2026-06-01 10:10:00', 2,  1, 'https://images.unsplash.com/photo-1595777457583-95e059d581b8'),
(3,  'Vestido blanco verano',    'Vestido blanco ideal para verano',                             95.00,  'S',  'Como nuevo', 'Disponible', '2026-06-01 10:20:00', 3,  1, 'https://images.unsplash.com/photo-1566174053879-31528523f8ae'),
(4,  'Top blanco basico',        'Top blanco clasico, talla S',                                 35.00,  'S',  'Como nuevo', 'Vendido',    '2026-06-01 10:30:00', 4,  2, 'https://images.unsplash.com/photo-1523381210434-271e8be1f52b'),
(5,  'Blusa estampada colorida', 'Blusa con estampado tropical, muy linda',                     55.00,  'M',  'Usado',      'Disponible', '2026-06-01 10:40:00', 5,  2, 'https://images.unsplash.com/photo-1564257631407-4deb1f99d992'),
(6,  'Blusa rosa pastel',        'Blusa rosa muy delicada y femenina',                          45.00,  'S',  'Como nuevo', 'Disponible', '2026-06-01 10:50:00', 6,  2, 'https://images.unsplash.com/photo-1485462537746-965f33f7f6a7'),
(7,  'Jean azul clasico',        'Jean azul en buen estado, talla 28',                          90.00,  '28', 'Usado',      'Disponible', '2026-06-01 11:00:00', 7,  3, 'https://images.unsplash.com/photo-1542272604-787c3835535d'),
(8,  'Pantalon negro formal',    'Pantalon negro formal casi sin uso',                          110.00, '30', 'Como nuevo', 'Disponible', '2026-06-01 11:10:00', 8,  3, 'https://images.unsplash.com/photo-1594938298603-c8148c4b4c3b'),
(9,  'Falda midi beige',         'Falda midi color beige muy elegante',                         70.00,  'S',  'Como nuevo', 'Disponible', '2026-06-01 11:20:00', 9,  4, 'https://images.unsplash.com/photo-1592301933927-35b597393c0a'),
(10, 'Falda floral corta',       'Falda corta con flores ideal para verano',                    45.00,  'M',  'Usado',      'Disponible', '2026-06-01 11:30:00', 10, 4, 'https://images.unsplash.com/photo-1583496661160-fb5218ees23b'),
(11, 'Abrigo gris largo',        'Abrigo gris largo para el frio',                             150.00, 'M',  'Como nuevo', 'Vendido',    '2026-06-01 11:40:00', 1,  5, 'https://images.unsplash.com/photo-1544022613-e87ca75a784a'),
(12, 'Chaqueta de cuero negra',  'Chaqueta de cuero sintetico muy cool',                       200.00, 'S',  'Usado',      'Disponible', '2026-06-01 11:50:00', 2,  5, 'https://images.unsplash.com/photo-1551028719-00167b16eac5'),
(13, 'Vestido boho largo',       'Vestido estilo boho largo floreado',                          85.00,  'M',  'Como nuevo', 'Disponible', '2026-06-02 09:00:00', 3,  1, 'https://images.unsplash.com/photo-1572804013309-59a88b7e92f1'),
(14, 'Top crop negro',           'Top crop negro basico, talla XS',                             30.00,  'XS', 'Como nuevo', 'Disponible', '2026-06-02 09:10:00', 4,  2, 'https://images.unsplash.com/photo-1503342217505-b0a15ec3261c'),
(15, 'Jean mom fit claro',       'Jean mom fit celeste, talla 30',                              75.00,  '30', 'Buen estado','Disponible', '2026-06-02 09:20:00', 5,  3, 'https://images.unsplash.com/photo-1542272604-787c3835535d'),
(16, 'Falda midi negra',         'Falda midi negra elegante, talla M',                          60.00,  'M',  'Nuevo',      'Disponible', '2026-06-02 09:30:00', 6,  4, 'https://images.unsplash.com/photo-1583496661160-fb5218ees23b'),
(17, 'Abrigo camel largo',       'Abrigo camel largo de invierno, talla L',                    180.00, 'L',  'Como nuevo', 'Disponible', '2026-06-02 09:40:00', 7,  5, 'https://images.unsplash.com/photo-1544022613-e87ca75a784a'),
(18, 'Vestido rojo elegante',    'Vestido rojo largo para ocasiones especiales',               150.00, 'M',  'Como nuevo', 'Disponible', '2026-06-02 09:50:00', 8,  1, 'https://images.unsplash.com/photo-1595777457583-95e059d581b8'),
(19, 'Blusa de seda beige',      'Blusa de seda color beige, talla M, impecable',              55.00,  'M',  'Nuevo',      'Disponible', '2026-06-02 10:00:00', 9,  2, 'https://images.unsplash.com/photo-1598554747436-c9293d6a588f'),
(20, 'Pantalon beige formal',    'Pantalon beige de tela formal, talla S',                     70.00,  'S',  'Nuevo',      'Disponible', '2026-06-02 10:10:00', 10, 3, 'https://images.unsplash.com/photo-1594938298603-c8148c4b4c3b');

SET IDENTITY_INSERT Publicaciones OFF;

-- COMPRAS

SET IDENTITY_INSERT Compras ON;

INSERT INTO Compras (Id, FechaCompra, Estado, UsuarioId, PublicacionId, MontoTotal) VALUES
(1, '2026-06-03 14:00:00', 'Completada', 1, 4,  35.00),
(2, '2026-06-03 15:00:00', 'Completada', 2, 11, 150.00),
(3, '2026-06-04 10:00:00', 'Completada', 3, 4,  35.00);

SET IDENTITY_INSERT Compras OFF;

-- CONVERSACIONES

SET IDENTITY_INSERT Conversaciones ON;

INSERT INTO Conversaciones (Id, CompradoraId, VendedoraId, PublicacionId, FechaCreacion) VALUES
(1, 1, 4,  4,    '2026-06-03 13:00:00'),
(2, 2, 1,  11,   '2026-06-03 14:30:00'),
(3, 3, 2,  2,    '2026-06-04 09:00:00'),
(4, 4, 3,  3,    '2026-06-04 10:00:00'),
(5, 5, 6,  5,    '2026-06-05 11:00:00');

SET IDENTITY_INSERT Conversaciones OFF;

-- MENSAJES

SET IDENTITY_INSERT Mensajes ON;

INSERT INTO Mensajes (Id, ConversacionId, RemitenteId, Texto, FechaEnvio, Leido) VALUES
(1, 1, 1, 'Hola! Me interesa el top blanco, sigue disponible?',      '2026-06-03 13:01:00', 1),
(2, 1, 4, 'Hola! Si, sigue disponible. En que talla lo necesitas?',  '2026-06-03 13:05:00', 1),
(3, 1, 1, 'Perfecto, talla S. Donde podemos encontrarnos?',          '2026-06-03 13:10:00', 1),
(4, 1, 4, 'Podemos en el centro, plaza 24 de Septiembre',            '2026-06-03 13:15:00', 0),
(5, 2, 2, 'Hola! Me interesa el abrigo gris',                        '2026-06-03 14:31:00', 1),
(6, 2, 1, 'Hola! Esta en excelente estado, solo usado 2 veces',      '2026-06-03 14:35:00', 1),
(7, 3, 3, 'Buenos dias! El vestido negro sigue disponible?',          '2026-06-04 09:01:00', 1),
(8, 3, 2, 'Si! Te lo puedo mostrar hoy si quieres',                  '2026-06-04 09:10:00', 0),
(9, 4, 4, 'Me encanta el vestido blanco, que talla es?',             '2026-06-04 10:01:00', 1),
(10,4, 3, 'Es talla S, esta casi nuevo!',                            '2026-06-04 10:05:00', 0);

SET IDENTITY_INSERT Mensajes OFF;

-- FAVORITOS

SET IDENTITY_INSERT Favoritos ON;

INSERT INTO Favoritos (Id, UsuarioId, PublicacionId, FechaAgregado) VALUES
(1, 1, 2,  '2026-06-02 12:00:00'),
(2, 1, 5,  '2026-06-02 12:05:00'),
(3, 2, 1,  '2026-06-02 12:10:00'),
(4, 2, 13, '2026-06-02 12:15:00'),
(5, 3, 7,  '2026-06-03 09:00:00'),
(6, 4, 18, '2026-06-03 09:05:00');

SET IDENTITY_INSERT Favoritos OFF;

-- NOTIFICACIONES

SET IDENTITY_INSERT Notificaciones ON;

INSERT INTO Notificaciones (Id, UsuarioId, Tipo, Mensaje, Leida, FechaCreacion) VALUES
(1, 1, 'Mensaje',  'sofiamendez te envio un mensaje',              0, '2026-06-03 14:31:00'),
(2, 4, 'Mensaje',  'maria_closet te envio un mensaje',             0, '2026-06-03 13:10:00'),
(3, 2, 'Mensaje',  'valecruz te envio un mensaje',                 0, '2026-06-04 09:01:00'),
(4, 1, 'Venta',    'Tu compra fue confirmada! La vendedora confirmo la venta.', 0, '2026-06-03 14:00:00'),
(5, 2, 'Venta',    'Tu compra fue confirmada! La vendedora confirmo la venta.', 1, '2026-06-03 15:00:00');

SET IDENTITY_INSERT Notificaciones OFF;


-- REPORTES

SET IDENTITY_INSERT Reportes ON;

INSERT INTO Reportes (Id, ReportanteId, PublicacionId, ReportadaId, Razon, Estado, FechaReporte, Discriminator, TituloPublicacion) VALUES
(1, 1, 12,  NULL, 'Las fotos no corresponden a la prenda real', 'Pendiente', '2026-06-05 10:00:00', 'ReportePrenda',  'Chaqueta de cuero negra'),
(2, 3, NULL, 11,  'Usuaria no responde los mensajes',            'Pendiente', '2026-06-05 11:00:00', 'ReporteUsuaria', NULL),
(3, 2, 5,   NULL, 'Descripcion engañosa del producto',           'Resuelto',  '2026-06-04 15:00:00', 'ReportePrenda',  'Blusa estampada colorida');

SET IDENTITY_INSERT Reportes OFF;