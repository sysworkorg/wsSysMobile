using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class DaoDeposito
    {

        private SqlConnection sqlConnection;

        public DaoDeposito(SqlConnection sqlConnection) 
        {
            this.sqlConnection = sqlConnection;
        }

        public List<Deposito> getDepositos()
        {


            List<Deposito> lista = new List<Deposito>();
            Deposito deposito = null;

            string sql = "SELECT idDeposito,descripcion FROM wsSysMobileDepositos";
            
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {

                deposito = new Deposito();
                deposito.idDeposito = reader.GetString(0).Trim();
                deposito.descripcion = reader.GetString(1);

                lista.Add(deposito);
            }

            reader.Close();

            return lista;
        }

    }
}