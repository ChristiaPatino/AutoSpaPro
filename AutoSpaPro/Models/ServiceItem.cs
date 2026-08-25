using System.ComponentModel.DataAnnotations;

namespace AutoSpaPro.Models;

public class ServiceItem
{
    public int Id { get; set; }

    [Required(ErrorMessage = "El nombre del servicio es obligatorio")]
    public string Name { get; set; } = string.Empty;

    public decimal Price { get; set; }

    [Range(15, 480, ErrorMessage = "Duración entre 15 y 480 minutos")]
    public int DurationMinutes { get; set; } = 60;

    public bool Active { get; set; } = true;
}
