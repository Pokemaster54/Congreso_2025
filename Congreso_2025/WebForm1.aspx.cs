using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Congreso_2025.DataBase;
using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.Clases.DataClasses;

namespace Congreso_2025
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        General general = new General();
        PonenteDAO ponenteDAO = new PonenteDAO();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                CargarPonentesEnTabla();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string nombre = txtAddName.Text;
            string fechaNacimiento = txtDate.Text;
            string origen = txtAddOrigin.Text;
            string descripcion = txtAddDescription.Text;
            addNewPonente(nombre, fechaNacimiento, origen, descripcion);
            cleanAddNewForm();
        }


        private void cleanAddNewForm()
        {
            txtAddName.Text = "";
            txtDate.Text = "";
            txtAddOrigin.Text = "";
            txtAddDescription.Text = "";

        }


        private void CargarPonentesEnTabla()
        {
            PonenteDAO ponenteDao = new PonenteDAO();
            try
            {
                List<Ponente> listaDePonentes = ponenteDao.ConsultarPonentes();

                UserRepeater.DataSource = listaDePonentes;

                UserRepeater.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<div class='alert alert-danger'>Error al cargar ponentes: {ex.Message}</div>");
            }
        }
        private void addNewPonente(string nombre, string fechaNacimiento, string origen, string descripcion)
        {
         PonenteDC ponente = new PonenteDC(nombre, Convert.ToDateTime(fechaNacimiento), origen, descripcion);
            string swal = "";
            if (ponenteDAO.InsertarPonente(ponente))
            {
                swal = "Swal.fire('Exito', 'Ponente añadido con potencia', 'success');";
            }
            else
            {
                swal = "Swal.fire('Error', 'No se pudo añadir el ponente', 'error');";
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", swal, true);

        }

        protected void btnGuardarEdicion_Click(object sender, EventArgs e)
        {
            
        }


        protected void btnConfirmarEliminacion_Click(object sender, EventArgs e)
        {

        }

    }

}