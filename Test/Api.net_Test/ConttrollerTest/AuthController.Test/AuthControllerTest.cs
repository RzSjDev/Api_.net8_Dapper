using api.net.Controllers.Auth;
using api.net.Dto.AuthDto;
using api.net.Models.ServiceResponse;
using api_net8.Infrastructure.Repositories.UserAuthRepo;
using dotnet_rpg.Dtos.User;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace AuthController_Test
{
    [TestFixture]
    public class AuthControllerTest
    {
        private Mock<IAuthUserRepo> _AuthUser;
        private AuthUserController _controller;
        private UserLoginDto _authUser_one;
        private UserRegestrationDto _authUser_two;
        public AuthControllerTest()
        {

            _authUser_one = new UserLoginDto()
            {
                UserNameOrEmail = "Test",
                Password = "Test"
            };
            _authUser_two = new UserRegestrationDto()
            {
              UserName = "Test",
              Password = "Test",
              UserEmail = "Test",
              ConfirmPassword= "Test"             
            };
        }
        [SetUp]
        public void setup()
        {
            _AuthUser = new Mock<IAuthUserRepo>();
            _controller = new AuthUserController(_AuthUser.Object);
        }

        [Test]
        public async Task LoginUser_UserOne_CheckTheStatusCodeIsOk()
        {
            //arrange
            ServiceResponseDto<string> responseDto = new ServiceResponseDto<string>();
            responseDto.Data = _authUser_one.UserNameOrEmail;
            _AuthUser.Setup(s => s.Login(It.IsAny<UserLoginDto>())).ReturnsAsync(responseDto);
            //act
            var result = await _controller.Login(_authUser_one);
            //assert

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Result, Is.Not.Null);

        }
        [Test]
        public async Task RegisterUser_UserOne_CheckTheStatusCodeIsOk()
        {
            //arrange
            ServiceResponseDto<int> responseDto = new ServiceResponseDto<int>();
            responseDto.Data =1;
            _AuthUser.Setup(s => s.Rigester(It.IsAny<UserRegestrationDto>(),"Test")).ReturnsAsync(responseDto);
            //act
            var result = await _controller.Rigester(_authUser_two);
            //assert

            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Result, Is.Not.Null);

        }

    }
}
