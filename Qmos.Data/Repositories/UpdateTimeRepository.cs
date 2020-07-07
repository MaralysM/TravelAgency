using Qmos.Entities;
using Qmos.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public class UpdateTimeRepository : RepositoryBase, IUpdateTimeRepository
    {
        string TABLE = $"{SCHEMA}update_time";

        public UpdateTimeRepository(IConfiguration configuration) : base(configuration)
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

        public async Task<IList<UpdateTime>> AllAsync()
        {
            List<UpdateTime> updateTime = new List<UpdateTime>();
            try
            {
                SqlConnection con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TABLE}";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    updateTime.Add(new UpdateTime
                    {
                        Id = (short)dr["id"],
                        time_refresh = dr["time_refresh"].ToString()
                    });
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return updateTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
