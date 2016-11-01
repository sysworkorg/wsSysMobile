using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class DaoRubro
    {
        private SqlConnection sqlConnection;

        public DaoRubro(SqlConnection sqlConnection) 
        {
            this.sqlConnection = sqlConnection;
        }

        public List<Rubro> getRubros() 
        { 
            List<Rubro> lista = new List<Rubro>();
            Rubro rubro = null;
            
            string sql = "SELECT IdRubro, Descripcion FROM wsSysMobileRubros";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            
            while (reader.Read()) 
            {

                rubro = new Rubro();
                rubro.idRubro = reader.GetString(0).Trim();
                rubro.descripcion = reader.GetString(1).Trim();
                lista.Add(rubro); 
            } 
            
            reader.Close(); 
            
            return lista; 
        }

    }
}