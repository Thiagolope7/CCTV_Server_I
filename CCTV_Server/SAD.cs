using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace CCTV_Server
{
    class SAD
    {
        public static SqlConnection Conexion = SqlConnect.ConexionSQL();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger
             (System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
       
        public static bool Activo()
        {
            string query = "SELECT  [IdFisicoCC] FROM [MEDELLIN_CONF].[dbo].[SAD_CONF] where Estado=1";
            Conexion.Open();
            SqlCommand comando = new SqlCommand(query, Conexion);

            SqlDataReader dr = comando.ExecuteReader();
            while (dr.Read())
            {
                
                if (dr.GetString(0) == ConfigurationManager.AppSettings["ip_name"])
                {                    
                    // log.Info("Estado Activo.");
                    Conexion.Close();
                    return true;

                }
                else
                {
                   
                    
                    //log.Info("Estado Pasivo.");
                    Conexion.Close();
                    return false;
                }
                //}
            }
            Conexion.Close();
            return false;


        }
    }
}
