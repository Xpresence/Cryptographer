namespace Cryptographer
{
    interface IDecryptor
    {
        T DecryptAES<T>(string encryptedKey, string key_AES, string iv_AES);
        bool VerifyHashRSA<T>(T data, string hash, string publicKey_RSA);

        string PublicKey_RSA { get; }
        string Key_AES { get; }
        string IV_AES { get; }
    }
}
