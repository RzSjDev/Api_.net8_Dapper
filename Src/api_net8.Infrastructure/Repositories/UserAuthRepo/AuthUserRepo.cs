using api.net.Dto.AuthDto;
using api.net.Dto.getCharacter;
using api.net.Models;
using api.net.Models.ServiceResponse;
using api_net8.Infrastructure.context;
using Dapper;
using dotnet_rpg.Dtos.User;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace api_net8.Infrastructure.Repositories.UserAuthRepo
{
    public class AuthUserRepo : IAuthUserRepo
    {
        private readonly DapperContext _db;
        private readonly IConfiguration _configuration;

        public AuthUserRepo(DapperContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }
        public async Task<bool> IsExist(string userName, string email)
        {
            var sql = "SELECT * FROM userAuths WHERE UserName = @UserName OR Email = @UserEmail";

            using (var connection = _db.CreateConnection())
            {
                var user = await connection.QueryFirstOrDefaultAsync<UserAuth>(sql, new { @UserName = userName, @UserEmail = email });
                if (user is not null)
                {
                    return true;
                }
                return false;
            }

        }

        public async Task<ServiceResponseDto<string>> Login(UserLoginDto userLogin)
        {

            var response = new ServiceResponseDto<string>();
            var sql = "SELECT * FROM userAuths WHERE UserName = @User OR Email=@User";
            using (var connection = _db.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<UserAuth>(sql, new { @User = userLogin.UserNameOrEmail } );
                if (user is null)
                {
                    response.succsess = false;
                    response.Message = "User not found.";
                }
                else if (!VerifyPasswordHash(userLogin.Password, user.Passwordhash, user.PasswordSalt))
                {
                    response.succsess = false;
                    response.Message = "Wrong password.";
                }
                else
                {
                    response.Message = "You Successfuly Login";
                    response.Data = CreateToken(user);
                }

                return response;
            }
            
        }

        public async Task<ServiceResponseDto<int>> Rigester(UserRegestrationDto userAuth, string Password)
        {
            var response = new ServiceResponseDto<int>();
            if (await IsExist(userAuth.UserName, userAuth.UserEmail))
            {
                response.succsess = false;
                response.Message = "User already exist";
                return response;
            }
            CreatePasswordHash(Password, out byte[] passwordHash, out byte[] passwordSalt);
            var parameters = new DynamicParameters();
            parameters.Add("UserName", userAuth.UserName, DbType.String);
            parameters.Add("Email", userAuth.UserEmail, DbType.String);
            parameters.Add("role", 4, DbType.String);
            parameters.Add("PasswordSalt", passwordSalt);
            parameters.Add("Passwordhash", passwordHash);
            

            var sql = "INSERT INTO userAuths (UserName, Email, role, PasswordSalt,Passwordhash) VALUES(@UserName, @Email, @role, @PasswordSalt,@Passwordhash);"
                           + "SELECT CAST(SCOPE_IDENTITY() as int); ";

            using (var connection = _db.CreateConnection())
            {  
                var userAuthId = await connection.QuerySingleAsync<int>(sql, parameters);
                response.Message = "You Successfuly Rigester";
                response.Data = userAuthId;
                return response;
            }
           

        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private string CreateToken(UserAuth user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserAuthId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var appSettingsToken = _configuration.GetSection("AppSettings:Token").Value;
            if (appSettingsToken is null)
                throw new Exception("AppSettings Token is null!");

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(appSettingsToken));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
