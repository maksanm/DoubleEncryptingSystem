using Decoder.Application.Interfaces;
using Decryptor.Infrastructure.Interfaces;
using System;

namespace Decoder.Infrastructure.Services
{
    internal class DecryptorService : IDecryptorService
    {
        private readonly IApplicationDbContext _context;
        private readonly IAsymmetricDecryptor _rsaDecryptor;

        public string RSAPublicKey => _rsaDecryptor.PublicKey;

        public DecryptorService(IApplicationDbContext context, IAsymmetricDecryptor rsaDecryptor)
        {
            _context = context;
            _rsaDecryptor = rsaDecryptor;
        }

        public string Decrypt(string encryptedMessage)
        {
            var messageIdAndAESKey = _rsaDecryptor.Decrypt(encryptedMessage).Split("$");
            var messageId = messageIdAndAESKey[0];
            var aesKey = messageIdAndAESKey[1];
            var message = _context.Message.Find(messageId);
            if (message is null)
                throw new ApplicationException("Message with provided id does not exist");
            return message.EncryptedValue.ToString();
        }
    }
}
