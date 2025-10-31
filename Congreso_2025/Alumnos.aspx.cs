using Congreso_2025.Clases;
using Congreso_2025.DataBase;
using iTextSharp.text;
using iTextSharp.text.pdf; // Para generar PDF
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace Congreso_2025
{
    public partial class Alumnos : Page
    {
        private readonly General general = new General();
        private dynamic listaActual; // Guardaremos la lista filtrada aquí

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarAlumnos();
            }
        }

        private void CargarFiltros()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                ddlCarrera.DataSource = db.Carrera.OrderBy(c => c.nombre_carrera).ToList();
                ddlCarrera.DataTextField = "nombre_carrera";
                ddlCarrera.DataValueField = "id_carrera";
                ddlCarrera.DataBind();
                ddlCarrera.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todas las carreras --", ""));

                ddlActividad.DataSource = db.Actividad.OrderBy(a => a.Nombre_actividad).ToList();
                ddlActividad.DataTextField = "Nombre_actividad";
                ddlActividad.DataValueField = "id_actividad";
                ddlActividad.DataBind();
                ddlActividad.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todas las actividades --", ""));
            }
        }

        private void CargarAlumnos()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                string carreraSel = ddlCarrera.SelectedValue;
                string actividadSel = ddlActividad.SelectedValue;

                var query = from al in db.Alumno
                            join ca in db.Carrera on al.id_carrera equals ca.id_carrera
                            join pa in db.Pago on al.id_pago equals pa.id_pago
                            join ep in db.Estado_pago on pa.id_estado_pago equals ep.id_estado_pago
                            where ep.nombre_tipo.ToUpper() == "PAGADO"
                            select new
                            {
                                al.id_alumno,
                                al.carne,
                                al.nombres_alumno,
                                al.apellidos_alumno,
                                Carrera = ca.nombre_carrera,
                                EstadoPago = ep.nombre_tipo,
                                Actividad = (from asg in db.asignacion_actividad
                                             join caa in db.carrera_actividad on asg.id_carrera_actividad equals caa.id_carrera_actividad
                                             join act in db.Actividad on caa.id_actividad equals act.id_actividad
                                             where asg.id_alumno == al.id_alumno
                                             select act.Nombre_actividad).FirstOrDefault()
                            };

                if (!string.IsNullOrEmpty(carreraSel))
                {
                    var nombreCarrera = db.Carrera.FirstOrDefault(c => c.id_carrera == carreraSel)?.nombre_carrera;
                    query = query.Where(q => q.Carrera == nombreCarrera);
                }

                if (!string.IsNullOrEmpty(actividadSel))
                {
                    var nombreActividad = db.Actividad.FirstOrDefault(a => a.id_actividad == actividadSel)?.Nombre_actividad;
                    query = query.Where(q => q.Actividad == nombreActividad);
                }

                var lista = query.OrderBy(q => q.apellidos_alumno).ThenBy(q => q.nombres_alumno).ToList();
                listaActual = lista; // Guardar para exportación

                gvAlumnos.DataSource = lista;
                gvAlumnos.DataBind();

                lblResultado.Text = lista.Count > 0
                    ? $"{lista.Count:N0} alumno(s) encontrados."
                    : "No se encontraron alumnos con esos filtros.";
            }
        }

        protected void FiltroChanged(object sender, EventArgs e)
        {
            CargarAlumnos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlCarrera.SelectedIndex = 0;
            ddlActividad.SelectedIndex = 0;
            CargarAlumnos();
        }

        // 🔹 Exportar a PDF
        protected void btnExportar_Click(object sender, EventArgs e)
        {
            if (listaActual == null)
                CargarAlumnos();

            var doc = new Document(PageSize.A4, 36, 36, 50, 36);
            var ms = new MemoryStream();
            PdfWriter.GetInstance(doc, ms);
            doc.Open();

            var titulo = new Paragraph("Listado de Alumnos — Congreso 2025",
                new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD))
            { Alignment = Element.ALIGN_CENTER };
            doc.Add(titulo);

            doc.Add(new Paragraph(
                $"Generado: {DateTime.Now:dd/MM/yyyy HH:mm} — Filtros: Carrera = {(ddlCarrera.SelectedItem?.Text ?? "Todas")}, Actividad = {(ddlActividad.SelectedItem?.Text ?? "Todas")}\n\n",
                new Font(Font.FontFamily.HELVETICA, 10)));

            PdfPTable table = new PdfPTable(6) { WidthPercentage = 100 };
            table.SetWidths(new float[] { 1.2f, 2f, 2f, 2.5f, 2.5f, 1.5f });

            string[] headers = { "Carne", "Nombres", "Apellidos", "Carrera", "Actividad", "Pago" };
            foreach (var h in headers)
            {
                var cell = new PdfPCell(new Phrase(h, new Font(Font.FontFamily.HELVETICA, 9, Font.BOLD, BaseColor.WHITE)))
                {
                    BackgroundColor = new BaseColor(0, 43, 91),
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Padding = 5
                };
                table.AddCell(cell);
            }

            foreach (var a in listaActual)
            {
                table.AddCell(new Phrase(a.carne));
                table.AddCell(new Phrase(a.nombres_alumno));
                table.AddCell(new Phrase(a.apellidos_alumno));
                table.AddCell(new Phrase(a.Carrera));
                table.AddCell(new Phrase(a.Actividad ?? "-"));
                table.AddCell(new Phrase(a.EstadoPago));
            }

            doc.Add(table);
            doc.Add(new Paragraph($"\nTotal de alumnos: {((System.Collections.ICollection)listaActual).Count}\n", new Font(Font.FontFamily.HELVETICA, 9, Font.ITALIC)));

            doc.Close();

            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("Content-Disposition", "attachment; filename=Listado_Alumnos.pdf");
            Response.BinaryWrite(ms.ToArray());
            Response.End();
        }
    }
}
