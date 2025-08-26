using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class EstadoActividadDAO
    {
        private General general = new General();
        public EstadoActividadDAO() { }

        public List<Estado_actividad> ConsultarEstadosActividad()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Estado_actividad
                             .OrderBy(e => e.nombre_estado_actividad)
                             .ToList();
                }
            }
            catch
            {
                return new List<Estado_actividad>();
            }
        }

        public bool InsertarEstadoActividad(string nombre_estado_actividad)
        {
            string nuevoId = GenerarSiguienteCodigoEstadoActividad();
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var nuevo = new Estado_actividad
                    {
                        id_estado_actividad = nuevoId,               // NVARCHAR(6)
                        nombre_estado_actividad = nombre_estado_actividad // NVARCHAR(50)
                    };
                    db.Estado_actividad.InsertOnSubmit(nuevo);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Estado_actividad CargarDatosEstadoActividad(Estado_actividad e)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Estado_actividad.FirstOrDefault(x => x.id_estado_actividad == e.id_estado_actividad);
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ActualizarEstadoActividad(Estado_actividad e)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Estado_actividad.FirstOrDefault(x => x.id_estado_actividad == e.id_estado_actividad);
                    if (cur == null) return false;

                    cur.nombre_estado_actividad = e.nombre_estado_actividad;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarEstadoActividad(string id)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Estado_actividad.FirstOrDefault(x => x.id_estado_actividad == id);
                    if (cur == null) return false;

                    db.Estado_actividad.DeleteOnSubmit(cur);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                // Puede fallar por FK desde Actividad
                return false;
            }
        }

        /// <summary>
        /// Genera IDs tipo "AC0001" (2 letras + 4 dígitos), acorde a NVARCHAR(6).
        /// </summary>
        public string GenerarSiguienteCodigoEstadoActividad()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                try
                {
                    var numerosStr = db.Estado_actividad
                        .Where(x => x.id_estado_actividad != null && x.id_estado_actividad.StartsWith("AC") && x.id_estado_actividad.Length == 6)
                        .Select(x => x.id_estado_actividad.Substring(2));

                    int ultimo = 0;
                    if (numerosStr.Any())
                    {
                        ultimo = numerosStr.AsEnumerable().Select(s => int.Parse(s)).Max();
                    }

                    int siguiente = ultimo + 1;
                    return $"AC{siguiente:D4}";
                }
                catch
                {
                    return "AC0001";
                }
            }
        }
    }
}
