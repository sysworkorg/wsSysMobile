using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class DaoPedido
    {
        private DaoCliente daoCliente; 
        
        private SqlConnection sqlConnection;
        private SqlTransaction sqlTransaction;

        public DaoPedido(SqlConnection sqlConnection) 
        {
            this.sqlConnection = sqlConnection;
        }


        public bool procesaJsonPedidos(String jsonPedidos) 
        {
            /*
                List<Pedido> listaPedidos = (List<Pedido>)JsonConvert.DeserializeObject(jsonPedidos, typeof(List<Pedido>));
                de Una o otra forma funcionan bien
                List<Pedido> listaPedidos = (List<Pedido>) JsonHelper.JsonDeserialize <List<Pedido>> (jsonPedidos);
            */
            bool generaPedidoEnV_MV_Cpte = CsShared.getInstance().getDatamanager().getGrabaPedidoVMVCpte();

            daoCliente = CsShared.getInstance().getDatamanager().getDaoCliente();
            List<Pedido> listaPedidos = (List<Pedido>) JsonHelper.JsonDeserialize <List<Pedido>> (jsonPedidos);
            
            sqlTransaction = sqlConnection.BeginTransaction();
            
            foreach (Pedido pedido in listaPedidos) 
            {
                if (generaPedidoEnV_MV_Cpte)
                {
                    if (!grabarPedidoV_MV_CPTE(pedido))
                        return false;
                }
                else 
                {
                    if (!grabarPedido(pedido))
                        return false;
                }
            }

            sqlTransaction.Commit();

            return true;
        }

        public bool procesaJsonPedido(String jsonPedido)
        {

            daoCliente = CsShared.getInstance().getDatamanager().getDaoCliente();
            Pedido pedido = (Pedido)JsonHelper.JsonDeserialize<Pedido>(jsonPedido);

            sqlTransaction = sqlConnection.BeginTransaction();

            if (!grabarPedido(pedido))
                return false;

            sqlTransaction.Commit();

            return true;
        }

        #region Grabacion Pedidos (wsSysMobile)

        public bool grabarPedido(Pedido pedido)
        {

            int pIdPedidoRES;
            int pResultado;


                SqlCommand sqlCommand = new SqlCommand("wsSysMobileSPAMPedidosCabecera", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Transaction = sqlTransaction;

                completaParametrosCommandCabecera(sqlCommand, pedido);

                try 
                {
                    sqlCommand.ExecuteNonQuery();
                    pResultado = Int32.Parse(sqlCommand.Parameters["@pResultado"].Value.ToString());
                    pIdPedidoRES = Int32.Parse(sqlCommand.Parameters["@pIdPedidoRES"].Value.ToString());

                }
                catch (Exception e)
                {
                    sqlTransaction.Rollback();
                    return false;
                }

                SqlCommand sqlCommandDetalle ;

                for (int pos = 0; pos < pedido.detallePedido.Count; pos++) 
                {

                    sqlCommandDetalle = new SqlCommand("wsSysMobileSPAMPedidosDetalle", sqlConnection);
                    sqlCommandDetalle.CommandType = CommandType.StoredProcedure;
                    sqlCommandDetalle.Transaction = sqlTransaction;

                    completaParametrosCommandDetalle(sqlCommandDetalle, pIdPedidoRES, pedido.detallePedido[pos]);

                    try
                    {
                        sqlCommandDetalle.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        sqlTransaction.Rollback();
                        return false;
                    }

                }


            return true;
        }

        private void completaParametrosCommandCabecera(SqlCommand sqlCommand, Pedido pedido) 
        {

            sqlCommand.Parameters.Add("@pIdPedido", SqlDbType.Int, 4);
            sqlCommand.Parameters["@pIdPedido"].Value = null;

            sqlCommand.Parameters.Add("@pIdCliente", SqlDbType.NVarChar, 15);
            sqlCommand.Parameters["@pIdCliente"].Value = pedido.idCliente;

            sqlCommand.Parameters.Add("@pIdVendedor", SqlDbType.NVarChar, 4);
            sqlCommand.Parameters["@pIdVendedor"].Value = pedido.idVendedor;

            sqlCommand.Parameters.Add("@pFecha", SqlDbType.DateTime);
            sqlCommand.Parameters["@pFecha"].Value = Util.fechaYYYYMMDDtoDDMMYYYY(pedido.fecha);

            sqlCommand.Parameters.Add("@pTotalNeto", SqlDbType.Money);
            sqlCommand.Parameters["@pTotalNeto"].Value = pedido.totalNeto;

            sqlCommand.Parameters.Add("@pTotalFinal", SqlDbType.Money);
            sqlCommand.Parameters["@pTotalFinal"].Value = pedido.totalFinal;

            sqlCommand.Parameters.Add("@pResultado", SqlDbType.SmallInt);
            sqlCommand.Parameters["@pResultado"].Direction = ParameterDirection.Output;

            sqlCommand.Parameters.Add("@pMensaje", SqlDbType.VarChar, 255);
            sqlCommand.Parameters["@pMensaje"].Direction = ParameterDirection.Output;

            sqlCommand.Parameters.Add("@pIdPedidoRES", SqlDbType.Int);
            sqlCommand.Parameters["@pIdPedidoRES"].Direction = ParameterDirection.Output;

        }

        private void completaParametrosCommandDetalle(SqlCommand sqlCommandDetalle, int pIdPedidoRES,PedidoItem pedidoItem) 
        {
            sqlCommandDetalle.Parameters.Add("@pIdPedidoItem", SqlDbType.Int, 4);
            sqlCommandDetalle.Parameters["@pIdPedidoItem"].Value = null;

            sqlCommandDetalle.Parameters.Add("@pIdPedido", SqlDbType.Int, 4);
            sqlCommandDetalle.Parameters["@pIdPedido"].Value = pIdPedidoRES;

            sqlCommandDetalle.Parameters.Add("@pIdArticulo", SqlDbType.NVarChar, 25);
            sqlCommandDetalle.Parameters["@pIdArticulo"].Value = pedidoItem.idArticulo;

            sqlCommandDetalle.Parameters.Add("@pCantidad", SqlDbType.Float, 25);
            sqlCommandDetalle.Parameters["@pCantidad"].Value = pedidoItem.cantidad;

            sqlCommandDetalle.Parameters.Add("@pImporteUnitario", SqlDbType.Money);
            sqlCommandDetalle.Parameters["@pImporteUnitario"].Value = pedidoItem.importeUnitario;

            sqlCommandDetalle.Parameters.Add("@pPorcDescuento", SqlDbType.Float);
            sqlCommandDetalle.Parameters["@pPorcDescuento"].Value = pedidoItem.porcDto;

            sqlCommandDetalle.Parameters.Add("@pTotal", SqlDbType.Money);
            sqlCommandDetalle.Parameters["@pTotal"].Value = pedidoItem.total;
        }
        #endregion

        #region Grabacion Pedidos Sistema (V_MV_CPTE)

        public bool grabarPedidoV_MV_CPTE(Pedido pedido)
        {

            int pIdPedidoRES;
            int pResultado;


            SqlCommand sqlCommand = new SqlCommand("wsSysMobileSPPedidosV_MV_CPTE", sqlConnection);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.Transaction = sqlTransaction;

            completaParametrosCommandV_MV_Cpte(sqlCommand, pedido);

            try
            {
                sqlCommand.ExecuteNonQuery();
                pResultado = Int32.Parse(sqlCommand.Parameters["@pResultado"].Value.ToString());
                pIdPedidoRES = Int32.Parse(sqlCommand.Parameters["@pIdComprobanteRES"].Value.ToString());

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                sqlTransaction.Rollback();
                return false;
            }

            SqlCommand sqlCommandDetalle;

            for (int pos = 0; pos < pedido.detallePedido.Count; pos++)
            {

                sqlCommandDetalle = new SqlCommand("wsSysMobileSPPedidosV_MV_CPTEINSUMOS", sqlConnection);
                sqlCommandDetalle.CommandType = CommandType.StoredProcedure;
                sqlCommandDetalle.Transaction = sqlTransaction;

                completaParametrosCommandDetalleV_MV_CPTEInsumos(sqlCommandDetalle, pIdPedidoRES, pedido.detallePedido[pos]);

                try
                {
                    sqlCommandDetalle.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.Write(e.Message);
                    sqlTransaction.Rollback();
                    return false;
                }

            }


            return true;
        }

        private void completaParametrosCommandV_MV_Cpte(SqlCommand sqlCommand, Pedido pedido)
        {

            sqlCommand.Parameters.Add("@pCliente", SqlDbType.NVarChar, 15);
            sqlCommand.Parameters["@pCliente"].Value = pedido.idCliente;

            sqlCommand.Parameters.Add("@pVendedor", SqlDbType.NVarChar, 4);
            sqlCommand.Parameters["@pVendedor"].Value = pedido.idVendedor;

            sqlCommand.Parameters.Add("@pFecha", SqlDbType.DateTime);
            sqlCommand.Parameters["@pFecha"].Value = Util.fechaYYYYMMDDtoDDMMYYYY(pedido.fecha);

            sqlCommand.Parameters.Add("@pResultado", SqlDbType.SmallInt);
            sqlCommand.Parameters["@pResultado"].Direction = ParameterDirection.Output;

            sqlCommand.Parameters.Add("@pMensaje", SqlDbType.VarChar, 255);
            sqlCommand.Parameters["@pMensaje"].Direction = ParameterDirection.Output;

            sqlCommand.Parameters.Add("@pIdComprobanteRES", SqlDbType.Int);
            sqlCommand.Parameters["@pIdComprobanteRES"].Direction = ParameterDirection.Output;

        }
        
        private void completaParametrosCommandDetalleV_MV_CPTEInsumos(SqlCommand sqlCommandDetalle, int pIdPedidoRES, PedidoItem pedidoItem)
        {

            sqlCommandDetalle.Parameters.Add("@pIdCpte", SqlDbType.Int, 4);
            sqlCommandDetalle.Parameters["@pIdCpte"].Value = pIdPedidoRES;

            sqlCommandDetalle.Parameters.Add("@pIdArticulo", SqlDbType.NVarChar, 25);
            sqlCommandDetalle.Parameters["@pIdArticulo"].Value = pedidoItem.idArticulo;

            sqlCommandDetalle.Parameters.Add("@pCantidad", SqlDbType.Float, 25);
            sqlCommandDetalle.Parameters["@pCantidad"].Value = pedidoItem.cantidad;

            sqlCommandDetalle.Parameters.Add("@pImporteUnitario", SqlDbType.Money);
            sqlCommandDetalle.Parameters["@pImporteUnitario"].Value = pedidoItem.importeUnitario;

            sqlCommandDetalle.Parameters.Add("@pPorcDescuento", SqlDbType.Float);
            sqlCommandDetalle.Parameters["@pPorcDescuento"].Value = pedidoItem.porcDto;

        }

        #endregion

    }
}