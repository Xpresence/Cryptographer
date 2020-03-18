using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

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



        public T DecryptAES<T>(string encryptedKey, string key_AES, string iv_AES)
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Key = Convert.FromBase64String(key_AES);
                aes.IV = Convert.FromBase64String(iv_AES);

                var encryptedBytes = Convert.FromBase64String(encryptedKey);

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new MemoryStream(encryptedBytes))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var sr = new StreamReader(cs))
                {
                    var decryptedData = sr.ReadToEnd();
                    return JsonSerializer.Deserialize<T>(decryptedData);
                }
            }
        }

        public bool VerifyHashRSA<T>(T data, string hash, string publicKey_RSA)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                try
                {
                    RSAParameters RSAKey;
                    {
                        var sr = new StringReader(publicKey_RSA);
                        var xs = new XmlSerializer(typeof(RSAParameters));
                        RSAKey = (RSAParameters)xs.Deserialize(sr);
                    }

                    rsa.ImportParameters(RSAKey);

                    var json = JsonSerializer.Serialize<T>(data);
                    Console.WriteLine("Serialized data in VerifyHashRSA is:");
                    Console.WriteLine(json);
                    Console.WriteLine();

                    var byteData = Encoding.UTF8.GetBytes(json);
                    var byteCryptedHash = Convert.FromBase64String(hash);

                    var verifyHash = rsa.VerifyData(byteData, new MD5CryptoServiceProvider(), byteCryptedHash);

                    return verifyHash;
                }
                catch (FormatException)
                {
                    return false;
                }
                finally
                {
                    rsa.PersistKeyInCsp = false;
                }
            }
        }
    }
}
