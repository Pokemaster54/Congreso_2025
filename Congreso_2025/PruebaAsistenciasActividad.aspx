<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaAsistenciasActividad.aspx.cs" Inherits="Congreso_2025.PruebaAsistenciasActividad" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="bootstrap537/css/bootstrap.min.css" rel ="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Prueba Actividad</title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="container bg-body-secondary p-3 mt-3 mb-3 col-8 fs-4 rounded-3">
     <div class="row mt-2">
         <div class="col-5 col-lg-4 text-lg-end">
             Seleccione un orden
         </div>
         <div class="col-lg-6 col-sm-12">
             <asp:DropDownList ID="DropDownListOrden" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListOrden_SelectedIndexChanged">
                 <asp:ListItem Text="-- Seleccione un orden --" Value="" />
                 <asp:ListItem Value="Descendente">Descendente</asp:ListItem>
                 <asp:ListItem Value="Ascendente">Ascendente</asp:ListItem>
             </asp:DropDownList>
         </div>
         </div>
          </div>
             <div class="h1 p-2 m-2 text-center border-bottom border-danger"></div><!-- tabal de prueba-->

            <div class="container bg-body-secondary p-3 mt-3 mb-3 col-8 fs-4 rounded-3 text-center table-responsive">
                <asp:GridView ID="GridViewLista" runat="server" CssClass="table  table-hover table-striped"></asp:GridView>
                </div>
 <div class="h1 p-2 m-2 text-center border-bottom border-danger"></div>
            </div>


    </form>
</body>
</html>
