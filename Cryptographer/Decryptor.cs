using System;
using System.Collections.Generic;
using System.Text;

namespace Cryptographer
{
    class Decryptor : IDecryptor
    {
        public string PublicKey_RSA { get; }
        public string Key_AES { get; }
        public string IV_AES { get; }

        public Decryptor(string publicKey_RSA, string key_AES, string iv_AES)
        {
            PublicKey_RSA = publicKey_RSA;
            Key_AES = key_AES;
            IV_AES = iv_AES;
        }



        public T DecryptAES<T>(string key, string key_AES, string IV_AES)
        {
            throw new NotImplementedException();
        }

        public bool VerifyHashRSA(string hash, string publicKey)
        {
            throw new NotImplementedException();
        }
    }
}
