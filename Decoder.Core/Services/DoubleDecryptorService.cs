using Decryptor.Core.Services.Interfaces;
using Decryptor.Core.Services.Interfaces.Decryptors;
using System;

namespace Decryptor.Core.Services
{
    public class DoubleDecryptorService : IDecryptorService
    {
        private readonly IApplicationDbContext _context;
        private readonly IAsymmetricDecryptor _asymmetricDecryptor;
        private readonly ISymmetricDecryptor _symmetricDecryptor;

        public string AsymmetricDecryptorPublicKey => _asymmetricDecryptor.PublicKey;

        public DoubleDecryptorService(IApplicationDbContext context, IAsymmetricDecryptor asymmetricDecryptor, ISymmetricDecryptor symmetricDecryptor)
        {
            _context = context;
            _asymmetricDecryptor = asymmetricDecryptor;
            _symmetricDecryptor = symmetricDecryptor;
        }

        public string Decrypt(string encryptedMessage)
        {
            var messageIdWithPasswordAndIV= _asymmetricDecryptor.Decrypt(encryptedMessage).Split("$");
            var messageId = messageIdWithPasswordAndIV[0];
            var symmetricPassword = messageIdWithPasswordAndIV[1];
            var symmetricIV = messageIdWithPasswordAndIV[2];
            var message = _context.Message.Find(messageId);
            if (message is null)
                throw new ApplicationException("Message with provided id does not exist");
            var decryptedMessage = _symmetricDecryptor.Decrypt(message.EncryptedValue, symmetricPassword, symmetricIV);
            return decryptedMessage;
        }
    }
}
