using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class EstadoAlumnoDAO
    {
        private General general = new General();
        public EstadoAlumnoDAO() { }

        public List<Estado_alumno> ConsultarEstadosAlumno()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Estado_alumno
                             .OrderBy(e => e.nombre_estado)
                             .ToList();
                }
            }
            catch
            {
                return new List<Estado_alumno>();
            }
        }

        public bool InsertarEstadoAlumno(string nombre_estado)
        {
            string nuevoId = GenerarSiguienteCodigoEstado();
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var nuevo = new Estado_alumno
                    {
                        id_estado = nuevoId,           // NVARCHAR(6)
                        nombre_estado = nombre_estado  // NVARCHAR(100)
                    };
                    db.Estado_alumno.InsertOnSubmit(nuevo);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Estado_alumno CargarDatosEstado(Estado_alumno e)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Estado_alumno.FirstOrDefault(x => x.id_estado == e.id_estado);
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ActualizarEstadoAlumno(Estado_alumno e)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Estado_alumno.FirstOrDefault(x => x.id_estado == e.id_estado);
                    if (cur == null) return false;

                    cur.nombre_estado = e.nombre_estado;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarEstadoAlumno(string id)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Estado_alumno.FirstOrDefault(x => x.id_estado == id);
                    if (cur == null) return false;

                    db.Estado_alumno.DeleteOnSubmit(cur);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                // Puede fallar por FK desde Alumno
                return false;
            }
        }

        /// <summary>
        /// Genera IDs tipo "EA0001" (2 letras + 4 dígitos), acorde a NVARCHAR(6).
        /// </summary>
        public string GenerarSiguienteCodigoEstado()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                try
                {
                    var numerosStr = db.Estado_alumno
                        .Where(x => x.id_estado != null && x.id_estado.StartsWith("EA") && x.id_estado.Length == 6)
                        .Select(x => x.id_estado.Substring(2));

                    int ultimo = 0;
                    if (numerosStr.Any())
                    {
                        ultimo = numerosStr.AsEnumerable().Select(s => int.Parse(s)).Max();
                    }

                    int siguiente = ultimo + 1;
                    return $"EA{siguiente:D4}";
                }
                catch
                {
                    return "EA0001";
                }
            }
        }
    }
}
