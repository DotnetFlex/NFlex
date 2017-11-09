using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator
{
    public class ConnectionInfo
    {
        public string Server { get; set; }
        public string ValidateType { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string GetConnectionString(string dbName)
        {
            //return string.Format("Data Source={0};Initial Catalog={3};Persist Security Info=True;MultipleActiveResultSets=True;pooling=true;max pool size=750; min pool size=5;packet size=4096;user id={1};Password={2};Network=dbmssocn;", Server,UserName,Password,dbName);
            //return string.Format("Data Source={0};Initial Catalog={3};user id={1};Password={2};", Server, UserName, Password, dbName);
            
            if (ValidateType == "Windows")
                return string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", Server, dbName);
            else
                return string.Format("Data Source={0};Initial Catalog={3};Persist Security Info=True;User ID={1};Password={2}", Server, UserName, Password, dbName);
        }
    }
}
