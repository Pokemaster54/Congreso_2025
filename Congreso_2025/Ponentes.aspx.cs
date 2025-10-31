using System;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Collections.Generic;
using Congreso_2025.Clases;
using Congreso_2025.DataBase;

namespace Congreso_2025
{
    public partial class Ponentes : Page
    {
        private readonly General general = new General();

        // DTO liviano solo para renderizar HTML
        private class ActividadResumen
        {
            public string Nombre { get; set; }
            public DateTime Inicio { get; set; }
            public DateTime Fin { get; set; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarreras();
                CargarPonentes();
            }
        }

        private void CargarCarreras()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                ddlCarrera.DataSource = db.Carrera.OrderBy(c => c.nombre_carrera).ToList();
                ddlCarrera.DataTextField = "nombre_carrera";
                ddlCarrera.DataValueField = "id_carrera";
                ddlCarrera.DataBind();
                ddlCarrera.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todas las carreras --", ""));
            }
        }

        private void CargarPonentes()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                string carreraSel = ddlCarrera.SelectedValue;

                // 1) Ponentes con sus actividades (guardamos también los IDs de carrera para filtrar)
                var query = from p in db.Ponente
                            select new
                            {
                                p.id_ponente,
                                p.nombre_ponente,
                                p.Origen,
                                Actividades = (from a in db.Actividad
                                               where a.id_ponente == p.id_ponente
                                               select new
                                               {
                                                   a.Nombre_actividad,
                                                   a.id_actividad,
                                                   a.hora_inicio,
                                                   a.hora_fin,
                                                   // Usamos IDs de carrera para filtrar de forma segura
                                                   CarrerasIds = (from ca in db.carrera_actividad
                                                                  where ca.id_actividad == a.id_actividad
                                                                  select ca.id_carrera).ToList()
                                               }).ToList()
                            };

                // 2) Filtrar por carrera si aplica (usando id_carrera)
                if (!string.IsNullOrEmpty(carreraSel))
                {
                    query = query.Where(p =>
                        p.Actividades.Any(a => a.CarrerasIds.Contains(carreraSel)));
                }

                // 3) Proyección final: convertir actividades al DTO ActividadResumen
                var lista = query
                    .ToList()
                    .Select(p => new
                    {
                        p.id_ponente,
                        p.nombre_ponente,
                        p.Origen,
                        ActividadesHTML = GenerarActividadesHTML(
                            p.Actividades.Select(a => new ActividadResumen
                            {
                                Nombre = a.Nombre_actividad,
                                Inicio = a.hora_inicio,
                                Fin = a.hora_fin
                            }))
                    })
                    .ToList();

                rptPonentes.DataSource = lista;
                rptPonentes.DataBind();

                lblResultado.Text = lista.Count > 0
                    ? $"{lista.Count:N0} ponente(s) encontrados."
                    : "No se encontraron ponentes con esas condiciones.";
            }
        }

        // Render simple de actividades
        private string GenerarActividadesHTML(IEnumerable<ActividadResumen> actividades)
        {
            var list = actividades?.ToList();
            if (list == null || list.Count == 0)
                return "<span class='text-muted fst-italic'>Sin actividades asignadas</span>";

            StringBuilder sb = new StringBuilder();
            foreach (var act in list)
            {
                string fecha = $"{act.Inicio:dd/MM HH:mm} - {act.Fin:HH:mm}";
                sb.Append($"<div class='badge-actividad d-block mb-1'>{act.Nombre} <small class='text-muted'>({fecha})</small></div>");
            }
            return sb.ToString();
        }

        protected void ddlCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarPonentes();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlCarrera.SelectedIndex = 0;
            CargarPonentes();
        }
    }
}
