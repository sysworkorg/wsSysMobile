using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;


namespace wsSysMobileREST.Areas.Api.Models
{
    public class DaoCtaCte
    {

        private SqlConnection sqlConnection;

        public DaoCtaCte(SqlConnection sqlConnection) 
        {
            this.sqlConnection = sqlConnection;
        }

        public List<CtaCte> getCtaCte(String cliente, String fechaDesde, String fechaHasta)
        {
            // las fechas vienen en formato YYYYMMDD (las invierto)
            fechaDesde = fechaDesde.Substring(6,2) + "/" + fechaDesde.Substring(4,2) + "/" + fechaDesde.Substring(0,4);
            fechaHasta = fechaHasta.Substring(6, 2) + "/" + fechaHasta.Substring(4, 2) + "/" + fechaHasta.Substring(0, 4);
         
            List<CtaCte> lista = new List<CtaCte>();
            SqlCommand cmd;
            SqlDataReader reader;
            

            // OBTENGO EL SALDO ANTERIOR
            string sql = "SELECT isnull(SUM(CASE [DEBE-HABER] WHEN 'D' THEN IMPORTE WHEN 'H' THEN IMPORTE * - 1 END),0) AS SumaDeImporte "
                    + " FROM dbo.MV_ASIENTOS WHERE CUENTA = '" + cliente + "' AND FECHA < '" + fechaDesde +"'";

            cmd = new SqlCommand(sql, sqlConnection);
            reader = cmd.ExecuteReader();

            if (reader.Read()) 
            {
                CtaCte ctaCte = new CtaCte();
                
                ctaCte.fecha = "";
                ctaCte.tc = "";
                ctaCte.sucNroLetra = "Saldo Anterior:";
                ctaCte.detalle = "";
                ctaCte.importe = reader.GetDecimal(0);
                 
                lista.Add(ctaCte);
 
            }
            reader.Close();
            
            // OBTENGO EL DETALLE DE MOVIMIENTOS
            sql = "SELECT fecha,tc, sucursal + numero + letra as sucNroLetra,detalle," 
            + " case [debe-haber] when 'D' then importe when 'H' then importe * -1 end as importe" + 
                                                            " FROM MV_ASIENTOS WHERE (cuenta = '" + cliente + "') and (fecha >= '" + fechaDesde + "' and " +
                                                            " fecha <= '" + fechaHasta +"') order by fecha, id";
            cmd = new SqlCommand(sql, sqlConnection);
            reader = cmd.ExecuteReader();


            while (reader.Read())
            {
                CtaCte ctaCte = new CtaCte();
                ctaCte.fecha =  String.Format("{0:dd/MM/yy}", reader.GetDateTime(0));

                ctaCte.tc = reader.GetString(1);
                ctaCte.sucNroLetra = reader.GetString(2);
                ctaCte.detalle = reader.GetString(3);
                ctaCte.importe = reader.GetDecimal(4);

                lista.Add(ctaCte);

            }

            reader.Close();

            //


            // OBTENGO EL SALDO ACTUAL
            sql = "SELECT isnull(SUM(CASE [DEBE-HABER] WHEN 'D' THEN IMPORTE WHEN 'H' THEN IMPORTE * - 1 END),0) AS SumaDeImporte "
                    + " FROM dbo.MV_ASIENTOS WHERE CUENTA = '" + cliente + "'";
            
            cmd = new SqlCommand(sql, sqlConnection);
            reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                CtaCte ctaCte = new CtaCte();
                ctaCte.fecha = "";

                ctaCte.tc = "";
                ctaCte.sucNroLetra = "Saldo Actual:";
                ctaCte.detalle = "";
                ctaCte.importe = reader.GetDecimal(0);

                lista.Add(ctaCte);

            }
            reader.Close();
            

            return lista;
        }
    }
}