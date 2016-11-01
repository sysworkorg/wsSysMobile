using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class DaoArticulo
    {
        private SqlConnection sqlConnection;

        public DaoArticulo(SqlConnection sqlConnection) 
        {
            this.sqlConnection = sqlConnection; 
        }

        public List<Articulo> getArticulos(int? pagina)
        {
            List<Articulo> lista = new List<Articulo>();
            string sql;

            if (pagina == null) 
                sql = "SELECT IdArticulo,Descripcion,IdRubro,Impuestos,TasaIva,Exento, Precio1,Precio2,Precio3,Precio4,Precio5,Precio6,Precio7,Precio8,Precio9,Precio10 FROM wsSysMobileArticulos";
            else
                sql = "EXEC dbo.wsSysMobileSPPaginacionArticulos " + pagina + "," + CsShared.getInstance().getDatamanager().getTamanioPaginacionRegistros();

            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            Articulo articulo = null;
            while (reader.Read())
            {

                articulo = new Articulo();
                articulo.idArticulo = (string)reader.GetString(0).Trim();
                articulo.descripcion = reader.GetString(1).Trim();
                articulo.idRubro = reader.GetString(2).Trim();
                articulo.impuestosInternos = reader.GetDecimal(3);
                articulo.iva = reader.GetDouble(4);
                articulo.exento = reader.GetBoolean(5);
                articulo.precio1 = reader.GetDecimal(6);
                articulo.precio2 = reader.GetDecimal(7);
                articulo.precio3 = reader.GetDecimal(8);
                articulo.precio4 = reader.GetDecimal(9);
                articulo.precio5 = reader.GetDecimal(10);
                articulo.precio6 = reader.GetDecimal(11);
                articulo.precio7 = reader.GetDecimal(12);
                articulo.precio8 = reader.GetDecimal(13);
                articulo.precio9 = reader.GetDecimal(14);
                articulo.precio10 = reader.GetDecimal(15);

                lista.Add(articulo);
            }

            reader.Close();

            return lista;
        }



        public Articulo getArticuloByIdArticulo(string idArticulo) 
        {
            Articulo articulo = null;
            string sql;

            sql = "SELECT IdArticulo,Descripcion,IdRubro,Impuestos,TasaIva,Exento, Precio1,Precio2,Precio3,Precio4,Precio5,Precio6,Precio7,Precio8,Precio9,Precio10 FROM wsSysMobileArticulos WHERE IdArticulo = '" + idArticulo + "'";

            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                articulo = new Articulo();

                articulo.idArticulo = (string)reader.GetString(0).Trim();
                articulo.descripcion = reader.GetString(1).Trim();
                articulo.idRubro = reader.GetString(2).Trim();
                articulo.impuestosInternos = reader.GetDecimal(3);
                articulo.iva = reader.GetDouble(4);
                articulo.exento = reader.GetBoolean(5);
                articulo.precio1 = reader.GetDecimal(6);
                articulo.precio2 = reader.GetDecimal(7);
                articulo.precio3 = reader.GetDecimal(8);
                articulo.precio4 = reader.GetDecimal(9);
                articulo.precio5 = reader.GetDecimal(10);
                articulo.precio6 = reader.GetDecimal(11);
                articulo.precio7 = reader.GetDecimal(12);
                articulo.precio8 = reader.GetDecimal(13);
                articulo.precio9 = reader.GetDecimal(14);
                articulo.precio10 = reader.GetDecimal(15);
            }


            return articulo;
        }
    
    }
}