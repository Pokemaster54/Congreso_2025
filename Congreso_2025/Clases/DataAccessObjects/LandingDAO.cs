using Congreso_2025.DataBase;
using System;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class LandingDAO
    {
        private readonly General general = new General();

        public LandingKpis ObtenerKpis()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var ahora = DateTime.Now;
                return new LandingKpis
                {
                    ActividadesProximas = db.Actividad.Count(a => a.hora_inicio >= ahora),
                    Ponentes = db.Ponente.Count(),
                    Ubicaciones = db.Ubicacion.Count(),
                    TiposActividad = db.Tipo_actividad.Count()
                };
            }
        }

        public NextActivityVM ObtenerSiguienteActividad()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var ahora = DateTime.Now;

                var q =
                    from a in db.Actividad
                    where a.hora_inicio >= ahora
                    join t in db.Tipo_actividad on a.id_tipo_actividad equals t.id_tipo_actividad
                    join e in db.Estado_actividad on a.id_estado_actividad equals e.id_estado_actividad
                    join p in db.Ponente on a.id_ponente equals p.id_ponente
                    join u in db.Ubicacion on a.id_ubicacion equals u.id_ubicacion
                    orderby a.hora_inicio ascending
                    select new NextActivityVM
                    {
                        IdActividad = a.id_actividad,
                        Nombre_actividad = a.Nombre_actividad,
                        FechaInicio = a.hora_inicio,
                        FechaFin = a.hora_fin,
                        Ponente = p.nombre_ponente,
                        Tipo = t.nombre_tipo_actividad,
                        Estado = e.nombre_estado_actividad,
                        Ubicacion = u.nombre_ubicacion
                    };

                return q.FirstOrDefault();
            }
        }
    }

    public class LandingKpis
    {
        public int ActividadesProximas { get; set; }
        public int Ponentes { get; set; }
        public int Ubicaciones { get; set; }
        public int TiposActividad { get; set; }
    }

    public class NextActivityVM
    {
        public string IdActividad { get; set; }
        public string Nombre_actividad { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Ponente { get; set; }
        public string Tipo { get; set; }
        public string Estado { get; set; }
        public string Ubicacion { get; set; }
    }
}
