namespace AutoSpaPro.Services;

/// <summary>
/// Toda la información del negocio en un solo lugar.
/// Edita estos valores cuando tengas el nombre y logo definitivos del cliente.
/// </summary>
public static class BusinessInfo
{
    public const string Name = "AutoSpa Premium";
    public const string Slogan = "¡Renueva y Protege tu Vehículo!";
    public const string Address = "Calle 6 Bis # 79 F - 27, Barrio Pio XII";
    public const string Phone1 = "317 274 48 56";
    public const string Phone2 = "314 550 85 85";

    // Para los enlaces de WhatsApp (wa.me) se necesita el número en formato internacional sin '+', sin espacios.
    public const string WhatsAppNumber = "573172744856";

    public static readonly string[] Services =
    [
        "Limpieza Profunda de Tapicería y Alfombras",
        "Lavado y Detallado de Motor",
        "Lavado General de Alta Calidad",
        "Corrección de Pintura",
        "Recubrimiento Cerámico (Ceramic Coating)",
        "Descontaminado de Cristales",
        "Parqueadero para Carros y Motos"
    ];

    public const string ResultsClaim = "¡Resultados Impecables y Duraderos!";
}
