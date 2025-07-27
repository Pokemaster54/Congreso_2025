using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Congreso_2025
{
    public partial class TipoUsu : System.Web.UI.Page
    {
       
        public void limpia()
        {
            TextBoxCodTipo.Enabled=ButtonEditar.Enabled=ButtonEliminar.Enabled=false; 
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            limpia();
        }
    }
}