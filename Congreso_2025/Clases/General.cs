using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Congreso_2025.Clases
{
    public class General
    {
        private string _cadenaDeConexion = "Data Source=sql1002.site4now.net;Initial Catalog=db_abf0a2_proyecto;User ID=db_abf0a2_proyecto_admin;Password=admin123;Encrypt=False";

        public string CadenaDeConexion
        {
            get { return _cadenaDeConexion; }
        }

        public General()
        {
            // Constructor vacío
        }
    }
}