namespace Decryptor.Core.Services.Interfaces.Decryptors
{
    public interface IAsymmetricDecryptor
    {
        string PublicKey { get; }
        string Decrypt(string encryptedData);
    }
}
