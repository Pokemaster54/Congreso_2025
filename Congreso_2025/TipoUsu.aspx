<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoUsu.aspx.cs" Inherits="Congreso_2025.TipoUsu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
Luis
    <link href="bootstrap537/css/bootstrap.min.css" rel ="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
 master
    <title>Tipo_Usuario</title>
</head>
<body >
    <form id="form1" runat="server">
 Luis
        <div class="h1 p-2 m-2 text-center border-bottom border-danger">Tipo de usuario</div>
        <!--Inicio de tabla de ingreso de datos-->
        <div class="container bg-body-secondary p-3 mt-3 mb-3 col-8 fs-4 rounded-3">
            <div class="row mt-2">
                <div class="col-5 col-lg-4 text-lg-end">
                    ID tipo:
                </div>
                <div class="col-lg-6 col-sm-12">
                    <asp:TextBox ID="TextBoxCodTipo" runat="server" CssClass="form-control "></asp:TextBox>
                </div>
                </div>
                 <div class="row mt-2 mb-2">
                <div class="col-5 col-lg-4 text-lg-end">
                    Tipo:
                </div>
                <div class=" col-lg-6 col-sm-12">
                    <asp:TextBox ID="TextBoxTipo" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            </div>
        <div class="h1 p-2 m-2 text-center border-bottom border-danger"></div>
        <!--Fin de tabla de ingresos-->
        <!--Inicio Botones-->
        <div class="container mt-3 mb-3 bg-body-secondary col-8 rounded-3 pt-2 pb-2">
            <div class="row ">
                <div class="col-lg-2 col-sm-10 d-grid gap-2 mx-sm-auto me-lg-0  text-lg-center text-sm-center m-2 ">
                    <asp:Button ID="ButtonAgregar" runat="server" class="btn btn-outline-success btn-lg fw-bold" Text="Agregar" />
                </div>
                <div class="col-lg-2 col-sm-10 d-grid gap-2 mx-sm-auto mx-lg-0 text-lg-center text-sm-center m-2">
                    <asp:Button ID="ButtonEditar" runat="server" class="btn btn-outline-warning btn-lg fw-bold" Text="Editar" />
                </div>
                 <div class="col-lg-2 col-sm-10 d-grid mx-sm-auto gap-2 ms-lg-0 text-lg-start text-sm-center m-2">
                     <asp:Button ID="ButtonEliminar" runat="server" class="btn btn-outline-danger btn-lg fw-bold" Text="Eliminar" />
                     </div>
                </div>
            </div>
                  <div class="h1 p-2 m-2 text-center border-bottom border-danger"></div>
          <!--Fin Botones-->
            <!--Inicio Editar-->
    <div class="container p-3 mt-3 col-8 rounded-3">
            <div class="row">
                <div class="col-lg-6 col-sm-11 d-grid gap-2 mx-sm-auto me-lg-0  text-lg-end text-sm-center m-2 ">
                    <asp:TextBox ID="TextBoxBuscar" runat="server" CssClass=" form-control" placeholder="Para editar ingrese el id"></asp:TextBox>
                </div>
                 <div class="col-lg-2 col-sm-10 d-grid mx-sm-auto gap-2 ms-lg-0 text-lg-start text-sm-center m-2">
                     <asp:Button ID="Button1" runat="server" class="btn btn-primary btn-lg fw-bold" Text="Buscar" />
            </div>
                </div>
        </div>
        <div class="h1 p-2 m-4 text-white text-center border-bottom border-danger"></div>
        <!--Fin Editar-->
        <!--Inicio de Grid-->
        <asp:GridView ID="GridViewListaTipoUs" runat="server" CssClass="table table-striped table-secondary table-hover"></asp:GridView>
        <!--Fin de Grid-->

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
master
    </form>
</body>
</html>
