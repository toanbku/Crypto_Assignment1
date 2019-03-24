using System;
using System.Security;
using System.Security.Cryptography;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatMaAnimal
{
    class Aes
    {
        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }


        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (AesCryptoServiceProvider AES = new AesCryptoServiceProvider())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        public static void EncryptFile(string file, string fileEncrypted, string password)
        {
            //string file = "C:\\SampleFile.DLL";
            //string password = "abcd1234";

            byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
            //byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordBytes = File.ReadAllBytes(password);
            
            // Hash the password with SHA256
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            //string fileEncrypted = "C:\\SampleFileEncrypted.DLL";


            File.WriteAllBytes(fileEncrypted, bytesEncrypted);

        }


        public static bool DecryptFile(string fileEncrypted, string file, string password)
        {
            //string fileEncrypted = "C:\\SampleFileEncrypted.DLL";
            //string password = "abcd1234";

            byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
            //byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] passwordBytes = File.ReadAllBytes(password);
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            //string file = "C:\\SampleFile.DLL";
            if (bytesDecrypted != null && bytesDecrypted.Length > 0)
            {
                File.WriteAllBytes(file, bytesDecrypted);
                return true;
            }
            return false;
        }
    }

}
