using Microsoft.AspNetCore.Mvc;
using System;

namespace Decoder.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        public MessageController()
        {
        }

        [HttpGet("{key}")]
        public ActionResult<string> DecryptMessage([FromRoute] string key)
        {
            key = Uri.UnescapeDataString(key);
            return StatusCode(418, key);
        }
    }
}
