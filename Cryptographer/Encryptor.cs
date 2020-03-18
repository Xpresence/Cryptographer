using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Cryptographer
{
    class Encryptor : IEncryptor
    {
        public string PrivateKey_RSA { get; }
        public string Key_AES { get; }
        public string IV_AES { get; }

        public Encryptor(string privateKey_RSA, string key_AES, string iv_AES)
        {
            PrivateKey_RSA = privateKey_RSA;
            Key_AES = key_AES;
            IV_AES = iv_AES;
        }

        public string EncryptKey<T>(T data)
        {

            var encryptedHash = EncryptHashRSA<T>(data, PrivateKey_RSA);

            var encryptedKey = EncryptAES<object>(new { Data = data, Hash = encryptedHash }, Key_AES, IV_AES);

            return encryptedKey;
        }

        public string EncryptAES<T>(T data, string key_AES, string iv_AES)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Convert.FromBase64String(key_AES);
                aes.IV = Convert.FromBase64String(iv_AES);

                var json = JsonSerializer.Serialize<T>(data);
                Console.WriteLine("Serialized data in EncryptAES is:");
                Console.WriteLine(json);
                Console.WriteLine();

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                {
                    using (var sw = new StreamWriter(cs))
                    {
                        sw.Write(json);
                    }

                    var encryptedData = ms.ToArray();
                    return Convert.ToBase64String(encryptedData);
                }
            }
        }

        public string EncryptHashRSA<T>(T data, string privateKey_RSA)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    RSAParameters RSAKey;
                    {
                        var sr = new StringReader(privateKey_RSA);
                        var xs = new XmlSerializer(typeof(RSAParameters));
                        RSAKey = (RSAParameters)xs.Deserialize(sr);
                    }

                    rsa.ImportParameters(RSAKey);

                    var json = JsonSerializer.Serialize<T>(data);
                    Console.WriteLine("Serialized data in EncryptHashRSA is:");
                    Console.WriteLine(json);
                    Console.WriteLine();

                    var byteData = Encoding.UTF8.GetBytes(json);
                    var encryptedHash = rsa.SignData(byteData, new MD5CryptoServiceProvider());

                    var base64Encrypted = Convert.ToBase64String(encryptedHash);
                    Console.WriteLine("Encrypted hash in EncryptHashRSA is:");
                    Console.WriteLine(base64Encrypted);
                    Console.WriteLine();

                    return base64Encrypted;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
