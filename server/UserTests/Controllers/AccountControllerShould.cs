using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;
using AutoFixture.Xunit2;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server_GM_IMP.Controllers;
using Server_GM_IMP.Models;
using Server_GM_IMP.Models.Users;
using Server_GM_IMP.Services;
using Server_GM_IMP_Tests.Utils;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Server_GM_IMP_Tests.Controllers
{
    public class AccountControllerShould
    {
        private readonly AccountController _sut;
        private readonly IFixture _fixture;
        private readonly Mock<IAuthService> _authServiceMock;

        private readonly List<User> _usersInDatabase = new List<User>();

        public AccountControllerShould()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization { ConfigureMembers = true });

            _authServiceMock = _fixture.Freeze<Mock<IAuthService>>();

            var usersDbSetMock = TestFunctions.GetDbSet(_usersInDatabase.AsQueryable());
            var usersDbContextMock = new Mock<UsersDbContext>();
            usersDbContextMock.Setup(x => x.Users).Returns(usersDbSetMock.Object);

            _fixture.Inject(usersDbContextMock.Object);

            _fixture.Customize<ControllerContext>(ob => ob
                .OmitAutoProperties());

            _sut = _fixture.Build<AccountController>()
                .OmitAutoProperties()
                .With(c => c.ControllerContext)
                .Create();
        }

        [Theory, AutoData]
        public async Task UpdateTheUserOnPutRequestIfEmailIsSame(User testUser, User modifiedUser)
        {
            _authServiceMock.Setup(x => x.GetUserFromClaim(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(modifiedUser));
            _usersInDatabase.Add(testUser);

            modifiedUser.email = testUser.email;
            modifiedUser.Id = testUser.Id; //Needed for test of db context

            await _sut.PutCurrent(modifiedUser);

            //No new user created
            _usersInDatabase.Count.Should().Be(1);
            //The only user is modified version
            _usersInDatabase.Should().ContainEquivalentOf(modifiedUser);
        }

        [Theory, AutoData]
        public async Task ReturnStatusOKWhenUserWasUpdate(User testUser, User modifiedUser)
        {
            _authServiceMock.Setup(x => x.GetUserFromClaim(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(modifiedUser));
            _usersInDatabase.Add(testUser);

            modifiedUser.email = testUser.email;

            var response = await _sut.PutCurrent(modifiedUser);
            
            (response as ObjectResult).StatusCode.Should().Be((int)HttpStatusCode.OK);
        }

        [Theory]
        [InlineAutoData(0)]
        [InlineAutoData(1)]
        [InlineAutoData(3)]
        public async Task ReturnConflictStatusWhenPuttedUserIsNotPresent(int databaseCount, User modifiedUser)
        {
            _authServiceMock.Setup(x => x.GetUserFromClaim(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(modifiedUser));
            //Create some random user in the database
            for (int i = 0; i < databaseCount; i++)
            {
                _usersInDatabase.Add(_fixture.Create<User>());
            }

            var response = await _sut.PutCurrent(modifiedUser);

            (response as StatusCodeResult).StatusCode.Should().Be((int)HttpStatusCode.Conflict);
        }

        [Theory]
        [InlineAutoData(0)]
        [InlineAutoData(1)]
        [InlineAutoData(3)]
        public async Task NotUpdateDatabaseIfUserIsNotPresent(int databaseCount, User modifiedUser)
        {
            _authServiceMock.Setup(x => x.GetUserFromClaim(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(modifiedUser));
            //Create some random user in the database
            for (int i = 0; i < databaseCount; i++)
            {
                _usersInDatabase.Add(_fixture.Create<User>());
            }

            var databaseBeforePut = new List<User>(_usersInDatabase);

            await _sut.PutCurrent(modifiedUser);

            _usersInDatabase.Should().BeEquivalentTo(databaseBeforePut);
        }

        [Theory, AutoData]
        public async Task NotUpdateUserIfPuttedUserIsDifferentFromAuthorizedOne(User testUser, User authorizedUser)
        {
            _authServiceMock.Setup(x => x.GetUserFromClaim(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(authorizedUser));
            _usersInDatabase.Add(testUser);
            _usersInDatabase.Add(authorizedUser);
            var databaseBeforePut = new List<User>(_usersInDatabase);

            var modifiedUser = _fixture.Create<User>();
            modifiedUser.email = testUser.email;

            var response = await _sut.PutCurrent(modifiedUser);
            
            (response as StatusCodeResult).StatusCode.Should().Be((int)HttpStatusCode.BadRequest);
            _usersInDatabase.Should().BeEquivalentTo(databaseBeforePut);
        }
    }
}
