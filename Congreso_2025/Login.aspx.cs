using Congreso_2025.Clases;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class Login : System.Web.UI.Page
    {
        General general = new General();
        protected void Page_Load(object sender, EventArgs e)
        {

            // Si ya está autenticado, redirige de una vez
            if (User?.Identity != null && User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/Default.aspx", true);
            }
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;

            var userName = txtUsuario.Text?.Trim();
            var pass = txtPassword.Text ?? string.Empty;

            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(pass))
            {
                lblMsg.Text = "Ingresa usuario y contraseña.";
                return;
            }

            try
            {
                using (var ctx = new MiLinQ(general.CadenaDeConexion))
                {
                    // Ajusta los nombres de entidades/propiedades si tu dbml usa otros
                    var usuario = ctx.Usuario
                        .FirstOrDefault(u => u.nombre_usuario == userName);

                    if (usuario == null)
                    {
                        lblMsg.Text = "Usuario o contraseña no válidos.";
                        return;
                    }

                    // TODO: si manejas hash/salt, reemplaza esta comparación simple
                    var passwordOk = string.Equals(usuario.password, pass, StringComparison.Ordinal);
                    if (!passwordOk)
                    {
                        lblMsg.Text = "Usuario o contraseña no válidos.";
                        return;
                    }

                    // Cargar el tipo (join a Tipo_usuario)
                    var tipo = ctx.Tipo_usuario.FirstOrDefault(t => t.id_tipo_usuario == usuario.id_tipo_usuario);
                    var nombreTipo = tipo?.nombre_tipo?.Trim() ?? string.Empty;

                    // Guarda datos de sesión útiles
                    Session["UsuarioId"] = usuario.id_usuario;
                    Session["NombreUsuario"] = usuario.nombre_usuario;
                    Session["TipoUsuarioId"] = usuario.id_tipo_usuario;
                    Session["TipoUsuarioNombre"] = nombreTipo;

                    // Autenticar con FormsAuthentication (cookie)
                    FormsAuthentication.SetAuthCookie(usuario.nombre_usuario, false);

                    // Redirigir según tipo
                    var redirect = GetRedirectUrlByTipo(Convert.ToInt32(usuario.id_tipo_usuario), nombreTipo);
                    Response.Redirect(redirect, true);
                }
            }
            catch (Exception ex)
            {
                // En producción, registra el error. No expongas detalles al usuario.
                lblMsg.Text = "Ocurrió un error al iniciar sesión. Inténtalo de nuevo.";
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private string GetRedirectUrlByTipo(int idTipo, string nombreTipo)
        {
            // Opción A: por nombre (más robusto cuando los IDs cambian)
            var rol = (nombreTipo ?? "").ToLowerInvariant();

            if (rol.Contains("admin")) return "~/Admin/Inicio.aspx";
            if (rol.Contains("catedr")) return "~/Catedratico/Inicio.aspx";
            if (rol.Contains("inscri")) return "~/Inscripciones/Inicio.aspx";  // quien inscribe
            if (rol.Contains("alumn")) return "~/Alumno/Inicio.aspx";

            // Opción B: por ID (si tienes IDs fijos). Descomenta/ajusta si es tu caso:
            // switch (idTipo)
            // {
            //     case 1: return "~/Admin/Inicio.aspx";
            //     case 2: return "~/Catedratico/Inicio.aspx";
            //     case 3: return "~/Inscripciones/Inicio.aspx";
            //     case 4: return "~/Alumno/Inicio.aspx";
            // }

            // Fallback
            return "~/Default.aspx";
        }
    }
}