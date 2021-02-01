using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TravelAgency.Repositories.Abstractions
{
    public abstract class RepositoryBase
    {
        public const string SCHEMA = "[TravelAgency].";
        public string ConnectionString { get; }
        
        public RepositoryBase(IConfiguration configuration)
        {
             ConnectionString = configuration.GetSection("ConnectionStrings:DefaultConnection").Value;
        }

        public SqlConnection Open() 
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                
                conn.Open();
                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<SqlConnection> OpenAsync()
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConnectionString);
                await conn.OpenAsync();
                return conn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Close(SqlConnection conn) => conn.Close();
    }
}
