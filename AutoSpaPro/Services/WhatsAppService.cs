using System.Globalization;
using System.Net;
using AutoSpaPro.Models;

namespace AutoSpaPro.Services;

/// <summary>
/// Genera enlaces "wa.me" con el mensaje ya redactado. Al hacer clic se abre WhatsApp
/// (web o app) listo para enviar — sin usar ninguna API paga. El día de mañana, si el
/// negocio crece y quieren que el envío sea 100% automático (sin dar clic), se puede
/// reemplazar este servicio por una integración con la API oficial de WhatsApp Business
/// o un proveedor como Twilio.
/// </summary>
public static class WhatsAppService
{
    public static string BuildReminderLink(ParkingCustomer customer)
    {
        var message = BuildReminderMessage(customer);
        var phone = NormalizePhone(customer.Phone);
        return $"https://wa.me/{phone}?text={WebUtility.UrlEncode(message)}";
    }

    public static string BuildReminderMessage(ParkingCustomer customer)
    {
        var fecha = customer.NextDueDate.ToString("d 'de' MMMM", new CultureInfo("es-CO"));
        var dias = customer.DaysUntilDue;

        var vencimiento = dias switch
        {
            < 0 => $"venció hace {Math.Abs(dias)} día(s)",
            0 => "vence HOY",
            1 => "vence mañana",
            _ => $"vence en {dias} días"
        };

        return $"Hola {customer.FullName} 👋, te recordamos desde {BusinessInfo.Name} que el pago de tu " +
               $"parqueadero ({customer.Plate}) {vencimiento} ({fecha}). Valor: ${customer.MonthlyFee:N0}. " +
               "¡Gracias por confiar en nosotros! 🚗🏍️";
    }

    private static string NormalizePhone(string phone)
    {
        var digits = new string(phone.Where(char.IsDigit).ToArray());
        return digits;
    }
}
