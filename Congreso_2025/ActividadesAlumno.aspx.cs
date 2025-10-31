using System;
using System.Linq;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Congreso_2025.Clases;
using Congreso_2025.DataBase;

namespace Congreso_2025
{
    public partial class ActividadesAlumno : System.Web.UI.Page
    {
        private readonly General general = new General();
        private string UsuarioActual => Context.User.Identity.Name;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarPonentes();
                CargarActividades();
            }
        }

        // 🔹 Obtener alumno actual
        private Alumno ObtenerAlumnoActual(MiLinQ db)
        {
            var usuario = db.Usuario.FirstOrDefault(u => u.nombre_usuario == UsuarioActual);
            if (usuario == null) return null;

            return db.Alumno.FirstOrDefault(a => a.id_usuario == usuario.id_usuario);
        }

        // 🔹 Cargar solo los ponentes de las actividades del alumno
        private void CargarPonentes()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var alumno = ObtenerAlumnoActual(db);
                if (alumno == null)
                {
                    ddlPonente.Items.Clear();
                    ddlPonente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Sin datos --", ""));
                    return;
                }

                // Ponentes de actividades asignadas al alumno
                var ponentes = (from aa in db.asignacion_actividad
                                join ca in db.carrera_actividad on aa.id_carrera_actividad equals ca.id_carrera_actividad
                                join a in db.Actividad on ca.id_actividad equals a.id_actividad
                                join p in db.Ponente on a.id_ponente equals p.id_ponente
                                where aa.id_alumno == alumno.id_alumno
                                select new { p.id_ponente, p.nombre_ponente })
                               .Distinct()
                               .OrderBy(p => p.nombre_ponente)
                               .ToList();

                ddlPonente.DataSource = ponentes;
                ddlPonente.DataTextField = "nombre_ponente";
                ddlPonente.DataValueField = "id_ponente";
                ddlPonente.DataBind();
                ddlPonente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todos --", ""));
            }
        }

        // 🔹 Cargar actividades según alumno y filtros
        private void CargarActividades()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var alumno = ObtenerAlumnoActual(db);
                if (alumno == null)
                {
                    lblMensaje.Text = "No se encontró información del alumno actual.";
                    gvActividades.DataSource = null;
                    gvActividades.DataBind();
                    return;
                }

                string ponenteSel = ddlPonente.SelectedValue;
                DateTime? fechaSel = null;
                if (DateTime.TryParse(txtFecha.Text, out DateTime f)) fechaSel = f;

                var query =
                    from aa in db.asignacion_actividad
                    join ca in db.carrera_actividad on aa.id_carrera_actividad equals ca.id_carrera_actividad
                    join a in db.Actividad on ca.id_actividad equals a.id_actividad
                    join p in db.Ponente on a.id_ponente equals p.id_ponente
                    join u in db.Ubicacion on a.id_ubicacion equals u.id_ubicacion
                    join t in db.Tipo_actividad on a.id_tipo_actividad equals t.id_tipo_actividad
                    where aa.id_alumno == alumno.id_alumno
                    select new
                    {
                        a.id_actividad,
                        a.Nombre_actividad,
                        p.id_ponente,
                        p.nombre_ponente,
                        t.nombre_tipo_actividad,
                        u.nombre_ubicacion,
                        a.hora_inicio,
                        a.hora_fin
                    };

                if (!string.IsNullOrEmpty(ponenteSel))
                    query = query.Where(x => x.id_ponente == ponenteSel);

                if (fechaSel.HasValue)
                    query = query.Where(x => x.hora_inicio.Date == fechaSel.Value.Date);

                var data = query.OrderBy(x => x.hora_inicio).ToList();

                gvActividades.DataSource = data;
                gvActividades.DataBind();

                lblMensaje.Text = data.Count == 0 ? "No se encontraron actividades con esos filtros." : "";
            }
        }

        protected void Filtro_Changed(object sender, EventArgs e)
        {
            CargarActividades();
        }

        // 🔹 Exportar a PDF (respeta filtros actuales)
        protected void btnExport_Click(object sender, EventArgs e)
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var alumno = ObtenerAlumnoActual(db);
                if (alumno == null) return;

                string ponenteSel = ddlPonente.SelectedValue;
                DateTime? fechaSel = null;
                if (DateTime.TryParse(txtFecha.Text, out DateTime f)) fechaSel = f;

                var data =
                    from aa in db.asignacion_actividad
                    join ca in db.carrera_actividad on aa.id_carrera_actividad equals ca.id_carrera_actividad
                    join a in db.Actividad on ca.id_actividad equals a.id_actividad
                    join p in db.Ponente on a.id_ponente equals p.id_ponente
                    join u in db.Ubicacion on a.id_ubicacion equals u.id_ubicacion
                    join t in db.Tipo_actividad on a.id_tipo_actividad equals t.id_tipo_actividad
                    where aa.id_alumno == alumno.id_alumno
                    select new
                    {
                        a.Nombre_actividad,
                        p.id_ponente,
                        p.nombre_ponente,
                        t.nombre_tipo_actividad,
                        u.nombre_ubicacion,
                        a.hora_inicio,
                        a.hora_fin
                    };

                if (!string.IsNullOrEmpty(ponenteSel))
                    data = data.Where(x => x.id_ponente == ponenteSel);

                if (fechaSel.HasValue)
                    data = data.Where(x => x.hora_inicio.Date == fechaSel.Value.Date);

                var lista = data.OrderBy(x => x.hora_inicio).ToList();
                if (lista.Count == 0) return;

                // PDF export
                Document doc = new Document(PageSize.A4, 40, 40, 40, 40);
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    var titulo = new Paragraph($"Actividades del alumno: {alumno.nombres_alumno} {alumno.apellidos_alumno}",
                        new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));
                    titulo.Alignment = Element.ALIGN_CENTER;
                    doc.Add(titulo);
                    doc.Add(new Paragraph("\n"));

                    PdfPTable table = new PdfPTable(5) { WidthPercentage = 100 };
                    table.AddCell("Actividad");
                    table.AddCell("Ponente");
                    table.AddCell("Tipo");
                    table.AddCell("Ubicación");
                    table.AddCell("Horario");

                    foreach (var a in lista)
                    {
                        table.AddCell(a.Nombre_actividad);
                        table.AddCell(a.nombre_ponente);
                        table.AddCell(a.nombre_tipo_actividad);
                        table.AddCell(a.nombre_ubicacion);
                        table.AddCell($"{a.hora_inicio:dd/MM/yyyy HH:mm} - {a.hora_fin:HH:mm}");
                    }

                    doc.Add(table);
                    doc.Close();

                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=ActividadesAlumno.pdf");
                    Response.BinaryWrite(ms.ToArray());
                    Response.End();
                }
            }
        }
    }
}
