namespace AutoSpaPro.Services;

/// <summary>
/// Credenciales del panel de administración, leídas desde appsettings.json / User Secrets.
/// NOTA PROTOTIPO: para producción real, mover a ASP.NET Core Identity con hash de contraseña
/// y, si hay más de un empleado usando el panel, una tabla de usuarios en la base de datos.
/// </summary>
public class AdminCredentialsOptions
{
    public const string SectionName = "AdminCredentials";
    public string Username { get; set; } = "admin";
    public string Password { get; set; } = "admin123";
}
