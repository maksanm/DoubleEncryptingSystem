namespace Decryptor.Infrastructure.Interfaces
{
    internal interface ISymmetricDecryptor : IDecryptor
    {
        string Decrypt(string encryptedText, string key, string IV);
    }
}
