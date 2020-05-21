using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Server_GM_IMP.Models;
using Server_GM_IMP.Models.Users;
using Server_GM_IMP.Services;
using Server_GM_IMP.Utils;

namespace Server_GM_IMP.Controllers
{
    [Route("v1/[controller]")]
    [Authorize]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UsersDbContext _dbContext;
        private readonly ServerConfiguration _serverConfiguration;
        private readonly IAuthService _authService;

        public AccountController(
            UsersDbContext usersDbContext,
            IOptions<ServerConfiguration> serverConfiguration,
            IAuthService authService)
        {
            _dbContext = usersDbContext;
            _serverConfiguration = serverConfiguration.Value;
            _authService = authService;
        }

        [HttpGet("Current")]
        public async Task<IActionResult> GetCurrent()
        {
            var user = await _authService.GetUserFromClaim(User);
            return Ok(user);
        }

        [HttpPut("Current")]
        public async Task<IActionResult> PutCurrent(User user)
        {
            throw new NotImplementedException();
        }
    }
}