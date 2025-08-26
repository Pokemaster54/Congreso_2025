using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class WebUsuario : System.Web.UI.Page
    {
        General general = new General();
        UsuarioDAO usuarioDAO = new UsuarioDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTiposUsuario();
                CargarUsuariosEnTabla();
                Session["idUsuario"] = "";
            }
        }

        private void CargarTiposUsuario()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var tipos = db.Tipo_usuario
                                  .OrderBy(t => t.nombre_tipo)
                                  .Select(t => new { t.id_tipo_usuario, t.nombre_tipo })
                                  .ToList();
                    ddlTipoUsuario.DataSource = tipos;
                    ddlTipoUsuario.DataTextField = "nombre_tipo";
                    ddlTipoUsuario.DataValueField = "id_tipo_usuario";
                    ddlTipoUsuario.DataBind();
                    ddlTipoUsuario.Items.Insert(0, new ListItem("-- Seleccione --", ""));
                }
            }
            catch (Exception ex)
            {
                // Puedes loguear o mostrar mensaje
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string nombreUsuario = txtUserName.Text?.Trim();
            string password = txtPassword.Text; // NOTA: plain-text según tu esquema actual (max 10)
            string tipoUsuario = ddlTipoUsuario.SelectedValue;

            AddOrUpdateUsuario(nombreUsuario, password, tipoUsuario);
            CleanForm();
        }

        private void AddOrUpdateUsuario(string nombreUsuario, string password, string idTipoUsuario)
        {
            string idUsuarioSession = Session["idUsuario"] as string;
            string swal;

            if (string.IsNullOrWhiteSpace(nombreUsuario) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(idTipoUsuario))
            {
                swal = "Swal.fire('Atención', 'Completa todos los campos.', 'warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", swal, true);
                return;
            }

            // Si hay id en sesión, es edición
            if (!string.IsNullOrEmpty(idUsuarioSession))
            {
                var u = new Usuario
                {
                    id_usuario = idUsuarioSession,
                    nombre_usuario = nombreUsuario,
                    password = password,
                    id_tipo_usuario = idTipoUsuario
                };

                if (usuarioDAO.ActualizarUsuario(u))
                {
                    swal = "Swal.fire('Éxito', 'Usuario actualizado con éxito', 'success');";
                    Session["idUsuario"] = "";
                    CargarUsuariosEnTabla();
                }
                else
                {
                    swal = "Swal.fire('Error', 'No se pudo actualizar el usuario', 'error');";
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", swal, true);
                return;
            }

            // Inserción
            if (usuarioDAO.InsertarUsuario(nombreUsuario, password, idTipoUsuario))
            {
                swal = "Swal.fire('Éxito', 'Usuario añadido con éxito', 'success');";
                CargarUsuariosEnTabla();
            }
            else
            {
                swal = "Swal.fire('Error', 'No se pudo añadir el usuario', 'error');";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", swal, true);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            string idUsuario = btn.CommandArgument;
            Session["idUsuario"] = idUsuario;
            CargarDatosParaEditar(idUsuario);
        }

        private void CargarDatosParaEditar(string idUsuario)
        {
            try
            {
                var usuario = usuarioDAO.CargarDatosUsuario(new Usuario { id_usuario = idUsuario });
                if (usuario != null)
                {
                    hfUsuarioId.Value = usuario.id_usuario;
                    txtUserName.Text = usuario.nombre_usuario;
                    txtPassword.Text = usuario.password; // según tu modelo actual
                    ddlTipoUsuario.SelectedValue = usuario.id_tipo_usuario;

                    lblFormTitle.Text = "Editar Usuario";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error: {ex.Message}');", true);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            string idUsuario = btn.CommandArgument;

            if (!string.IsNullOrEmpty(idUsuario))
            {
                if (usuarioDAO.EliminarUsuario(idUsuario))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                        "Swal.fire('Éxito', 'Usuario eliminado con éxito', 'success');", true);
                    CargarUsuariosEnTabla();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "Swal.fire('Error', 'No se pudo eliminar el usuario', 'error');", true);
                }
            }
        }

        private void CargarUsuariosEnTabla()
        {
            try
            {
                var lista = usuarioDAO.ConsultarUsuariosConTipo(); // incluye nombre del tipo
                UserRepeater.DataSource = lista;
                UserRepeater.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<div class='alert alert-danger'>Error al cargar usuarios: {ex.Message}</div>");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CleanForm();
            Session["idUsuario"] = "";
        }

        private void CleanForm()
        {
            hfUsuarioId.Value = "";
            txtUserName.Text = "";
            txtPassword.Text = "";
            ddlTipoUsuario.ClearSelection();
            if (ddlTipoUsuario.Items.Count > 0) ddlTipoUsuario.SelectedIndex = 0;
            lblFormTitle.Text = "Añadir Nuevo Usuario";
        }
    }
}
