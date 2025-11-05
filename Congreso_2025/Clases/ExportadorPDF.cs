using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Congreso_2025.Clases
{
    /// <summary>
    /// Clase utilitaria para exportar listas o tablas a PDF con el estilo institucional del sistema.
    /// </summary>
    public static class ExportadorPDF
    {
        /// <summary>
        /// Genera un documento PDF con una tabla y lo envía al navegador como descarga.
        /// </summary>
        /// <param name="titulo">Título principal del PDF.</param>
        /// <param name="columnas">Encabezados de la tabla.</param>
        /// <param name="filas">Lista de filas (cada fila es una lista de strings).</param>
        /// <param name="nombreArchivo">Nombre del archivo PDF (sin extensión).</param>
        public static void ExportarTabla(string titulo, List<string> columnas, List<List<string>> filas, string nombreArchivo)
        {
            // Configurar documento
            Document doc = new Document(PageSize.A4, 40, 40, 60, 50);
            using (MemoryStream ms = new MemoryStream())
            {
                PdfWriter.GetInstance(doc, ms);
                doc.Open();

                // === Colores y fuentes ===
                BaseColor azul = new BaseColor(0, 43, 91);
                Font fontTitulo = new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD, azul);
                Font fontSub = new Font(Font.FontFamily.HELVETICA, 10, Font.ITALIC, BaseColor.DARK_GRAY);
                Font fontHeader = new Font(Font.FontFamily.HELVETICA, 11, Font.BOLD, BaseColor.WHITE);
                Font fontBody = new Font(Font.FontFamily.HELVETICA, 10, Font.NORMAL, BaseColor.BLACK);

                // === Logo institucional ===
                string logoPath = HttpContext.Current.Server.MapPath("~/assets/logo.png");
                if (File.Exists(logoPath))
                {
                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                    logo.ScaleAbsolute(50, 50);
                    logo.Alignment = Element.ALIGN_LEFT;
                    doc.Add(logo);
                }

                // === Título principal ===
                Paragraph pTitulo = new Paragraph(titulo, fontTitulo)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10f
                };
                doc.Add(pTitulo);

                // Fecha de generación
                Paragraph pFecha = new Paragraph($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}", fontSub)
                {
                    Alignment = Element.ALIGN_RIGHT,
                    SpacingAfter = 10f
                };
                doc.Add(pFecha);

                // === Tabla ===
                PdfPTable tabla = new PdfPTable(columnas.Count)
                {
                    WidthPercentage = 100
                };

                // Encabezados
                foreach (var col in columnas)
                {
                    PdfPCell celda = new PdfPCell(new Phrase(col, fontHeader))
                    {
                        BackgroundColor = azul,
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 6
                    };
                    tabla.AddCell(celda);
                }

                // Filas
                foreach (var fila in filas)
                {
                    foreach (var celda in fila)
                    {
                        PdfPCell c = new PdfPCell(new Phrase(celda ?? "", fontBody))
                        {
                            HorizontalAlignment = Element.ALIGN_LEFT,
                            Padding = 5
                        };
                        tabla.AddCell(c);
                    }
                }

                doc.Add(tabla);

                // === Pie de página ===
                doc.Add(new Paragraph("\n"));
                Paragraph pie = new Paragraph("Sistema Congreso Académico 2025 — Universidad de Guatemala",
                    new Font(Font.FontFamily.HELVETICA, 9, Font.ITALIC, azul))
                {
                    Alignment = Element.ALIGN_CENTER
                };
                doc.Add(pie);

                // === Finalizar documento ===
                doc.Close();

                // === Descargar PDF ===
                HttpResponse response = HttpContext.Current.Response;
                response.Clear();
                response.Buffer = true;
                response.ContentType = "application/pdf";
                response.AddHeader("content-disposition", $"attachment;filename={nombreArchivo}.pdf");
                response.BinaryWrite(ms.ToArray());
                response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
    }
}
