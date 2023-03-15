using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;

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
            key = HttpUtility.UrlDecode(key);
            return StatusCode(418, key);
        }
    }
}
