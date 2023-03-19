using Decryptor.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Decoder.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IDecryptorService _decryptorService;

        public MessageController(IDecryptorService decryptorService)
        {
            _decryptorService = decryptorService;
        }

        [HttpGet("{key}")]
        public ActionResult<string> DecryptMessage([FromRoute] string key)
        {
            try
            {
                key = Uri.UnescapeDataString(key);
                var message = _decryptorService.Decrypt(key);
                return StatusCode(418, message);
            }
            catch(ApplicationException ex)
            {
                return BadRequest(ex.Message);
            }  
        }
    }
}
