using API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V2
{
    [ApiController]
    [Route("user/v2")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [FeatureFlagInativeRedirect("UserV2", "/user/v1")]
        public ActionResult Get() => Ok("User V2");
    }
}
