using System.ComponentModel.DataAnnotations;

namespace AutoSpaPro.Models;

public enum VehicleType
{
    Carro,
    Moto
}

public enum PaymentStatus
{
    AlDia,
    ProximoAVencer,
    Vencido
}

public class ParkingCustomer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "El teléfono de WhatsApp es obligatorio")]
    public string Phone { get; set; } = string.Empty; // Formato: 573001234567 (sin '+', sin espacios)

    public VehicleType VehicleType { get; set; } = VehicleType.Carro;

    [Required(ErrorMessage = "La placa es obligatoria")]
    public string Plate { get; set; } = string.Empty;

    public decimal MonthlyFee { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Today;

    /// <summary>Día del mes (1-31) en que vence el pago.</summary>
    [Range(1, 31, ErrorMessage = "El día debe estar entre 1 y 31")]
    public int PaymentDueDay { get; set; } = DateTime.Today.Day;

    public DateTime? LastPaymentDate { get; set; }

    public bool Active { get; set; } = true;

    public string? Notes { get; set; }

    // ----- Propiedades calculadas (no se guardan en BD) -----

    public DateTime NextDueDate
    {
        get
        {
            var baseDate = LastPaymentDate ?? StartDate;
            var year = baseDate.Year;
            var month = baseDate.Month;

            // Si ya se pagó este mes, la próxima fecha es el mes siguiente
            var candidate = SafeDate(year, month, PaymentDueDay);
            if (LastPaymentDate.HasValue && candidate <= LastPaymentDate.Value)
            {
                month++;
                if (month > 12) { month = 1; year++; }
                candidate = SafeDate(year, month, PaymentDueDay);
            }

            return candidate;
        }
    }

    private static DateTime SafeDate(int year, int month, int day)
    {
        var daysInMonth = DateTime.DaysInMonth(year, month);
        return new DateTime(year, month, Math.Min(day, daysInMonth));
    }

    public int DaysUntilDue => (NextDueDate.Date - DateTime.Today).Days;

    public PaymentStatus Status
    {
        get
        {
            if (DaysUntilDue < 0) return PaymentStatus.Vencido;
            if (DaysUntilDue <= 3) return PaymentStatus.ProximoAVencer;
            return PaymentStatus.AlDia;
        }
    }
}
