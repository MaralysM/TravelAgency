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
                cmd.CommandText = $"select E.TX_Name element_name, CE.name child_name, * from {TABLE} TP INNER JOIN [Security].[SM_ELEMENT] E ON TP.id_element = E.ID_Element LEFT JOIN [Qmos].[child_element] CE ON TP.id_child = CE.id";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TransitionParameters.Add(new ReferenceParameters
                    {
                        Id = (short)dr["id"],
                        id_element = (int)dr["id_element"],
                        reference = dr["reference"].ToString() == "" ? "0" : (dr["reference"].ToString()),
                        name_element = dr["element_name"].ToString(),
                        id_child = dr["id_child"].ToString() == "" ? (short)0 : (short)dr["id_child"],
                        refmax = dr["ref_max"].ToString() == "" ? "0" : (dr["ref_max"].ToString()),
                        refmin = dr["ref_min"].ToString() == "" ? "0" : (dr["ref_min"].ToString()),
                        childElement = new ChildElement() {Name = dr["child_name"].ToString() }
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

        public async Task<IList<ChildElement>> AllChildElement()
        {
            List<ChildElement> ChildElement = new List<ChildElement>();
            try
            {
                SqlConnection con = await OpenAsync();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"select * from [Qmos].[child_element]";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ChildElement.Add(new ChildElement
                    {
                        Id = (short)dr["id"],
                        id_element = (int)dr["id_element"],
                        Name = dr["name"].ToString()
                    });
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return ChildElement;
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
                int result = 0;
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    $"UPDATE  [Qmos].[reference_parameters] " +
                    $"SET  reference = {entity.reference.ToString().Replace(',', '.')}" +
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

        public List<ReferenceParameters> FindByIdElement(int id_element)
        {
            List<ReferenceParameters> entity = new List<ReferenceParameters>();
            try
             {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $"select * from [Qmos].[reference_parameters] where id_element = {id_element}";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    entity.Add(new ReferenceParameters
                    {
                        Id = (short)dr["id"],
                        id_element = (int)dr["id_element"],
                        reference = dr["reference"].ToString() == "" ? "0" : (dr["reference"].ToString().Replace(",", ".")),
                        id_child = dr["id_child"].ToString() == "" ? (short)0 : (short)dr["id_child"],
                        refmax = dr["ref_max"].ToString() == "" ? "0" : (dr["ref_max"].ToString()),
                        refmin = dr["ref_min"].ToString() == "" ? "0" : (dr["ref_min"].ToString())
                    });
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

        public short Save(ReferenceParameters entity)
        {
            try
            {
                string campos = entity.id_element == 16 ? ", [id_child], [ref_min], [ref_max]" : "";
                string values = entity.id_element == 16 ? $", {entity.id_child}, {entity.refmin.ToString().Replace(',', '.')}, {entity.refmax.ToString().Replace(',', '.')}" : "";
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = $" INSERT INTO {TABLE}([id_element],[reference] {campos})" +
                    $"OUTPUT INSERTED.id " +
                    $" VALUES({entity.id_element}, {entity.reference.ToString().Replace(',', '.')} {values});";
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

        public void Remove(short id)
        {
            try
            {
                var con = Open();
                SqlCommand cmd = con.CreateCommand();
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
    }
}
