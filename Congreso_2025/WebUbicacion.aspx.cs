using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.DataBase;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class WebUbicacion : System.Web.UI.Page
    {
        General general = new General();
        UbicacionDAO ubicacionDAO = new UbicacionDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var lista = ubicacionDAO.ConsultarUbicaciones();
            gvUbicaciones.DataSource = lista;
            gvUbicaciones.DataBind();
        }

        // Alta
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text?.Trim();
            string indicaciones = txtIndicaciones.Text?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','Ingresa el nombre de la ubicación.','warning');", true);
                return;
            }

            if (ubicacionDAO.InsertarUbicacion(nombre, indicaciones))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Ubicación añadida con éxito.','success');", true);
                ClearForm();
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo añadir la ubicación.','error');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearForm();
            gvUbicaciones.EditIndex = -1;
            BindGrid();
        }

        private void ClearForm()
        {
            txtNombre.Text = "";
            txtIndicaciones.Text = "";
        }

        // Edición en línea
        protected void gvUbicaciones_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvUbicaciones.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvUbicaciones_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvUbicaciones.EditIndex = -1;
            BindGrid();
        }

        protected void gvUbicaciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvUbicaciones.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = gvUbicaciones.Rows[e.RowIndex];

            TextBox txtEditNombre = (TextBox)row.FindControl("txtEditNombre");
            TextBox txtEditIndicaciones = (TextBox)row.FindControl("txtEditIndicaciones");

            if (txtEditNombre == null) return;

            string nombre = txtEditNombre.Text?.Trim();
            string indicaciones = txtEditIndicaciones?.Text?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','El nombre no puede estar vacío.','warning');", true);
                return;
            }

            var entidad = new Ubicacion
            {
                id_ubicacion = id,
                nombre_ubicacion = nombre,
                indicaciones = indicaciones
            };

            if (ubicacionDAO.ActualizarUbicacion(entidad))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Ubicación actualizada con éxito.','success');", true);
                gvUbicaciones.EditIndex = -1;
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo actualizar la ubicación.','error');", true);
            }
        }

        protected void gvUbicaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvUbicaciones.DataKeys[e.RowIndex].Value.ToString();

            if (ubicacionDAO.EliminarUbicacion(id))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Ubicación eliminada con éxito.','success');", true);
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
        protected void gvUbicaciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < gvUbicaciones.Columns.Count && i < e.Row.Cells.Count; i++)
                {
                    string header = gvUbicaciones.Columns[i].HeaderText;
                    e.Row.Cells[i].Attributes["data-label"] = header;
                }
            }
        }
    }
}
