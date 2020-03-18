namespace Cryptographer
{
    interface IEncryptor
    {
        string EncryptKey<T>(T data);
        string EncryptHashRSA<T>(T data, string privateKey);
        string EncryptAES<T>(T data, string key, string IV);

        string PrivateKey_RSA { get; }
        string Key_AES { get; }
        string IV_AES { get; }
    }
}