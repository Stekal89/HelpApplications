using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace EnDeCrypter.Model
{
    public class DeEncrypter
    {
        /// <summary>
        /// Generate a random EncryptionKey
        /// </summary>
        /// <returns></returns>
        public string GenerateEncryptionKey()
        {
            string EncryptionKey = string.Empty;

            Random Robj = new Random();
            int Rnumber = Robj.Next();
            /// ##################################################################################
            /// This will not be a good solution, I prefer to make an static key, so you can encrypt
            /// and decrypt the Password every time.
            /// ##################################################################################
            EncryptionKey = "XYZ" + Convert.ToString(Rnumber);

            return EncryptionKey;
        }
        /// <summary>
        /// Encryption of the Password
        /// </summary>
        /// <param name="clearText">Passwordstring as Plaintext</param>
        /// <param name="EncryptionKey">Encryption Key</param>
        /// <returns></returns>
        public string Encrypt(string clearText, string EncryptionKey)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                // Bytes can be modified 
                encryptor.Key = pdb.GetBytes(16);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Decryption of the Encrypted Password
        /// </summary>
        /// <param name="cipherText"></param>
        /// <param name="EncryptionKey"></param>
        /// <returns></returns>
        public string Decrypt(string cipherText, string EncryptionKey)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                // Bytes can be modified 
                encryptor.Key = pdb.GetBytes(16);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
