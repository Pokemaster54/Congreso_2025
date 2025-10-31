using System;
using System.Linq;
using System.Web;
using Congreso_2025.DataBase;
using Congreso_2025.Clases;

namespace Congreso_2025
{
    public partial class MenuCatedratico : System.Web.UI.MasterPage
    {
        private readonly General general = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Context.User == null || !Context.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/Login.aspx", true);
                    return;
                }

                // Validar tipo de usuario (solo CATEDRATICO)
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var tipo = (from u in db.Usuario
                                where u.nombre_usuario == Context.User.Identity.Name
                                select u.id_tipo_usuario).FirstOrDefault();

                    if (tipo == null || tipo.Trim().ToUpperInvariant() != "TU0003")
                    {
                        Response.Redirect("~/WebLanding.aspx", true);
                        return;
                    }
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            Session.Abandon();

            var cookie = new HttpCookie(".ASPXAUTH");
            cookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(cookie);

            Response.Redirect("~/Login.aspx", true);
        }
    }
}
