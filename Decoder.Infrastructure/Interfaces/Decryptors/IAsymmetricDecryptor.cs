namespace Decryptor.Infrastructure.Interfaces
{
    interface IAsymmetricDecryptor
    {
        string PublicKey { get; }
        string Decrypt(string encryptedData);
    }
}
