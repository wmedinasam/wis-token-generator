namespace GeneradorLlaves.Blazor.Models;

public sealed class KeyModel
{
    public string Instancia { get; set; } = string.Empty;

    public string Nit { get; set; } = string.Empty;

    public string NombreCliente { get; set; } = string.Empty;

    public string FechaVencimiento { get; set; } = string.Empty;

    public string Sistema { get; set; } = string.Empty;
}
