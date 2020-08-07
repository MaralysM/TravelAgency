using Qmos.Entities;
using Qmos.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public class TransitionParametersRepository : RepositoryBase, ITransitionParametersRepository
    {
        string TABLE = $"{SCHEMA}update_time";
        const string TAG = "UpdateTime_DATA";

        public TransitionParametersRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task Add(UpdateTime entity)
        {
            try
            {
                var con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $" INSERT INTO {TABLE}([time_refresh]) VALUES('{entity.time_refresh}');";
                await cmd.ExecuteNonQueryAsync();
                Close(con);
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new UniqueKeyException($"{TAG}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IList<TransitionParametersHeader>> AllAsync()
        {
            List<TransitionParametersHeader> TransitionParameters = new List<TransitionParametersHeader>();
            try
            {
                SqlConnection con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TABLE}";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TransitionParameters.Add(new TransitionParametersHeader
                    {
                        Id = (short)dr["id"],
                        Name = dr["name"].ToString()
                    });
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return TransitionParameters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<UpdateTime> UpdateAsync(UpdateTime entity, params object[] Id)
        {
            try
            {
                var con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"UPDATE {TABLE} SET time_refresh = '{entity.time_refresh}' WHERE id = { entity.Id }";                
                await cmd.ExecuteNonQueryAsync();
                Close(con);
                return entity;
            }
            catch (SqlException ex) when (ex.Number == 2627)
            {
                throw new UniqueKeyException($"{TAG}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<UpdateTime> FindByIdAsync(params object[] values)
        {
            UpdateTime entity = new UpdateTime();
            try
            {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TABLE} WHERE id = {values[0]};";
                var dr = await cmd.ExecuteReaderAsync();
                while (dr.Read())
                {
                    entity.Id = short.Parse(dr["id"].ToString());
                    entity.time_refresh = dr["time_refresh"].ToString();
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Remove(UpdateTime entity)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"DELETE FROM {TABLE} WHERE id = {entity.Id};";
                cmd.ExecuteNonQuery();
                Close(con);
            }
            catch (SqlException ex) when (ex.Number == 1451)
            {
                throw new DeleteWithRelationshipException($"{TAG}: {ex.Message}");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
