using System;

namespace Congreso_2025.Clases.DTOs
{
    public class ActividadProximaDTO
    {
        public string Nombre_actividad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ponente { get; set; }
        public string Ubicacion { get; set; }
        public string Tipo { get; set; }
    }
}
