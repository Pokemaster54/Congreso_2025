using Congreso_2025.Clases.DataAccessObjects;
using System;
using System.Globalization;
using System.Web.UI;

namespace Congreso_2025
{
    public partial class WebLandingCatedratico : Page
    {
        private readonly CatedraticoLandingDAO catedraticoDAO = new CatedraticoLandingDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarDatos();
        }

        private void CargarDatos()
        {
            // KPIs globales (sin filtrar por usuario)
            var k = catedraticoDAO.ObtenerKpisGlobales();
            lblKpiActividades.Text = k.Actividades.ToString("N0");
            lblKpiPonentes.Text = k.Ponentes.ToString("N0");
            lblKpiUbicaciones.Text = k.Ubicaciones.ToString("N0");
            lblKpiTipos.Text = k.TiposActividad.ToString("N0");

            // Próxima actividad del congreso (no depende del usuario)
            var next = catedraticoDAO.ObtenerProximaActividad();
            if (next == null)
            {
                pnlNext.Visible = false;
                return;
            }

            lblNextNombre.Text = next.Nombre_actividad;
            lblNextFecha.Text = next.FechaInicio.ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            lblNextPonente.Text = next.Ponente;
            lblNextUbicacion.Text = next.Ubicacion;
            lblNextTipo.Text = next.Tipo;
            pnlNext.Visible = true;
        }
    }
}
