using Qmos.Entities;
using Qmos.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public class LoggerActionsRepository : RepositoryBase, ILogger_actionsRepository
    {
        string TABLE = $"{SCHEMA}logger_actions";

        public LoggerActionsRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public void Add(LoggerActions dataObject)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO {TABLE} (date,message,Time,Type_action,User_ID)" +
                    $" VALUES('{dataObject.Date.ToString("yyyyMMdd")}', '{dataObject.Message}','{dataObject.Time.ToString("yyyyMMdd HH:mm:ss")}','{dataObject.TypeAction.ToString()}','{dataObject.UserId}')";
                cmd.ExecuteNonQuery();
                Close(con);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<LoggerActions>> AllAsync()
        {
            List<LoggerActions> loggerErrors = new List<LoggerActions>();
            try
            {
                SqlConnection con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TABLE}";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    loggerErrors.Add(new LoggerActions
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
