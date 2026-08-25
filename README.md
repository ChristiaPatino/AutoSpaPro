# AutoSpa Premium — Prototipo web (Blazor .NET 10)

Prototipo de sitio web para un negocio de **lavadero de carros y motos + parqueadero**,
pensado para mostrarle al cliente antes de cerrar el proyecto. Costo de operación: **$0**
(sin API de pago, sin hosting pago — todo corre local con SQLite).

## ¿Qué incluye?

### Sitio público
1. **Landing** (`/`) con el eslogan, servicios, sección de parqueadero y contacto, en la
   paleta azul-gris solicitada, con espacio reservado para el logo del cliente.
2. **Agendar cita** (`/agendar`) — el cliente elige servicio, fecha y hora disponible
   (con validación automática de cupos por franja horaria) y queda registrada al instante.

### Panel privado (`/admin`) — protegido con usuario y contraseña
1. **Inicio**: dashboard con ingresos del mes, citas de hoy y alertas de parqueaderos por vencer.
2. **Citas**: calendario mensual visual — clic en un día para ver/gestionar sus citas
   (confirmar, marcar completada, cancelar, o mandar recordatorio por WhatsApp).
3. **Parqueadero**: registro de clientes con parqueadero mensual, estado (al día / próximo
   a vencer / vencido) y recordatorio de pago por WhatsApp con un clic.
4. **Contabilidad**: gráfica comparativa de ingresos mes a mes (lavados vs. parqueadero),
   con tabla de detalle y variación porcentual respecto al mes anterior.
5. **Servicios y precios**: catálogo editable de los servicios que se ofrecen — el precio
   que se configura aquí es el que aparece al agendar una cita y el que alimenta la contabilidad.

Todo el envío de WhatsApp (recordatorios de pago, confirmaciones de cita) funciona con
enlaces `wa.me` — **sin ningún costo ni API paga**, con el mensaje ya redactado.

## Cómo correrlo

Requisitos: [.NET 10 SDK](https://dotnet.microsoft.com/download) instalado.

```bash
cd AutoSpaPro
dotnet restore
dotnet run
```

Abre el navegador en la URL que muestre la consola (normalmente `https://localhost:5001` o similar).

La base de datos SQLite (`autospa.db`) se crea automáticamente la primera vez que corres el
proyecto, con datos de ejemplo ya cargados (clientes de parqueadero, servicios, citas pasadas
y futuras) para que el panel y la gráfica de contabilidad se vean bien desde la primera demo.

> Si ya habías corrido una versión anterior de este proyecto, borra el archivo `autospa.db`
> antes de volver a correrlo — así se recrea con las tablas nuevas (citas y servicios).

## Acceso al panel

- URL: `/admin` (o el botón **"Panel del cliente"** en la barra de navegación)
- Usuario: `admin`
- Contraseña: `admin123`

Puedes cambiar estas credenciales en `appsettings.json`, sección `AdminCredentials`.

## Qué es "prototipo" aquí (y qué falta para producción real)

- **Recordatorios de WhatsApp**: hoy funcionan con un botón que abre WhatsApp con el mensaje
  listo (`wa.me`) — requiere que alguien dé clic. Para que se envíen **solos**, automáticamente,
  todos los días sin intervención humana (recordatorios de pago y confirmaciones de cita), se
  necesita conectar la API oficial de WhatsApp Business (Meta) o un proveedor como Twilio —
  ambos tienen costo por mensaje.
- **Login**: es un usuario único guardado en configuración, suficiente para que el dueño
  entre a administrar. Si más adelante quieren varios usuarios/empleados con permisos
  distintos, se migra a ASP.NET Core Identity.
- **Logo**: hay un espacio reservado (`LOGO`) en el header, login y sidebar del panel —
  listo para reemplazar por el logo real del cliente.
- **Nombre del negocio**: usé "AutoSpa Premium" como marcador de posición (no venía en la
  info que compartiste). Está centralizado en `Services/BusinessInfo.cs` — cambiarlo ahí
  actualiza toda la página automáticamente.
- **Cupos del calendario**: por ahora asume 2 bahías/puestos de lavado simultáneos
  (`Services/AppointmentSlots.cs`, constante `BaysAvailable`) — ajústalo al número real
  de vehículos que pueden atender a la vez.

## Estructura del proyecto

```
AutoSpaPro/
├── Components/
│   ├── Layout/          MainLayout (público) y AdminLayout (panel)
│   ├── Shared/           GroupedBarChart (gráfica SVG reutilizable)
│   └── Pages/
│       ├── Home.razor            Landing pública
│       ├── Agendar.razor         Agendar cita (público)
│       └── Admin/
│           ├── Login.razor       Login del panel
│           ├── AdminHome.razor   Dashboard general (/admin)
│           ├── Citas.razor       Calendario y gestión de citas
│           ├── Dashboard.razor   Parqueadero: clientes y estados de pago
│           ├── Contabilidad.razor  Gráfica e ingresos mes a mes
│           ├── Servicios.razor   Catálogo de servicios y precios
│           └── ClienteForm.razor   Crear / editar cliente de parqueadero
├── Data/                 ApplicationDbContext (EF Core + SQLite)
├── Models/                ParkingCustomer, PaymentRecord, Appointment, ServiceItem, ChartSeries
├── Services/               BusinessInfo, WhatsAppService, AppointmentSlots
└── Program.cs              Configuración, autenticación por cookie
```

