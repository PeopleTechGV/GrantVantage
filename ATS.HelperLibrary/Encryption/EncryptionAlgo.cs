using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS.HelperLibrary.Encryption
{
   public static class EncryptionAlgo
    {
        /// <summary>
        /// This is descript and encypt value based on key
        /// </summary>
        /// <param name="ActualValue">Actual value</param>
        /// <param name="EncryptedKey">Encryption</param>
        /// <returns>Get EncryptedValue based on EncryptedKey</returns>
        public static String EncryptionData(String ActualValue)
        {
            return EncryptionLibrary.Encrypt(ActualValue, Constant.PASSWORD_ENCKEY);
        }
        public static String DecryptData(String EncryptedValue)
        {
            return EncryptionLibrary.Decrypt(EncryptedValue, Constant.PASSWORD_ENCKEY);
        }
    }
}
