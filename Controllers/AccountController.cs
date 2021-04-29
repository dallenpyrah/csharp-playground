using System;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using csharp_playground.Models;
using csharp_playground.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace csharp_playground.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ProfilesService _ps;

        public AccountController(ProfilesService ps)
        {
            _ps = ps;
        }

        [HttpGet]
        public async Task<ActionResult<Profile>> Get()
        {
            try
            {
                Profile userInfo = await HttpContext.GetUserInfoAsync<Profile>();
                return Ok(_ps.GetOrCreateProfile(userInfo));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}