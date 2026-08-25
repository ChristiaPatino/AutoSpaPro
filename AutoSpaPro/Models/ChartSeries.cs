namespace AutoSpaPro.Models;

/// <summary>Una serie de datos para el gráfico de barras (ej: "Lavados" o "Parqueadero").</summary>
public record ChartSeries(string Name, string ColorVar, List<decimal> Values);
