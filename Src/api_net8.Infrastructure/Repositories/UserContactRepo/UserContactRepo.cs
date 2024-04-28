using api.net.Dto.getCharacter;
using api.net.Models;
using api.net.Models.ServiceResponse;
using api_net8.Infrastructure.context;
using Azure;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace api_net8.Infrastructure.Repositories.UserContactRepo
{
    public class UserContactRepo : IUserContactRepo
    {
        private readonly DapperContext _db;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserContactRepo(DapperContext db, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _db = db;
        }
        private int GetUserId()
        {
            int response = int.Parse(_httpContextAccessor.HttpContext!.User
           .FindFirst(ClaimTypes.NameIdentifier)!.Value);
            return response;
        }
        public async Task<ServiceResponseDto<GetUserDto>> AddUserContact(AddUserDto addUserContact)
        {
            var serviceResponse = new ServiceResponseDto<GetUserDto>();
            var sql = "INSERT INTO usersContacts (name, family, age, phone, city,email,UserAuthId) VALUES(@name, @family, @age, @phone, @city,@email,@UserAuthId);"
                       + "SELECT CAST(SCOPE_IDENTITY() as int); ";

            var parameters = new DynamicParameters();
            parameters.Add("name", addUserContact.name, DbType.String);
            parameters.Add("family", addUserContact.family, DbType.String);
            parameters.Add("age", addUserContact.age, DbType.Int64);
            parameters.Add("phone", addUserContact.phone, DbType.String);
            parameters.Add("city", addUserContact.city, DbType.String);
            parameters.Add("email", addUserContact.email, DbType.String);
            parameters.Add("UserAuthId", GetUserId(), DbType.Int64);

            var response=new GetUserDto() 
            {
                name = addUserContact.name,
                family = addUserContact.family,
                age = addUserContact.age,
                phone = addUserContact.phone,
                city = addUserContact.city,
                email = addUserContact.email,
            };

            using (var connection = _db.CreateConnection())
            {
                var AddContact = await connection.QuerySingleAsync<GetUserDto>(sql, parameters);                      
                serviceResponse.Data = response;
                serviceResponse.Message = "Contact Successfuly Added";
                return serviceResponse;
            }
            
        }

        public async Task<ServiceResponseDto<GetUserDto>> DeleteUserContact(int id)
        {
            var serviceResponse = new ServiceResponseDto<GetUserDto>();
      
            try
            {
                var sql = "DELETE FROM usersContacts WHERE UserId = @Id AND UserAuthId=@UserId";
                using (var connection = _db.CreateConnection())
                {
                    var characters = getUsersContactById(id);
                    var dbContact = await connection.ExecuteAsync(sql, new { @Id = id, @UserId = GetUserId() });
                    if (dbContact == 0)
                    {
                        throw new Exception($"Character with Id '{id}' not found.");
                    }
                    serviceResponse.Data = characters.Result.Data;
                    serviceResponse.succsess = true;
                    serviceResponse.Message = $"Contact With Id {id} Successfuly Deleted!!!";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.succsess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponseDto<List<GetUserDto>>> getAllUsersContact()
        {           
            var serviceResponse = new ServiceResponseDto<List<GetUserDto>>();
            var sql = "SELECT * FROM usersContacts WHERE UserAuthId=@UserId ";
                using (var connection = _db.CreateConnection())
                {
                    var dbCharacters = await connection.QueryAsync<GetUserDto>(sql, new { @UserId=GetUserId() });
                    serviceResponse.Data = dbCharacters.ToList();
                    return serviceResponse;
                }   
        }

        public async Task<ServiceResponseDto<GetUserDto>> getUsersContactById(int id)
        {

            var serviceResponse = new ServiceResponseDto<GetUserDto>();
            var sql = "SELECT * FROM usersContacts WHERE UserId = @Id AND UserAuthId=@UserId";

            using (var connection = _db.CreateConnection())
            {
                var dbContact = await connection.QuerySingleOrDefaultAsync<GetUserDto>(sql, new { @Id = id, @UserId=GetUserId() });
                if (dbContact is null)
                {
                    throw new Exception($"Character with Id {id} not found.");
                }
                serviceResponse.Data = dbContact;
                return serviceResponse;
            }
            
        }

        public async Task<ServiceResponseDto<GetUserDto>> UpdateUserContact(UpdateUserDto UpdateUserContact)
        {
            var serviceResponse = new ServiceResponseDto<GetUserDto>();
            try
            {
                var sql = "UPDATE usersContacts SET name = @name, family = @family, age = @age, " +
                "phone = @phone, city = @city, email=@email WHERE UserId = @UserId AND UserAuthId = @UserAuthId";
                var parameters = new DynamicParameters();
                parameters.Add("UserId", UpdateUserContact.Id, DbType.Int32);
                parameters.Add("name", UpdateUserContact.name, DbType.String);
                parameters.Add("family", UpdateUserContact.family, DbType.String);
                parameters.Add("age", UpdateUserContact.age, DbType.Int64);
                parameters.Add("phone", UpdateUserContact.phone, DbType.String);
                parameters.Add("city", UpdateUserContact.city, DbType.String);
                parameters.Add("email", UpdateUserContact.email, DbType.String);
                parameters.Add("UserAuthId", GetUserId(), DbType.Int64);

                using (var connection = _db.CreateConnection())
                {
                    var characters = getUsersContactById(UpdateUserContact.Id);
                    var dbContact = await connection.ExecuteAsync(sql, parameters);
                    if (dbContact == 0)
                        throw new Exception($"Character with Id {UpdateUserContact.Id} not found.");

                    serviceResponse.Data = characters.Result.Data;
                    serviceResponse.Message = $"Contact With Id {UpdateUserContact.Id} Successfuly Updated!!";
                }
         
            }
            catch (Exception ex)
            {
                serviceResponse.succsess = false;
                serviceResponse.Message = ex.Message;
            }

            return serviceResponse;
        }

 
    }
}

