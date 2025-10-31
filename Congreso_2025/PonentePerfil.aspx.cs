using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using Congreso_2025.Clases;
using Congreso_2025.DataBase;

namespace Congreso_2025
{
    public partial class PonentePerfil : Page
    {
        private readonly General general = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];
                if (string.IsNullOrEmpty(id))
                {
                    lblMensaje.Text = "No se especificó un ponente.";
                    return;
                }
                CargarPerfil(id);
            }
        }

        private void CargarPerfil(string idPonente)
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var ponente = db.Ponente.FirstOrDefault(p => p.id_ponente == idPonente);
                if (ponente == null)
                {
                    lblMensaje.Text = "El ponente no existe o fue eliminado.";
                    return;
                }

                lblNombre.Text = ponente.nombre_ponente;
                lblOrigen.Text = ponente.Origen;
                lblNacimiento.Text = ponente.fecha_nacimiento.ToString("dd/MM/yyyy");
                lblDescripcion.Text = ponente.descripcion;
                pnlPerfil.Visible = true;

                // Actividades del ponente
                var acts = (from a in db.Actividad
                            where a.id_ponente == idPonente
                            select new
                            {
                                a.Nombre_actividad,
                                a.hora_inicio,
                                a.hora_fin,
                                Carreras = (from ca in db.carrera_actividad
                                            join c in db.Carrera on ca.id_carrera equals c.id_carrera
                                            where ca.id_actividad == a.id_actividad
                                            select c.nombre_carrera).ToList()
                            }).ToList();

                if (acts.Count == 0)
                {
                    litActividades.Text = "<p class='text-muted fst-italic'>No tiene actividades asignadas.</p>";
                }
                else
                {
                    StringBuilder sb = new StringBuilder("<ul>");
                    foreach (var a in acts)
                    {
                        sb.Append($"<li><strong>{a.Nombre_actividad}</strong> — {a.hora_inicio:dd/MM HH:mm} a {a.hora_fin:HH:mm}<br>");
                        sb.Append($"<span class='text-muted'><small>Carreras: {string.Join(", ", a.Carreras)}</small></span></li>");
                    }
                    sb.Append("</ul>");
                    litActividades.Text = sb.ToString();
                }
            }
        }
    }
}
