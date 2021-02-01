using TravelAgency.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace TravelAgency.Data.Repositories.Abstractions
{
    public class ProcessBaseRepository: RepositoryBase
    {
        public readonly string tableEmployees = $"{SCHEMA}employee";

        public ProcessBaseRepository(IConfiguration configuration) : base(configuration) { }

        public List<int> GetEmployeesSubordinate(int idSupervisor)
        {
            List<int> list = new List<int>();
            try
            {
                SqlConnection con = Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText =
                    $"SELECT id FROM {tableEmployees} WHERE id_employee_supervisor = {idSupervisor} AND active = 1";
                var dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    list.Add(dr["id"] == DBNull.Value ? 0 : (int)dr["id"]);
                }
                dr.Close();
                cmd.Dispose();
                Close(con);
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
