using Congreso_2025.Clases;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class CarreraDAO
    {
        private General general = new General();
        public CarreraDAO() { }

        public List<id_carrera> ConsultarCarreras()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.id_carrera
                             .OrderBy(c => c.nombre_carrera)
                             .ToList();
                }
            }
            catch
            {
                return new List<id_carrera>();
            }
        }

        public bool InsertarCarrera(string nombre_carrera)
        {
            string nuevoId = GenerarSiguienteCodigoCarrera();
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var nuevo = new id_carrera
                    {
                        id_carreras = nuevoId,            // NVARCHAR(6)
                        nombre_carrera = nombre_carrera  // NVARCHAR(100)
                    };
                    db.id_carrera.InsertOnSubmit(nuevo);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public id_carrera CargarDatosCarrera(id_carrera c)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.id_carrera.FirstOrDefault(x => x.id_carreras == c.id_carreras);
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ActualizarCarrera(id_carrera c)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.id_carrera.FirstOrDefault(x => x.id_carreras == c.id_carreras);
                    if (cur == null) return false;

                    cur.nombre_carrera = c.nombre_carrera;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarCarrera(string id)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.id_carrera.FirstOrDefault(x => x.id_carreras == id);
                    if (cur == null) return false;

                    db.id_carrera.DeleteOnSubmit(cur);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                // Puede fallar por FK desde Alumno o carrera_actividad
                return false;
            }
        }

        /// <summary>
        /// Genera IDs tipo "CR0001" (2 letras + 4 dígitos), acorde a NVARCHAR(6).
        /// Cambia el prefijo si ya usas otro estándar.
        /// </summary>
        public string GenerarSiguienteCodigoCarrera()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                try
                {
                    var numerosStr = db.id_carrera
                        .Where(x => x.id_carreras != null && x.id_carreras.StartsWith("CR") && x.id_carreras.Length == 6)
                        .Select(x => x.id_carreras.Substring(2));

                    int ultimo = 0;
                    if (numerosStr.Any())
                    {
                        ultimo = numerosStr.AsEnumerable().Select(s => int.Parse(s)).Max();
                    }

                    int siguiente = ultimo + 1;
                    return $"CR{siguiente:D4}";
                }
                catch
                {
                    return "CR0001";
                }
            }
        }
    }
}
