using Microsoft.AspNetCore.Mvc;

namespace Decoder.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RsaController : ControllerBase
    {
        public RsaController()
        {
        }

        [HttpGet("public")]
        public void GetPublicRsaKey()
        {

        }
    }
}
