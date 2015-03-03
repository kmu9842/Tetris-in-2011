using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Tetris
{
    public partial class frm_main : Form
    {
        private const string SECURITYCODE = "OIz%bF*r0lEi,'<";

        /*/// <summary>
        /// HMACSHA512
        /// </summary>
        /// <param name="Input">해쉬처리할 데이터</param>
        /// <param name="Key">키</param>
        /// <returns>해쉬</returns>
        private static string HMACSHA512_HASH(string Input, string Key)
        {
            HMACSHA512 hmac = new HMACSHA512(Encoding.Unicode.GetBytes(Key));
            return Convert.ToBase64String(hmac.ComputeHash(Encoding.Unicode.GetBytes(Input)));
        }*/

        /// <summary>
        /// AES256 암호화
        /// </summary>
        /// <param name="InputText">암호화할 데이터</param>
        /// <param name="Password">키</param>
        /// <returns>암호화된 데이터</returns>
        internal static string EncryptString(string InputText)//, string Password)
        {
            string Password = SECURITYCODE;
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] PlainText = System.Text.Encoding.Unicode.GetBytes(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            ICryptoTransform Encryptor = RijndaelCipher.CreateEncryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(PlainText, 0, PlainText.Length);
            cryptoStream.FlushFinalBlock();
            byte[] CipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            string EncryptedData = Convert.ToBase64String(CipherBytes);
            return EncryptedData;
        }

        /// <summary>
        /// AES256 복호화
        /// </summary>
        /// <param name="InputText">복호화할 데이터</param>
        /// <param name="Password">키</param>
        /// <returns>복호화된 데이터</returns>
        internal static string DecryptString(string InputText)//, string Password)
        {
            string Password = SECURITYCODE;
            RijndaelManaged RijndaelCipher = new RijndaelManaged();
            byte[] EncryptedData = Convert.FromBase64String(InputText);
            byte[] Salt = Encoding.ASCII.GetBytes(Password.Length.ToString());
            PasswordDeriveBytes SecretKey = new PasswordDeriveBytes(Password, Salt);
            ICryptoTransform Decryptor = RijndaelCipher.CreateDecryptor(SecretKey.GetBytes(32), SecretKey.GetBytes(16));
            MemoryStream memoryStream = new MemoryStream(EncryptedData);
            CryptoStream cryptoStream = new CryptoStream(memoryStream, Decryptor, CryptoStreamMode.Read);
            byte[] PlainText = new byte[EncryptedData.Length];
            int DecryptedCount = cryptoStream.Read(PlainText, 0, PlainText.Length);
            memoryStream.Close();
            cryptoStream.Close();
            string DecryptedData = Encoding.Unicode.GetString(PlainText, 0, DecryptedCount);
            return DecryptedData;
        }
    }
}