using Microsoft.AspNetCore.Mvc;

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
        public void DecryptMessage([FromRoute] string key)
        {

        }
    }
}
