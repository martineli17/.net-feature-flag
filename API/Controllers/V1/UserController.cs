using API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.V1
{
    [ApiController]
    [Route("user/v1")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [FeatureFlagActiveRedirect("UserV2", "/user/v2")]
        public ActionResult Get() => Ok("User V1");
    }
}
