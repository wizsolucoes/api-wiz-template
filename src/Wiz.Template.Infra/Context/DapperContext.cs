using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace Wiz.Template.Infra.Context
{
    public class DapperContext
    {
        private readonly DbConnection _conn;

        public DapperContext(DbConnection conn)
        {
            _conn = conn;
        }

        public DbConnection DapperConnection
        {
            get
            {
                return _conn;
            }
        }
    }
}
