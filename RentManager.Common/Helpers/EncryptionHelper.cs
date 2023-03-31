using System.Security.Cryptography;
using System.Text;

namespace RentManager.Common.Helpers;

public static class EncryptionHelper
{
    public static byte[] GetEncryptionKey()
    {
        var encryptionKey = RandomNumberGenerator.GetBytes(64);
        return encryptionKey;
    }

    public static string ConvertByteArrayToString(byte[] byteArray)
    {
        return System.Text.Encoding.UTF8.GetString(byteArray);
    }

    public static byte[] ConvertStringToByteArray(string str)
    {
        return System.Text.Encoding.UTF8.GetBytes(str);
    }

    public static string ConvertByteArrayToBase64String(byte[] byteArray)
    {
        return Convert.ToBase64String(byteArray);
    }

    public static byte[] ConvertBase64StringToByteArray(string base64String)
    {
        return Convert.FromBase64String(base64String);
    }

    private static readonly byte[] salt = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }; // salt for password-based encryption

    public static string EncryptString(string inputString, string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        using (var aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 1000, HashAlgorithmName.SHA256).GetBytes(32);
            aes.Key = key;
            aes.IV = new byte[16];
            using (var encryptor = aes.CreateEncryptor())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(inputBytes, 0, inputBytes.Length);
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
    }

    public static string DecryptString(string inputString, string password)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        using (var aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 1000, HashAlgorithmName.SHA256).GetBytes(32);
            aes.Key = key;
            aes.IV = new byte[16];
            using (var decryptor = aes.CreateDecryptor())
            {
                byte[] inputBytes = Convert.FromBase64String(inputString);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(inputBytes, 0, inputBytes.Length);
                    }
                    return Encoding.UTF8.GetString(ms.ToArray());
                }
            }
        }
    }

    public static byte[] HashString(string inputString)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputString);
            return sha256.ComputeHash(inputBytes);
        }
    }

    public static bool VerifyHash(string inputString, byte[] hashBytes)
    {
        byte[] inputHash = HashString(inputString);
        if (inputHash.Length != hashBytes.Length)
        {
            return false;
        }
        for (int i = 0; i < inputHash.Length; i++)
        {
            if (inputHash[i] != hashBytes[i])
            {
                return false;
            }
        }
        return true;
    }
}