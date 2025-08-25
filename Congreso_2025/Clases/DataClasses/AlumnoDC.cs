using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Congreso_2025.Clases.DataClasses
{
    public class AlumnoDC
    {
        public int IdAlumno { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellido { get; set; } = "";
        public DateTime? FechaNacimiento { get; set; }
        public string Email { get; set; } = "";
        public bool? Activo { get; set; }
    }
}