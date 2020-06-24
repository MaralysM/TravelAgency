using Qmos.Entities;
using Qmos.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public class LoggerErrorRepository : RepositoryBase, ILoggerErrorRepository
    {
        string TABLE = $"{SCHEMA}logger_errors";

        public LoggerErrorRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public void Add(LoggerError dataObject)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO {TABLE} (date,message) VALUES('{dataObject.Date.ToString("yyyyMMdd HH:mm:ss")}', '{dataObject.Message}')";
                cmd.ExecuteNonQuery();
                Close(con);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<LoggerError>> AllAsync()
        {
            List<LoggerError> loggerErrors = new List<LoggerError>();
            try
            {
                SqlConnection con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TABLE}";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    loggerErrors.Add(new LoggerError
                    {
                        Id = (int)dr["id"],
                        Date = DateTime.Parse(dr["date"].ToString()),
                        Message = dr["message"].ToString()
                    });
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return loggerErrors;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
