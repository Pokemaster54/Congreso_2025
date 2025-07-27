using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Congreso_2025.Clases.DataClasses
{
    public class PonenteDC
    {
        private string idPonente;
        private string nombre;
        private DateTime fechaNacimiento;
        private string origen;
        private string descripcion;
        public PonenteDC(string nombre, DateTime fechaNacimiento, string origen, string descripcion)
        {
            Nombre = nombre;
            FechaNacimiento = fechaNacimiento;
            Origen = origen;
            Descripcion = descripcion;
        }

        public PonenteDC(string idPonente, string nombre, DateTime fechaNacimiento, string origen, string descripcion)
        {
            IdPonente = idPonente;
            Nombre = nombre;
            FechaNacimiento = fechaNacimiento;
            Origen = origen;
            Descripcion = descripcion;
        }

        public string IdPonente
        {
            get { return idPonente; }
            set { idPonente = value; }
        }
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }
        public DateTime FechaNacimiento
        {
            get { return fechaNacimiento; }
            set { fechaNacimiento = value; }
        }
        public string Origen
        {
            get { return origen; }
            set { origen = value; }
        }
        public string Descripcion
        {
            get { return descripcion; }
            set { descripcion = value; }
        }
    }


}