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
        string TABLE = $"{SCHEMA}transition_parameters_header";
        const string TAG = "transitionparametersheaderDATA";

        public TransitionParametersRepository(IConfiguration configuration) : base(configuration)
        {

        }
        public short AddHeader(TransitionParametersHeader entity)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $" INSERT INTO {TABLE}([name],[active])" +
                    $"OUTPUT INSERTED.id " +
                    $" VALUES('{entity.Name}', '{entity.Active}');";
                var result = cmd.ExecuteScalar();
                Close(con);
                return (short)result;
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

        public bool AddDetail(long idHeader, TransitionParametersHeader entity)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"INSERT INTO [Qmos].[transition_parameters_details] " +
              $"(id_transition_parameters_header, time_transition, order_transition, id_element) " +
              $"VALUES ({idHeader},' {entity.TransitionParametersDetailsEntity.time_transition}', {entity.TransitionParametersDetailsEntity.order_transition}, {entity.TransitionParametersDetailsEntity.id_element}) ";
                int result = cmd.ExecuteNonQuery();
                Close(con);
                return result > 0;
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

        public bool UpdateDetail(TransitionParametersHeader entity)
        {
            try
            {
                int result = 0;
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    $"UPDATE  [Qmos].[transition_parameters_details] " +
                    $"SET id_transition_parameters_header = {entity.TransitionParametersDetailsEntity.id_transition_parameters_header}, time_transition = '{entity.TransitionParametersDetailsEntity.time_transition}', order_transition = {entity.TransitionParametersDetailsEntity.order_transition},id_element = {entity.TransitionParametersDetailsEntity.id_element} " +
                    $"WHERE id = {entity.TransitionParametersDetailsEntity.Id} ";
                result = cmd.ExecuteNonQuery();
                Close(con);

                return result > 0;
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

        public bool UpdateHeader(TransitionParametersHeader entity)
        {
            try
            {
                int result = 0;
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    $"UPDATE  {TABLE} " +
                    $"SET name = '{entity.Name}', active = '{entity.Active}' " +
                    $"WHERE id = {entity.Id} ";
                result = cmd.ExecuteNonQuery();
                Close(con);

                return result > 0;
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
                        Name = dr["name"].ToString(),
                        Active = Convert.ToBoolean(dr["active"])
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


        public async Task<TransitionParametersHeader> FindByIdAsync(params object[] values)
        {
            TransitionParametersHeader entity = new TransitionParametersHeader();
            try
            {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $" SELECT * FROM [Qmos].[transition_parameters_header] TPH " +
                $" INNER JOIN[Qmos].[transition_parameters_details] TPD ON TPH.id = TPD.id_transition_parameters_header";
               // $" WHERE TPH.id = {values[0]};";
                var dr = await cmd.ExecuteReaderAsync();
                while (dr.Read())
                {
                    TransitionParametersHeader tc = InstanceHeaderFromDataReader(dr);
                    tc.transitionParametersDetails = AllDetailByIdHeader(tc.Id);
                    entity = tc;
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
        public TransitionParametersDetails FindDetailById(short id)
        {
            TransitionParametersDetails entity = new TransitionParametersDetails();
            try
            {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"select * from [Qmos].[transition_parameters_details] where id = {id}";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entity = InstanceDetailFromDataReader(dr);
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

        private TransitionParametersHeader InstanceHeaderFromDataReader(SqlDataReader dr) =>
        new TransitionParametersHeader
        {
            Id = short.Parse(dr["id"].ToString()),
            Name = dr["name"].ToString(),
            Active = Convert.ToBoolean(dr["active"])
        };
        public IList<TransitionParametersDetails> AllDetailByIdHeader(long idHeader)
        {
            List<TransitionParametersDetails> listEntity = new List<TransitionParametersDetails>();
            try
            {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"select E.TX_Name element_name, * from [Qmos].[transition_parameters_details] TPD INNER JOIN [Security].[SM_ELEMENT] E ON TPD.id_element =E.ID_Element" +
                    $" where id_transition_parameters_header = {idHeader}";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    listEntity.Add(InstanceDetailFromDataReader(dr));
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return listEntity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private TransitionParametersDetails InstanceDetailFromDataReader(SqlDataReader dr) =>
        new TransitionParametersDetails
        {
            Id = short.Parse(dr["id"].ToString()),
            id_transition_parameters_header = short.Parse(dr["id_transition_parameters_header"].ToString()),
            time_transition = dr["time_transition"].ToString(),
            order_transition = short.Parse(dr["order_transition"].ToString()),
            id_element = int.Parse(dr["id_element"].ToString()),
            element_name = dr["element_name"].ToString()
        };
        public void Remove(short id)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"DELETE FROM [Qmos].[transition_parameters_details] WHERE id_transition_parameters_header = {id};";
                cmd.ExecuteNonQuery();
                cmd.CommandText = $"DELETE FROM {TABLE} WHERE id = {id};";
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

        public void RemoveDetail(short id)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"DELETE FROM [Qmos].[transition_parameters_details] WHERE id = {id};";
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
