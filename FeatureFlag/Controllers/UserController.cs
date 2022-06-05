using _Flagsmith.Models;
using _Flagsmith.Repositories;
using FeatureFlag.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FeatureFlag.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        public UserController()
        {
        }

        [HttpPost]
        public ActionResult Add([FromBody] User user)
        {
            UserRepository.Add(user);
            return Created("", user);
        }

        [HttpGet("get")]
        [FeatureFlagActiveRedirect("users_cache", "/user/feature/get")]
        public ActionResult<IEnumerable<User>> Get()
        {
            var response = new
            {
                users = UserRepository.Get(),
                from = "database"
            };

            return Ok(response);
        }
    }
}
