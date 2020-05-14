﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Server_GM_IMP.Models;
using Server_GM_IMP.Services;
using Server_GM_IMP.Utils;

namespace Server_GM_IMP.Controllers
{
    [Route("v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ServerConfiguration _serverConfiguration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IAuthService authService, 
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _authService = authService;
            _serverConfiguration = configuration.Get<ServerConfiguration>();
            _logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("google")]
        public async Task<IActionResult> Google([FromBody]UserView userView)
        {
            try
            {
                _logger.LogDebug($"Authorization by google with user token {userView.tokenId}");
                var payload = GoogleJsonWebSignature.ValidateAsync(userView.tokenId, new GoogleJsonWebSignature.ValidationSettings()).Result;
                var user = await _authService.Authenticate(payload.Email, payload.Name);

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, Security.Encrypt(_serverConfiguration.JwtEmailEncryption,user.email)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_serverConfiguration.JwtSecret));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(String.Empty,
                  String.Empty,
                  claims,
                  expires: DateTime.Now.AddSeconds(55 * 60),
                  signingCredentials: creds);
                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Authorization by google with user token {userView.tokenId} resulted in exception {ex}");
                BadRequest(ex.Message);
            }
            return BadRequest();
        }
    }
}