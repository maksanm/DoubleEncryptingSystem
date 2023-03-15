namespace Encryptor.Client.Interfaces.Encryptors
{
    internal interface IAsymmetricEncryptor : IEncryptor
    {
        string Encrypt(string plainText, string publicKey);
    }
}
