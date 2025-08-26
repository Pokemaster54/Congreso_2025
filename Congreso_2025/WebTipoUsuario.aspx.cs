using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class WebTipoUsuario : System.Web.UI.Page
    {
        General general = new General();
        TipoUsuarioDAO tipoDAO = new TipoUsuarioDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarTiposEnTabla();
                Session["idTipoUsuario"] = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreTipo.Text?.Trim();
            AddOrUpdateTipoUsuario(nombre);
            CleanForm();
        }

        private void AddOrUpdateTipoUsuario(string nombre)
        {
            string idSession = Session["idTipoUsuario"] as string;
            string swal;

            if (string.IsNullOrWhiteSpace(nombre))
            {
                swal = "Swal.fire('Atención', 'Ingresa el nombre del tipo.', 'warning');";
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", swal, true);
                return;
            }

            if (!string.IsNullOrEmpty(idSession))
            {
                // Edición
                var t = new Tipo_usuario
                {
                    id_tipo_usuario = idSession,
                    nombre_tipo = nombre
                };

                if (tipoDAO.ActualizarTipoUsuario(t))
                {
                    swal = "Swal.fire('Éxito', 'Tipo de usuario actualizado con éxito', 'success');";
                    Session["idTipoUsuario"] = "";
                    CargarTiposEnTabla();
                }
                else
                {
                    swal = "Swal.fire('Error', 'No se pudo actualizar el tipo', 'error');";
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", swal, true);
                return;
            }

            // Inserción
            if (tipoDAO.InsertarTipoUsuario(nombre))
            {
                swal = "Swal.fire('Éxito', 'Tipo de usuario añadido con éxito', 'success');";
                CargarTiposEnTabla();
            }
            else
            {
                swal = "Swal.fire('Error', 'No se pudo añadir el tipo', 'error');";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", swal, true);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            string idTipo = btn.CommandArgument;
            Session["idTipoUsuario"] = idTipo;
            CargarDatosParaEditar(idTipo);
        }

        private void CargarDatosParaEditar(string idTipo)
        {
            try
            {
                var tipo = tipoDAO.CargarDatosTipoUsuario(new Tipo_usuario { id_tipo_usuario = idTipo });
                if (tipo != null)
                {
                    hfTipoUsuarioId.Value = tipo.id_tipo_usuario;
                    txtNombreTipo.Text = tipo.nombre_tipo;
                    lblFormTitle.Text = "Editar Tipo de usuario";
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
            string idTipo = btn.CommandArgument;

            if (!string.IsNullOrEmpty(idTipo))
            {
                if (tipoDAO.EliminarTipoUsuario(idTipo))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                        "Swal.fire('Éxito', 'Tipo eliminado con éxito', 'success');", true);
                    CargarTiposEnTabla();
                }
                else
                {
                    // Posible clave foránea (usuarios que lo referencian)
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "Swal.fire('Error', 'No se pudo eliminar el tipo. Verifica si está en uso por algún usuario.', 'error');", true);
                }
            }
        }

        private void CargarTiposEnTabla()
        {
            try
            {
                var lista = tipoDAO.ConsultarTiposUsuario();
                UserRepeater.DataSource = lista;
                UserRepeater.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<div class='alert alert-danger'>Error al cargar tipos: {ex.Message}</div>");
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CleanForm();
            Session["idTipoUsuario"] = "";
        }

        private void CleanForm()
        {
            hfTipoUsuarioId.Value = "";
            txtNombreTipo.Text = "";
            lblFormTitle.Text = "Añadir Nuevo Tipo de usuario";
        }
    }
}
