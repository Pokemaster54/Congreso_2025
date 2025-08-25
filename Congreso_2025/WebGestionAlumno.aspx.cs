using Congreso_2025.Clases;
using Congreso_2025.DataBase; // para la clase Alumno del LINQ to SQL
using System;
using System.Globalization;
using System.Web.UI.WebControls;

namespace TuProyecto
{
    public partial class WebGestionAlumno : System.Web.UI.Page
    {
        private readonly AlumnoDAO alumnoDAO = new AlumnoDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarGrid();
        }

        private void CargarGrid()
        {
            lblMsg.Text = "";
            gvAlumnos.DataSource = alumnoDAO.Listar(txtFiltro.Text);
            gvAlumnos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e) => CargarGrid();

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltro.Text = "";
            CargarGrid();
        }

        protected void gvAlumnos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAlumnos.PageIndex = e.NewPageIndex;
            CargarGrid();
        }

        protected void gvAlumnos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvAlumnos.EditIndex = e.NewEditIndex;
            CargarGrid();
        }

        protected void gvAlumnos_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvAlumnos.EditIndex = -1;
            CargarGrid();
        }

        protected void gvAlumnos_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            lblMsg.Text = "";

            // Key desde DataKeyNames (string porque id_alumno es string)
            string id = (string)e.Keys["id_alumno"];

            var row = gvAlumnos.Rows[e.RowIndex];
            var txtCarne = (TextBox)row.FindControl("txtCarne");
            var txtNombres = (TextBox)row.FindControl("txtNombres");
            var txtApellidos = (TextBox)row.FindControl("txtApellidos");
            var txtIdEstado = (TextBox)row.FindControl("txtIdEstado");
            var txtIdUsuario = (TextBox)row.FindControl("txtIdUsuario");
            var txtIdPago = (TextBox)row.FindControl("txtIdPago");

            var idEstado = txtIdEstado.Text;
            var idUsuario = txtIdUsuario.Text;
            var idPago = txtIdPago.Text;

            // Creamos una entidad Alumno con los datos editados (sin DTO)
            var alumno = new Alumno
            {
                id_alumno = id,
                carne = txtCarne.Text?.Trim(),
                nombres_alumno = txtNombres.Text?.Trim(),
                apellidos_alumno = txtApellidos.Text?.Trim(),
                id_estado = idEstado,
                id_usuario = idUsuario,
                id_pago = idPago
            };

            if (alumnoDAO.Actualizar(alumno, out string error))
            {
                gvAlumnos.EditIndex = -1;
                CargarGrid();
            }
            else
            {
                lblMsg.Text = "No se pudo guardar: " + error;
            }
        }

        protected void gvAlumnos_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            lblMsg.Text = "";
            string id = (string)e.Keys["id_alumno"];

            if (alumnoDAO.Eliminar(id, out string error))
                CargarGrid();
            else
                lblMsg.Text = "No se pudo eliminar: " + error;
        }

        private int? TryParseNullableInt(string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return null;
            if (int.TryParse(s.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture, out int v))
                return v;
            return null;
        }
    }
}
