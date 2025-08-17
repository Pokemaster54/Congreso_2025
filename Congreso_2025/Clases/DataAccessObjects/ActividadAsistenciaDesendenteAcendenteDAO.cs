using Congreso_2025.Clases.DataClasses;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class ActividadAsistenciaDesendenteAcendenteDAO
    {
        private General general = new General();
        public ActividadAsistenciaDesendenteAcendenteDAO() { }


        //para hacer el listado de asistencias por acividad en orden ascendente o descendente
        //hice pruebas en el cual le ingrese de forma manual al dropdounlist los valores es decir Ascendente y Descendente
        public List<ActividadDAO> ObtenerActividadesOrdenadas(string orden)
        {

                try
                {
                    using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                    {

                        var asistenciaActividades = from asignacion in miLinQ.asignacion_actividad
                                                    join carActividad in miLinQ.carrera_actividad on asignacion.id_carrera_actividad equals carActividad.id_carrera_actividad
                                                    join actividad in miLinQ.Actividad on carActividad.id_actividad equals actividad.id_actividad
                                                    group asignacion by new
                                                    {
                                                        actividad.id_actividad,
                                                        actividad.Nombre_actividad
                                                    } into grupo

                                                    select new ActividadDAO
                                                    {
                                                        Cantidad = grupo.Count(),
                                                        ID = grupo.Key.id_actividad,
                                                        Actividad = grupo.Key.Nombre_actividad,
                                                    };

                    
                    if (orden == "Ascendente") // Ascendente
                            asistenciaActividades = asistenciaActividades.OrderBy(a => a.Cantidad);
                        else // Descendente por defecto
                            asistenciaActividades = asistenciaActividades.OrderByDescending(a => a.Cantidad);

                    return asistenciaActividades.ToList();
                }
                }
                catch
                {
                    return null;
                }
        }       
    }
}