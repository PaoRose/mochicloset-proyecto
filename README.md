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

**Importante:** la única forma de crear la base de datos desde cero (tablas, relaciones y datos de prueba) es ejecutando el script SQL. El backend NO crea las tablas automáticamente.

### Paso 1 — Crear la base de datos con el script SQL

1. Abre SQL Server Management Studio
2. Abre el archivo `/database/mochicloset.sql` de este repositorio
3. Ejecútalo completo (Execute / F5)

Este script crea la base de datos `MochiClosetDB`, todas las tablas, relaciones (foreign keys) y los datos de prueba (usuarias, prendas, categorías, compras, etc.).

### Paso 2 — Verificación automática al iniciar el backend (opcional, ya incluido)

El backend tiene un componente (`DbSeeder.cs`, en la carpeta `Seed/`) que se ejecuta automáticamente cada vez que corres `dotnet run`. Este componente:

- Verifica y corrige las foreign keys especiales del sistema (las que usan `ON DELETE SET NULL` o `ON DELETE CASCADE`), por si la base de datos no las tuviera configuradas exactamente así
- Verifica si la tabla `Usuarios` tiene datos. Si está vacía, agrega datos de prueba. Si ya tiene datos (como después de correr el script del Paso 1), no hace nada

**Este componente no crea tablas.** Si la base de datos no existe o no tiene la estructura creada, el backend no va a poder conectarse y dará error al iniciar. Por eso el Paso 1 (script SQL) es obligatorio, no opcional.

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

1. Asegúrate de haber completado el Paso 1 de "Configuración de la base de datos" (ejecutar el script SQL) antes de seguir.

2. Abre una terminal y navega a la carpeta del backend:

```bash
cd mochicloset-backend
```

3. Restaura las dependencias:

```bash
dotnet restore
```

4. Ejecuta el proyecto:

```bash
dotnet run
```

El backend estará disponible en `http://localhost:5160`

Al iniciar, deberías ver en la consola un mensaje como:

```
Correcciones de Foreign Keys verificadas/aplicadas.
```

(Este mensaje confirma que el backend pudo conectarse a la base de datos y verificó las relaciones. Si la base de datos no existe o el script SQL no se ejecutó, el backend mostrará un error de conexión en su lugar.)

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
│   ├── Seed/                   # Verificación de FKs y datos de prueba (no crea tablas)
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
    └── mochicloset.sql         # Script SQL obligatorio: crea la BD, tablas y datos de prueba
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
- El script `/database/mochicloset.sql` es obligatorio para crear la base de datos desde cero; el backend no crea tablas automáticamente

---

## Desarrolladoras
- Paola Rosenda Quinteros Pérez
- María Alicia Belaunde Villagomez
