using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LostOrderUtils
{
    public class SqlBuilder
    {
        public static string BuildInsertSql()
        {
            string sql = "";
            List<Order> o = new List<Order>();
            try
            {
                using (IDbConnection db = new SqlConnection(String.Format(ModelReader.bpmConnectionStringFormat, "WIN-JG56R3MF2FF", "bristol", "Supervisor", "Supervisor")))
                {
                    o = db.Query<Order>("Select * from [Order] where Date > '2018-02-12'").ToList();
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
            o[0].Id = Guid.NewGuid();


            return sql;
        }
    }
}
