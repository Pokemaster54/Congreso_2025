<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TipoUsu.aspx.cs" Inherits="Congreso_2025.TipoUsu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <link href="bootstrap537/css/bootstrap.min.css" rel ="stylesheet" />
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <title>Tipo_Usuario</title>
</head>
<body >
    <form id="form1" runat="server">
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
                <div class="col-lg-2 col-sm-10 d-grid gap-2 mx-auto   text-lg-center text-sm-center m-2 ">
                    <asp:Button ID="ButtonAgregar" runat="server" class="btn btn-outline-success btn-lg fw-bold" Text="Agregar" OnClick="ButtonAgregar_Click" />
                </div>                  
          <!--Fin Botones-->
                </div>
            </div>
                <div class="h1 p-2 m-2 text-center border-bottom border-danger"></div>
        <!--Inicio de Grid-->
            <div class="container bg-body-secondary p-3 mt-3 mb-3 col-8 fs-4 rounded-3 text-center table-responsive">
                <asp:GridView ID="GridViewListaTipo" runat="server" CssClass="table  table-hover table-striped" AutoGenerateColumns="False" OnRowCommand="GridViewListaTipo_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="id_tipo_usuario" HeaderText="Id Tipo" />
                        <asp:TemplateField HeaderText="Tipo">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("nombre_tipo") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control" Text='<%# Bind("nombre_tipo") %>'></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:ButtonField ButtonType="Button" Text="Editar" ControlStyle-CssClass="btn btn-warning">
<ControlStyle CssClass="btn btn-warning"></ControlStyle>
                        </asp:ButtonField>
                        <asp:ButtonField ButtonType="Button" Text="Eliminar" ControlStyle-CssClass="btn btn-danger">
<ControlStyle CssClass="btn btn-danger"></ControlStyle>
                        </asp:ButtonField>
                    </Columns>
                </asp:GridView>
                </div>
        <!--Fin de Grid-->
    </form>
</body>
</html>
