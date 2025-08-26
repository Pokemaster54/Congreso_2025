using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.DataBase;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class WebTipoActividad : System.Web.UI.Page
    {
        General general = new General();
        TipoActividadDAO tipoDAO = new TipoActividadDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var lista = tipoDAO.ConsultarTiposActividad();
            gvTipos.DataSource = lista;
            gvTipos.DataBind();
        }

        // Alta
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreTipo.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','Ingresa el nombre del tipo.','warning');", true);
                return;
            }

            if (tipoDAO.InsertarTipoActividad(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Tipo añadido con éxito.','success');", true);
                txtNombreTipo.Text = "";
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo añadir el tipo.','error');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtNombreTipo.Text = "";
            gvTipos.EditIndex = -1;
            BindGrid();
        }

        // Edición en línea
        protected void gvTipos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvTipos.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvTipos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvTipos.EditIndex = -1;
            BindGrid();
        }

        protected void gvTipos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvTipos.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = gvTipos.Rows[e.RowIndex];
            TextBox txtEditNombre = (TextBox)row.FindControl("txtEditNombre");
            string nombre = txtEditNombre?.Text?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','El nombre no puede estar vacío.','warning');", true);
                return;
            }

            var entidad = new Tipo_actividad { id_tipo_actividad = id, nombre_tipo_actividad = nombre };
            if (tipoDAO.ActualizarTipoActividad(entidad))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Tipo actualizado con éxito.','success');", true);
                gvTipos.EditIndex = -1;
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo actualizar el tipo.','error');", true);
            }
        }

        protected void gvTipos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvTipos.DataKeys[e.RowIndex].Value.ToString();

            if (tipoDAO.EliminarTipoActividad(id))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Tipo eliminado con éxito.','success');", true);
                BindGrid();
            }
            else
            {
                // Puede estar en uso por Actividad (FK)
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo eliminar. Verifica si está en uso por alguna actividad.','error');", true);
            }
        }

        // data-label para vista apilada en móvil
        protected void gvTipos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < gvTipos.Columns.Count && i < e.Row.Cells.Count; i++)
                {
                    string header = gvTipos.Columns[i].HeaderText;
                    e.Row.Cells[i].Attributes["data-label"] = header;
                }
            }
        }
    }
}
