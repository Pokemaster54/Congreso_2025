using System;
using System.Linq;
using System.Web.UI;
using Congreso_2025.Clases;
using Congreso_2025.DataBase;

namespace Congreso_2025
{
    public partial class WebLandingAlumno : Page
    {
        private readonly General general = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarResumenAlumno();
                CargarNombreAlumno();

            }
        }

        private string UsuarioActual => Context.User.Identity.Name;

        private void CargarResumenAlumno()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var alumno = db.Alumno.FirstOrDefault(a => a.Usuario.nombre_usuario == UsuarioActual);
                if (alumno == null)
                {
                    lblKpiProximas.Text = "0";
                    lblKpiPonentes.Text = "0";
                    lblKpiUbicaciones.Text = "0";
                    lblKpiTipos.Text = "0";
                    lblNextNombre.Text = "—";
                    lblNextPonente.Text = "—";
                    lblNextUbicacion.Text = "—";
                    lblNextTipo.Text = "—";
                    lblNextFecha.Text = "—";
                    return;
                }

                var actividades =
                    from aa in db.asignacion_actividad
                    join ca in db.carrera_actividad on aa.id_carrera_actividad equals ca.id_carrera_actividad
                    join a in db.Actividad on ca.id_actividad equals a.id_actividad
                    join p in db.Ponente on a.id_ponente equals p.id_ponente
                    join u in db.Ubicacion on a.id_ubicacion equals u.id_ubicacion
                    join t in db.Tipo_actividad on a.id_tipo_actividad equals t.id_tipo_actividad
                    where aa.id_alumno == alumno.id_alumno
                    select new
                    {
                        a.Nombre_actividad,
                        a.hora_inicio,
                        a.hora_fin,
                        p.nombre_ponente,
                        u.nombre_ubicacion,
                        t.nombre_tipo_actividad
                    };

                var lista = actividades.ToList();

                lblKpiProximas.Text = lista.Count(x => x.hora_inicio > DateTime.Now).ToString("N0");
                lblKpiPonentes.Text = lista.Select(x => x.nombre_ponente).Distinct().Count().ToString("N0");
                lblKpiUbicaciones.Text = lista.Select(x => x.nombre_ubicacion).Distinct().Count().ToString("N0");
                lblKpiTipos.Text = lista.Select(x => x.nombre_tipo_actividad).Distinct().Count().ToString("N0");

                var next = lista
                    .Where(x => x.hora_inicio > DateTime.Now)
                    .OrderBy(x => x.hora_inicio)
                    .FirstOrDefault();

                if (next != null)
                {
                    lblNextNombre.Text = next.Nombre_actividad;
                    lblNextFecha.Text = $"{next.hora_inicio:dd/MM/yyyy HH:mm} - {next.hora_fin:HH:mm}";
                    lblNextPonente.Text = next.nombre_ponente;
                    lblNextUbicacion.Text = next.nombre_ubicacion;
                    lblNextTipo.Text = next.nombre_tipo_actividad;
                }
                else
                {
                    lblNextNombre.Text = "Sin próximas actividades";
                    lblNextFecha.Text = "—";
                    lblNextPonente.Text = "—";
                    lblNextUbicacion.Text = "—";
                    lblNextTipo.Text = "—";
                }
            }
        }
        private void CargarNombreAlumno()
        {
            string usuario = User.Identity.Name;

            if (string.IsNullOrEmpty(usuario)) return;

            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var alumno = (from a in db.Alumno
                              join u in db.Usuario on a.id_usuario equals u.id_usuario
                              where u.nombre_usuario == usuario
                              select new
                              {
                                  Nombre = a.nombres_alumno,
                                  Apellido = a.apellidos_alumno
                              }).FirstOrDefault();

                if (alumno != null)
                {
                    lblNombreAlumno.Text = alumno.Nombre + " " + alumno.Apellido;
                }
                else
                {
                    lblNombreAlumno.Text = usuario;
                }
            }
        }
    }
}
