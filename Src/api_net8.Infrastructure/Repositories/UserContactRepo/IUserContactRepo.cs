using api.net.Dto.getCharacter;
using api.net.Models;
using api.net.Models.ServiceResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_net8.Infrastructure.Repositories.UserContactRepo
{
    public interface IUserContactRepo
    {
        public Task<ServiceResponseDto<List<GetUserDto>>> getAllUsersContact();
        public Task<ServiceResponseDto<GetUserDto>> getUsersContactById(int id);
        public Task<ServiceResponseDto<GetUserDto>> AddUserContact(AddUserDto addUserContact);
        public Task<ServiceResponseDto<GetUserDto>> UpdateUserContact(UpdateUserDto UpdateUserContact);
        public Task<ServiceResponseDto<GetUserDto>> DeleteUserContact(int id);
    }
}
