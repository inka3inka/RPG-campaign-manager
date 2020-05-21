using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Server_GM_IMP.Models;
using Server_GM_IMP.Models.Users;
using Server_GM_IMP.Services;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using System.Threading.Tasks;
using System.Security.Claims;
using Server_GM_IMP.Utils;
using System.IdentityModel.Tokens.Jwt;

namespace Server_GM_IMP_Tests.Services
{
    public class AuthServiceShould
    {
        private readonly AuthService _sut;
        private readonly IFixture _fixture;
        private readonly Mock<DbSet<User>> _usersDbSetMock;
        private readonly Mock<ISecurityFunctions> _securityFunctionsMock;
        private readonly ServerConfiguration _serverConfigurationMock;

        private readonly List<User> _usersInDatabase = new List<User>();

        public AuthServiceShould()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });
            _serverConfigurationMock = _fixture.Freeze<ServerConfiguration>();
            var configurationOptions = Options.Create(_serverConfigurationMock) ;
            _fixture.Inject(configurationOptions);
            _securityFunctionsMock = _fixture.Freeze<Mock<ISecurityFunctions>>();

            var queryableUsers = _usersInDatabase.AsQueryable();

            _usersDbSetMock = new Mock<DbSet<User>>();
            _usersDbSetMock.As<IQueryable<User>>().Setup(m => m.Provider).Returns(queryableUsers.Provider);
            _usersDbSetMock.As<IQueryable<User>>().Setup(m => m.Expression).Returns(queryableUsers.Expression);
            _usersDbSetMock.As<IQueryable<User>>().Setup(m => m.ElementType).Returns(queryableUsers.ElementType);
            _usersDbSetMock.As<IQueryable<User>>().Setup(m => m.GetEnumerator()).Returns(queryableUsers.GetEnumerator());

            var usersDbContextMock = new Mock<UsersDbContext>();
            usersDbContextMock.Setup(x => x.Users).Returns(_usersDbSetMock.Object);

            _fixture.Inject(usersDbContextMock.Object);
            _sut = _fixture.Create<AuthService>();
        }
/*
        [Theory, AutoData]
        public async Task CreateANewUserIfUserWithGivenEMailIsNotPresent(string email)
        {
            var user = await _sut.Authenticate(email);

            _usersDbSetMock.Verify(x => x.Add(It.Is<User>(u => u.email == email)), Times.Once());
            user.email.Should().Be(email);
        }

        [Theory, AutoData]
        public async Task GiveTheNewUserTheDefaultName(string email, string defaultName)
        {
            var user = await _sut.Authenticate(email, defaultName);

            _usersDbSetMock.Verify(x => x.Add(It.Is<User>(u => u.email == email && u.name == defaultName)), Times.Once());
            user.email.Should().Be(email);
            user.name.Should().Be(defaultName);
        }

        [Theory, AutoData]
        public async Task ReturnExistingUserIfItIsFound(string email)
        {
            _usersInDatabase.Add(new User { email = email });
            
            var user = await _sut.Authenticate(email);

            _usersDbSetMock.Verify(x => x.Add(It.IsAny<User>()), Times.Never());
            user.email.Should().Be(email);
        }

        [Theory, AutoData]
        public async Task ReturnUserFromUserIdentity(string email, string encryptedEmail)
        {
            //Populate database
            var testUser = _fixture.Create<User>();
            testUser.email = email;
            _usersInDatabase.Add(testUser);
            //Prepare user
            var encodedEmail = _securityFunctionsMock.Setup(x => x.Decrypt(_serverConfigurationMock.JwtSecret, email, true)).Returns(email);
            var userIdentity = new Mock<ClaimsPrincipal>();
            var mockClaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, encryptedEmail)
            };
            userIdentity.Setup(u => u.Claims).Returns(mockClaims);

            //Test
            var returnedUsed = await _sut.GetUserFromClaim(userIdentity.Object);

            returnedUsed.Should().Be(testUser);
        }
        */
    }
}
