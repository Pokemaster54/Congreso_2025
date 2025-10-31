using System;
using System.Linq;                     // NECESARIO para LINQ
using System.Web;
using System.Web.UI;
using Congreso_2025.Clases;            // General
using Congreso_2025.DataBase;          // MiLinQ

namespace Congreso_2025
{
    public partial class ResetPassword : Page
    {
        private readonly General general = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblTemporal.Text = "";

            string user = txtUsuario.Text.Trim();
            string p1 = txtNueva.Text;        // no Trim por si aceptas espacios
            string p2 = txtConfirmar.Text;

            if (string.IsNullOrWhiteSpace(user) || string.IsNullOrWhiteSpace(p1) || string.IsNullOrWhiteSpace(p2))
            {
                lblMsg.Text = "Completa usuario y nueva contraseña.";
                Show("Atención", lblMsg.Text, "warning");
                return;
            }
            if (p1 != p2)
            {
                lblMsg.Text = "Las contraseñas no coinciden.";
                Show("Atención", lblMsg.Text, "warning");
                return;
            }
            if (p1.Length < 4)
            {
                lblMsg.Text = "Usa al menos 4 caracteres.";
                Show("Atención", lblMsg.Text, "warning");
                return;
            }

            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var u = db.Usuario.FirstOrDefault(x => x.nombre_usuario == user);
                // Si tu DataContext usa plural: db.Usuarios.FirstOrDefault(...)

                if (u == null)
                {
                    lblMsg.Text = "Usuario no encontrado.";
                    Show("Error", lblMsg.Text, "error");
                    return;
                }

                u.password = p1;
                db.SubmitChanges();

                lblTemporal.CssClass = "mt-3 d-block text-success";
                lblTemporal.Text = "Contraseña actualizada. Ahora puedes iniciar sesión.";
                Show("Éxito", lblTemporal.Text, "success");
            }
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblTemporal.Text = "";

            string user = txtUsuario.Text.Trim();
            if (string.IsNullOrWhiteSpace(user))
            {
                lblMsg.Text = "Escribe tu usuario para generar la temporal.";
                Show("Atención", lblMsg.Text, "warning");
                return;
            }

            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var u = db.Usuario.FirstOrDefault(x => x.nombre_usuario == user);
                // Si tu DataContext usa plural: db.Usuarios.FirstOrDefault(...)

                if (u == null)
                {
                    lblMsg.Text = "Usuario no encontrado.";
                    Show("Error", lblMsg.Text, "error");
                    return;
                }

                string temp = GenerarTemporal(8);
                u.password = temp;
                db.SubmitChanges();

                lblTemporal.CssClass = "mt-3 d-block text-success";
                lblTemporal.Text = $"Tu contraseña temporal es: {HttpUtility.HtmlEncode(temp)}";
                Show("Temporal generada", $"Tu contraseña temporal es: {temp}", "success");
            }
        }

        private string GenerarTemporal(int len)
        {
            // Aleatorio criptográfico, evitando caracteres ambiguos
            const string chars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnpqrstuvwxyz23456789";
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var data = new byte[len];
            rng.GetBytes(data);
            var buf = new char[len];
            for (int i = 0; i < len; i++) buf[i] = chars[data[i] % chars.Length];
            return new string(buf);
        }

        /// <summary> Muestra SweetAlert si existe; si no, usa alert() y deja labels pobladas. </summary>
        private void Show(string title, string message, string icon)
        {
            string t = HttpUtility.JavaScriptStringEncode(title);
            string m = HttpUtility.JavaScriptStringEncode(message);
            string i = HttpUtility.JavaScriptStringEncode(icon);

            string js = $@"(function(){{
  if (window.Swal) {{
    Swal.fire({{ icon: '{i}', title: '{t}', text: '{m}' }});
  }} else {{
    alert('{t}: {m}');
  }}
}})();";

            if (ScriptManager.GetCurrent(this) != null)
                ScriptManager.RegisterStartupScript(this, GetType(), "swalResetMsg", js, true);
            else
                ClientScript.RegisterStartupScript(GetType(), "swalResetMsg", js, true);
        }
    }
}
