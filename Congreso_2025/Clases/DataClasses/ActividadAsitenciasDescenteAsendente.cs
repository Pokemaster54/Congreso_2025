using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Congreso_2025.Clases.DataClasses
{
    public class ActividadAsitenciasDescenteAsendente
    {
        private string idActividad;
        private string nombreActividad;
        private string orden;
        private DateTime fecha;
        public ActividadAsitenciasDescenteAsendente(string idActividad, string nombreActividad, int numeroAsistencias)
        {
            IdActividad = idActividad;
            NombreActividad = nombreActividad;
            Orden = orden;
            Fecha = fecha;

        }
        public string IdActividad
        {
            get { return idActividad; }
            set { idActividad = value; }
        }
        public string Orden
        {
            get { return orden; }
            set { orden = value; }
        }
        public string NombreActividad
            {
            get { return nombreActividad; }
            set { nombreActividad = value; }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }
    }
    public class ActividadDAO
    {
        public int Cantidad { get; set; }
        public string ID { get; set; }
        public string Actividad { get; set; }
        public DateTime fecha { get; set; }

    }
}