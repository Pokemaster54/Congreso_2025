using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using Congreso_2025.Clases;
using Congreso_2025.DataBase;

namespace Congreso_2025
{
    public partial class Login : Page
    {
        private readonly General general = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // 🔒 Si hay una cookie vieja, destrúyela para evitar redirección inmediata
                if (Request.Cookies[".ASPXAUTH"] != null)
                {
                    var cookie = new HttpCookie(".ASPXAUTH");
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    Response.Cookies.Add(cookie);
                    FormsAuthentication.SignOut();
                }

                // Ahora sí verifica sesión activa
                if (User.Identity.IsAuthenticated)
                {
                    using (var db = new MiLinQ(general.CadenaDeConexion))
                    {
                        var tipo = (from u in db.Usuario
                                    where u.nombre_usuario == User.Identity.Name
                                    select u.id_tipo_usuario).FirstOrDefault();

                        if (tipo != null && tipo.Trim().ToUpperInvariant() == "TU0003")
                            Response.Redirect("~/WebLandingCatedratico.aspx", true);
                        else
                            Response.Redirect("~/WebLanding.aspx", true);
                    }
                }
            }
        }


        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string usuario = txtUsuario.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(pass))
            {
                lblMensaje.Text = "Ingresa usuario y contraseña.";
                ShowMessage("Atención", lblMensaje.Text, "warning");
                return;
            }

            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var dato = (from u in db.Usuario
                            where u.nombre_usuario == usuario && u.password == pass
                            select new
                            {
                                u.nombre_usuario,
                                u.id_tipo_usuario
                            }).FirstOrDefault();

                if (dato == null)
                {
                    lblMensaje.Text = "Usuario o contraseña incorrectos.";
                    ShowMessage("Error", lblMensaje.Text, "error");
                    return;
                }

                // ✅ Registrar cookie de autenticación
                FormsAuthentication.SetAuthCookie(dato.nombre_usuario, false);

                string tipo = dato.id_tipo_usuario?.Trim().ToUpperInvariant() ?? "";
                string destino = "~/WebLanding.aspx"; // por defecto administrador

                // 🔹 Redirección por tipo de usuario
                switch (tipo)
                {
                    case "TU0001": // Administrador
                        destino = "~/WebLanding.aspx";
                        break;

                    case "TU0002": // Alumno
                        destino = "~/WebLandingAlumno.aspx";
                        break;

                    case "TU0003": // Catedrático
                        destino = "~/WebLandingCatedratico.aspx";
                        break;

                    default:
                        destino = "~/WebLanding.aspx";
                        break;
                }

                // 🔹 Redirigir directamente
                Response.Redirect(destino, true);
            }
        }

        private void ShowMessage(string title, string message, string icon)
        {
            string safeTitle = HttpUtility.JavaScriptStringEncode(title);
            string safeMsg = HttpUtility.JavaScriptStringEncode(message);
            string safeIcon = HttpUtility.JavaScriptStringEncode(icon);

            string js = $@"
                (function(){{
                    if (window.Swal) {{
                        Swal.fire({{
                            icon: '{safeIcon}',
                            title: '{safeTitle}',
                            text: '{safeMsg}'
                        }});
                    }} else {{
                        alert('{safeTitle}: {safeMsg}');
                    }}
                }})();";

            if (ScriptManager.GetCurrent(this) != null)
                ScriptManager.RegisterStartupScript(this, GetType(), "swalMsg", js, true);
            else
                ClientScript.RegisterStartupScript(GetType(), "swalMsg", js, true);
        }
    }
}
