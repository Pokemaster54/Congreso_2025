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

        public List<Carrera> ConsultarCarreras()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Carrera
                             .OrderBy(c => c.nombre_carrera)
                             .ToList();
                }
            }
            catch
            {
                return new List<Carrera>();
            }
        }

        public bool InsertarCarrera(string nombre_carrera)
        {
            string nuevoId = GenerarSiguienteCodigoCarrera();
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var nuevo = new Carrera
                    {
                        id_carrera = nuevoId,            // NVARCHAR(6)
                        nombre_carrera = nombre_carrera  // NVARCHAR(100)
                    };
                    db.Carrera.InsertOnSubmit(nuevo);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Carrera CargarDatosCarrera(Carrera c)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Carrera.FirstOrDefault(x => x.id_carrera == c.id_carrera);
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ActualizarCarrera(Carrera c)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Carrera.FirstOrDefault(x => x.id_carrera == c.id_carrera);
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
                    var cur = db.Carrera.FirstOrDefault(x => x.id_carrera == id);
                    if (cur == null) return false;

                    db.Carrera.DeleteOnSubmit(cur);
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
                    var numerosStr = db.Carrera
                        .Where(x => x.id_carrera != null && x.id_carrera.StartsWith("CR") && x.id_carrera.Length == 6)
                        .Select(x => x.id_carrera.Substring(2));

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
