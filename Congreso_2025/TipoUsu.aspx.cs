using Congreso_2025.Clases;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Congreso_2025.DataBase;



namespace Congreso_2025
{

    public partial class TipoUsu : System.Web.UI.Page
    {
        General general = new General();

        public void Carga()
        {
            using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
            {
                var consultaTU = from tu in miLinQ.Tipo_usuario 
                                 select tu;
                GridViewListaTipo.DataSource = consultaTU;
                GridViewListaTipo.DataBind();
                string ultimoId = miLinQ.Tipo_usuario
                    .Max(tu => tu.id_tipo_usuario);
                string ultimo = ultimoId.Substring(ultimoId.Length - 3);//se debe corregir a Length-4
                int nuevoN = int.Parse(ultimo) + 1;
                    string NuevoId = "TU" + nuevoN.ToString("D4");
                TextBoxCodTipo.Text=NuevoId;
            }
            TextBoxCodTipo.Enabled = false;
            TextBoxTipo.Text = "";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Request.QueryString["confirmar"] =="true")
            {
                string idt = Request.QueryString["id"];
                elimina(idt);
            }

            if (!IsPostBack)
            {
                Carga();
            }

        }
        public void guarda()
        {
            try
            {
                if (TextBoxTipo.Text != "" && TextBoxCodTipo.Text != "")
                {
                    try
                    {
                        using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                        {
                            DataBase.Tipo_usuario NuevoTipo = new DataBase.Tipo_usuario
                            {
                                id_tipo_usuario = TextBoxCodTipo.Text,
                                nombre_tipo = TextBoxTipo.Text
                            };
                            miLinQ.Tipo_usuario.InsertOnSubmit(NuevoTipo);
                            miLinQ.SubmitChanges();
                            Response.Write("<script>alert('Los datos se actualizaron correctamente')</script>");
                        }
                        Carga();

                    }
                    catch
                    {
                        Response.Write("<script>alert('Ocurrio un erro al intenta ingresar los datos')</script>");
                    }
                }
                else
                    Response.Write("<script>alert('Son necesarios todos los campos')</script>");
            }
            catch
            {
                Response.Write("<script>alert('Ocurrio un error al intentar Ingresar los datos')</script>");
            }
        }
        protected void ButtonAgregar_Click(object sender, EventArgs e)
        {
            guarda();
        }
        public void Edita(string valorId,string valorTipo)
        {
            try
            {
                if (valorTipo != "" && valorTipo != null)
                {
                    using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                    {
                        var consulta = from tu in miLinQ.Tipo_usuario
                                       where tu.id_tipo_usuario == valorId
                                       select tu;
                        foreach (var fila in consulta)
                        {
                            fila.nombre_tipo = valorTipo;
                        }
                        miLinQ.SubmitChanges();
                        Carga();
                        Response.Write("<script>alert('Se actualizaron los datos de forma correcta')</script>");
                    }
                }
                else
                    Response.Write("<script>alert('Es necesario que complete el campo')</script>");
            }
            catch
            {
                Response.Write("<script>alert('Ocurrio un error al intentar actualizar los datos')</script>");
            }

        }

        public void elimina(string valorId)
        {
            try
            {
                using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                {
                    var consulta = from tu in miLinQ.Tipo_usuario
                                   where tu.id_tipo_usuario == valorId
                                   select tu;
                    miLinQ.Tipo_usuario.DeleteOnSubmit(consulta.FirstOrDefault());
                    miLinQ.SubmitChanges();
                    Response.Write($@"<script>if('Los datos se eliminaron exitosamente') {{window.location = 'TipoUsu.aspx?id={""}&confirmar=false';}}</script>");
                }
            }
            catch 
            {
                Response.Write("<script>alert('Ocurrio un error al intentar eliminar los datos')</script>");
            }
        }
        protected void GridViewListaTipo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string valorTipo ="";
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row =GridViewListaTipo.Rows[index];
            TextBox ValorTb = (TextBox)row.FindControl("TextBoxTipo");
                if (ValorTb != null)
                {
                    valorTipo = ValorTb.Text;
                }
            string valorId = row.Cells[0].Text;
                if (e.CommandName == "Editar")
                {
                    Edita(valorId,valorTipo);
                }
                else if (e.CommandName == "Eliminar")
                {
                Response.Write($@"<script>if (confirm('¿Deseas eliminar {valorTipo}?')) {{window.location = 'TipoUsu.aspx?id={valorId}&confirmar=true';}}</script>");
            }
        }
    }
}