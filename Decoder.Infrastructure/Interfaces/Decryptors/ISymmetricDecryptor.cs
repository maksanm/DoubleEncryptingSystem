namespace Decryptor.Infrastructure.Interfaces
{
    internal interface ISymmetricDecryptor : IDecryptor
    {
        string Decrypt(string encryptedData, string key);
    }
}
