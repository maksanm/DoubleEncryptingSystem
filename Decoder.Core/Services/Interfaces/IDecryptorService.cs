namespace Decryptor.Core.Services.Interfaces
{
    public interface IDecryptorService
    {
        string AsymmetricDecryptorPublicKey { get; }
        string Decrypt(string encryptedMessage);
    }
}
