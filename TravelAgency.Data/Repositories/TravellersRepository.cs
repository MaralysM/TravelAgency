using TravelAgency.Entities;
using TravelAgency.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TravelAgency.Data
{
    public class TravellersRepository : RepositoryBase, ITravellersRepository
    {
        string TABLE = $"{SCHEMA}Travellers";
        const string TAG = "Travellers_DATA";

        public TravellersRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IList<Travellers>> AllAsync()
        {
            List<Travellers> travellers = new List<Travellers>();
            try
            {
                SqlConnection con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TABLE}";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    travellers.Add(GetEntityFromDataReader(dr));
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return travellers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Add(Travellers entity)
        {
            try
            {
                var con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO {TABLE} (TX_FirstName, TX_SecondName, TX_LastName, TX_SecondLastName, TX_Phone, TX_IdentificationCard,TX_Address) " +
                    $"VALUES ('{entity.TX_FirstName}','{entity.TX_SecondName}', '{entity.TX_LastName}', '{entity.TX_SecondLastName}', '{entity.TX_Phone}', '{entity.TX_IdentificationCard}', '{entity.TX_Address}');";
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



        public async Task<Travellers> UpdateAsync(Travellers entity, params object[] Id)
        {
            try
            {
                var con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"UPDATE {TABLE} SET TX_FirstName = '{entity.TX_FirstName}', TX_SecondName = '{entity.TX_SecondName}', " +
                    $"TX_LastName = '{entity.TX_LastName}',  TX_SecondLastName = '{entity.TX_SecondLastName}', TX_Phone = '{entity.TX_Phone}', " +
                    $"TX_IdentificationCard = '{entity.TX_IdentificationCard}', TX_Address = '{entity.TX_Address}' " +
                    $"WHERE ID_Travellers = { entity.ID_Travellers }";                
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


        public async Task<Travellers> FindByIdAsync(params object[] values)
        {
            Travellers entity = new Travellers();
            try
            {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TABLE} WHERE ID_Travellers = {values[0]};";
                var dr = await cmd.ExecuteReaderAsync();
                while (dr.Read())
                {
                    entity = GetEntityFromDataReader(dr);
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

        private Travellers GetEntityFromDataReader(SqlDataReader dr)
        {
            return new Travellers
            {
                ID_Travellers = (int)dr["ID_Travellers"],
                Name = string.Concat(dr["TX_FirstName"].ToString(), " ", dr["TX_SecondName"].ToString(), " ", dr["TX_LastName"].ToString(), " ", dr["TX_SecondLastName"].ToString()),
                TX_FirstName = dr["TX_FirstName"].ToString(),
                TX_SecondName = dr["TX_SecondName"].ToString(),
                TX_LastName = dr["TX_LastName"].ToString(),
                TX_SecondLastName = dr["TX_SecondLastName"].ToString(),
                TX_Phone = dr["TX_Phone"].ToString(),
                TX_IdentificationCard = dr["TX_IdentificationCard"].ToString(),
                TX_Address = dr["TX_Address"].ToString()
            };
        }

        public void Remove(Travellers entity)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"DELETE FROM {TABLE} WHERE ID_Travellers = {entity.ID_Travellers};";
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
