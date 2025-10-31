using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.DataBase;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class WebCarrera : System.Web.UI.Page
    {
        General general = new General();
        CarreraDAO carreraDAO = new CarreraDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();
            }
        }

        private void BindGrid()
        {
            var lista = carreraDAO.ConsultarCarreras();
            gvCarreras.DataSource = lista;
            gvCarreras.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string nombre = txtNombreCarrera.Text?.Trim();
            if (string.IsNullOrWhiteSpace(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','Ingresa el nombre de la id_carrera.','warning');", true);
                return;
            }

            if (carreraDAO.InsertarCarrera(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','id_carrera añadida con éxito.','success');", true);
                txtNombreCarrera.Text = "";
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo añadir la id_carrera.','error');", true);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtNombreCarrera.Text = "";
            gvCarreras.EditIndex = -1;
            BindGrid();
        }

        protected void gvCarreras_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvCarreras.EditIndex = e.NewEditIndex;
            BindGrid();
        }

        protected void gvCarreras_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvCarreras.EditIndex = -1;
            BindGrid();
        }

        protected void gvCarreras_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string id = gvCarreras.DataKeys[e.RowIndex].Value.ToString();
            GridViewRow row = gvCarreras.Rows[e.RowIndex];
            TextBox txtEditNombre = (TextBox)row.FindControl("txtEditNombre");
            string nombre = txtEditNombre?.Text?.Trim();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','El nombre no puede estar vacío.','warning');", true);
                return;
            }

            var entidad = new Carrera { id_carrera = id, nombre_carrera = nombre };
            if (carreraDAO.ActualizarCarrera(entidad))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','id_carrera actualizada con éxito.','success');", true);
                gvCarreras.EditIndex = -1;
                BindGrid();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo actualizar la id_carrera.','error');", true);
            }
        }

        protected void gvCarreras_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string id = gvCarreras.DataKeys[e.RowIndex].Value.ToString();

            if (carreraDAO.EliminarCarrera(id))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','id_carrera eliminada con éxito.','success');", true);
                BindGrid();
            }
            else
            {
                // Puede fallar por FK desde Alumno o carrera_actividad
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo eliminar. Verifica si está en uso por alumnos o actividades.','error');", true);
            }
        }

        protected void gvCarreras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int i = 0; i < gvCarreras.Columns.Count && i < e.Row.Cells.Count; i++)
                {
                    string header = gvCarreras.Columns[i].HeaderText;
                    e.Row.Cells[i].Attributes["data-label"] = header;
                }
            }
        }
    }
}
