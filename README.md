# Mochi Closet

Plataforma web para la compra y venta de ropa de segunda mano entre mujeres en Santa Cruz de la Sierra, Bolivia.

---

## Requisitos previos

Antes de ejecutar el proyecto, asegúrate de tener instalado:

| Herramienta | Versión recomendada | Descarga |
|-------------|---------------------|----------|
| .NET SDK | 10.0 o superior | https://dotnet.microsoft.com/download |
| Node.js | 18.0 o superior | https://nodejs.org |
| Angular CLI | 21.0 o superior | `npm install -g @angular/cli` |
| SQL Server | 2019 o superior | https://www.microsoft.com/sql-server |
| SQL Server Management Studio | Cualquier versión | https://aka.ms/ssmsfullsetup |

---

## Configuración de la base de datos

Existen dos formas de preparar la base de datos. Ambas producen exactamente el mismo resultado (mismas tablas, mismas relaciones, mismos datos de prueba), la diferencia es solo el método. **Se recomienda la Opción 1** porque no requiere abrir SQL Server Management Studio.

### Opción 1 — Automática, sin scripts manuales (recomendada)

El backend trae un componente (`DbSeeder.cs`, dentro de la carpeta `Seed/`) que arma la base de datos por su cuenta la primera vez que corre.

1. En SQL Server Management Studio, crea una base de datos vacía con el nombre `MochiClosetDB`:
   ```sql
   CREATE DATABASE MochiClosetDB;
   ```
2. Configura la cadena de conexión (ver sección "Configurar la cadena de conexión" más abajo)
3. Ejecuta el backend con `dotnet run`

Al iniciar, el backend detecta que la base de datos está vacía y automáticamente: crea todas las tablas y relaciones, y agrega los datos de prueba (usuarias, prendas, categorías, etc.). No hace falta tocar SSMS para nada más.

### Opción 2 — Manual, ejecutando el script SQL

Si preferís ver y controlar el SQL directamente, en lugar de la Opción 1:

1. Abre SQL Server Management Studio
2. Abre el archivo `/database/mochicloset.sql` de este repositorio
3. Ejecútalo completo (Execute / F5)

Este script crea la base de datos `MochiClosetDB` desde cero, con las mismas tablas, relaciones y datos de prueba que la Opción 1 generaría automáticamente.

**Importante:** elegí solo una de las dos opciones, no las dos. Si ya usaste la Opción 1 y el backend ya pobló la base de datos, no necesitás correr el script de la Opción 2.

### Configurar la cadena de conexión

Abre el archivo:

```
mochicloset-backend/appsettings.json
```

Reemplaza `NOMBRE_SERVIDOR` con el nombre de tu instancia de SQL Server:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=NOMBRE_SERVIDOR;Database=MochiClosetDB;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

**¿Cómo sé el nombre de mi servidor?**
Es el nombre que aparece en el campo "Server name" cuando abres SQL Server Management Studio. Ejemplos comunes:

| Tipo de instalación | Nombre del servidor |
|---------------------|---------------------|
| SQL Server Express | `.\SQLEXPRESS` |
| SQL Server local | `localhost` |
| LocalDB | `(localdb)\MSSQLLocalDB` |
| Con nombre de equipo | `NOMBREPC\SQLEXPRESS` |

---

## Ejecutar el Backend

1. Abre una terminal y navega a la carpeta del backend:

```bash
cd mochicloset-backend
```

2. Restaura las dependencias:

```bash
dotnet restore
```

3. Ejecuta el proyecto:

```bash
dotnet run
```

El backend estará disponible en `http://localhost:5160`

Si configuraste la Opción 1 de la base de datos, al iniciar deberías ver en la consola mensajes como:

```
Correcciones de Foreign Keys verificadas/aplicadas.
Base de datos poblada con datos de prueba: ...
```

(El segundo mensaje solo aparece la primera vez, cuando la base de datos está vacía.)

---

## Ejecutar el Frontend

1. Abre una nueva terminal y navega a la carpeta del frontend:

```bash
cd mochicloset-frontend
```

2. Instala las dependencias:

```bash
npm install
```

3. Ejecuta la aplicación:

```bash
ng serve
```

4. Abre el navegador en:

```
http://localhost:4200
```

---

## Credenciales de prueba

### Administradora
| Campo | Valor |
|-------|-------|
| Email | `admin@mochicloset.com` |
| Contraseña | `admin1234` |

### Usuaria de prueba
| Campo | Valor |
|-------|-------|
| Email | `maria@gmail.com` |
| Contraseña | `12345678` |

---

## Estructura del proyecto

```
mochicloset-proyecto/
├── mochicloset-backend/        # API REST en C# (.NET 10)
│   ├── Controllers/            # Endpoints HTTP
│   ├── Negocio/                # Lógica de negocio
│   ├── Datos/                  # Entidades
│   ├── Data/                   # DbContext
│   ├── Seed/                   # Población automática de datos de prueba (Opción 1)
│   └── appsettings.json        # Configuración (cadena de conexión)
│
├── mochicloset-frontend/       # Aplicación Angular
│   └── src/app/
│       ├── login/
│       ├── registro/
│       ├── home/
│       ├── explorar/
│       ├── detalle-prenda/
│       ├── perfil/
│       ├── editar-perfil/
│       ├── publicar/
│       ├── chat/
│       ├── notificaciones/
│       ├── admin/
│       └── recibo/
│
└── database/
    └── mochicloset.sql         # Script SQL manual (Opción 2, ver arriba)
```

---

## Funcionalidades principales

- Registro e inicio de sesión con validaciones
- Publicar prendas con imagen, precio, talla y condición
- Explorar catálogo con filtros por categoría, talla, condición y precio
- Guardar prendas en favoritos
- Chat entre compradora y vendedora
- Recibo de compra
- Notificaciones del sistema
- Panel de administración para gestionar prendas, usuarias y reportes

---

## Notas importantes

- Las imágenes de prendas se manejan por URL externa, no se suben archivos al servidor
- El chat se actualiza automáticamente cada 3 segundos
- Las publicaciones de usuarias suspendidas no aparecen en el explorar
- El backend debe estar corriendo antes de iniciar el frontend
- La base de datos puede prepararse de dos formas (automática con el backend, o manual con el script SQL), ver sección "Configuración de la base de datos"

---

## Desarrolladoras
- Paola Rosenda Quinteros Pérez
- María Alicia Belaunde Villagomez
