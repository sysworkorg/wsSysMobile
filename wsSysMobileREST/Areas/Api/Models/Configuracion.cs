using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class Configuracion
    {
        /// <summary>
        /// Establecer valor
        /// </summary>
        /// <param name="seccion">Sección</param>
        /// <param name="clave">Clave</param>
        /// <param name="valor">Valor a establecer</param>
        public static void setValue(string seccion, string clave, string valor)
        {

            System.Configuration.Configuration config =
            ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            

            /*System.Configuration.Configuration config =
            ConfigurationManager.OpenExeConfiguration(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location + "\\web.config" ));
            */

            //Borramos la configuración actual
            config.AppSettings.Settings.Remove(seccion + "." + clave);

            config.Save(ConfigurationSaveMode.Modified);
            //Force a reload of the changed section.
            ConfigurationManager.RefreshSection("appSettings");
            //Grabamos la configuración nueva
            config.AppSettings.Settings.Add(seccion + "." + clave, valor);

            // Save the configuration file.
            config.Save(ConfigurationSaveMode.Modified);
            
            //Force a reload of the changed section.
            ConfigurationManager.RefreshSection("appSettings");
        }

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <param name="seccion">Sección</param>
        /// <param name="clave">Clave</param>
        /// <param name="predeterminado">Valor predeterminado (en caso de no encontrarse)</param>
        /// <returns>Valor encontrado</returns>
        public static string GetValue(string seccion, string clave, string predeterminado)
        {

            string _return = "";

            try
            {
                _return = ConfigurationManager.AppSettings[seccion + "." + clave];
                if (_return == "")
                {
                    _return = predeterminado;
                }

                return (_return);
            }
            catch
            {
                return (predeterminado);
            }
        }

        /// <summary>
        /// Obtener valor
        /// </summary>
        /// <param name="seccion">Sección</param>
        /// <param name="clave">Clave</param>
        /// <returns>Valor encontrado</returns>
        public static string GetValue(string seccion, string clave)
        {

            string _return = "";
            try
            {
                _return = ConfigurationManager.AppSettings[seccion + "." + clave];
                if (_return == "")
                {
                    _return = "";
                }

                return (_return);
            }
            catch
            {
                return ("");
            }


        } 
 
    
    
    }
}