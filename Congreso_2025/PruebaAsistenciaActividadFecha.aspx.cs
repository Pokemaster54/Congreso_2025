using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.Clases.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class PruebaAsistenciaActividadFecha : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void cargaLista()
        {
            ActividadAsistenciaDesendenteAcendenteDAO actividadDOA = new ActividadAsistenciaDesendenteAcendenteDAO();
            if (DropDownListOrden.Text != "")
            {
                DateTime fecha=CalendarFecha.SelectedDate;
                string orden = DropDownListOrden.Text;
                List<ActividadDAO> listaActividades = actividadDOA.ObtenerActividadesPorFechas(orden,fecha);
                GridViewLista.DataSource = listaActividades;
                GridViewLista.DataBind();
            }
        }

        protected void DropDownListOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaLista();
        }

        protected void CalendarFecha_SelectionChanged(object sender, EventArgs e)
        {
            cargaLista();
        }
    }
}