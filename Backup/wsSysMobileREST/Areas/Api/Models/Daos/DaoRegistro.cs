using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class DaoRegistro
    {
        private SqlConnection sqlConnection;

        public DaoRegistro(SqlConnection sqlConnection) 
        {
            this.sqlConnection = sqlConnection;
        }

        public List<Registro> obtenerRegistros()
        {

            List<Registro> lista = new List<Registro>();
            Registro registro = null;
            
            string sql = "SELECT tabla,cantidad FROM wsSysMobileTotalRegistrosTablas";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                registro = new Registro();
                registro.tabla = reader.GetString(0);
                registro.cantidadRegistros = reader.GetInt32(1);

                lista.Add(registro);
            }
            
            reader.Close();
            
            return lista;
        }

    }
}