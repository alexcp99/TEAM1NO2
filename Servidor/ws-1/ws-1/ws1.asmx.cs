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
            Medicion obj = new Medicion(1, 1, 23334455, 32, 11, 322);
            var jsondata = JsonConvert.SerializeObject(obj);
            //return jsondata.ToString();
            HttpContext.Current.Response.AppendHeader("Access-Control-Allow-Origin", "*");
            HttpContext.Current.Response.ContentType = "application/json; charset=utf-8";
            HttpContext.Current.Response.Write(jsondata.ToString());
            HttpContext.Current.Response.End();
            //return new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(obj);
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
                insertCommand.CommandText = "INSERT INTO mediciones VALUES (NULL, @idsensor, @date, @lat, @long, @valor)";
                insertCommand.Parameters.AddWithValue("@idsensor", SqlDbType.Int);
                insertCommand.Parameters.AddWithValue("@date", SqlDbType.Int);
                insertCommand.Parameters.AddWithValue("@lat", SqlDbType.Real);
                insertCommand.Parameters.AddWithValue("@long", SqlDbType.Real);
                insertCommand.Parameters.AddWithValue("@valor", SqlDbType.Real);

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
