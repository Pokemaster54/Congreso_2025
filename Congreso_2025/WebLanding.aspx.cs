using Congreso_2025.Clases.DataAccessObjects;
using System;
using System.Globalization;
using System.Web.UI;

namespace Congreso_2025
{
    public partial class WebLanding : Page
    {
        private readonly LandingDAO landingDAO = new LandingDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDatos();
            }
        }

        private void CargarDatos()
        {
            // KPIs
            var k = landingDAO.ObtenerKpis();
            lblKpiProximas.Text = k.ActividadesProximas.ToString("N0");
            lblKpiPonentes.Text = k.Ponentes.ToString("N0");
            lblKpiUbicaciones.Text = k.Ubicaciones.ToString("N0");
            lblKpiTipos.Text = k.TiposActividad.ToString("N0");

            // Próxima actividad
            var next = landingDAO.ObtenerSiguienteActividad();
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
