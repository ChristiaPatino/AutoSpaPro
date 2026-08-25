using System.ComponentModel.DataAnnotations;

namespace AutoSpaPro.Models;

public enum AppointmentStatus
{
    Pendiente,
    Confirmada,
    Completada,
    Cancelada
}

public class Appointment
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    public string Phone { get; set; } = string.Empty;

    public VehicleType VehicleType { get; set; } = VehicleType.Carro;

    public string? Plate { get; set; }

    public int ServiceItemId { get; set; }
    public ServiceItem? ServiceItem { get; set; }

    public DateTime AppointmentDate { get; set; } = DateTime.Today;

    public TimeSpan AppointmentTime { get; set; } = new(9, 0, 0);

    public AppointmentStatus Status { get; set; } = AppointmentStatus.Pendiente;

    /// <summary>Precio congelado al momento de agendar (puede diferir si luego cambia el precio del servicio).</summary>
    public decimal Price { get; set; }

    public string? Notes { get; set; }

    public DateTime StartDateTime => AppointmentDate.Date + AppointmentTime;
}
