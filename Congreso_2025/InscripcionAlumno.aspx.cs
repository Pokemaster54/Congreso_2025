using Congreso_2025.Clases;
using Congreso_2025.DataBase;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;

namespace Congreso_2025
{
    public partial class InscripcionAlumno : Page
    {
        private readonly General general = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarCarreras();
        }

        private void CargarCarreras()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                ddlCarrera.DataSource = db.Carrera.OrderBy(c => c.nombre_carrera).ToList();
                ddlCarrera.DataTextField = "nombre_carrera";
                ddlCarrera.DataValueField = "id_carrera";
                ddlCarrera.DataBind();
                ddlCarrera.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Seleccione --", ""));
            }
        }

        protected void ddlCarrera_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var idCarrera = ddlCarrera.SelectedValue;
                if (string.IsNullOrEmpty(idCarrera))
                {
                    gvActividades.DataSource = null;
                    gvActividades.DataBind();
                    return;
                }

                var acts = from ca in db.carrera_actividad
                           join a in db.Actividad on ca.id_actividad equals a.id_actividad
                           join p in db.Ponente on a.id_ponente equals p.id_ponente
                           join t in db.Tipo_actividad on a.id_tipo_actividad equals t.id_tipo_actividad
                           where ca.id_carrera == idCarrera
                           select new
                           {
                               a.Nombre_actividad,
                               t.nombre_tipo_actividad,
                               p.nombre_ponente,
                               a.hora_inicio
                           };

                gvActividades.DataSource = acts.ToList();
                gvActividades.DataBind();
            }
        }

        protected void btnPreview_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCarne.Text) || string.IsNullOrWhiteSpace(txtNombres.Text) ||
                string.IsNullOrWhiteSpace(txtApellidos.Text) || string.IsNullOrEmpty(ddlCarrera.SelectedValue))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "msg",
                    "Swal.fire('Atención','Completa todos los campos requeridos','warning');", true);
                return;
            }

            lblUser.Text = txtCarne.Text.Trim();
            lblPass.Text = "123";
            lblNombre.Text = $"{txtNombres.Text} {txtApellidos.Text}";
            lblCarrera.Text = ddlCarrera.SelectedItem.Text;
            lblBoleta.Text = txtBoleta.Text.Trim();

            pnlResumen.Visible = true;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                string carne = txtCarne.Text.Trim();

                // Verificar duplicado
                if (db.Alumno.Any(a => a.carne == carne))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "dup",
                        "Swal.fire('Error','El alumno ya está inscrito','error');", true);
                    return;
                }

                // Crear nuevo usuario
                string nuevoIdUsuario = "U" + (db.Usuario.Count() + 1).ToString("D4");
                var usuario = new Usuario
                {
                    id_usuario = nuevoIdUsuario,
                    nombre_usuario = carne,
                    password = "123",
                    id_tipo_usuario = "TU0002" // tipo alumno
                };
                db.Usuario.InsertOnSubmit(usuario);

                // Crear pago
                string nuevoIdPago = "PG" + (db.Pago.Count() + 1).ToString("D4");
                var pago = new Pago
                {
                    id_pago = nuevoIdPago,
                    id_estado_pago = "EP0001", // pagado
                    num_boleta = txtBoleta.Text.Trim(),
                    fecha_pago = calFechaPago.SelectedDate,
                    monto = 0,
                    fecha_registro = DateTime.Now
                };
                db.Pago.InsertOnSubmit(pago);

                // Crear alumno
                string nuevoIdAlumno = "A" + (db.Alumno.Count() + 1).ToString("D5");
                var alumno = new Alumno
                {
                    id_alumno = nuevoIdAlumno,
                    carne = carne,
                    nombres_alumno = txtNombres.Text.Trim(),
                    apellidos_alumno = txtApellidos.Text.Trim(),
                    id_estado = "EA0001", // activo
                    id_usuario = nuevoIdUsuario,
                    id_pago = nuevoIdPago,
                    id_carrera = ddlCarrera.SelectedValue
                };
                db.Alumno.InsertOnSubmit(alumno);

                // Asignar actividades de la carrera
                var actividadesCarrera = from ca in db.carrera_actividad
                                         where ca.id_carrera == ddlCarrera.SelectedValue
                                         select ca;

                int asignCount = db.asignacion_actividad.Count();
                foreach (var ca in actividadesCarrera)
                {
                    string idAsign = "AS" + (++asignCount).ToString("D4");
                    db.asignacion_actividad.InsertOnSubmit(new asignacion_actividad
                    {
                        id_asignacion = idAsign,
                        id_alumno = nuevoIdAlumno,
                        id_carrera_actividad = ca.id_carrera_actividad,
                        bit = true
                    });
                }

                db.SubmitChanges();

                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Alumno inscrito correctamente','success');", true);

                LimpiarCampos();
            }
        }

        private void LimpiarCampos()
        {
            txtCarne.Text = txtNombres.Text = txtApellidos.Text = txtBoleta.Text = "";
            ddlCarrera.SelectedIndex = 0;
            gvActividades.DataSource = null;
            gvActividades.DataBind();
            pnlResumen.Visible = false;
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Cerrar sesión
            FormsAuthentication.SignOut();

            // Limpiar cookie
            if (Request.Cookies[".ASPXAUTH"] != null)
            {
                var cookie = new HttpCookie(".ASPXAUTH");
                cookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(cookie);
            }

            // Redirigir a login
            Response.Redirect("~/Login.aspx", true);
        }

    }
}
