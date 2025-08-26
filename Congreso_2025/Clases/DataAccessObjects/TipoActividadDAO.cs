using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class TipoActividadDAO
    {
        private General general = new General();
        public TipoActividadDAO() { }

        public List<Tipo_actividad> ConsultarTiposActividad()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Tipo_actividad
                             .OrderBy(t => t.nombre_tipo_actividad)
                             .ToList();
                }
            }
            catch
            {
                return new List<Tipo_actividad>();
            }
        }

        public bool InsertarTipoActividad(string nombre_tipo_actividad)
        {
            string nuevoId = GenerarSiguienteCodigoTipoActividad();
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var nuevo = new Tipo_actividad
                    {
                        id_tipo_actividad = nuevoId,           // NVARCHAR(6)
                        nombre_tipo_actividad = nombre_tipo_actividad // NVARCHAR(50)
                    };
                    db.Tipo_actividad.InsertOnSubmit(nuevo);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Tipo_actividad CargarDatosTipoActividad(Tipo_actividad t)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Tipo_actividad.FirstOrDefault(x => x.id_tipo_actividad == t.id_tipo_actividad);
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ActualizarTipoActividad(Tipo_actividad t)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Tipo_actividad.FirstOrDefault(x => x.id_tipo_actividad == t.id_tipo_actividad);
                    if (cur == null) return false;

                    cur.nombre_tipo_actividad = t.nombre_tipo_actividad;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarTipoActividad(string id)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Tipo_actividad.FirstOrDefault(x => x.id_tipo_actividad == id);
                    if (cur == null) return false;

                    db.Tipo_actividad.DeleteOnSubmit(cur);
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
        /// Genera IDs tipo "TA0001" (2 letras + 4 dígitos), acorde a NVARCHAR(6).
        /// </summary>
        public string GenerarSiguienteCodigoTipoActividad()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                try
                {
                    var numerosStr = db.Tipo_actividad
                        .Where(x => x.id_tipo_actividad != null && x.id_tipo_actividad.StartsWith("TA") && x.id_tipo_actividad.Length == 6)
                        .Select(x => x.id_tipo_actividad.Substring(2));

                    int ultimo = 0;
                    if (numerosStr.Any())
                    {
                        ultimo = numerosStr.AsEnumerable().Select(s => int.Parse(s)).Max();
                    }

                    int siguiente = ultimo + 1;
                    return $"TA{siguiente:D4}";
                }
                catch
                {
                    return "TA0001";
                }
            }
        }
    }
}
