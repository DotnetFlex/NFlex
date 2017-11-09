using CodeGenerator.Schemas;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class DatabaseInfo
    {
        public ConnectionInfo ConnectionInfo { get; set; }
        public string DatabaseName { get; set; }

        private List<TableInfo> tableList;

        public DatabaseInfo(ConnectionInfo connInfo,string dbName)
        {
            ConnectionInfo = connInfo;
            DatabaseName = dbName;
        }

        private SqlConnection CreateConn()
        {
            SqlConnection conn = new SqlConnection(ConnectionInfo.GetConnectionString(DatabaseName));
            return conn;
        }

        public static List<DatabaseInfo> GetDatabaseList(ConnectionInfo connInfo)
        {
            var list = new List<DatabaseInfo>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connInfo.GetConnectionString("master")))
                {
                    SqlCommand cmd = new SqlCommand("Select name from master..sysdatabases ", conn);
                    conn.Open();
                    var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (reader.Read())
                    {
                        list.Add(new DatabaseInfo(connInfo, reader["name"].ToString()));
                    }
                    reader.Close();
                }
            }
            catch { }
            return list;
        }

        public Task<List<TableSchema>> ReLoadDatabaseSchema()
        {
            return Task.Run(()=> {
                return new SchemaLoader().Load(ConnectionInfo,DatabaseName);
            });
        }
        
        public Task<List<TableInfo>> LoadTablesAsync(bool reload=false)
        {
            return Task.Run(() =>
            {
                if (tableList == null || reload) 
                    tableList= new SchemaLoader().GetTables(ConnectionInfo.GetConnectionString(DatabaseName));
                return tableList;
            });
        }

        public override string ToString()
        {
            return DatabaseName;
        }
    }
}
