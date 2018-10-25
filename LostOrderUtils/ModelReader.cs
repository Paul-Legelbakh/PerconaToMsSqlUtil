using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LostOrderUtils
{
    public class JsonEntity
    {
        public string mssql { get; set; }
        public string mysql { get; set; }
        public Dictionary<string, string> fields;
    }

    public class ModelReader
    {
        public static string importDll = "";

        public static readonly string bpmConnectionStringFormat = "Data Source={0}; Initial Catalog={1}; Persist Security Info=True; MultipleActiveResultSets=True; User ID={2}; Password={3}; Pooling = true; Max Pool Size = 100; Async = true";

        public static readonly string perconaConnectionStringFormat = "Server={0}; Database={1}; Convert Zero Datetime=True; Uid={2}; Pwd={3}; charset=utf8";
        public ModelReader()
        {
            
        }

        public int ProgressBarDataFromMsSql(string connectionStringMsSql,
            List<JsonEntity> jsonEntities, DateTime startDate, DateTime dueDate)
        {
            int count = 0;
            using (SqlConnection dbConnection = new SqlConnection(connectionStringMsSql))
            {
                try
                {
                    for (int i = 0; i < jsonEntities.Count; i++)
                    {
                        dbConnection.Open();
                        string sqlQuery = String.Format(@"SELECT COUNT(Id) FROM {0} WHERE (ModifiedOn BETWEEN '{1}' AND '{2}')",
                        jsonEntities[i].mssql, startDate.ToString("yyyy-MM-dd"), dueDate.ToString("yyyy-MM-dd"));
                        using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                        {
                            try
                            {
                                using (SqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        count += reader.GetInt32(0);
                                    }
                                }
                            }
                            catch (Exception exc)
                            {
                                Console.WriteLine(String.Format("Error. Message: {0}; Source: {1}", exc.Message, exc.Source));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return count;
        }

        public int ProgressBarDataFromMySql(string connectionStringMySql,
            List<JsonEntity> jsonEntities, DateTime startDate, DateTime dueDate)
        {
            int count = 0;
            using (MySqlConnection dbConnection = new MySqlConnection(connectionStringMySql))
            {
                try
                {
                    for (int i = 0; i < jsonEntities.Count; i++)
                    {
                        dbConnection.Open();
                        string sqlQuery = String.Format(@"SELECT COUNT(Id) FROM {0} WHERE (ModifiedOn BETWEEN '{1}' AND '{2}')",
                        jsonEntities[i].mssql, startDate.ToString("yyyy-MM-dd"), dueDate.ToString("yyyy-MM-dd"));
                        using (MySqlCommand command = new MySqlCommand(sqlQuery, dbConnection))
                        {
                            try
                            {
                                using (MySqlDataReader reader = command.ExecuteReader())
                                {
                                    while (reader.Read())
                                    {
                                        count += reader.GetInt32(0);
                                    }
                                }
                            }
                            catch (Exception exc)
                            {
                                Console.WriteLine(String.Format("Error. Message: {0}; Source: {1}", exc.Message, exc.Source));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return count;
        }

        #region MySql Percona
        public List<string> GetMySqlData(string connectionStringMySql,
            JsonEntity jsonEntity, DateTime startDate, DateTime dueDate, int offset = 0, int limit = 100)
        {
            string tablesMySqlString = string.Join(",", jsonEntity.fields.Values);
            string sqlQuery = String.Format(@"SELECT {0} FROM {1} WHERE (ModifiedOn BETWEEN '{2}' AND '{3}') 
                                            ORDER BY ModifiedOn ASC LIMIT {4} OFFSET {5}",
                                            tablesMySqlString, jsonEntity.mssql, startDate.ToString("yyyy-MM-dd"),
                                            dueDate.ToString("yyyy-MM-dd"), limit, offset);
            List<string> rows = new List<string>();

            using (MySqlConnection dbConnection = new MySqlConnection(connectionStringMySql))
            {
                try
                {
                    dbConnection.Open();
                    using (MySqlCommand command = new MySqlCommand(sqlQuery, dbConnection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string str = "";
                                for (int i = 0; i < jsonEntity.fields.Count; i++)
                                {
                                    if (reader.GetValue(i).ToString() != "")
                                    {
                                        if (reader.GetValue(i).GetType() == typeof(Byte[]))
                                        {
                                            Byte[] bytes = (byte[])reader.GetValue(i);
                                            Guid guid = new Guid(bytes);
                                            str += "'" + guid + "',";
                                        }
                                        else if(reader.GetValue(i).GetType() == typeof(decimal))
                                        {
                                            int value = (int)reader.GetDecimal(i);
                                            str += "'" + value.ToString() + "',";
                                        }
                                        else if (reader.GetValue(i).GetType() == typeof(DateTime))
                                        {
                                            DateTime date = reader.GetDateTime(i);
                                            str += "'" + date.ToString("yyyy-MM-dd H:mm:ss") + "',";
                                        }
                                        else
                                        {
                                            str += "'" + reader.GetValue(i).ToString().Replace("'", "''") + "',";
                                        }
                                    }
                                    else if(reader.IsDBNull(i))
                                    {
                                        if(jsonEntity.fields.ElementAt(i).Value.Contains("Id"))
                                        {
                                            str += "NULL,";
                                        }
                                        else
                                        {
                                            str += "'',";
                                        }
                                    }
                                    else
                                    {
                                        str += "'',";
                                    }
                                }
                                str = str.Substring(0, str.Length - 1);
                                rows.Add(str);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return rows;
        }

        public void PushMySqlToMsSql(string connectionStringMsSql,
            JsonEntity jsonEntity, List<string> mySqlData)
        {
            string tablesMsSqlString = string.Join(",", jsonEntity.fields.Keys);
            using (SqlConnection dbConnection = new SqlConnection(connectionStringMsSql))
            {
                try
                {
                    dbConnection.Open();
                    for (int i = 0; i < mySqlData.Count; i++)
                    {
                        Console.WriteLine(mySqlData[i]);
                        string sqlQuery = String.Format(@"INSERT INTO {0} ({1}) VALUES ({2});",
                        jsonEntity.mysql, tablesMsSqlString, mySqlData[i]);
                        using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                        {
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception exc)
                            {
                                Console.WriteLine(String.Format("Error. Message: {0}; Source: {1}", exc.Message, exc.Source));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }

        #endregion
        #region MS Sql Server
        public List<string> GetMsSqlData(string connectionStringMsSql,
            JsonEntity jsonEntity, DateTime startDate, DateTime dueDate, int offset = 0, int limit = 100)
        {

            string tablesMsSqlString = string.Join(",", jsonEntity.fields.Keys);
            string sqlQuery = String.Format(@"SELECT {0} FROM {1} WHERE (ModifiedOn BETWEEN '{2}' AND '{3}') 
                                            ORDER BY ModifiedOn ASC OFFSET {4} ROWS FETCH NEXT {5} ROWS ONLY",
                                            tablesMsSqlString, jsonEntity.mssql, startDate.ToString("yyyy-MM-dd"),
                                            dueDate.ToString("yyyy-MM-dd"), offset, limit);
            List<string> rows = new List<string>();

            using (SqlConnection dbConnection = new SqlConnection(connectionStringMsSql))
            {
                try
                {
                    dbConnection.Open();
                    using (SqlCommand command = new SqlCommand(sqlQuery, dbConnection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string str = "";
                                for (int i = 0; i < jsonEntity.fields.Count; i++)
                                {
                                    if (reader.GetValue(i).ToString() != "")
                                    {
                                        if (reader.GetValue(i).GetType() == typeof(Guid))
                                        {
                                            str += "UuidToBin(\"" + reader.GetValue(i).ToString() + "\"),";
                                        }
                                        else if (reader.GetValue(i).GetType() == typeof(DateTime))
                                        {
                                            var date = reader.GetDateTime(i);
                                            str += "\"" + date.ToString("yyyy-MM-dd H:mm:ss") + "\",";
                                        }
                                        else
                                        {
                                            str += "\"" + reader.GetValue(i).ToString().Replace("\"", "\"\"") + "\",";
                                        }
                                    }
                                    else
                                    {
                                        str += "\"\",";
                                    }
                                }
                                str = str.Substring(0, str.Length - 1);
                                rows.Add(str);
                            }
                        }
                    }
                }
                    catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
            return rows;
        }

        public void PushMsSqlToMySql(string connectionStringMySql,
            JsonEntity jsonEntity, List<string> msSqlData)
        {
            string tablesMySqlString = string.Join(",", jsonEntity.fields.Values);
            using (MySqlConnection dbConnection = new MySqlConnection(connectionStringMySql))
            {
                try
                {
                    dbConnection.Open();
                    for (int i = 0; i < msSqlData.Count; i++) {
                        Console.WriteLine(msSqlData[i]);
                        string sqlQuery = String.Format(@"INSERT IGNORE INTO {0} ({1}) VALUES ({2});",
                        jsonEntity.mysql, tablesMySqlString, msSqlData[i]);
                        using (MySqlCommand command = new MySqlCommand(sqlQuery, dbConnection))
                        {
                            try
                            {
                                command.ExecuteNonQuery();
                            }
                            catch (Exception exc)
                            {
                                Console.WriteLine(String.Format("Error. Message: {0}; Source: {1}", exc.Message, exc.Source));
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    dbConnection.Close();
                }
            }
        }
        #endregion
        public List<JsonEntity> LoadJson(string configAddress)
        {
            List<JsonEntity> jsonEntities = new List<JsonEntity>();
            using (StreamReader r = new StreamReader(configAddress))
            {
                try {
                    string json = r.ReadToEnd();
                    var convertedJson = JArray.Parse(json);
                    foreach (JObject token in convertedJson)
                    {
                        JsonEntity obj = new JsonEntity()
                        {
                            mssql = token["mssql"].ToString(),
                            mysql = token["mysql"].ToString(),
                            fields = JsonConvert.DeserializeObject<Dictionary<string, string>>(token["fields"].ToString())
                        };
                        jsonEntities.Add(obj);
                    }
                } catch(Exception ex)
                {
                    throw ex;
                }
            }
            return jsonEntities;
        }
    }
}
