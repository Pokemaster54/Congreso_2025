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
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Carga();
            }

        }
        public void guarda()
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
                        Response.Write("<script>alert('Los datos se guardaron correctamente')</script>");
                    }
                    Carga();

                }
                catch
                {
                    Response.Write("<script>alert('Ocurrio un erro al intenta guardar los datos')</script>");
                }
            }
            else
                Response.Write("<script>alert('Son necesarios todos los campos')</script>");
        }
        protected void ButtonAgregar_Click(object sender, EventArgs e)
        {
            guarda();
        }


        protected void GridViewListaTipo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName=="Editar")
            {

            }
            if(e.CommandName=="Eliminar")
            {

            }
        }
    }
}