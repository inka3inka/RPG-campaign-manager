using Google.Apis.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Server_GM_IMP.Models;
using Server_GM_IMP.Models.Users;
using Server_GM_IMP.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Server_GM_IMP.Services
{
    public class AuthService : IAuthService
    {
        private readonly UsersDbContext _usersDbContext;
        private readonly ServerConfiguration _serverConfiguration;
        private readonly ISecurityFunctions _securityFunctions;

        public AuthService(
            UsersDbContext usersDbContext,
            ISecurityFunctions securityFunctions,
            IOptions<ServerConfiguration> serverConfiguration)
        {
            _usersDbContext = usersDbContext;
            _securityFunctions = securityFunctions;
            _serverConfiguration = serverConfiguration.Value;
        }

        public async Task<User> Authenticate(string email, string userName = "")
        {
            var user = await _usersDbContext.Users.Where(u => u.email == email).FirstOrDefaultAsync();
            if(user != null)
            {
                return user;
            }
            
            var newUser = new User { email = email, name = userName };
            _usersDbContext.Users.Add(newUser);
            await _usersDbContext.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> GetUserFromClaim(ClaimsPrincipal claim)
        {
            var encryptedEmail = claim.Claims.Where(c => c.Type == JwtRegisteredClaimNames.Sub).FirstOrDefault()?.Value;
            var email = _securityFunctions.Decrypt(_serverConfiguration.JwtEmailEncryption, encryptedEmail);

            return await _usersDbContext.Users.Where(u => u.email == email).FirstOrDefaultAsync();
        }
    }
}
