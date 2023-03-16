using Decoder.Application.Interfaces;
using Decryptor.Infrastructure.Interfaces;
using System;

namespace Decoder.Infrastructure.Services
{
    internal class DecryptorService : IDecryptorService
    {
        private readonly IApplicationDbContext _context;
        private readonly IAsymmetricDecryptor _rsaDecryptor;
        private readonly ISymmetricDecryptor _aesDecryptor;

        public string RSAPublicKey => _rsaDecryptor.PublicKey;

        public DecryptorService(IApplicationDbContext context, IAsymmetricDecryptor rsaDecryptor, ISymmetricDecryptor aesDecryptor)
        {
            _context = context;
            _rsaDecryptor = rsaDecryptor;
            _aesDecryptor = aesDecryptor;
        }

        public string Decrypt(string encryptedMessage)
        {
            var messageIdAndAESKeys = _rsaDecryptor.Decrypt(encryptedMessage).Split("$");
            var messageId = messageIdAndAESKeys[0];
            var aesKey = messageIdAndAESKeys[1];
            var aesIV = messageIdAndAESKeys[2];
            var message = _context.Message.Find(messageId);
            if (message is null)
                throw new ApplicationException("Message with provided id does not exist");
            var decryptedMessage = _aesDecryptor.Decrypt(message.EncryptedValue, aesKey, aesIV);
            return decryptedMessage;
        }
    }
}
