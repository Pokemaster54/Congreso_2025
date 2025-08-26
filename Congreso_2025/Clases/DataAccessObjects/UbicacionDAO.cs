using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class UbicacionDAO
    {
        private General general = new General();
        public UbicacionDAO() { }

        public List<Ubicacion> ConsultarUbicaciones()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Ubicacion
                             .OrderBy(u => u.nombre_ubicacion)
                             .ToList();
                }
            }
            catch
            {
                return new List<Ubicacion>();
            }
        }

        public bool InsertarUbicacion(string nombre_ubicacion, string indicaciones)
        {
            string nuevoId = GenerarSiguienteCodigoUbicacion();
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var nuevo = new Ubicacion
                    {
                        id_ubicacion = nuevoId,           // NVARCHAR(6)
                        nombre_ubicacion = nombre_ubicacion, // NVARCHAR(100)
                        indicaciones = indicaciones       // NVARCHAR(1000)
                    };
                    db.Ubicacion.InsertOnSubmit(nuevo);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public Ubicacion CargarDatosUbicacion(Ubicacion u)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Ubicacion.FirstOrDefault(x => x.id_ubicacion == u.id_ubicacion);
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ActualizarUbicacion(Ubicacion u)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Ubicacion.FirstOrDefault(x => x.id_ubicacion == u.id_ubicacion);
                    if (cur == null) return false;

                    cur.nombre_ubicacion = u.nombre_ubicacion;
                    cur.indicaciones = u.indicaciones;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarUbicacion(string id)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Ubicacion.FirstOrDefault(x => x.id_ubicacion == id);
                    if (cur == null) return false;

                    db.Ubicacion.DeleteOnSubmit(cur);
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
        /// Genera IDs tipo "UB0001" (2 letras + 4 dígitos) acorde a NVARCHAR(6).
        /// </summary>
        public string GenerarSiguienteCodigoUbicacion()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                try
                {
                    var numerosStr = db.Ubicacion
                        .Where(x => x.id_ubicacion != null && x.id_ubicacion.StartsWith("UB") && x.id_ubicacion.Length == 6)
                        .Select(x => x.id_ubicacion.Substring(2));

                    int ultimo = 0;
                    if (numerosStr.Any())
                    {
                        ultimo = numerosStr.AsEnumerable().Select(s => int.Parse(s)).Max();
                    }

                    int siguiente = ultimo + 1;
                    return $"UB{siguiente:D4}";
                }
                catch
                {
                    return "UB0001";
                }
            }
        }
    }
}
