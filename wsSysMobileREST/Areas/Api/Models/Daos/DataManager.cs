using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models.Daos
{
    public class DataManager
    {
        private static SqlConnection sqlConnection = null;
        private string cadenaConexion;

        private static DaoArticulo daoArticulo;
        private static DaoCliente daoCliente;
        private static DaoCtaCte daoCtaCte;
        private static DaoDeposito daoDeposito;
        private static DaoPedido daoPedido;
        private static DaoRegistro daoRegistro;
        private static DaoRubro daoRubro;
        private static DaoStock daoStock;
        private static DaoVendedor daoVendedor;

        private static int tamanioPaginaRegistros;
        private static bool grabaPedidoVMVCpte;

        public DataManager() 
        {
            if (sqlConnection == null) 
            {
                cadenaConexion = ConfigurationManager.ConnectionStrings["WSSysMobile"].ToString();

                sqlConnection = new SqlConnection(cadenaConexion);
                sqlConnection.Open();

                setDaoArticulo (new DaoArticulo(sqlConnection));
                setDaoCliente (new DaoCliente(sqlConnection));
                setDaoCtaCte (new DaoCtaCte (sqlConnection));
                setDaoDeposito (new DaoDeposito (sqlConnection));
                setDaoRegistro( new DaoRegistro (sqlConnection));
                setDaoRubro( new DaoRubro (sqlConnection));
                setDaoStock (new DaoStock (sqlConnection));
                setDaoVendedor (new DaoVendedor (sqlConnection));
                setDaoPedido(new DaoPedido(sqlConnection));

                try
                {
                    tamanioPaginaRegistros = Int16.Parse(ConfigurationManager.AppSettings.Get("pageSize"));
                }
                catch (NullReferenceException e)
                {
                    tamanioPaginaRegistros = 100;
                }

                try
                {
                    grabaPedidoVMVCpte = ConfigurationManager.AppSettings.Get("grabaPedidoVMVCpte").Equals("true");
                }
                catch (NullReferenceException e)
                {
                    grabaPedidoVMVCpte = false;
                }

            }

        }

        public bool getGrabaPedidoVMVCpte()
        {
            return grabaPedidoVMVCpte;
        }
        
        public int getTamanioPaginacionRegistros()
        {
            return tamanioPaginaRegistros;
        }

        private void setDaoArticulo(DaoArticulo daoArticulo)
        {
            DataManager.daoArticulo = daoArticulo;
        }
 
        public DaoArticulo getDaoArticulo() 
        {
            return DataManager.daoArticulo;
        }


        private void setDaoCliente(DaoCliente daoCliente)
        {
            DataManager.daoCliente = daoCliente;
        }

        public DaoCliente getDaoCliente()
        {
            return DataManager.daoCliente;
        }


        private void setDaoCtaCte(DaoCtaCte daoCtaCte)
        {
            DataManager.daoCtaCte = daoCtaCte;
        }

        public DaoCtaCte getDaoCtaCte()
        {
            return DataManager.daoCtaCte;
        }

        private void setDaoDeposito(DaoDeposito daoDeposito)
        {
            DataManager.daoDeposito = daoDeposito;
        }

        public DaoDeposito getDaoDeposito()
        {
            return DataManager.daoDeposito;
        }
        
        private void setDaoPedido(DaoPedido daoPedido)
        {
            DataManager.daoPedido = daoPedido;
        }

        public DaoPedido getDaoPedido()
        {
            return DataManager.daoPedido;
        }

        private void setDaoRegistro(DaoRegistro daoRegistro)
        {
            DataManager.daoRegistro = daoRegistro;
        }

        public DaoRegistro getDaoRegistro()
        {
            return DataManager.daoRegistro;
        }

        private void setDaoRubro(DaoRubro daoRubro)
        {
            DataManager.daoRubro = daoRubro;
        }

        public DaoRubro getDaoRubro()
        {
            return DataManager.daoRubro;
        }
        
        private void setDaoStock(DaoStock daoStock)
        {
            DataManager.daoStock = daoStock;
        }

        public DaoStock getDaoStock()
        {
            return DataManager.daoStock;
        }

        private void setDaoVendedor(DaoVendedor daoVendedor)
        {
            DataManager.daoVendedor= daoVendedor;
        }

        public DaoVendedor getDaoVendedor()
        {
            return DataManager.daoVendedor;
        }
    

    }
}