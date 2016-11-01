using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wsSysMobileREST.Areas.Api.Models.Daos;

namespace wsSysMobileREST.Areas.Api.Models
{
    public class CsShared
    {
        private static CsShared csSharedInstancia;
        private static DataManager dataManager {get;set;}


        private CsShared() 
        {
            // Por tratarse de una clase Singleton, establezco el constructor como privado, 
            // para poder accederlo solo desde la clase
        }

        public static CsShared getInstance()
        {
            if (csSharedInstancia == null) 
            {
                csSharedInstancia = new CsShared();
            }

            return csSharedInstancia;
        }

        public void setDataManager(DataManager dataManager) 
        {
            CsShared.dataManager = dataManager;
        }

        public DataManager getDatamanager() 
        {
            return dataManager;
        }
    }
}