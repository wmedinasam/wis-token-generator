using System.Security.Cryptography;
using System.Text;
using GeneradorLlaves.Blazor.Models;
using Newtonsoft.Json;

namespace GeneradorLlaves.Blazor.Services;

public sealed class KeyGeneratorService
{
    private const string Key = "26f30b3f82358cf800590be9181bae35";
    private const string InitializationVector = "36431d924ea0a481";

    public string EncryptText(string text) => Transform(text, encrypt: true);

    public string DecryptText(string text) => Transform(text, encrypt: false);

    public string GenerateLicense(KeyModel model)
    {
        ArgumentNullException.ThrowIfNull(model);

        var serializedModel = new KeyModel
        {
            Instancia = EncryptText(model.Instancia),
            Nit = model.Nit,
            NombreCliente = model.NombreCliente,
            FechaVencimiento = model.FechaVencimiento,
            Sistema = model.Sistema
        };

        return EncryptText(JsonConvert.SerializeObject(serializedModel));
    }

    public KeyModel DecryptLicense(string encryptedLicense)
    {
        var json = DecryptText(encryptedLicense);
        var model = JsonConvert.DeserializeObject<KeyModel>(json)
            ?? throw new InvalidDataException("La llave no contiene un modelo válido.");

        model.Instancia = DecryptText(model.Instancia);
        return model;
    }

    private static string Transform(string text, bool encrypt)
    {
        ArgumentNullException.ThrowIfNull(text);

        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(Key);
        aes.IV = Encoding.UTF8.GetBytes(InitializationVector);
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;

        using var transform = encrypt ? aes.CreateEncryptor() : aes.CreateDecryptor();
        var input = encrypt
            ? Encoding.UTF8.GetBytes(text)
            : Convert.FromBase64String(text);
        var output = transform.TransformFinalBlock(input, 0, input.Length);

        return encrypt
            ? Convert.ToBase64String(output)
            : Encoding.UTF8.GetString(output);
    }
}
