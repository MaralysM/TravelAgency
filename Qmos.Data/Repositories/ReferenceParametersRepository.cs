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
                        ref1 = dr["ref1"].ToString() == "" ? 0 : decimal.Parse(dr["ref1"].ToString()),
                        ref2 = dr["ref2"].ToString() == "" ? 0 : decimal.Parse(dr["ref2"].ToString()),
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

    }
}
