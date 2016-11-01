using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class DaoVendedor
    {
        private SqlConnection sqlConnection;

        public DaoVendedor(SqlConnection sqlConnection) 
        {
            this.sqlConnection = sqlConnection;
        }

        public List<Vendedor> getVendedores()
        {
            List<Vendedor> lista = new List<Vendedor>();
            Vendedor vendedor = null;
            
            string sql = "SELECT idVendedor, nombre,codigoValidacion FROM wsSysMobileVendedores";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();


            while (reader.Read())
            {

                vendedor = new Vendedor();
                vendedor.idVendedor = reader.GetString(0).Trim();
                vendedor.nombre = reader.GetString(1).Trim();
                vendedor.codigoValidacion = reader.GetString(2).Trim();

                lista.Add(vendedor);
            }

            reader.Close();

            return lista;
        }

    }
}