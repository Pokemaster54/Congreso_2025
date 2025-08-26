using Congreso_2025.Clases;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace Congreso_2025
{
    public class Asistencia
    {
        private General general = new General();
        public class CargaDropLista
        {
            //obtiene el valor del primer dropdownlist (Carrera) para que cargue el segundo dropdownlist (Actividad)
            public static List<ListItem> OptieneValor1(ObtieneValores valores)//los valores de dropSeleccion ya estan establecidos, obtiene el valor del primer dropdownlist para que cargue el segundo dropdownlist
            {
                var Objetos2 = new List<ListItem>();
                using (MiLinQ miLinQ = new MiLinQ(valores.Cadena))
                {
                    switch (valores.ValorSel)
                    {
                        case 1://no permite cargar las carreras por problemas en linQ hay que volver a insertar la tabla carreras

                            //var consultaCar = from car in miLinQ.id_carrera
                            var consultaCar = from car in miLinQ.carrera_actividad
                                              select new
                                              {
                                                  id = car.id_carrera,
                                                  Carrera = car.id_carrera,
                                              };
                            foreach (var fila in consultaCar)
                            {
                                Objetos2.Add(new ListItem(fila.Carrera, fila.id));
                            }

                            break;
                        case 2:

                            {
                                var consultaTipo = from tipo in miLinQ.Tipo_actividad
                                                   select new
                                                   {
                                                       id = tipo.id_tipo_actividad,
                                                       Tipo = tipo.nombre_tipo_actividad
                                                   };

                                foreach (var fila in consultaTipo)
                                {
                                    Objetos2.Add(new ListItem(fila.Tipo, fila.id));
                                }
                            }
                            break;
                        case 3:
                            var consultaEvento = from act in miLinQ.Actividad
                                                 select new
                                                 {
                                                     id = act.id_actividad,
                                                     Actividad = act.Nombre_actividad
                                                 };

                            foreach (var fila in consultaEvento)
                            {
                                Objetos2.Add(new ListItem(fila.Actividad, fila.id));
                            }
                            break;
                        case 4:
                            var consultaFecha = from act in miLinQ.Actividad
                                                where act.hora_inicio == valores.FechaAct
                                                select act;
                            break;
                    }

                }
                return Objetos2;
            }
        }
        public class ObtieneValores
        {
            public int ValorSel { get; set; } = 0;
            public string Cadena { get; set; } = "";
            public DateTime FechaAct { get; set; } = DateTime.Now;
            public string ValorDrop2 { get; set; } = "";
            public ObtieneValores() { }
        }

        public class GridLista
        {
            //obtiene los valores del segundo dropdownlist (Actividad) y carga el valor de las asistencias.
            public static DataTable ObtieneValor2(ObtieneValores Valores)
            {
                DataTable dt = new DataTable();
                using (MiLinQ miLinQ = new MiLinQ(Valores.Cadena))
                {
                    DataRow nuevaFila = dt.NewRow();
                    switch (Valores.ValorSel)
                    {
                        case 1:
                            dt.Columns.Add("Carne");
                            dt.Columns.Add("Nombres");
                            dt.Columns.Add("Asistencia");
                            dt.Columns.Add("Carrera");
                            dt.Columns.Add("Fecha Actividad");
                            var consultaCarrera = from asig in miLinQ.asignacion_actividad
                                                  join Caract in miLinQ.carrera_actividad on asig.id_carrera_actividad equals Caract.id_carrera_actividad
                                                  //join car in miLinQ.id_carrera on Caract.id_carrera equals car.id_carreras
                                                  join act in miLinQ.Actividad on Caract.id_actividad equals act.id_actividad
                                                  join tipoAct in miLinQ.Tipo_actividad on act.id_tipo_actividad equals tipoAct.id_tipo_actividad
                                                  where Caract.id_carrera == Valores.ValorDrop2
                                                  select new
                                                  {
                                                      carne = asig.id_alumno,
                                                      Nombre = asig.Alumno.nombres_alumno + " " + asig.Alumno.apellidos_alumno,
                                                      Asistencia1 = asig.bit,
                                                      Carrera = Caract.id_carrera,
                                                      Fecha_Actividad = act.hora_inicio
                                                  };

                            foreach (var fila in consultaCarrera)
                            {
                                nuevaFila = dt.NewRow();
                                nuevaFila[0] = fila.carne;
                                nuevaFila[1] = fila.Nombre;
                                nuevaFila[2] = fila.Asistencia1;
                                nuevaFila[3] = fila.Carrera;
                                nuevaFila[4] = fila.Fecha_Actividad;
                                dt.Rows.Add(nuevaFila);
                            }
                            break;
                        case 2:
                            dt.Columns.Add("Carne");
                            dt.Columns.Add("Nombres");
                            dt.Columns.Add("Asistencia");
                            dt.Columns.Add("Carrera");
                            dt.Columns.Add("Fecha Actividad");
                            var consultaTipo = from asig in miLinQ.asignacion_actividad
                                               join Caract in miLinQ.carrera_actividad on asig.id_carrera_actividad equals Caract.id_carrera_actividad
                                               //join car in miLinQ.id_carrera on Caract.id_carrera equals car.id_carreras
                                               join act in miLinQ.Actividad on Caract.id_actividad equals act.id_actividad
                                               join tipoAct in miLinQ.Tipo_actividad on act.id_tipo_actividad equals tipoAct.id_tipo_actividad
                                               where tipoAct.id_tipo_actividad == Valores.ValorDrop2
                                               select new
                                               {
                                                   carne = asig.id_alumno,
                                                   Nombre = asig.Alumno.nombres_alumno + " " + asig.Alumno.apellidos_alumno,
                                                   Asistencia1 = asig.bit,
                                                   Carrera = Caract.id_carrera,
                                                   Fecha_Actividad = act.hora_inicio
                                               };

                            foreach (var fila in consultaTipo)
                            {
                                nuevaFila = dt.NewRow();
                                nuevaFila[0] = fila.carne;
                                nuevaFila[1] = fila.Nombre;
                                nuevaFila[2] = fila.Asistencia1;
                                nuevaFila[3] = fila.Carrera;
                                nuevaFila[4] = fila.Fecha_Actividad;
                                dt.Rows.Add(nuevaFila);
                            }
                            break;
                        case 3:
                            dt.Columns.Add("Carne");
                            dt.Columns.Add("Nombres");
                            dt.Columns.Add("Asistencia");
                            dt.Columns.Add("Carrera");
                            dt.Columns.Add("Fecha Actividad");
                            var consultaActividad = from asig in miLinQ.asignacion_actividad
                                                    join Caract in miLinQ.carrera_actividad on asig.id_carrera_actividad equals Caract.id_carrera_actividad
                                                    //join car in miLinQ.id_carrera on Caract.id_carrera equals car.id_carreras
                                                    join act in miLinQ.Actividad on Caract.id_actividad equals act.id_actividad
                                                    join tipoAct in miLinQ.Tipo_actividad on act.id_tipo_actividad equals tipoAct.id_tipo_actividad
                                                    where act.id_actividad == Valores.ValorDrop2
                                                    select new
                                                    {
                                                        carne = asig.id_alumno,
                                                        Nombre = asig.Alumno.nombres_alumno + " " + asig.Alumno.apellidos_alumno,
                                                        Asistencia1 = asig.bit,
                                                        Carrera = Caract.id_carrera,
                                                        Fecha_Actividad = act.hora_inicio
                                                    };

                            foreach (var fila in consultaActividad)
                            {
                                nuevaFila = dt.NewRow();
                                nuevaFila[0] = fila.carne;
                                nuevaFila[1] = fila.Nombre;
                                nuevaFila[2] = fila.Asistencia1;
                                nuevaFila[3] = fila.Carrera;
                                nuevaFila[4] = fila.Fecha_Actividad;
                                dt.Rows.Add(nuevaFila);
                            }
                            break;
                        case 4:
                            dt.Columns.Add("Carne");
                            dt.Columns.Add("Nombres");
                            dt.Columns.Add("Asistencia");
                            dt.Columns.Add("Carrera");
                            dt.Columns.Add("Fecha Actividad");
                            var consultaFecha = from asig in miLinQ.asignacion_actividad
                                                join Caract in miLinQ.carrera_actividad on asig.id_carrera_actividad equals Caract.id_carrera_actividad
                                                //join car in miLinQ.id_carrera on Caract.id_carrera equals car.id_carreras
                                                join act in miLinQ.Actividad on Caract.id_actividad equals act.id_actividad
                                                join tipoAct in miLinQ.Tipo_actividad on act.id_tipo_actividad equals tipoAct.id_tipo_actividad
                                                where act.hora_inicio == Valores.FechaAct
                                                select new
                                                {
                                                    carne = asig.id_alumno,
                                                    Nombre = asig.Alumno.nombres_alumno + " " + asig.Alumno.apellidos_alumno,
                                                    Asistencia1 = asig.bit,
                                                    Carrera = Caract.id_carrera,
                                                    Fecha_Actividad = act.hora_inicio
                                                };


                            foreach (var fila in consultaFecha)
                            {

                                nuevaFila = dt.NewRow();
                                nuevaFila[0] = fila.carne;
                                nuevaFila[1] = fila.Nombre;
                                nuevaFila[2] = fila.Asistencia1;
                                nuevaFila[3] = fila.Carrera;
                                nuevaFila[4] = fila.Fecha_Actividad;
                                dt.Rows.Add(nuevaFila);
                            }
                            break;
                        default:

                            break;
                    }

                    return dt;
                }



            }
        }
    }
}

    