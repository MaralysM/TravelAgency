using Qmos.Entities;
using Qmos.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public class ReferenceParametersRepository : RepositoryBase, IReferenceParametersRepository
    {
        string TABLE = $"{SCHEMA}reference_parameters";
        const string TAG = "reference_parametersDATA";

        public ReferenceParametersRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public async Task<IList<ReferenceParameters>> AllAsync()
        {
            List<ReferenceParameters> TransitionParameters = new List<ReferenceParameters>();
            try
            {
                SqlConnection con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"select E.TX_Name element_name, * from {TABLE} TP INNER JOIN [Security].[SM_ELEMENT] E ON TP.id_element =E.ID_Element";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TransitionParameters.Add(new ReferenceParameters
                    {
                        Id = (short)dr["id"],
                        id_element = (int)dr["id_element"],
                        Name = dr["cant_ref"].ToString(),
                        ref1 = dr["ref1"].ToString() == "" ? "0" : (dr["ref1"].ToString()),
                        ref2 = dr["ref2"].ToString() == "" ? "0" : (dr["ref2"].ToString()),
                        name_element = dr["element_name"].ToString()
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

        public bool UpdateReference(ReferenceParameters entity)
        {
            try
            {
                string _update = entity.ref1 == "0" ? $" ref2 = {entity.ref2.ToString().Replace(',', '.')} " : $" ref1 = {entity.ref1.ToString().Replace(',', '.')} ";
                int result = 0;
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    $"UPDATE  [Qmos].[reference_parameters] " +
                    $"SET {_update}" +
                    $"OUTPUT INSERTED.id " +
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

        public ReferenceParameters FindByIdElement(int id_element)
        {
            ReferenceParameters entity = new ReferenceParameters();
            try
             {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"select * from [Qmos].[reference_parameters] where id_element = {id_element}";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entity.Id = (short)dr["id"];
                    entity.id_element = (int)dr["id_element"];
                    entity.Name = dr["cant_ref"].ToString();
                    entity.ref1 = dr["ref1"].ToString() == "" ? "0" : (dr["ref1"].ToString().Replace(",","."));
                    entity.ref2 = dr["ref2"].ToString() == "" ? "0" : (dr["ref2"].ToString().Replace(",", "."));
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

    }
}
