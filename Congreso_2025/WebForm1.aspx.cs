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

namespace Congreso_2025
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        General general = new General();
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

        private void addNewPonente(string nombre, string fechaNacimiento, string origen, string descripcion)
        {
            try
            {
                using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                {
                    var query = from ponente in miLinQ.Ponentes
                                where ponente.Nombre == nombre && ponente.FechaNacimiento == fechaNacimiento
                                select ponente;
                }

            }
            catch
            {

            }
        }


    }
}
}