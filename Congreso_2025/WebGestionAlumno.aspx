<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebGestionAlumno.aspx.cs" Inherits="TuProyecto.WebGestionAlumno" %>
<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <title>Gestión de Alumnos</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body { padding: 20px; }
        .grid td, .grid th { vertical-align: middle !important; }
        .w-90 { width: 90px; }
    </style>
</head>
<body>
<form id="form1" runat="server" class="container">
    <h3 class="mb-3">Gestión de Alumnos (listar / editar / eliminar)</h3>

    <div class="row g-2 align-items-center mb-3">
        <div class="col-auto"><label for="<%= txtFiltro.ClientID %>" class="col-form-label">Buscar:</label></div>
        <div class="col-auto">
            <asp:TextBox ID="txtFiltro" runat="server" CssClass="form-control" placeholder="carne, nombres o apellidos" />
        </div>
        <div class="col-auto">
            <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-primary" OnClick="btnBuscar_Click" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" CssClass="btn btn-secondary ms-2" OnClick="btnLimpiar_Click" />
        </div>
        <div class="col">
            <asp:Label ID="lblMsg" runat="server" CssClass="ms-3 text-danger"></asp:Label>
        </div>
    </div>

    <asp:GridView ID="gvAlumnos" runat="server" CssClass="table table-striped table-bordered grid"
        AutoGenerateColumns="false" DataKeyNames="id_alumno"
        OnRowEditing="gvAlumnos_RowEditing"
        OnRowCancelingEdit="gvAlumnos_RowCancelingEdit"
        OnRowUpdating="gvAlumnos_RowUpdating"
        OnRowDeleting="gvAlumnos_RowDeleting"
        AllowPaging="true" PageSize="10"
        OnPageIndexChanging="gvAlumnos_PageIndexChanging">
        <Columns>
            <asp:BoundField DataField="id_alumno" HeaderText="ID" ReadOnly="true" />

            <asp:TemplateField HeaderText="Carne">
                <ItemTemplate><%# Eval("carne") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCarne" runat="server" CssClass="form-control" Text='<%# Bind("carne") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Nombres">
                <ItemTemplate><%# Eval("nombres_alumno") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" Text='<%# Bind("nombres_alumno") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Apellidos">
                <ItemTemplate><%# Eval("apellidos_alumno") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" Text='<%# Bind("apellidos_alumno") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="IdEstado">
                <ItemTemplate><%# Eval("id_estado") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtIdEstado" runat="server" CssClass="form-control w-90" Text='<%# Bind("id_estado") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="IdUsuario">
                <ItemTemplate><%# Eval("id_usuario") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="form-control w-90" Text='<%# Bind("id_usuario") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="IdPago">
                <ItemTemplate><%# Eval("id_pago") %></ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtIdPago" runat="server" CssClass="form-control w-90" Text='<%# Bind("id_pago") %>' />
                </EditItemTemplate>
            </asp:TemplateField>

            <asp:CommandField ShowEditButton="true" EditText="Editar" UpdateText="Guardar" CancelText="Cancelar" />
            <asp:CommandField ShowDeleteButton="true" DeleteText="Eliminar" />
        </Columns>
    </asp:GridView>
</form>
</body>
</html>
