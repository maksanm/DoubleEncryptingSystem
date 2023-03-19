namespace Decryptor.Core.Services.Interfaces
{
    public interface IDecryptorService
    {
        string RSAPublicKey { get; }
        string Decrypt(string encryptedMessage);
    }
}
