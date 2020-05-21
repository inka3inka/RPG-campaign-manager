using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_GM_IMP.Utils
{
    public interface ISecurityFunctions
    {
        string Encrypt(string key, string toEncrypt, bool useHashing = true);
        string Decrypt(string key, string cipherString, bool useHashing = true);
    }
}
