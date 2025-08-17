<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PruebaAsistencia.aspx.cs" Inherits="Congreso_2025.PruebaAsistencia" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
        <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="bootstrap537/css/bootstrap.min.css" rel ="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>Asistencia</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="h1 p-2 m-2 text-center border-bottom border-danger">Tipo de usuario</div>
        <!--Inicio de tabla de ingreso de datos-->
        <div class="container bg-body-secondary p-3 mt-3 mb-3 col-8 fs-4 rounded-3">
            
                <div class="col">
                    <div class="Row">
                        <div class="col-3">
                            <asp:Label ID="Label1" runat="server" Text="Buscar por"></asp:Label>
                        </div>
                        <div class="col-3">
                            <asp:DropDownList ID="DropDownListElemento" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownListElentos_SelectedIndexChanged">
                                <asp:ListItem Value="1">Carrera</asp:ListItem>
                                <asp:ListItem Value="2">Tipo</asp:ListItem>
                                <asp:ListItem Value="3">Evento</asp:ListItem>
                                <asp:ListItem Value="4">Fecha</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col">
                        <asp:DropDownList ID="DropDownListSeleccion" runat="server" OnSelectedIndexChanged="DropDownListSeleccion_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                    </div>
                                        <div class="col">
                        <asp:Calendar ID="CalendarFecha" runat="server"></asp:Calendar>
                    </div>
                </div>
           
                </div>
                        <div class=" table">
                    <asp:GridView ID="GridViewLista" runat="server" CssClass=" table-striped"></asp:GridView>
                </div>

    </form>
</body>
</html>
