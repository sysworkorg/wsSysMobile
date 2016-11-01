using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class DaoStock
    {
        private SqlConnection sqlConnection;

        public DaoStock(SqlConnection sqlConnection)
        {
            this.sqlConnection = sqlConnection;
        }

        public List<Stock> getStock(String articulo)
        {

            List<Stock> lista = new List<Stock>();

            SqlCommand cmdDepositos;

            string sql = "";
            Stock stock;

            double real = 0;
            double comprometido = 0;
            double sugerido = 0;


            // OBTENGO EL STOCK GENERAL
            stock = new Stock();
            stock = obtieneStockGeneral(articulo);
            real = stock.stock;
            lista.Add(stock);

            // OBTENGO EL STOCK COMPROMETIDO 
            stock = new Stock();
            stock = obtieneStockComprometido(articulo);
            comprometido = stock.stock;
            lista.Add(stock);

            // OBTENGO EL STOCK SUGERIDO
            stock = new Stock();
            stock.idDeposito = "@SUGERIDO";
            sugerido = real - comprometido;
            stock.stock = sugerido;
            lista.Add(stock);

            // OBTENGO EL STOCK POR DEPOSITO
            sql = "SELECT IDDEPOSITO,DESCRIPCION FROM wsSysMobileDepositos";
            cmdDepositos = new SqlCommand(sql, sqlConnection);

            DataTable dt = new DataTable();
            dt.TableName = "ListaDepositos";
            dt.Load(cmdDepositos.ExecuteReader());

            foreach (DataRow fila in dt.Rows)
            {
                stock = new Stock();
                stock = obtieneStockArticuloPorDeposito(articulo, fila[0].ToString());
                if (stock != null)
                    lista.Add(stock);
            }
            
            return lista;
        }

        private Stock obtieneStockGeneral(string idArticulo)
        {
            // OBTENGO EL STOCK GENERAL
            Stock stock = new Stock();
            stock.idDeposito = "@REAL";

            string sql = "SELECT STOCK FROM wsSysMobileStockArticulos WHERE IDARTICULO = '" + idArticulo + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                stock.stock = reader.IsDBNull(0) ? 0 : reader.GetDouble(0);
            else
                stock.stock = 0;

            reader.Close();

            return stock;
        }

        private Stock obtieneStockComprometido(string idArticulo)
        {

            // OBTENGO EL STOCK COMPROMETIDO 
            Stock stock = new Stock();
            stock.idDeposito = "@COMPROMETIDO";
            string sql = "SELECT stock FROM wsSysMobileStockComprometidoArticulos WHERE IDARTICULO = '" + idArticulo + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                stock.stock = reader.IsDBNull(0) ? 0 : reader.GetDouble(0);
            else
                stock.stock = 0;

            reader.Close();

            return stock;
        }

        private Stock obtieneStockArticuloPorDeposito(string idArticulo, string idDeposito)
        {
            Stock stock = new Stock();

            string sql = "SELECT STOCK FROM wsSysMobileStockArticulosDepositos where IDARTICULO = '" + idArticulo + "' AND idDeposito = '" + idDeposito + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();
            
            stock.idDeposito = idDeposito;
            if (reader.Read())
                stock.stock = reader.IsDBNull(0) ? 0 : reader.GetDouble(0);
            else
                stock = null;

            reader.Close();
            return stock;
        }
    }
}