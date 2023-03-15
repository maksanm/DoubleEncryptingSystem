using System.Threading.Tasks;

namespace Encryptor.Client.Interfaces
{
    internal interface IApiClient
    {
        /// <summary>
        /// Gets public RSA key used for asymmetric encryption
        /// </summary>
        /// <returns>RSA key string representation</returns>
        Task<string> GetPublicRSAKey();

        /// <summary>
        /// Get decrypted message from database
        /// </summary>
        /// <param name="encryptedMessageInfo">RSA-encrypted message id and AES-key separated by space</param>
        /// <returns>Decrypted message</returns>
        Task<string> GetDecryptedMessage(string encryptedMessageInfo);
    }
}
