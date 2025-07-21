using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string nombre = txtAddName.Text;
            string fechaNacimiento = txtDate.Text;
            string origen = txtAddOrigin.Text;
            string descripcion = txtAddDescription.Text;

            string swal = "Swal.fire('Añadido', 'Ponente añadido con éxito', 'success');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", swal, true);

            cleanAddNewForm();
        }


        private void cleanAddNewForm()
        {
            txtAddName.Text = "";
            txtDate.Text = "";
            txtAddOrigin.Text = "";
            txtAddDescription.Text = "";

        }
    }
}