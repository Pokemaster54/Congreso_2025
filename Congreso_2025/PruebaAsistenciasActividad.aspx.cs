using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Congreso_2025.Clases.DataClasses;

namespace Congreso_2025
{
    public partial class PruebaAsistenciasActividad : System.Web.UI.Page
    {
        General general = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                try
                {

                }
                catch
                {
                    
                }
            }
        }

        public void cargaLista()
        {
            ActividadAsistenciaDesendenteAcendenteDAO actividadDOA= new ActividadAsistenciaDesendenteAcendenteDAO();
            if(DropDownListOrden.Text!="")
            {
                string orden= DropDownListOrden.Text;
                List<ActividadDAO> listaActividades = actividadDOA.ObtenerActividadesOrdenadas(orden);
                GridViewLista.DataSource= listaActividades;
                GridViewLista.DataBind();
            }
        }
        protected void DropDownListOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargaLista();
        }
    }
}