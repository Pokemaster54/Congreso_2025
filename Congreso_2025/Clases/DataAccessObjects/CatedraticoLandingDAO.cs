using Congreso_2025.Clases.DTOs;
using Congreso_2025.DataBase;
using System;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class CatedraticoLandingDAO
    {
        General general = new General();

        /// <summary>
        /// Obtiene los KPIs globales del sistema para los catedráticos.
        /// </summary>
        public (int Actividades, int Ponentes, int Ubicaciones, int TiposActividad) ObtenerKpisGlobales()
        {
            MiLinQ milinq = new MiLinQ(general.CadenaDeConexion); // Usa tu DataContext existente

            try
            {
                int actividades = milinq.Actividad.Count();
                int ponentes = milinq.Ponente.Count();
                int ubicaciones = milinq.Ubicacion.Count();
                int tipos = milinq.Tipo_actividad.Count();

                return (actividades, ponentes, ubicaciones, tipos);
            }
            catch
            {
                return (0, 0, 0, 0);
            }
        }

        /// <summary>
        /// Devuelve la próxima actividad del congreso según la fecha/hora actual.
        /// </summary>
        public ActividadProximaDTO ObtenerProximaActividad()
        {
        MiLinQ milinq = new MiLinQ(general.CadenaDeConexion); // Usa tu DataContext existente

            try
            {
                var next = (from a in milinq.Actividad
                            join p in milinq.Ponente on a.id_ponente equals p.id_ponente
                            join u in milinq.Ubicacion on a.id_ubicacion equals u.id_ubicacion
                            join t in milinq.Tipo_actividad on a.id_tipo_actividad equals t.id_tipo_actividad
                            where a.hora_inicio > DateTime.Now
                            orderby a.hora_inicio ascending
                            select new ActividadProximaDTO
                            {
                                Nombre_actividad = a.Nombre_actividad,
                                FechaInicio = a.hora_inicio,
                                FechaFin = a.hora_fin,
                                Ponente = p.nombre_ponente,
                                Ubicacion = u.nombre_ubicacion,
                                Tipo = t.nombre_tipo_actividad
                            }).FirstOrDefault();

                return next;
            }
            catch
            {
                return null;
            }
        }
    }
}
