using GeneradorLlaves.Blazor.Models;
using GeneradorLlaves.Blazor.Services;

namespace GeneradorLlaves.Blazor.Tests;

public sealed class KeyGeneratorServiceTests
{
    private const string LegacyEncryptedLicense =
        "v1F9CARt0P0N/MojnkFlIWXp9OYLaWxASFmg124UPNmVvfVh8GVDQNXmYQaBmThkOnxZB3D/Ac2bRewiz+23nSkeznITlwNW4gCAA4ijaH3jkk+HaA8N0eeTV4M7Y5S/t9H5Xxez7cHMm457V9X0GSyDKvQvjDzvNeRARh1y5mWSoSX6MGJhsTbTtipBu8W1";

    private readonly KeyGeneratorService service = new();

    [Fact]
    public void EncryptText_MatchesLegacyCiphertext()
    {
        var encrypted = service.EncryptText("prueba de compatibilidad");

        Assert.Equal("/pOo8rBuNWbTpgcCflrObkFkSfIS9OFfTOdkPcUYM1A=", encrypted);
    }

    [Fact]
    public void DecryptText_ReadsLegacyCiphertext()
    {
        var decrypted = service.DecryptText("/pOo8rBuNWbTpgcCflrObkFkSfIS9OFfTOdkPcUYM1A=");

        Assert.Equal("prueba de compatibilidad", decrypted);
    }

    [Fact]
    public void GenerateLicense_MatchesLegacyJsonAndEncryptionFormat()
    {
        var model = new KeyModel
        {
            Instancia = "mi-instancia",
            Nit = "900123",
            NombreCliente = "Acme",
            FechaVencimiento = "2027-12-31",
            Sistema = "Sistema X"
        };

        var encrypted = service.GenerateLicense(model);

        Assert.Equal(LegacyEncryptedLicense, encrypted);
    }

    [Fact]
    public void DecryptLicense_ReadsLegacyEncryptedLicense()
    {
        var model = service.DecryptLicense(LegacyEncryptedLicense);

        Assert.Equal("mi-instancia", model.Instancia);
        Assert.Equal("900123", model.Nit);
        Assert.Equal("Acme", model.NombreCliente);
        Assert.Equal("2027-12-31", model.FechaVencimiento);
        Assert.Equal("Sistema X", model.Sistema);
    }

    [Fact]
    public void GenerateAndDecryptLicense_PreservesAllFields()
    {
        var original = new KeyModel
        {
            Instancia = "db-área-ñ",
            Nit = "NIT-42",
            NombreCliente = "Compañía Uno",
            FechaVencimiento = "31/12/2027",
            Sistema = "Gestión"
        };

        var encrypted = service.GenerateLicense(original);
        var decrypted = service.DecryptLicense(encrypted);

        Assert.Equal(original.Instancia, decrypted.Instancia);
        Assert.Equal(original.Nit, decrypted.Nit);
        Assert.Equal(original.NombreCliente, decrypted.NombreCliente);
        Assert.Equal(original.FechaVencimiento, decrypted.FechaVencimiento);
        Assert.Equal(original.Sistema, decrypted.Sistema);
    }
}
