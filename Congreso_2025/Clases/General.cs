using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Congreso_2025.Clases
{
    public class General
    {

        // Cadena de conexión a la base de datos, dejarla privada, obtenerla mediante un método getter
        private string _cadenaDeConexion = "Data Source=congresoudeo2025.c9ykky2iegp1.us-east-2.rds.amazonaws.com;Initial Catalog=Congreso;User ID=Admin;Password=udeocongreso0.;Encrypt=False";
        
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