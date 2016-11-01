using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class DaoCliente
    {
        private SqlConnection sqlConnection;


        public DaoCliente(SqlConnection sqlConnection) 
        {
            this.sqlConnection = sqlConnection; 
        }

        public List<Cliente> getClientes(int? pagina)
        {

            List<Cliente> lista = new List<Cliente>();
            string sql;
            
            if (pagina==null)
                sql = "SELECT codigo,codigoOpcional,razon_social,calle,numero,piso,departamento,localidad,numero_Documento,iva,clase, descuento, cpteDefault,idVendedor,telefono,mail FROM wsSysMobileClientes";
            else
                sql = "EXEC dbo.wsSysMobileSPPaginacionClientes " + pagina + "," + CsShared.getInstance().getDatamanager().getTamanioPaginacionRegistros();

            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            string Tmpdireccion;
            Cliente cliente = null;

            while (reader.Read())
            {

                cliente = new Cliente();
                cliente.codigo = reader.GetString(0);
                cliente.codigoOpcional = reader.GetString(1).Trim();
                cliente.razonSocial = reader.GetString(2).Trim();

                Tmpdireccion = reader.GetString(3).Trim() + " " + reader.GetString(4).Trim();

                if (!reader.IsDBNull(5))
                {
                    if (!reader.GetString(5).Trim().Equals(""))
                    {

                        Tmpdireccion = Tmpdireccion + " Piso: " + reader.GetString(5).Trim();
                    }
                }
                if (!reader.IsDBNull(6))
                {
                    if (!reader.GetString(6).Trim().Equals(""))
                    {
                        Tmpdireccion = Tmpdireccion + " Dpto: " + reader.GetString(6).Trim();
                    }
                }
                cliente.calleNroPisoDpto = Tmpdireccion;

                cliente.localidad = reader.GetString(7).Trim();
                cliente.cuit = reader.GetString(8).Trim();
                cliente.iva = (byte)(int.Parse((string)reader.GetString(9)));
                cliente.claseDePrecio = (byte)reader.GetInt16(10);
                cliente.porcDto = reader.GetDouble(11);
                cliente.cpteDefault = reader.GetString(12).Trim();
                cliente.idVendedor = reader.GetString(13).Trim();
                cliente.telefono = (reader.IsDBNull(14)) ? "" : reader.GetString(14).Trim();
                cliente.email = (reader.IsDBNull(15)) ? "" : reader.GetString(15).Trim();


                lista.Add(cliente);
            }

            reader.Close();

            return lista;
        }
        
        public SqlDataReader obtenerDatosClienteSysComPorCodigo(string cuentaContable,string campos) 
        {
            
            string sql = "SELECT " + campos + " FROM VT_CLIENTES WHERE codigo = '" + cuentaContable + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            return reader;
        }
        public Cliente obtenerClientePorCodigo(string cuentaContable) 
        {
            Cliente cliente = null;
            string sql;

            sql = "SELECT codigo,codigoOpcional,razon_social,calle,numero,piso,departamento,localidad,numero_Documento,iva,clase, descuento, cpteDefault,idVendedor,telefono,mail FROM wsSysMobileClientes WHERE codigo = '" + cuentaContable + "'";
            SqlCommand cmd = new SqlCommand(sql, sqlConnection);
            SqlDataReader reader = cmd.ExecuteReader();

            string Tmpdireccion;

            if (reader.Read())
            {
                cliente = new Cliente();

                cliente.codigo = reader.GetString(0);
                cliente.codigoOpcional = reader.GetString(1).Trim();
                cliente.razonSocial = reader.GetString(2).Trim();

                Tmpdireccion = reader.GetString(3).Trim() + " " + reader.GetString(4).Trim();

                if (!reader.IsDBNull(5))
                {
                    if (!reader.GetString(5).Trim().Equals(""))
                    {

                        Tmpdireccion = Tmpdireccion + " Piso: " + reader.GetString(5).Trim();
                    }
                }
                if (!reader.IsDBNull(6))
                {
                    if (!reader.GetString(6).Trim().Equals(""))
                    {
                        Tmpdireccion = Tmpdireccion + " Dpto: " + reader.GetString(6).Trim();
                    }
                }
                cliente.calleNroPisoDpto = Tmpdireccion;

                cliente.localidad = reader.GetString(7).Trim();
                cliente.cuit = reader.GetString(8).Trim();
                cliente.iva = (byte)(int.Parse((string)reader.GetString(9)));
                cliente.claseDePrecio = (byte)reader.GetInt16(10);
                cliente.porcDto = reader.GetDouble(11);
                cliente.cpteDefault = reader.GetString(12).Trim();
                cliente.idVendedor = reader.GetString(13).Trim();
                cliente.telefono = (reader.IsDBNull(14)) ? "" : reader.GetString(14).Trim();
                cliente.email = (reader.IsDBNull(15)) ? "" : reader.GetString(15).Trim();
            }

            return cliente;

        }
    }
}