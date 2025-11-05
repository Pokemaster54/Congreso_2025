using Congreso_2025.Clases;
using Congreso_2025.DataBase;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace Congreso_2025
{
    public partial class Actividades : Page
    {
        private readonly General general = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarFiltros();
                CargarActividades();
            }
        }

        private void CargarFiltros()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                // 🔹 Carreras
                ddlCarrera.DataSource = db.Carrera.OrderBy(c => c.nombre_carrera).ToList();
                ddlCarrera.DataTextField = "nombre_carrera";
                ddlCarrera.DataValueField = "id_carrera";
                ddlCarrera.DataBind();
                ddlCarrera.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todas las carreras --", ""));

                // 🔹 Ponentes
                ddlPonente.DataSource = db.Ponente.OrderBy(p => p.nombre_ponente).ToList();
                ddlPonente.DataTextField = "nombre_ponente";
                ddlPonente.DataValueField = "id_ponente";
                ddlPonente.DataBind();
                ddlPonente.Items.Insert(0, new System.Web.UI.WebControls.ListItem("-- Todos los ponentes --", ""));
            }
        }

        private void CargarActividades()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                string carreraSel = ddlCarrera.SelectedValue;
                string ponenteSel = ddlPonente.SelectedValue;

                var query = from a in db.Actividad
                            join p in db.Ponente on a.id_ponente equals p.id_ponente
                            join u in db.Ubicacion on a.id_ubicacion equals u.id_ubicacion
                            select new
                            {
                                a.id_actividad,
                                a.Nombre_actividad,
                                a.hora_inicio,
                                a.hora_fin,
                                p.id_ponente,
                                p.nombre_ponente,
                                u.nombre_ubicacion,
                                Carreras = (from ca in db.carrera_actividad
                                            join c in db.Carrera on ca.id_carrera equals c.id_carrera
                                            where ca.id_actividad == a.id_actividad
                                            select c.nombre_carrera).ToList(),
                                Inscritos = (from asg in db.asignacion_actividad
                                             where asg.id_carrera_actividad ==
                                                 (from ca in db.carrera_actividad
                                                  where ca.id_actividad == a.id_actividad
                                                  select ca.id_carrera_actividad).FirstOrDefault()
                                             select asg.id_alumno).Count()
                            };

                // 🔹 Filtros combinados
                if (!string.IsNullOrEmpty(carreraSel))
                {
                    query = query.Where(a =>
                        (from ca in db.carrera_actividad
                         where ca.id_actividad == a.id_actividad && ca.id_carrera == carreraSel
                         select ca).Any());
                }

                if (!string.IsNullOrEmpty(ponenteSel))
                {
                    query = query.Where(a => a.id_ponente == ponenteSel);
                }

                var lista = query.ToList().Select(a => new
                {
                    a.id_actividad,
                    a.Nombre_actividad,
                    a.nombre_ponente,
                    a.id_ponente,
                    a.nombre_ubicacion,
                    Horario = $"{a.hora_inicio:dd/MM HH:mm} - {a.hora_fin:HH:mm}",
                    CarrerasHTML = GenerarCarrerasHTML(a.Carreras),
                    a.Inscritos
                }).ToList();

                rptActividades.DataSource = lista;
                rptActividades.DataBind();

                lblResultado.Text = lista.Count > 0
                    ? $"{lista.Count:N0} actividad(es) encontradas."
                    : "No se encontraron actividades con esos filtros.";
            }
        }

        private string GenerarCarrerasHTML(System.Collections.Generic.List<string> carreras)
        {
            if (carreras == null || carreras.Count == 0)
                return "<span class='text-muted fst-italic'>Sin carreras asignadas</span>";

            StringBuilder sb = new StringBuilder();
            foreach (var c in carreras)
                sb.Append($"<span class='badge-carrera'>{c}</span> ");
            return sb.ToString();
        }

        protected void Filtros_Changed(object sender, EventArgs e)
        {
            CargarActividades();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            ddlCarrera.SelectedIndex = 0;
            ddlPonente.SelectedIndex = 0;
            CargarActividades();
        }

        protected void btnVerInscritos_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            string idActividad = e.CommandArgument.ToString();

            ViewState["actividadActual"] = idActividad;

            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                var alumnos = (from asg in db.asignacion_actividad
                               join ca in db.carrera_actividad on asg.id_carrera_actividad equals ca.id_carrera_actividad
                               join al in db.Alumno on asg.id_alumno equals al.id_alumno
                               join c in db.Carrera on ca.id_carrera equals c.id_carrera
                               where ca.id_actividad == idActividad
                               select new
                               {
                                   al.carne,
                                   nombre = al.nombres_alumno + " " + al.apellidos_alumno,
                                   carrera = c.nombre_carrera
                               }).ToList();

                rptInscritos.DataSource = alumnos;
                rptInscritos.DataBind();
                pnlInscritos.Visible = true;
            }
        }


        protected void btnCerrarModal_Click(object sender, EventArgs e)
        {
            pnlInscritos.Visible = false;
        }

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {
            if (ViewState["actividadActual"] == null)
            {
                // Si no hay actividad seleccionada, no exportamos
                return;
            }

            string idActividad = ViewState["actividadActual"].ToString();

            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                // Obtenemos los alumnos inscritos directamente desde la base de datos
                var alumnosBD = (from asg in db.asignacion_actividad
                                 join ca in db.carrera_actividad on asg.id_carrera_actividad equals ca.id_carrera_actividad
                                 join al in db.Alumno on asg.id_alumno equals al.id_alumno
                                 join c in db.Carrera on ca.id_carrera equals c.id_carrera
                                 join act in db.Actividad on ca.id_actividad equals act.id_actividad
                                 join p in db.Ponente on act.id_ponente equals p.id_ponente
                                 where ca.id_actividad == idActividad
                                 select new
                                 {
                                     act.Nombre_actividad,
                                     p.nombre_ponente,
                                     al.carne,
                                     nombre = al.nombres_alumno + " " + al.apellidos_alumno,
                                     carrera = c.nombre_carrera
                                 }).ToList();

                if (alumnosBD.Count == 0)
                {
                    return;
                }

                // Tomamos datos del encabezado
                string nombreActividad = alumnosBD.First().Nombre_actividad;
                string nombrePonente = alumnosBD.First().nombre_ponente;

                // === Crear PDF ===
                Document doc = new Document(PageSize.A4, 40, 40, 50, 50);
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfWriter.GetInstance(doc, ms);
                    doc.Open();

                    // Encabezado
                    var titulo = new Paragraph("Lista de Alumnos Inscritos", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD))
                    {
                        Alignment = Element.ALIGN_CENTER
                    };
                    doc.Add(titulo);

                    doc.Add(new Paragraph($"\nActividad: {nombreActividad}", new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD)));
                    doc.Add(new Paragraph($"Ponente: {nombrePonente}", new Font(Font.FontFamily.HELVETICA, 11)));
                    doc.Add(new Paragraph($"\nFecha de generación: {DateTime.Now:dd/MM/yyyy HH:mm}\n\n"));

                    // Tabla
                    PdfPTable table = new PdfPTable(3);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 20f, 40f, 40f });

                    Font headerFont = new Font(Font.FontFamily.HELVETICA, 12, Font.BOLD, BaseColor.WHITE);
                    BaseColor azul = new BaseColor(0, 43, 91);
                    string[] headers = { "Carné", "Nombre", "Carrera" };

                    foreach (string h in headers)
                    {
                        PdfPCell cell = new PdfPCell(new Phrase(h, headerFont))
                        {
                            BackgroundColor = azul,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            Padding = 6
                        };
                        table.AddCell(cell);
                    }

                    Font bodyFont = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL);
                    foreach (var a in alumnosBD)
                    {
                        table.AddCell(new Phrase(a.carne, bodyFont));
                        table.AddCell(new Phrase(a.nombre, bodyFont));
                        table.AddCell(new Phrase(a.carrera, bodyFont));
                    }

                    doc.Add(table);
                    doc.Close();

                    
                    Response.Clear();
                    Response.Buffer = true;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", $"attachment;filename=Inscritos_{nombreActividad}.pdf");
                    Response.BinaryWrite(ms.ToArray());
                    Response.Flush();  
                    HttpContext.Current.ApplicationInstance.CompleteRequest(); 

                }
            }
        }

    }
}
