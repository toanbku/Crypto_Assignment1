using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Security.Cryptography;
using System.IO;

namespace MatMaAnimal
{
    class TrippleDES
    {

        private static readonly byte[] saltBytes = { 17, 27, 37, 47, 57, 67, 77, 87 };

        private const int numOfIter = 1777;

        private const int keyMaxSize = 192, keyMinSize = 128, keySkipSize = 64;
        // Real key sizes (truncate some unused bits) are respectively 168, 112 and 56.
        private const int IVsize = 8;

        public static bool EncryptFile(string inFPath, string outFPath, string keyFPath, int keySize = keyMaxSize)
        {
            try
            {
                // key is in Unicode form
                byte[] keyB = File.ReadAllBytes(keyFPath);

                //Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(inFPath, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outFPath, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                //Console.WriteLine("hhhh");

                // generate a random salt, random number of iteration
                //byte[] saltBytes = new byte[8];
                //using ( RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider() )
                //{
                //    // Fill the array with a random value.
                //    rngCsp.GetBytes(saltBytes);
                //}
                //int numOfIter = ( new Random() ).Next(1000, 2000);
                var key = new Rfc2898DeriveBytes(keyB, saltBytes, numOfIter);

                //
                TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider()
                {
                    KeySize = keySize
                };

                //tDes.BlockSize = 64;

                // blockSizeBytes can take any arbitrary number
                int blockSizeBytes = 4096; //tDes.BlockSize / 8;
                tDes.Key = key.GetBytes(tDes.KeySize / 8); // same salt, same password, same number of iteration
                //tDes.IV = key.GetBytes(tDes.BlockSize / 8); // lead to same pair of key and IV
                // so must randomize the IV for each time of encryption
                //using (RNGCryptoServiceProvider rngCsp = new RNGCryptoServiceProvider())
                //{
                //    // Fill the array with a random value.
                //    rngCsp.GetBytes(tDes.IV);
                //}

                // Write the following to the FileStream
                // for the encrypted file (outFs):
                // - the IV
                // - the encrypted cipher content

                //Console.WriteLine("hhh");
                fout.Write(tDes.IV, 0, IVsize);
                //foreach (byte h in tDes.IV)
                //{
                //    Console.Write(h);
                //}
                //Console.WriteLine();

                //foreach (byte h in tDes.Key)
                //{
                //    Console.Write(h);
                //}
                //Console.WriteLine();

                //Create variables to help with read and write.
                byte[] bin = new byte[blockSizeBytes]; //This is intermediate storage for the encryption.
                long rdlen = 0;              //This is the total number of bytes written.
                long totlen = fin.Length;    //This is the total length of the input file.
                int len;                     //This is the number of bytes to be written at a time.

                // Now write the cipher text using
                // a CryptoStream for encrypting.
                CryptoStream encStream = new CryptoStream(fout, tDes.CreateEncryptor(), CryptoStreamMode.Write);

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

        public static bool DecryptFile(string inFPath, string outFPath, string keyFPath, int keySize = keyMaxSize)
        {
            try
            {
                byte[] keyB = File.ReadAllBytes(keyFPath);

                //Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(inFPath, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outFPath, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                //
                TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider()
                {
                    KeySize = keySize
                };
                //BlockSize = 64;
                int blockSizeBytes = 4096;//tDes.BlockSize / 8;

                var key = new Rfc2898DeriveBytes(keyB, saltBytes, numOfIter);
                tDes.Key = key.GetBytes(tDes.KeySize / 8);

                // Extract the IV
                //fin.Seek(0, SeekOrigin.Begin);
                byte[] IVdata = new byte[IVsize];
                fin.Read(IVdata, 0, IVsize);
                tDes.IV = IVdata;
                //foreach (byte h in tDes.IV)
                //{
                //    Console.Write(h);
                //}
                //Console.WriteLine();

                //foreach (byte h in tDes.Key)
                //{
                //    Console.Write(h);
                //}
                //Console.WriteLine();

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
                fin.Seek(8, SeekOrigin.Begin);
                CryptoStream decStream = new CryptoStream(fout, tDes.CreateDecryptor(), CryptoStreamMode.Write);

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
    }



}
