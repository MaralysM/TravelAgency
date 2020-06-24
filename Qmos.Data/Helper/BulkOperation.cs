using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Qmos.Data.Helper
{
    public class BulkOperation
    {
        public BulkOperation()
        {
            
        }
              

        public static void InsertUpdate(SqlConnection con, DataTable Data, string TABLE, string SCHEMA, string[] COLUMN_MATCHER, string[] DROP_COLUMN = null)
        {
            string CreateTableScript = SqlTableCreator.GetCreateFromDataTableSQL($"#{TABLE.Replace(SCHEMA, "")}_TEMP", Data).Replace("\n", "");

            string[] Columns = Data.Columns.Cast<DataColumn>().Where(x=>!x.ColumnName.Contains("ID_")).Select(x => x.ColumnName).ToArray();
            if (DROP_COLUMN != null)
            {
                var list = new List<string>(Columns);
                for (int i = 0; i < DROP_COLUMN.Length; i++)
                {
                    
                    list.Remove(DROP_COLUMN[i]);
                }
                Columns = list.ToArray();
            }

            StringBuilder Text = new StringBuilder();

            using (SqlCommand command = new SqlCommand(CreateTableScript, con))
            {
                command.ExecuteNonQuery();

                using (var sqlBulkCopy = new SqlBulkCopy(con))
                {
                    sqlBulkCopy.DestinationTableName = $"#{TABLE.Replace(SCHEMA, "")}_TEMP";
                    sqlBulkCopy.BatchSize = 1000;

                    foreach (string Column in Columns)
                    {
                        sqlBulkCopy.ColumnMappings.Add(Column, Column);
                    }
                    sqlBulkCopy.WriteToServer(Data);
                    sqlBulkCopy.Close();
                }
                Text.Clear();

                Text.Append($"UPDATE A SET ");

                foreach (string Column in Columns)
                {
                    Text.Append($"A.{Column} = B.{Column},");
                }

                Text.Remove(Text.ToString().Length - 1, 1);

                Text.Append($" FROM {TABLE} AS A INNER JOIN #{TABLE.Replace(SCHEMA, "")}_TEMP AS B ON ");
                for (int i = 0; i < COLUMN_MATCHER.Length; i++)
                {
                    Text.Append($"A.{COLUMN_MATCHER[i]} = B.{COLUMN_MATCHER[i]}");
                    if (i < COLUMN_MATCHER.Length-1)
                        Text.Append(" AND ");
                }

                command.CommandText = Text.ToString();
                command.ExecuteNonQuery();
                Text.Clear();


                Text.Append($"INSERT INTO {TABLE} ({string.Join(",", Columns)}) SELECT {string.Join(",", Columns)} FROM #{TABLE.Replace(SCHEMA, "")}_TEMP AS T WHERE T.{COLUMN_MATCHER[0]} NOT IN (");
                if (COLUMN_MATCHER.Length > 1)
                {
                    Text.Append($"SELECT O.{COLUMN_MATCHER[0]} FROM {TABLE} AS O WHERE ");
                    for (int i = 0; i < COLUMN_MATCHER.Length; i++)
                    {
                        Text.Append($"T.{COLUMN_MATCHER[i]} = O.{COLUMN_MATCHER[i]}");
                        if (i < COLUMN_MATCHER.Length - 1)
                            Text.Append(" AND ");
                    }
                }
                else {
                    for (int i = 0; i < COLUMN_MATCHER.Length; i++)
                    {
                        Text.Append($"SELECT O.{COLUMN_MATCHER[i]} FROM {TABLE} AS O");
                    }
                }
                Text.Append($")");

                command.CommandText = Text.ToString();
                command.ExecuteNonQuery();

                command.CommandText = $"DROP TABLE #{TABLE.Replace(SCHEMA, "")}_TEMP;";
                command.ExecuteNonQuery();
            }
        }
    }
}
