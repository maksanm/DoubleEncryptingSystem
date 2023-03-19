using Decryptor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Decoder.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RsaController : ControllerBase
    {
        private readonly IDecryptorService _decryptorService;

        public RsaController(IDecryptorService decryptorService)
        {
            _decryptorService = decryptorService;
        }

        [HttpGet("public")]
        public ActionResult<string> GetRSAPublicKey()
        {
            return Ok(_decryptorService.AsymmetricDecryptorPublicKey);
        }
    }
}
