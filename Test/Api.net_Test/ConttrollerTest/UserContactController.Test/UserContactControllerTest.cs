using api.net.Dto.getCharacter;
using NUnit.Framework;
using Moq;
using api.net.Controllers.UserContact;
using Microsoft.AspNetCore.Mvc;
using api.net.Models.ServiceResponse;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using api_net8.Infrastructure.Repositories.UserContactRepo;

namespace UserContactController_Test
{
    [TestFixture]
    public class UserContactControllerTest
    {
        private Mock<IUserContactRepo> _userRepo;
        private UserContactController _controller;
        private AddUserDto _usersContact_one;
        private GetUserDto _usersContact_three;
        private UpdateUserDto _usersContact_two;
        public UserContactControllerTest()
        {
            _usersContact_one = new AddUserDto()
            {
                name = "ali",
                family = "karimi",
                age = 30,
                city = "tehran",
                phone = "9121112233",
                email = "ali@gmail.com"
            };
            _usersContact_two = new UpdateUserDto()
            {
                Id = 3, 
                name = "abbas",
                family = "mohammadi",
                age = 30,
                city = "tehran",
                phone = "9124412233",
                email = "abbas@gmail.com",
            };
            _usersContact_three = new GetUserDto()
            {
                name = "ali",
                family = "karimi",
                age = 30,
                city = "tehran",
                phone = "9121112233",
                email = "ali@gmail.com"
            };
        }
        [SetUp]
        public void setup()
        {
            _userRepo = new Mock<IUserContactRepo>();  
            _controller= new UserContactController(_userRepo.Object);
        }

        [Test]
        public async Task Addcharacters_ContactOne_CheckTheStatusCodeIsOk()
        {
            //arrange
            ServiceResponseDto<GetUserDto> responseDto = new ServiceResponseDto<GetUserDto>();
            responseDto.Data =_usersContact_three;
            _userRepo.Setup(s => s.AddUserContact(It.IsAny<AddUserDto>())).ReturnsAsync(responseDto);
            //act
            var result =await _controller.AddContact(_usersContact_one);
            //assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result, Is.Not.Null);
        }
        [Test]
        public async Task Getcharacters_ContactOne_CheckTheStatusCodeIsOk()
        {
            //arrange
            ServiceResponseDto<List<GetUserDto>> responseDto = new ServiceResponseDto<List<GetUserDto>>();
            responseDto.Data = new List<GetUserDto>() { _usersContact_three };
            _userRepo.Setup(s => s.getAllUsersContact()).ReturnsAsync(responseDto);
            //act
            var result = await _controller.Get();
            //assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Result, Is.Not.Null);

        }
        [Test]
        public async Task GetcharactersWithId_ContactOne_CheckTheStatusCodeIsOk()
        {
            //arrange
            ServiceResponseDto<GetUserDto> responseDto = new ServiceResponseDto<GetUserDto>();
            responseDto.Data = _usersContact_three;
            _userRepo.Setup(s => s.getUsersContactById(It.IsAny<int>())).ReturnsAsync(responseDto);
            //act
            var result = await _controller.GetSingle(1);
            //assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Result, Is.Not.Null);

        }
        [Test]
        public async Task UpdateContactWithId_ContactOne_CheckTheStatusCodeIsOk()
        {
            //arrange
            ServiceResponseDto<GetUserDto> responseDto = new ServiceResponseDto<GetUserDto>();
            responseDto.Data = _usersContact_three;
            _userRepo.Setup(s => s.UpdateUserContact(It.IsAny<UpdateUserDto>())).ReturnsAsync(responseDto);
            //act
            var result = await _controller.UpdateContact(_usersContact_two);
            //assert 
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Result, Is.Not.Null);
        }
        [Test]
        public async Task DeleteContactWithId_ContactOne_CheckTheStatusCodeIsOk()
        {
            //arrange
            ServiceResponseDto<GetUserDto> responseDto = new ServiceResponseDto<GetUserDto>();
            responseDto.Data = _usersContact_three;
            _userRepo.Setup(s => s.DeleteUserContact(It.IsAny<int>())).ReturnsAsync(responseDto);
            //act
            var result = await _controller.DeleteContact(1);
            //assert
            Assert.That(result.Result, Is.TypeOf<OkObjectResult>());
            Assert.That(result.Result, Is.Not.Null);
        }
    }
}



