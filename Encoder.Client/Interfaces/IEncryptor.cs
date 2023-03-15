namespace Encryptor.Client.Interfaces
{
    internal interface IEncryptor
    {
        string Encrypt(string plainText, string key);
    }
}
