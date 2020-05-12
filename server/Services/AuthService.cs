using Google.Apis.Auth;
using Server_GM_IMP.Models;
using Server_GM_IMP.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server_GM_IMP.Services
{
    public class AuthService : IAuthService
    {
        private readonly UsersDbContext _usersDbContext;
        public AuthService(UsersDbContext usersDbContext)
        {
            _usersDbContext = usersDbContext;
        }

        public async Task<User> Authenticate(string email, string userName = "")
        {
            var user = _usersDbContext.Users.Where(u => u.email == email).FirstOrDefault();
            if(user != null)
            {
                return user;
            }

            var newUser = new User { email = email, name = userName };
            _usersDbContext.Users.Add(newUser);
            await _usersDbContext.SaveChangesAsync();
            return newUser;
        }

    }
}
