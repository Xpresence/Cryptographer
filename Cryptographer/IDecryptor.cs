namespace Cryptographer
{
    interface IDecryptor
    {
        //T DecryptKey<T>(string key);
        T DecryptAES<T>(string key, string key_AES, string IV_AES);
        bool VerifyHashRSA(string hash, string publicKey);

        string PublicKey_RSA { get; }
        string Key_AES { get; }
        string IV_AES { get; }
    }
}
