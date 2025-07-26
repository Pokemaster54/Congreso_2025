<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoUsu.aspx.cs" Inherits="Congreso_2025.TipoUsu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <title>Tipo_Usuario</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="h1 p-2 m-4 text-center border-bottom border-danger text-white">Ingreso de usuario</div>
        <!--Inicio de tabla de ingreso de datos-->
        <div class="container">
            <div class="row">
                <div class="col">
                    ID tipo:
                </div>
                <div class="col">
                    <asp:TextBox ID="TextBoxIdTipo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                            <div class="row">
                <div class="col">
                    Tipo:
                </div>
                <div class="col">
                    <asp:TextBox ID="TextBoxTipo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
        </div>
            </div>
        <div class="h1 p-2 m-4 text-center border-bottom border-danger text-white"></div>
        <!--Fin de tabla de ingresos-->
    </form>
</body>
</html>
