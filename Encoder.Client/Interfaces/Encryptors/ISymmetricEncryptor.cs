namespace Encryptor.Client.Interfaces.Encryptors
{
    internal interface ISymmetricEncryptor : IEncryptor
    {
        (string encryptedText, string IV) Encrypt(string plainText, string key);
    }
}
