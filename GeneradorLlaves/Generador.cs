using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GeneradorLlaves
{
    public static class Generador
    {
        public static string key = "26f30b3f82358cf800590be9181bae35";
        public static string iv = "36431d924ea0a481";
        //public static string key = "01ad161734e9f30b2d651fe1ad34e163";
        //public static string iv = "729a9c1f9388f630";

        public static AesCryptoServiceProvider CrearEncriptor(string Key, string IV)
        {
            AesCryptoServiceProvider encriptor = new AesCryptoServiceProvider();
            byte[] keybytes = Encoding.UTF8.GetBytes(Key);
            byte[] ivbytes = Encoding.UTF8.GetBytes(IV);
            encriptor.Key = keybytes;
            encriptor.IV = ivbytes;
            return encriptor;
        }
        public static string EncriptarDatos(AesCryptoServiceProvider aesEnc, string Texto)
        {
            try
            {
                ICryptoTransform ct = aesEnc.CreateEncryptor();
                byte[] input = Encoding.UTF8.GetBytes(Texto);
                string TextoCifrado = Convert.ToBase64String(ct.TransformFinalBlock(input, 0, input.Length));
                return TextoCifrado;
            }
            catch (Exception)
            {

                MessageBox.Show("LLAVE INCORRECTA!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }
        public static string DesencriptarDatos(AesCryptoServiceProvider aesEnc, string TextoCifrado)
        {
            try
            {
                byte[] b = Convert.FromBase64String(TextoCifrado);
                ICryptoTransform ct = aesEnc.CreateDecryptor();
                byte[] output = ct.TransformFinalBlock(b, 0, b.Length);
                return Encoding.UTF8.GetString(output);
            }
            catch (Exception)
            {
                MessageBox.Show("LLAVE INCORRECTA!!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

        }
        public static string DesencriptarDatos(this string Texto)
        {
            var encriptor = CrearEncriptor(key, iv);
            return DesencriptarDatos(encriptor, Texto);
        }
        public static string EncriptarDatos(this string Texto)
        {
            var encriptor = CrearEncriptor(key, iv);
            return EncriptarDatos(encriptor, Texto);
        }
    }
}
