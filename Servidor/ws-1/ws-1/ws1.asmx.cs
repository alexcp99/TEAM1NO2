using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Services;

namespace ws_1
{
    /// <summary>
    /// Descripción breve de ws1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
     [System.Web.Script.Services.ScriptService]
    public class ws1 : System.Web.Services.WebService
    {

        private static string LoadConnectionString(String id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;

        }

        
        [WebMethod]
        public void UltimaLectura()
        {
            string a = System.AppDomain.CurrentDomain.BaseDirectory + "db\\dabase.db";
            string connectionString = string.Format("Data Source = {0}; Version = 3;", a);
            bool testigo = false;
            //Medicion obj = new Medicion();
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = "select * from mediciones ORDER BY idmedicion DESC LIMIT 1";
                    using (SQLiteDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Medicion obj = new Medicion(Convert.ToInt32(dr[0]), Convert.ToInt32(dr[1]), Convert.ToInt32(dr[2]), Convert.ToDouble(dr[3]), Convert.ToDouble(dr[4]), Convert.ToDouble(dr[5]));

                            testigo = true;
                            if (testigo)
                            {
                                var jsondata = JsonConvert.SerializeObject(obj);
                                HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
                                HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
                                HttpContext.Current.Response.Write(jsondata.ToString());
                                HttpContext.Current.Response.End();
                            }
                        }
                    }
                    conn.Close();
                }
            }
            


        }

        [WebMethod]
        public void GrabaMedicion(int idsen, int date, double lat, double longi, double valor)
        {
            string a = System.AppDomain.CurrentDomain.BaseDirectory + "db\\dabase.db";
            string connectionString = string.Format("Data Source = {0}; Version = 3;", a);
            //HttpContext.Current.Response.Write(connectionString);
            using (SQLiteConnection cnn = new SQLiteConnection(connectionString))
            {
                //HttpContext.Current.Response.Write(LoadConnectionString());
                //HttpContext.Current.Response.End();
                cnn.Open();
                SQLiteCommand insertCommand = new SQLiteCommand();
                insertCommand.Connection = cnn;
                // Use parameterized query to prevent SQL injection attacks
                string query = string.Format("INSERT INTO mediciones VALUES (NULL, {0}, {1}, {2}, {3}, {4})" , idsen,date,lat,longi,valor);
                insertCommand.CommandText = query;

                insertCommand.ExecuteReader();

                cnn.Close();
            }
        }

        [WebMethod]
        public void DatosEntreFechas(string inicio, string fin)
        {
            
        }

    }
}
