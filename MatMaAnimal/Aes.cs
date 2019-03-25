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
        private static readonly byte[] saltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };
        private const int numOfIter = 1317;
        private const int keyMaxSize = 256, keyMinSize = 128, keySkipSize = 64; // in bits
        private const int IVsize = 16; // in bytes

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

        public static bool EncryptFile(string file, string fileEncrypted, string password, int keySize = keyMaxSize)
        {
            ////string file = "C:\\SampleFile.DLL";
            ////string password = "abcd1234";

            //byte[] bytesToBeEncrypted = File.ReadAllBytes(file);
            ////byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            //byte[] passwordBytes = File.ReadAllBytes(password);

            //// Hash the password with SHA256
            //passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            //byte[] bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            ////string fileEncrypted = "C:\\SampleFileEncrypted.DLL";


            //File.WriteAllBytes(fileEncrypted, bytesEncrypted);


            try
            {
                // key is in Unicode form
                byte[] keyB = File.ReadAllBytes(password);

                //Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(file, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(fileEncrypted, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                // Hash the password with SHA256
                keyB = SHA256.Create().ComputeHash(keyB);
                var key = new Rfc2898DeriveBytes(keyB, saltBytes, numOfIter);

                //
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider()
                {
                    KeySize = keySize
                };

                //aes.BlockSize = 128;

                // blockSizeBytes can take any arbitrary number
                int blockSizeBytes = 4096;
                aes.Key = key.GetBytes(aes.KeySize / 8); // same salt, same password, same number of iteration
                
                // so must randomize the IV for each time of encryption
                

                // Write the following to the FileStream
                // for the encrypted file (outFs):
                // - the IV
                // - the encrypted cipher content

                //Console.WriteLine("hhh");
                fout.Write(aes.IV, 0, IVsize);
                

                //Create variables to help with read and write.
                byte[] bin = new byte[blockSizeBytes]; //This is intermediate storage for the encryption.
                long rdlen = 0;              //This is the total number of bytes written.
                long totlen = fin.Length;    //This is the total length of the input file.
                int len;                     //This is the number of bytes to be written at a time.

                // Now write the cipher text using
                // a CryptoStream for encrypting.
                CryptoStream encStream = new CryptoStream(fout, aes.CreateEncryptor(), CryptoStreamMode.Write);

                //Console.WriteLine("Encrypting...");

                //Read from the input file, then encrypt and write to the output file.
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, blockSizeBytes);
                    encStream.Write(bin, 0, len);
                    rdlen += len;
                    //Console.WriteLine("{0} bytes processed", rdlen);
                }
                fin.Close();
                //encStream.FlushFinalBlock();
                encStream.Close();
                fout.Close();
                //Console.WriteLine("hhh");
                return true;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return false;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file access error occurred: {0}", e.Message);
                return false;
            }
            catch (Exception)
            {
                throw;
            }

        }


        public static bool DecryptFile(string fileEncrypted, string file, string password, int keySize = keyMaxSize)
        {
            ////string fileEncrypted = "C:\\SampleFileEncrypted.DLL";
            ////string password = "abcd1234";

            //byte[] bytesToBeDecrypted = File.ReadAllBytes(fileEncrypted);
            ////byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            //byte[] passwordBytes = File.ReadAllBytes(password);
            //passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            //byte[] bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            ////string file = "C:\\SampleFile.DLL";
            //if (bytesDecrypted != null && bytesDecrypted.Length > 0)
            //{
            //    File.WriteAllBytes(file, bytesDecrypted);
            //    return true;
            //}
            //return false;

            try
            {
                byte[] keyB = File.ReadAllBytes(password);

                //Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(fileEncrypted, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                //
                AesCryptoServiceProvider aes = new AesCryptoServiceProvider()
                {
                    KeySize = keySize
                };
                //BlockSize = 128;
                int blockSizeBytes = 4096;

                keyB = SHA256.Create().ComputeHash(keyB);
                var key = new Rfc2898DeriveBytes(keyB, saltBytes, numOfIter);
                aes.Key = key.GetBytes(aes.KeySize / 8);

                // Extract the IV
                //fin.Seek(0, SeekOrigin.Begin);
                byte[] IVdata = new byte[IVsize];
                fin.Read(IVdata, 0, IVsize);
                aes.IV = IVdata;

                //Create variables to help with read and write.
                byte[] bin = new byte[blockSizeBytes]; //This is intermediate storage for the encryption.
                long rdlen = 0;              //This is the total number of bytes written.
                long totlen = fin.Length - IVsize;    //This is the total length of the input file.
                int len;                     //This is the number of bytes to be written at a time.

                // Decrypt the cipher text from
                // the FileSteam of the encrypted
                // file (fin) into the FileStream
                // for the decrypted file (fout).

                // Start at the beginning
                // of the cipher text.
                fin.Seek(IVsize, SeekOrigin.Begin);
                CryptoStream decStream = new CryptoStream(fout, aes.CreateDecryptor(), CryptoStreamMode.Write);

                //Console.WriteLine("Decrypting...");

                //Read from the input file, then encrypt and write to the output file.
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, blockSizeBytes);
                    decStream.Write(bin, 0, len);
                    rdlen += len;
                    //Console.WriteLine("{0} bytes processed", rdlen);
                }
                fin.Close();
                decStream.Close();
                fout.Close();

                return true;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine("A Cryptographic error occurred: {0}", e.Message);
                return false;
            }
            catch (UnauthorizedAccessException e)
            {
                Console.WriteLine("A file access error occurred: {0}", e.Message);
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

}
