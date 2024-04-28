using api.net.Models.ServiceResponse;
using api.net.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotnet_rpg.Dtos.User;
using api.net.Dto.AuthDto;

namespace api_net8.Infrastructure.Repositories.UserAuthRepo
{
    public interface IAuthUserRepo
    {
        Task<ServiceResponseDto<int>> Rigester(UserRegestrationDto userAuth, string Password);
        Task<ServiceResponseDto<string>> Login(UserLoginDto userLogin);
        Task<bool> IsExist(string user, string email);
    }
}
