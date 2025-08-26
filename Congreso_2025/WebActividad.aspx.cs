using Congreso_2025.Clases;
using Congreso_2025.Clases.DataAccessObjects;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Congreso_2025
{
    public partial class WebActividad : System.Web.UI.Page
    {
        General general = new General();
        ActividadDAO actividadDAO = new ActividadDAO();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarTabla();
                Session["idActividad"] = "";
            }
        }

        private void CargarCombos()
        {
            // Tipo
            ddlTipo.Items.Clear();
            ddlTipo.Items.Add(new ListItem("-- Seleccione --", ""));
            ddlTipo.DataSource = actividadDAO.ObtenerTiposActividad();
            ddlTipo.DataTextField = "nombre_tipo_actividad";
            ddlTipo.DataValueField = "id_tipo_actividad";
            ddlTipo.DataBind();

            // Estado
            ddlEstado.Items.Clear();
            ddlEstado.Items.Add(new ListItem("-- Seleccione --", ""));
            ddlEstado.DataSource = actividadDAO.ObtenerEstadosActividad();
            ddlEstado.DataTextField = "nombre_estado_actividad";
            ddlEstado.DataValueField = "id_estado_actividad";
            ddlEstado.DataBind();

            // Ponente
            ddlPonente.Items.Clear();
            ddlPonente.Items.Add(new ListItem("-- Seleccione --", ""));
            ddlPonente.DataSource = actividadDAO.ObtenerPonentes();
            ddlPonente.DataTextField = "nombre_ponente";
            ddlPonente.DataValueField = "id_ponente";
            ddlPonente.DataBind();

            // Ubicación
            ddlUbicacion.Items.Clear();
            ddlUbicacion.Items.Add(new ListItem("-- Seleccione --", ""));
            ddlUbicacion.DataSource = actividadDAO.ObtenerUbicaciones();
            ddlUbicacion.DataTextField = "nombre_ubicacion";
            ddlUbicacion.DataValueField = "id_ubicacion";
            ddlUbicacion.DataBind();
        }

        private void CargarTabla()
        {
            try
            {
                var lista = actividadDAO.ConsultarActividadesListado(); // incluye nombres
                rptActividades.DataSource = lista;
                rptActividades.DataBind();
            }
            catch (Exception ex)
            {
                Response.Write($"<div class='alert alert-danger'>Error al cargar actividades: {ex.Message}</div>");
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string idEdit = Session["idActividad"] as string;

            string nombre = txtNombre.Text?.Trim();
            string idTipo = ddlTipo.SelectedValue;
            string idEstado = ddlEstado.SelectedValue;
            string idPonente = ddlPonente.SelectedValue;
            string idUbicacion = ddlUbicacion.SelectedValue;

            if (string.IsNullOrWhiteSpace(nombre) ||
                string.IsNullOrEmpty(idTipo) ||
                string.IsNullOrEmpty(idEstado) ||
                string.IsNullOrEmpty(idPonente) ||
                string.IsNullOrEmpty(idUbicacion) ||
                string.IsNullOrWhiteSpace(txtInicioFecha.Text) ||
                string.IsNullOrWhiteSpace(txtInicioHora.Text) ||
                string.IsNullOrWhiteSpace(txtFinFecha.Text) ||
                string.IsNullOrWhiteSpace(txtFinHora.Text))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','Completa todos los campos obligatorios.','warning');", true);
                return;
            }

            DateTime inicio, fin;
            try
            {
                inicio = ParseDateTime(txtInicioFecha.Text, txtInicioHora.Text);
                fin = ParseDateTime(txtFinFecha.Text, txtFinHora.Text);
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','Formato de fecha/hora inválido.','warning');", true);
                return;
            }

            if (fin <= inicio)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                    "Swal.fire('Atención','La hora de fin debe ser posterior al inicio.','warning');", true);
                return;
            }

            int? inscritos = null;
            if (!string.IsNullOrWhiteSpace(txtInscritos.Text))
            {
                if (int.TryParse(txtInscritos.Text, out var ins))
                    inscritos = ins;
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "warn",
                        "Swal.fire('Atención','Inscritos debe ser un número.','warning');", true);
                    return;
                }
            }

            string swal;
            if (!string.IsNullOrEmpty(idEdit))
            {
                // UPDATE
                var entidad = new Actividad
                {
                    id_actividad = idEdit,
                    Nombre_actividad = nombre,
                    id_tipo_actividad = idTipo,
                    id_estado_actividad = idEstado,
                    id_ponente = idPonente,
                    id_ubicacion = idUbicacion,
                    hora_inicio = inicio,
                    hora_fin = fin,
                    inscritos = inscritos
                };

                if (actividadDAO.ActualizarActividad(entidad))
                {
                    swal = "Swal.fire('Éxito','Actividad actualizada con éxito.','success');";
                    Session["idActividad"] = "";
                    lblFormTitle.Text = "Añadir Nueva Actividad";
                    LimpiarFormulario();
                    CargarTabla();
                }
                else
                {
                    swal = "Swal.fire('Error','No se pudo actualizar la actividad.','error');";
                }
                ScriptManager.RegisterStartupScript(this, GetType(), "alerta", swal, true);
                return;
            }

            // INSERT
            if (actividadDAO.InsertarActividad(nombre, idTipo, idEstado, idPonente, idUbicacion, inicio, fin, inscritos))
            {
                swal = "Swal.fire('Éxito','Actividad añadida con éxito.','success');";
                LimpiarFormulario();
                CargarTabla();
            }
            else
            {
                swal = "Swal.fire('Error','No se pudo añadir la actividad.','error');";
            }
            ScriptManager.RegisterStartupScript(this, GetType(), "alerta", swal, true);
        }

        protected void lnkEdit_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            string id = btn.CommandArgument;
            Session["idActividad"] = id;
            CargarDatosParaEditar(id);
        }

        private void CargarDatosParaEditar(string id)
        {
            try
            {
                var act = actividadDAO.CargarActividadPorId(id);
                if (act != null)
                {
                    hfActividadId.Value = act.id_actividad;
                    txtNombre.Text = act.Nombre_actividad;
                    ddlTipo.SelectedValue = act.id_tipo_actividad;
                    ddlEstado.SelectedValue = act.id_estado_actividad;
                    ddlPonente.SelectedValue = act.id_ponente;
                    ddlUbicacion.SelectedValue = act.id_ubicacion;

                    txtInicioFecha.Text = act.hora_inicio.ToString("yyyy-MM-dd");
                    txtInicioHora.Text = act.hora_inicio.ToString("HH:mm");
                    txtFinFecha.Text = act.hora_fin.ToString("yyyy-MM-dd");
                    txtFinHora.Text = act.hora_fin.ToString("HH:mm");

                    txtInscritos.Text = act.inscritos.HasValue ? act.inscritos.Value.ToString() : "";

                    lblFormTitle.Text = "Editar Actividad";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"Swal.fire('Error','{ex.Message}','error');", true);
            }
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            var btn = (LinkButton)sender;
            string id = btn.CommandArgument;

            if (actividadDAO.EliminarActividad(id))
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Éxito','Actividad eliminada con éxito.','success');", true);
                CargarTabla();
            }
            else
            {
                // Puede estar referenciada por carrera_actividad
                ScriptManager.RegisterStartupScript(this, GetType(), "err",
                    "Swal.fire('Error','No se pudo eliminar. Verifica si está en uso.','error');", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
            Session["idActividad"] = "";
            lblFormTitle.Text = "Añadir Nueva Actividad";
        }

        private void LimpiarFormulario()
        {
            hfActividadId.Value = "";
            txtNombre.Text = "";
            ddlTipo.ClearSelection(); ddlTipo.Items[0].Selected = true;
            ddlEstado.ClearSelection(); ddlEstado.Items[0].Selected = true;
            ddlPonente.ClearSelection(); ddlPonente.Items[0].Selected = true;
            ddlUbicacion.ClearSelection(); ddlUbicacion.Items[0].Selected = true;

            txtInicioFecha.Text = "";
            txtInicioHora.Text = "";
            txtFinFecha.Text = "";
            txtFinHora.Text = "";
            txtInscritos.Text = "";
        }

        private DateTime ParseDateTime(string date, string time)
        {
            // date: yyyy-MM-dd, time: HH:mm
            return DateTime.ParseExact($"{date} {time}", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);
        }
    }
}
