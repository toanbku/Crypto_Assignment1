using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MatMaAnimal
{
    class RSA
    {
        public static void Gen_key(string pathFolder, int sizeKey)
        {
            // pathFolder: folder luu key
            // sizeKey: kich thuoc cua key (2048, 3072, 4096, 8192, 16384)
            RSACryptoServiceProvider rsaGenKeys = new RSACryptoServiceProvider(sizeKey);
            string privateXml = rsaGenKeys.ToXmlString(true);
            string publicXml = rsaGenKeys.ToXmlString(false);
            File.WriteAllText(pathFolder + ".pubk", publicXml);
            File.WriteAllText(pathFolder + ".prvk", privateXml);
        }

        public static void EncryptFile(string file, string fileEncrypted, string publicKeyPath)
        {
            // file: File can ma hoa
            // fileEncrypted: Dia chi file sau khi ma hoa
            // publicKeyPath: Dia chi file public key
            String publicXml = File.ReadAllText(publicKeyPath);
            byte[] dataToEncrypt = File.ReadAllBytes(file);
            RSACryptoServiceProvider rsaPublic = new RSACryptoServiceProvider();
            rsaPublic.FromXmlString(publicXml);
            byte[] encryptedRSA = rsaPublic.Encrypt(dataToEncrypt, false);
            File.WriteAllBytes(fileEncrypted, encryptedRSA);
        }

        public static bool DecryptFile(string file, string fileDecrypted, string privateKeyPath)
        {
            // file: file can giai ma
            // fileDecrypted: Dia chi file sau khi giai ma
            // privateKeyPath: Dia chi file private Key

            String privateXml = File.ReadAllText(privateKeyPath);

            byte[] dataToDecrypt = File.ReadAllBytes(file);
            RSACryptoServiceProvider rsaPrivate = new RSACryptoServiceProvider();
            rsaPrivate.FromXmlString(privateXml);
            byte[] decryptedRSA = rsaPrivate.Decrypt(dataToDecrypt, false);
            if (decryptedRSA != null && decryptedRSA.Length > 0)
            {
                File.WriteAllBytes(fileDecrypted, decryptedRSA);
                return true;
            }
            return false;
        }
    }
}
