namespace Cryptographer
{
    interface IEncryptor
    {
        string EncryptKey<T>(T data);
        string EncryptHashRSA<T>(T data, string privateKey_RSA);
        string EncryptAES<T>(T data, string key_AES, string iv_AES);

        string PrivateKey_RSA { get; }
        string Key_AES { get; }
        string IV_AES { get; }
    }
}