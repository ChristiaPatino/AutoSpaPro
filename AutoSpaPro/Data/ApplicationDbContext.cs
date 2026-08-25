using AutoSpaPro.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoSpaPro.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<ParkingCustomer> ParkingCustomers => Set<ParkingCustomer>();
    public DbSet<PaymentRecord> PaymentRecords => Set<PaymentRecord>();
    public DbSet<ServiceItem> ServiceItems => Set<ServiceItem>();
    public DbSet<Appointment> Appointments => Set<Appointment>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParkingCustomer>().Property(p => p.MonthlyFee).HasPrecision(10, 2);
        modelBuilder.Entity<PaymentRecord>().Property(p => p.Amount).HasPrecision(10, 2);
        modelBuilder.Entity<ServiceItem>().Property(p => p.Price).HasPrecision(10, 2);
        modelBuilder.Entity<Appointment>().Property(p => p.Price).HasPrecision(10, 2);

        // Datos de ejemplo para que el cliente vea el prototipo funcionando
        modelBuilder.Entity<ParkingCustomer>().HasData(
            new ParkingCustomer
            {
                Id = 1,
                FullName = "Carlos Ramírez",
                Phone = "573001234567",
                VehicleType = VehicleType.Carro,
                Plate = "ABC123",
                MonthlyFee = 150000,
                StartDate = new DateTime(2026, 6, 5),
                PaymentDueDay = 5,
                LastPaymentDate = new DateTime(2026, 7, 5),
                Active = true
            },
            new ParkingCustomer
            {
                Id = 2,
                FullName = "Laura Gómez",
                Phone = "573109876543",
                VehicleType = VehicleType.Moto,
                Plate = "XYZ89D",
                MonthlyFee = 80000,
                StartDate = new DateTime(2026, 6, 20),
                PaymentDueDay = 20,
                LastPaymentDate = new DateTime(2026, 7, 20),
                Active = true
            },
            new ParkingCustomer
            {
                Id = 3,
                FullName = "Andrés Torres",
                Phone = "573201112233",
                VehicleType = VehicleType.Carro,
                Plate = "JKL456",
                MonthlyFee = 150000,
                StartDate = new DateTime(2026, 6, 1),
                PaymentDueDay = 1,
                LastPaymentDate = null,
                Active = true
            },
            new ParkingCustomer
            {
                Id = 4,
                FullName = "Mónica Ríos",
                Phone = "573157778899",
                VehicleType = VehicleType.Moto,
                Plate = "MNO321",
                MonthlyFee = 80000,
                StartDate = new DateTime(2026, 6, 1),
                PaymentDueDay = 30,
                LastPaymentDate = new DateTime(2026, 8, 1),
                Active = true
            }
        );

        // ----- Catálogo de servicios -----
        modelBuilder.Entity<ServiceItem>().HasData(
            new ServiceItem { Id = 1, Name = "Limpieza Profunda de Tapicería y Alfombras", Price = 90000, DurationMinutes = 90, Active = true },
            new ServiceItem { Id = 2, Name = "Lavado y Detallado de Motor", Price = 60000, DurationMinutes = 45, Active = true },
            new ServiceItem { Id = 3, Name = "Lavado General de Alta Calidad", Price = 35000, DurationMinutes = 40, Active = true },
            new ServiceItem { Id = 4, Name = "Corrección de Pintura", Price = 250000, DurationMinutes = 180, Active = true },
            new ServiceItem { Id = 5, Name = "Recubrimiento Cerámico (Ceramic Coating)", Price = 600000, DurationMinutes = 240, Active = true },
            new ServiceItem { Id = 6, Name = "Descontaminado de Cristales", Price = 50000, DurationMinutes = 45, Active = true }
        );

        // ----- Historial de pagos de parqueadero (varios meses, para la gráfica de contabilidad) -----
        modelBuilder.Entity<PaymentRecord>().HasData(
            new PaymentRecord { Id = 1, ParkingCustomerId = 1, PaymentDate = new DateTime(2026, 5, 5), Amount = 150000 },
            new PaymentRecord { Id = 2, ParkingCustomerId = 2, PaymentDate = new DateTime(2026, 5, 20), Amount = 80000 },
            new PaymentRecord { Id = 3, ParkingCustomerId = 4, PaymentDate = new DateTime(2026, 5, 1), Amount = 80000 },
            new PaymentRecord { Id = 4, ParkingCustomerId = 1, PaymentDate = new DateTime(2026, 6, 5), Amount = 150000 },
            new PaymentRecord { Id = 5, ParkingCustomerId = 2, PaymentDate = new DateTime(2026, 6, 20), Amount = 80000 },
            new PaymentRecord { Id = 6, ParkingCustomerId = 3, PaymentDate = new DateTime(2026, 6, 3), Amount = 150000 },
            new PaymentRecord { Id = 7, ParkingCustomerId = 4, PaymentDate = new DateTime(2026, 6, 1), Amount = 80000 },
            new PaymentRecord { Id = 8, ParkingCustomerId = 1, PaymentDate = new DateTime(2026, 7, 5), Amount = 150000 },
            new PaymentRecord { Id = 9, ParkingCustomerId = 2, PaymentDate = new DateTime(2026, 7, 20), Amount = 80000 },
            new PaymentRecord { Id = 10, ParkingCustomerId = 4, PaymentDate = new DateTime(2026, 7, 3), Amount = 80000 },
            new PaymentRecord { Id = 11, ParkingCustomerId = 4, PaymentDate = new DateTime(2026, 8, 1), Amount = 80000 }
        );

        // ----- Citas de ejemplo: pasadas completadas (alimentan la contabilidad) y próximas -----
        modelBuilder.Entity<Appointment>().HasData(
            new Appointment { Id = 1, CustomerName = "Julián Peña", Phone = "573201230001", VehicleType = VehicleType.Carro, Plate = "AAA111", ServiceItemId = 3, AppointmentDate = new DateTime(2026, 5, 8), AppointmentTime = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Completada, Price = 35000 },
            new Appointment { Id = 2, CustomerName = "Sofía Marín", Phone = "573201230002", VehicleType = VehicleType.Moto, Plate = "BBB222", ServiceItemId = 6, AppointmentDate = new DateTime(2026, 5, 14), AppointmentTime = new TimeSpan(11, 0, 0), Status = AppointmentStatus.Completada, Price = 50000 },
            new Appointment { Id = 3, CustomerName = "Camilo Vidal", Phone = "573201230003", VehicleType = VehicleType.Carro, Plate = "CCC333", ServiceItemId = 1, AppointmentDate = new DateTime(2026, 5, 22), AppointmentTime = new TimeSpan(14, 0, 0), Status = AppointmentStatus.Completada, Price = 90000 },
            new Appointment { Id = 4, CustomerName = "Diana Salcedo", Phone = "573201230004", VehicleType = VehicleType.Carro, Plate = "DDD444", ServiceItemId = 3, AppointmentDate = new DateTime(2026, 6, 3), AppointmentTime = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Completada, Price = 35000 },
            new Appointment { Id = 5, CustomerName = "Esteban Rojas", Phone = "573201230005", VehicleType = VehicleType.Carro, Plate = "EEE555", ServiceItemId = 2, AppointmentDate = new DateTime(2026, 6, 10), AppointmentTime = new TimeSpan(10, 0, 0), Status = AppointmentStatus.Completada, Price = 60000 },
            new Appointment { Id = 6, CustomerName = "Fabiana León", Phone = "573201230006", VehicleType = VehicleType.Moto, Plate = "FFF666", ServiceItemId = 3, AppointmentDate = new DateTime(2026, 6, 18), AppointmentTime = new TimeSpan(15, 0, 0), Status = AppointmentStatus.Completada, Price = 35000 },
            new Appointment { Id = 7, CustomerName = "Gustavo Nieto", Phone = "573201230007", VehicleType = VehicleType.Carro, Plate = "GGG777", ServiceItemId = 5, AppointmentDate = new DateTime(2026, 6, 25), AppointmentTime = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Completada, Price = 600000 },
            new Appointment { Id = 8, CustomerName = "Helena Cruz", Phone = "573201230008", VehicleType = VehicleType.Carro, Plate = "HHH888", ServiceItemId = 3, AppointmentDate = new DateTime(2026, 7, 2), AppointmentTime = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Completada, Price = 35000 },
            new Appointment { Id = 9, CustomerName = "Iván Duarte", Phone = "573201230009", VehicleType = VehicleType.Moto, Plate = "III999", ServiceItemId = 6, AppointmentDate = new DateTime(2026, 7, 9), AppointmentTime = new TimeSpan(11, 0, 0), Status = AppointmentStatus.Completada, Price = 50000 },
            new Appointment { Id = 10, CustomerName = "Jimena Ortiz", Phone = "573201230010", VehicleType = VehicleType.Carro, Plate = "JJJ000", ServiceItemId = 4, AppointmentDate = new DateTime(2026, 7, 16), AppointmentTime = new TimeSpan(13, 0, 0), Status = AppointmentStatus.Completada, Price = 250000 },
            new Appointment { Id = 11, CustomerName = "Kevin Osorio", Phone = "573201230011", VehicleType = VehicleType.Carro, Plate = "KKK111", ServiceItemId = 3, AppointmentDate = new DateTime(2026, 7, 24), AppointmentTime = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Completada, Price = 35000 },
            new Appointment { Id = 12, CustomerName = "Lina Puentes", Phone = "573201230012", VehicleType = VehicleType.Moto, Plate = "LLL222", ServiceItemId = 3, AppointmentDate = new DateTime(2026, 7, 30), AppointmentTime = new TimeSpan(16, 0, 0), Status = AppointmentStatus.Completada, Price = 35000 },
            new Appointment { Id = 13, CustomerName = "Mario Beltrán", Phone = "573201230013", VehicleType = VehicleType.Carro, Plate = "MMM333", ServiceItemId = 1, AppointmentDate = new DateTime(2026, 8, 4), AppointmentTime = new TimeSpan(10, 0, 0), Status = AppointmentStatus.Completada, Price = 90000 },
            new Appointment { Id = 14, CustomerName = "Natalia Quintero", Phone = "573201230014", VehicleType = VehicleType.Carro, Plate = "NNN444", ServiceItemId = 3, AppointmentDate = new DateTime(2026, 8, 12), AppointmentTime = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Completada, Price = 35000 },
            new Appointment { Id = 15, CustomerName = "Oscar Villamil", Phone = "573201230015", VehicleType = VehicleType.Moto, Plate = "OOO555", ServiceItemId = 6, AppointmentDate = new DateTime(2026, 8, 18), AppointmentTime = new TimeSpan(12, 0, 0), Status = AppointmentStatus.Cancelada, Price = 50000 },
            new Appointment { Id = 16, CustomerName = "Paula Restrepo", Phone = "573201230016", VehicleType = VehicleType.Carro, Plate = "PPP666", ServiceItemId = 3, AppointmentDate = new DateTime(2026, 8, 21), AppointmentTime = new TimeSpan(9, 0, 0), Status = AppointmentStatus.Pendiente, Price = 35000 },
            new Appointment { Id = 17, CustomerName = "Ricardo Aya", Phone = "573201230017", VehicleType = VehicleType.Carro, Plate = "QQQ777", ServiceItemId = 2, AppointmentDate = new DateTime(2026, 8, 22), AppointmentTime = new TimeSpan(11, 0, 0), Status = AppointmentStatus.Confirmada, Price = 60000 },
            new Appointment { Id = 18, CustomerName = "Silvia Cuervo", Phone = "573201230018", VehicleType = VehicleType.Moto, Plate = "RRR888", ServiceItemId = 3, AppointmentDate = new DateTime(2026, 8, 23), AppointmentTime = new TimeSpan(15, 0, 0), Status = AppointmentStatus.Pendiente, Price = 35000 }
        );

        base.OnModelCreating(modelBuilder);
    }
}
