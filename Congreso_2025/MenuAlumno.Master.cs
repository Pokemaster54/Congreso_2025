using System;
using System.Web;
using System.Web.Security;

namespace Congreso_2025
{
    public partial class MenuAlumno : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Verifica si hay sesión activa
                if (Context.User == null || !Context.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
        }
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
    
}
