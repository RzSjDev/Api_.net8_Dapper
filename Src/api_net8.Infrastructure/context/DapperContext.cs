using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using api.net.Data;
using Microsoft.EntityFrameworkCore;

namespace api_net8.Infrastructure.context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;


        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("myApiDb");
           
        }

        public IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

    }
}
