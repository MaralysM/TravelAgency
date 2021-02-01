using TravelAgency.Entities;
using TravelAgency.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TravelAgency.Data
{
    public class RequestsRepository : RepositoryBase, IRequestsRepository
    {
        string TABLE = $"{SCHEMA}Requests";
        const string TAG = "Requests_DATA";

        public RequestsRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<IList<Requests>> AllAsync()
        {
            List<Requests> requests = new List<Requests>();
            try
            {
                SqlConnection con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT R.ID_Requests, T.*, TV.* FROM [TravelAgency].[Requests] R "+
                                    $"INNER JOIN[TravelAgency].[Travels] T ON R.ID_Travels = T.ID_Travels " +
                                    $"INNER JOIN[TravelAgency].[Travellers] TV ON R.ID_Travellers = TV.ID_Travellers";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    requests.Add(GetEntityFromDataReader(dr));
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return requests;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Add(Requests entity)
        {
            try
            {
                var con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO {TABLE} (ID_Travels, ID_Travellers) " +
                    $"VALUES ({entity.travels.ID_Travels},{entity.travellers.ID_Travellers});";
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

        public async Task<Requests> UpdateAsync(Requests entity, params object[] Id)
        {
            try
            {
                var con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"UPDATE {TABLE} SET ID_Travels = {entity.travels.ID_Travels}, ID_Travellers = {entity.travellers.ID_Travellers}" +
                    $"WHERE ID_Requests = { entity.ID_Requests }";                
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

        public async Task<Requests> FindByIdAsync(params object[] values)
        {
            Requests entity = new Requests();
            try
            {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT R.ID_Requests, T.*, TV.* FROM [TravelAgency].[Requests] R " +
                    $"INNER JOIN[TravelAgency].[Travels] T ON R.ID_Travels = T.ID_Travels " +
                    $"INNER JOIN[TravelAgency].[Travellers] TV ON R.ID_Travellers = TV.ID_Travellers WHERE ID_Requests = {values[0]}";
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

        private Requests GetEntityFromDataReader(SqlDataReader dr)
        {
            return new Requests
            {
                ID_Requests = (int)dr["ID_Requests"],
                travellers = new Travellers { ID_Travellers = (int)dr["ID_Travellers"] , Name =  string.Concat(dr["TX_FirstName"].ToString(), " ", dr["TX_SecondName"].ToString(), " ", dr["TX_LastName"].ToString(), dr["TX_SecondLastName"].ToString()), TX_Phone = dr["TX_Phone"].ToString(), TX_IdentificationCard = dr["TX_IdentificationCard"].ToString(), TX_Address = dr["TX_Address"].ToString() },
                travels = new Travels { ID_Travels = (int)dr["ID_Travels"], NU_TravelCode = (long)dr["NU_TravelCode"], NU_NumberOfPlace = (int)dr["NU_NumberOfPlace"], TX_Destination = dr["TX_Destination"].ToString(), TX_Origin= dr["TX_Origin"].ToString(), NU_Price = (decimal)dr["NU_Price"] }
            };
        }

        public void Remove(Requests entity)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"DELETE FROM {TABLE} WHERE ID_Requests = {entity.ID_Requests};";
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
