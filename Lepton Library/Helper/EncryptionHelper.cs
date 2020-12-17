using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lepton_Library.Helper
{
    public class EncryptionHelper
    {
        public static string EncryptToAESCiphertext(string entryptStr, string key)
        {
            try
            {
                //byte[] keyArray = Encoding.UTF8.GetBytes(Key);
                byte[] keyArray = Convert.FromBase64String(ToBase64String(key));
                byte[] toEncryptArray = Encoding.UTF8.GetBytes(entryptStr);

                RijndaelManaged rDel = new RijndaelManaged();
                rDel.Key = keyArray;
                rDel.Mode = CipherMode.ECB;
                rDel.Padding = PaddingMode.PKCS7;

                ICryptoTransform cTransform = rDel.CreateEncryptor();
                byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

                return Convert.ToBase64String(resultArray, 0, resultArray.Length);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string ToBase64String(string entryptStr)
        {
            return Convert.ToBase64String(UnicodeEncoding.UTF8.GetBytes(entryptStr));
        }
    }
}
