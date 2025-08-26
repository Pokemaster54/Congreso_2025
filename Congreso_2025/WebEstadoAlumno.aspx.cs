using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.DataBase;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class WebEstadoAlumno : System.Web.UI.Page
    {
        General general = new General();
        EstadoAlumnoDAO estadoDAO = new EstadoAlumnoDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var lista = estadoDAO.ConsultarEstadosAlumno();
            gvEstados.DataSource = lista;
            gvEstados.DataBind();
        }

        // Alta
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreEstado.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','Ingresa el nombre del estado.','warning');", true);
                return;
            }

            if (estadoDAO.InsertarEstadoAlumno(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Estado añadido con éxito.','success');", true);
                txtNombreEstado.Text = "";
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo añadir el estado.','error');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtNombreEstado.Text = "";
            gvEstados.EditIndex = -1;
            BindGrid();
        }

        // Edición en línea
        protected void gvEstados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvEstados.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvEstados_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvEstados.EditIndex = -1;
            BindGrid();
        }

        protected void gvEstados_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvEstados.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = gvEstados.Rows[e.RowIndex];
            TextBox txtEditNombre = (TextBox)row.FindControl("txtEditNombre");
            string nombre = txtEditNombre?.Text?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','El nombre no puede estar vacío.','warning');", true);
                return;
            }

            var entidad = new Estado_alumno { id_estado = id, nombre_estado = nombre };
            if (estadoDAO.ActualizarEstadoAlumno(entidad))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Estado actualizado con éxito.','success');", true);
                gvEstados.EditIndex = -1;
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo actualizar el estado.','error');", true);
            }
        }

        protected void gvEstados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvEstados.DataKeys[e.RowIndex].Value.ToString();

            if (estadoDAO.EliminarEstadoAlumno(id))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Estado eliminado con éxito.','success');", true);
                BindGrid();
            }
            else
            {
                // Probablemente existe referencia desde Alumno
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo eliminar. Verifica si está en uso por algún alumno.','error');", true);
            }
        }

        // Agrega data-label para vista apilada en móvil
        protected void gvEstados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < gvEstados.Columns.Count && i < e.Row.Cells.Count; i++)
                {
                    string header = gvEstados.Columns[i].HeaderText;
                    e.Row.Cells[i].Attributes["data-label"] = header;
                }
            }
        }
    }
}
