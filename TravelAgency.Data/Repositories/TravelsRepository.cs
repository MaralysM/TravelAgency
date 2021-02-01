using TravelAgency.Entities;
using TravelAgency.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace TravelAgency.Data
{
    public class TravelsRepository : RepositoryBase, ITravelsRepository
    {
        string TABLE = $"{SCHEMA}Travels";
        const string TAG = "Travels_DATA";

        public TravelsRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public async Task<IList<Travels>> AllAsync()
        {
            List<Travels> travels = new List<Travels>();
            try
            {
                SqlConnection con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TABLE}";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    travels.Add(GetEntityFromDataReader(dr));
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return travels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Add(Travels entity)
        {
            try
            {
                var con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO {TABLE} (NU_TravelCode, NU_NumberOfPlace, TX_Destination, TX_Origin, NU_Price) " +
                    $"VALUES ({entity.NU_TravelCode},{entity.NU_NumberOfPlace}, '{entity.TX_Destination}', '{entity.TX_Origin}', {entity.NU_Price.ToString().Replace(',', '.')});";
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



        public async Task<Travels> UpdateAsync(Travels entity, params object[] Id)
        {
            try
            {
                var con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"UPDATE {TABLE} SET NU_TravelCode = {entity.NU_TravelCode}, NU_NumberOfPlace = {entity.NU_NumberOfPlace}, " +
                    $"TX_Destination = '{entity.TX_Destination}',  TX_Origin = '{entity.TX_Origin}', NU_Price = {entity.NU_Price.ToString().Replace(',', '.')} " +
                    $"WHERE ID_Travels = { entity.ID_Travels }";                
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


        public async Task<Travels> FindByIdAsync(params object[] values)
        {
            Travels entity = new Travels();
            try
            {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"SELECT * FROM {TABLE} WHERE ID_Travels = {values[0]};";
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

        private Travels GetEntityFromDataReader(SqlDataReader dr)
        {
            return new Travels
            {
                ID_Travels = (int)dr["ID_Travels"],
                NU_TravelCode = (long)dr["NU_TravelCode"],
                NU_NumberOfPlace = (int)dr["NU_NumberOfPlace"],
                TX_Destination = dr["TX_Destination"].ToString(),
                TX_Origin = dr["TX_Origin"].ToString(),
                NU_Price = (decimal)dr["NU_Price"],
                Name = string.Concat((long)dr["NU_TravelCode"], " - ", dr["TX_Origin"].ToString(), " / ", dr["TX_Destination"].ToString())
           };
        }

        public void Remove(Travels entity)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"DELETE FROM {TABLE} WHERE ID_Travels = {entity.ID_Travels};";
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
