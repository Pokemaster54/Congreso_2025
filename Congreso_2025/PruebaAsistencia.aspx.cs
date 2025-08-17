using Congreso_2025.Clases;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Congreso_2025.Asistencia;

namespace Congreso_2025
{
    public partial class PruebaAsistencia : System.Web.UI.Page
    {
        Asistencia asistencia = new Asistencia();  
        General cadena=new General();
        protected void Page_Load(object sender, EventArgs e)
        {
           
            
            if (!IsPostBack)
            {

            }

        }


        protected void DropDownListElentos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ObtieneValores valores= new ObtieneValores();
           valores.ValorSel = Convert.ToInt32(DropDownListElemento.SelectedValue);
            valores.Cadena= cadena.CadenaDeConexion;
            valores.FechaAct = CalendarFecha.SelectedDate;
            var objetos1 = CargaDropLista.OptieneValor1(valores);
                DropDownListSeleccion.Items.Clear();
                DropDownListSeleccion.Items.AddRange(objetos1.ToArray());         
        }

        protected void DropDownListSeleccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObtieneValores valores = new ObtieneValores();
                valores.ValorSel = Convert.ToInt32(DropDownListElemento.SelectedValue);
                valores.Cadena = cadena.CadenaDeConexion;
                valores.FechaAct =  CalendarFecha.SelectedDate;
                valores.ValorDrop2 = DropDownListSeleccion.SelectedValue;
                valores.ValorDrop2 = DropDownListSeleccion.SelectedValue;
                DataTable Lista = GridLista.ObtieneValor2(valores);
                GridViewLista.DataSource = Lista;
                GridViewLista.DataBind();
            }
        }
    }
}