namespace Decryptor.Core.Services.Interfaces.Decryptors
{
    public interface ISymmetricDecryptor : IDecryptor
    {
        string Decrypt(string encryptedText, string password, string IV);
    }
}
