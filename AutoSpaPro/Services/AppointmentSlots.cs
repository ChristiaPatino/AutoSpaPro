using AutoSpaPro.Models;

namespace AutoSpaPro.Services;

public static class AppointmentSlots
{
    public static readonly TimeSpan OpeningTime = new(8, 0, 0);
    public static readonly TimeSpan ClosingTime = new(17, 0, 0);
    public static readonly TimeSpan SlotDuration = TimeSpan.FromHours(1);

    /// <summary>Cuántos vehículos se pueden atender al mismo tiempo (bahías del lavadero).</summary>
    public const int BaysAvailable = 2;

    public static List<TimeSpan> AllSlots()
    {
        var slots = new List<TimeSpan>();
        for (var t = OpeningTime; t < ClosingTime; t += SlotDuration)
        {
            slots.Add(t);
        }
        return slots;
    }

    /// <summary>Horarios que todavía tienen cupo ese día, dadas las citas activas (no canceladas) existentes.</summary>
    public static List<TimeSpan> AvailableSlots(DateTime date, IEnumerable<Appointment> existingAppointments)
    {
        var takenCounts = existingAppointments
            .Where(a => a.AppointmentDate.Date == date.Date && a.Status != AppointmentStatus.Cancelada)
            .GroupBy(a => a.AppointmentTime)
            .ToDictionary(g => g.Key, g => g.Count());

        var isToday = date.Date == DateTime.Today;

        return AllSlots()
            .Where(slot => !takenCounts.TryGetValue(slot, out var count) || count < BaysAvailable)
            .Where(slot => !isToday || slot > DateTime.Now.TimeOfDay)
            .ToList();
    }
}
