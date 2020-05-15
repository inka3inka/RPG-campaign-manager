using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Server_GM_IMP.Models;
using Server_GM_IMP.Models.Users;
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

        public AccountController(
            UsersDbContext usersDbContext,
            IOptions<ServerConfiguration> serverConfiguration)
        {
            _dbContext = usersDbContext;
            _serverConfiguration = serverConfiguration.Value;
        }

        [HttpGet("Current")]
        public async Task<IActionResult> Current()
        {
            await Task.Delay(1);
            var encryptedEmail = User.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
            var email = Security.Decrypt(_serverConfiguration.JwtEmailEncryption, encryptedEmail);

            var user = _dbContext.Users.Where(u => u.email == email).FirstOrDefault();

            return Ok(user);
        }
    }
}