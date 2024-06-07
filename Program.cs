using System;
using System.Runtime.InteropServices;
using System.IO;
using System.Security.Cryptography;
using System.Text;

class Program{
    private static readonly byte[] Key = Encoding.UTF8.GetBytes("==ganti dengan 32bit key atau 32 huruf / angka acak =="); 
    private static readonly byte[] IV = Encoding.UTF8.GetBytes("==ganti dengan 16bit key atau 16bit huruf / angka acak untuk deskripsi==");  

     static string EncryptPassword(string password)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(password);
                }

                byte[] encrypted = msEncrypt.ToArray();
                return Convert.ToBase64String(encrypted);
            }
        }
    }

    
    static string DecryptPassword(string encryptedPassword)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Key;
            aesAlg.IV = IV;

            ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedPassword)))
            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))
            {
                return srDecrypt.ReadToEnd();
            }
        }
    }
    static void Main(){
         Console.Write("Masukkan Kalimat Untuk di Encrypt: ");
         string? input = Console.ReadLine();
         Console.WriteLine("Enkripsi: " + EncryptPassword(input) );
         string? deskripsi = EncryptPassword(input);
         Console.WriteLine("Deskripsi: " + DecryptPassword(deskripsi) );
    }

}