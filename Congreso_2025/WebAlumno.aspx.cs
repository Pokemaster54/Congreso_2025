using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

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
                    int totalUsuarios = db.Usuario.Count();
                    string nuevoIdUsuario = "U" + (totalUsuarios + 1).ToString("0000");

                    var usuario = new DataBase.Usuario
                    {
                        id_usuario = nuevoIdUsuario,
                        nombre_usuario = txtCarne.Text.Trim(),
                        password = "123",
                        id_tipo_usuario = "TU0002"
                    };

                    db.Usuario.InsertOnSubmit(usuario);
                    db.SubmitChanges();

                    int totalAlumnos = db.Alumno.Count();
                    string nuevoIdAlumno = "AL" + (totalAlumnos + 1).ToString("000");

                    var nuevoAlumno = new DataBase.Alumno
                    {
                        id_alumno = nuevoIdAlumno,
                        carne = txtCarne.Text.Trim(),
                        nombres_alumno = txtNombre.Text.Trim(),
                        apellidos_alumno = txtApellido.Text.Trim(),
                        id_carrera = ddlCarrera.SelectedValue,
                        id_estado = ddlEstado.SelectedValue,
                        id_usuario = nuevoIdUsuario,
                        id_pago = "PG0001"
                    };

                    db.Alumno.InsertOnSubmit(nuevoAlumno);
                }
                else
                {
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

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var lista = (from a in db.Alumno
                                 join c in db.Carrera on a.id_carrera equals c.id_carrera
                                 join est in db.Estado_alumno on a.id_estado equals est.id_estado
                                 select new
                                 {
                                     a.carne,
                                     nombreCompleto = a.nombres_alumno + " " + a.apellidos_alumno,
                                     carrera = c.nombre_carrera,
                                     estado = est.nombre_estado
                                 }).ToList();

                    if (lista == null || lista.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                            "Swal.fire('Sin datos','No hay alumnos para exportar.','info');", true);
                        return;
                    }

                    var columnas = new List<string> { "Carné", "Nombre completo", "Carrera", "Estado" };

                    var filas = lista.Select(a => new List<string>
            {
                a.carne,
                a.nombreCompleto,
                a.carrera,
                a.estado
            }).ToList();

                    ExportadorPDF.ExportarTabla(
                        "Listado de Alumnos",
                        columnas,
                        filas,
                        "Alumnos_Congreso2025"
                    );
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"Swal.fire('Error','{ex.Message}','error');", true);
            }
        }

    }
}