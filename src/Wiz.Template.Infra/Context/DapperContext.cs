using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Wiz.Template.Infra.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection DapperConnection
        {
            get
            {
                return new SqlConnection(_configuration["ConnectionStrings:CustomerDB"]);
            }
        }
    }
}
