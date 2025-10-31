using System;
using System.Linq;
using System.Web.UI;
using Congreso_2025.Clases;
using Congreso_2025.DataBase;

namespace Congreso_2025
{
    public partial class WebAlumno : Page
    {
        private readonly General general = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarAlumnos();
            }
        }

        private void CargarCombos()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                ddlCarrera.DataSource = db.Carrera.ToList();
                ddlCarrera.DataTextField = "nombre_carrera";
                ddlCarrera.DataValueField = "id_carrera";
                ddlCarrera.DataBind();

                ddlEstado.DataSource = db.Estado_alumno.ToList();
                ddlEstado.DataTextField = "nombre_estado";
                ddlEstado.DataValueField = "id_estado";
                ddlEstado.DataBind();
            }
        }

        private void CargarAlumnos()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var lista = from a in db.Alumno
                            join c in db.Carrera on a.id_carrera equals c.id_carrera
                            join e in db.Estado_alumno on a.id_estado equals e.id_estado
                            select new
                            {
                                a.id_alumno,
                                a.carne,
                                nombreCompleto = a.nombres_alumno + " " + a.apellidos_alumno,
                                c.nombre_carrera,
                                e.nombre_estado
                            };

                rptAlumnos.DataSource = lista.ToList();
                rptAlumnos.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                string idAlumno = hfAlumnoId.Value;

                if (string.IsNullOrEmpty(idAlumno))
                {
                    // Nuevo alumno
                    var nuevo = new DataBase.Alumno
                    {
                        id_alumno = "AL" + (db.Alumno.Count() + 1).ToString("000"),
                        carne = txtCarne.Text.Trim(),
                        nombres_alumno = txtNombre.Text.Trim(),
                        apellidos_alumno = txtApellido.Text.Trim(),
                        id_carrera = ddlCarrera.SelectedValue,
                        id_estado = ddlEstado.SelectedValue,
                        id_usuario = "U0000", // placeholder
                        id_pago = "P0000"
                    };
                    db.Alumno.InsertOnSubmit(nuevo);
                }
                else
                {
                    // Editar alumno existente
                    var alumno = db.Alumno.FirstOrDefault(x => x.id_alumno == idAlumno);
                    if (alumno != null)
                    {
                        alumno.carne = txtCarne.Text.Trim();
                        alumno.nombres_alumno = txtNombre.Text.Trim();
                        alumno.apellidos_alumno = txtApellido.Text.Trim();
                        alumno.id_carrera = ddlCarrera.SelectedValue;
                        alumno.id_estado = ddlEstado.SelectedValue;
                    }
                }

                db.SubmitChanges();
            }

            LimpiarFormulario();
            CargarAlumnos();
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            var btn = (System.Web.UI.WebControls.LinkButton)sender;
            string id = btn.CommandArgument;

            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var alumno = db.Alumno.FirstOrDefault(x => x.id_alumno == id);
                if (alumno != null)
                {
                    hfAlumnoId.Value = alumno.id_alumno;
                    txtCarne.Text = alumno.carne;
                    txtNombre.Text = alumno.nombres_alumno;
                    txtApellido.Text = alumno.apellidos_alumno;
                    ddlCarrera.SelectedValue = alumno.id_carrera;
                    ddlEstado.SelectedValue = alumno.id_estado;
                    lblFormTitle.Text = "Editar Alumno";
                }
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var btn = (System.Web.UI.WebControls.LinkButton)sender;
            string id = btn.CommandArgument;

            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var alumno = db.Alumno.FirstOrDefault(x => x.id_alumno == id);
                if (alumno != null)
                {
                    db.Alumno.DeleteOnSubmit(alumno);
                    db.SubmitChanges();
                }
            }

            CargarAlumnos();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            hfAlumnoId.Value = "";
            txtCarne.Text = "";
            txtNombre.Text = "";
            txtApellido.Text = "";
            ddlCarrera.SelectedIndex = 0;
            ddlEstado.SelectedIndex = 0;
            lblFormTitle.Text = "Añadir Nuevo Alumno";
        }
    }
}
