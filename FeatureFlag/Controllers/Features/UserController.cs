using _Flagsmith.Models;
using _Flagsmith.Repositories;
using API.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace FeatureFlag.Controllers.Features
{
    [ApiController]
    [Route("user/feature")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IMemoryCache _cache;

        public UserController(ILogger<UserController> logger, IMemoryCache cache)
        {
            _logger = logger;
            _cache = cache;
        }

        [HttpPost]
        public ActionResult Add([FromBody] User user)
        {
            if (user.Cpf.Length != 11)
            {
                UserRepository.Add(user);
                _cache.Set("users", UserRepository.Get());
                return Created("", user);
            }
            _logger.LogInformation("User cpf is invalid: {0}", user.Cpf);
            return BadRequest();
        }

        [HttpGet("get")]
        [FeatureFlagInativeRedirect("users_cache", "/user/get")]
        public ActionResult<IEnumerable<User>> Get()
        {
            var response = new
            {
                users = _cache.Get<IEnumerable<User>>("users") ?? UserRepository.Get(),
                from = "cache"
            };

            return Ok(response);
        }
    }
}
