using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.Clases.DataClasses;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        General general = new General();
        PonenteDAO ponenteDAO = new PonenteDAO();
        string idPonenteSeleccionado = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                CargarPonentesEnTabla();
                Session["idPonente"] = "";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string nombre = txtName.Text;
            string fechaNacimiento = txtDate.Text;
            string origen = txtOrigin.Text;
            string descripcion = txtDescription.Text;
            addNewPonente(nombre, fechaNacimiento, origen, descripcion);
            cleanForm();
        }



        private void addNewPonente(string nombre, string fechaNacimiento, string origen, string descripcion)
        {
            PonenteDC ponente = new PonenteDC(nombre, Convert.ToDateTime(fechaNacimiento), origen, descripcion);
            Ponente comprobacion = null;
            string swal = "";
            string idPonente = Session["idPonente"] as string;

            if (!string.IsNullOrEmpty(idPonente))
            {
                comprobacion = ponenteDAO.CargarDatosPonente(new Ponente() { id_ponente = idPonente });
            }
            if (comprobacion == null)
            {
                if (ponenteDAO.InsertarPonente(ponente))
                {
                    swal = "Swal.fire('Exito', 'Ponente añadido con éxito', 'success');";
                    CargarPonentesEnTabla();
                }
                else
                {
                    swal = "Swal.fire('Error', 'No se pudo añadir el ponente', 'error');";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", swal, true);
            }
            else
            {
                Ponente nuevo = new Ponente() { id_ponente = idPonente, nombre_ponente = nombre, fecha_nacimiento = Convert.ToDateTime(fechaNacimiento), Origen = origen, descripcion = descripcion };

                if (ponenteDAO.ActualizarPonente(nuevo))
                {
                    swal = "Swal.fire('Exito', 'Ponente editado con éxito', 'success');";
                    Session["idPonente"] = "";
                    CargarPonentesEnTabla();
                }
                else
                {
                    swal = "Swal.fire('Error', 'No se pudo actualizar el ponente', 'error');";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", swal, true);
            }

        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string ponenteId = btn.CommandArgument;
            Session["idPonente"] = ponenteId;
            CargarDatosParaEditar(Session["idPonente"] as string);
        }

        protected void CargarDatosParaEditar(string ponenteId)
        {
            try
            {
                Ponente ponente = new Ponente { id_ponente = ponenteId };
                ponente = ponenteDAO.CargarDatosPonente(ponente);

                if (ponente != null && ponente.nombre_ponente != null)
                {
                    hfPonenteId.Value = ponente.id_ponente;
                    txtName.Text = ponente.nombre_ponente;
                    txtDate.Text = ponente.fecha_nacimiento.ToString("yyyy-MM-dd");
                    txtOrigin.Text = ponente.Origen;
                    txtDescription.Text = ponente.descripcion;

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error: {ex.Message}');", true);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string ponenteId = hfPonenteId.Value;
                Ponente ponente = new Ponente { id_ponente = ponenteId };
                ponente = ponenteDAO.CargarDatosPonente(ponente);

                if (ponente != null)
                {
                    ponenteDAO.ActualizarPonente(ponente);
                    cleanForm();
                    CargarPonentesEnTabla();
                    ScriptManager.RegisterStartupScript(this, GetType(), "success",
                        "alert('Ponente actualizado exitosamente'); $('#editModal').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                        "alert('No se encontró el ponente a actualizar');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al actualizar ponente: {ex.Message}');", true);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            LinkButton btn = (LinkButton)sender;
            string ponenteId = btn.CommandArgument;
            if (!string.IsNullOrEmpty(ponenteId))
            {

                if (ponenteDAO.EliminarPonente(ponenteId))
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alerta", "Swal.fire('Exito', 'Ponente editado con éxito', 'success');", true);
                    CargarPonentesEnTabla();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"alert('Error al eliminar ponente');", true);
                }
            }
        }

        private void CargarPonentesEnTabla()
        {
            PonenteDAO ponenteDao = new PonenteDAO();
            try
            {
                List<Ponente> listaDePonentes = ponenteDao.ConsultarPonentes();

                UserRepeater.DataSource = listaDePonentes;
                UserRepeater.DataBind();


            }
            catch (Exception ex)
            {
                Response.Write($"<div class='alert alert-danger'>Error al cargar ponentes: {ex.Message}</div>");
            }
        }
        private void cleanForm()
        {
            txtName.Text = "";
            txtDate.Text = "";
            txtOrigin.Text = "";
            txtDescription.Text = "";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            cleanForm();
            Session["idPonente"] = "";
        }

        protected void btnExportarPDF_Click(object sender, EventArgs e)
        {
            try
            {
                // 1️⃣ Obtener lista de ponentes desde el DAO
                var ponentes = ponenteDAO.ConsultarPonentes();

                if (ponentes == null || ponentes.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                        "Swal.fire('Sin datos','No hay ponentes para exportar.','info');", true);
                    return;
                }

                // 2️⃣ Encabezados de columnas
                var columnas = new List<string>
        {
            "Código",
            "Nombre",
            "Origen",
            "Fecha de nacimiento",
            "Descripción"
        };

                // 3️⃣ Transformar los datos a listas de texto
                var filas = ponentes.Select(p => new List<string>
        {
            p.id_ponente,
            p.nombre_ponente,
            p.Origen,
            p.fecha_nacimiento.ToString("dd/MM/yyyy"),
            p.descripcion ?? ""
        }).ToList();

                // 4️⃣ Exportar con la clase genérica
                ExportadorPDF.ExportarTabla(
                    "Listado de Ponentes",
                    columnas,
                    filas,
                    "Ponentes_Congreso2025"
                );
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"Swal.fire('Error','{ex.Message}','error');", true);
            }
        }

    }

}