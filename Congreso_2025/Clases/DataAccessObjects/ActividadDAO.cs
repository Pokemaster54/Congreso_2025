using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class ActividadDAO
    {
        private readonly General general = new General();
        public ActividadDAO() { }

        public List<ActividadListado> ConsultarActividadesListado()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var q = from a in db.Actividad
                        join t in db.Tipo_actividad on a.id_tipo_actividad equals t.id_tipo_actividad
                        join e in db.Estado_actividad on a.id_estado_actividad equals e.id_estado_actividad
                        join p in db.Ponente on a.id_ponente equals p.id_ponente
                        join u in db.Ubicacion on a.id_ubicacion equals u.id_ubicacion
                        orderby a.hora_inicio descending
                        select new ActividadListado
                        {
                            id_actividad = a.id_actividad,
                            Nombre_actividad = a.Nombre_actividad,
                            nombre_tipo_actividad = t.nombre_tipo_actividad,
                            nombre_estado_actividad = e.nombre_estado_actividad,
                            nombre_ponente = p.nombre_ponente,
                            nombre_ubicacion = u.nombre_ubicacion,
                            hora_inicio = a.hora_inicio,
                            hora_fin = a.hora_fin,

                            inscritos = (
                                from asg in db.asignacion_actividad
                                join ca in db.carrera_actividad
                                    on asg.id_carrera_actividad equals ca.id_carrera_actividad
                                where ca.id_actividad == a.id_actividad
                                select asg.id_alumno
                            ).Count()
                        };

                return q.ToList();
            }
        }

        public Actividad CargarActividadPorId(string id)
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                return db.Actividad.FirstOrDefault(x => x.id_actividad == id);
            }
        }

        public bool InsertarActividad(string nombre, string idTipo, string idEstado, string idPonente, string idUbicacion,
                                      DateTime inicio, DateTime fin, int? inscritos)
        {
            string nuevoId = GenerarSiguienteCodigoActividad();
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var act = new Actividad
                    {
                        id_actividad = nuevoId,
                        Nombre_actividad = nombre,
                        id_tipo_actividad = idTipo,
                        id_estado_actividad = idEstado,
                        id_ponente = idPonente,
                        id_ubicacion = idUbicacion,
                        hora_inicio = inicio,
                        hora_fin = fin,

                        inscritos = inscritos
                    };
                    db.Actividad.InsertOnSubmit(act);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ActualizarActividad(Actividad a)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Actividad.FirstOrDefault(x => x.id_actividad == a.id_actividad);
                    if (cur == null) return false;

                    cur.Nombre_actividad = a.Nombre_actividad;
                    cur.id_tipo_actividad = a.id_tipo_actividad;
                    cur.id_estado_actividad = a.id_estado_actividad;
                    cur.id_ponente = a.id_ponente;
                    cur.id_ubicacion = a.id_ubicacion;
                    cur.hora_inicio = a.hora_inicio;
                    cur.hora_fin = a.hora_fin;
                    cur.inscritos = a.inscritos;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarActividad(string id)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Actividad.FirstOrDefault(x => x.id_actividad == id);
                    if (cur == null) return false;

                    db.Actividad.DeleteOnSubmit(cur);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public List<Tipo_actividad> ObtenerTiposActividad()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
                return db.Tipo_actividad.OrderBy(x => x.nombre_tipo_actividad).ToList();
        }

        public List<Estado_actividad> ObtenerEstadosActividad()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
                return db.Estado_actividad.OrderBy(x => x.nombre_estado_actividad).ToList();
        }

        public List<Ponente> ObtenerPonentes()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
                return db.Ponente.OrderBy(x => x.nombre_ponente).ToList();
        }

        public List<Ubicacion> ObtenerUbicaciones()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
                return db.Ubicacion.OrderBy(x => x.nombre_ubicacion).ToList();
        }

        public string GenerarSiguienteCodigoActividad()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                try
                {
                    var nums = db.Actividad
                        .Where(x => x.id_actividad.StartsWith("AT") && x.id_actividad.Length == 6)
                        .Select(x => x.id_actividad.Substring(2));

                    int ultimo = nums.Any() ? nums.AsEnumerable().Select(int.Parse).Max() : 0;
                    int sig = ultimo + 1;
                    return $"AT{sig:D4}";
                }
                catch
                {
                    return "AT0001";
                }
            }
        }
    }

    public class ActividadListado
    {
        public string id_actividad { get; set; }
        public string Nombre_actividad { get; set; }
        public string nombre_tipo_actividad { get; set; }
        public string nombre_estado_actividad { get; set; }
        public string nombre_ponente { get; set; }
        public string nombre_ubicacion { get; set; }
        public DateTime hora_inicio { get; set; }
        public DateTime hora_fin { get; set; }

        public int inscritos { get; set; }
    }
}