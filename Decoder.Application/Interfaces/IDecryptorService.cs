namespace Decoder.Application.Interfaces
{
    public interface IDecryptorService
    {
        string RSAPublicKey { get; }
        string Decrypt(string encryptedMessage);
    }
}
