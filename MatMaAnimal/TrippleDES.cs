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

        private static readonly byte[] saltBytes = { 1, 2, 3, 4, 5, 6, 7, 8 };

        private const int numOfIter = 1000;

        private const int keyMaxSize = 192, keyMinSize = 128, keySkipSize = 64;

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
                //
                TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
                tDes.KeySize = keySize;
                //BlockSize = 64;
                int blockSizeBytes = tDes.BlockSize / 8;

                var key = new Rfc2898DeriveBytes(keyB, saltBytes, numOfIter);
                tDes.Key = key.GetBytes(tDes.KeySize / 8);
                tDes.IV = key.GetBytes(tDes.BlockSize / 8);

                //Create variables to help with read and write.
                byte[] bin = new byte[blockSizeBytes]; //This is intermediate storage for the encryption.
                long rdlen = 0;              //This is the total number of bytes written.
                long totlen = fin.Length;    //This is the total length of the input file.
                int len;                     //This is the number of bytes to be written at a time.

                
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

                encStream.Close();

                return true;

            }
            catch(CryptographicException e)
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
                // assumes the size of keyFile is in (128, 192, 256)

                //string keyHexStr = File.ReadAllText(keyFPath);
                //// Handle invalid key keyHexStr.
                //byte[] Key = new byte[keyHexStr.Length];
                //for (int i = 0; i < keyHexStr.Length; i++)
                //{
                //    int val = Convert.ToInt32(keyHexStr[i].ToString(), 16);
                //    Key[i] = Convert.ToByte(val);
                //}

                byte[] keyB = File.ReadAllBytes(keyFPath);

                //Create the file streams to handle the input and output files.
                FileStream fin = new FileStream(inFPath, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outFPath, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                //
                TripleDESCryptoServiceProvider tDes = new TripleDESCryptoServiceProvider();
                tDes.KeySize = keySize;
                //BlockSize = 64;
                int blockSizeBytes = tDes.BlockSize / 8;

                var key = new Rfc2898DeriveBytes(keyB, saltBytes, numOfIter);
                tDes.Key = key.GetBytes(tDes.KeySize / 8);
                tDes.IV = key.GetBytes(blockSizeBytes);

                //Create variables to help with read and write.
                byte[] bin = new byte[blockSizeBytes]; //This is intermediate storage for the encryption.
                long rdlen = 0;              //This is the total number of bytes written.
                long totlen = fin.Length;    //This is the total length of the input file.
                int len;                     //This is the number of bytes to be written at a time.


                CryptoStream encStream = new CryptoStream(fout, tDes.CreateDecryptor(), CryptoStreamMode.Write);

                //Console.WriteLine("Decrypting...");

                //Read from the input file, then encrypt and write to the output file.
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, blockSizeBytes);
                    encStream.Write(bin, 0, len);
                    rdlen += len;
                    //Console.WriteLine("{0} bytes processed", rdlen);
                }

                encStream.Close();

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
