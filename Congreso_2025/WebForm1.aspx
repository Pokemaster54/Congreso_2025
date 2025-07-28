<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Congreso_2025.WebForm1" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <meta charset="UTF-8">
    <title>Ponente</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body class="bg-light py-5">
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <!-- Hidden field para almacenar el ID del ponente cuando se está editando -->
        <asp:HiddenField ID="hfPonenteId" runat="server" />

        <div class="container">
            <h2 class="mb-4">Panel Ponente</h2>

            <!-- Formulario para Añadir/Editar -->
            <div class="card mb-4">
                <div class="card-header">
                    <h5 class="card-title mb-0">
                        <asp:Label ID="lblFormTitle" runat="server" Text="Añadir Nuevo Ponente"></asp:Label>
                    </h5>
                </div>
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label for="txtName" class="form-label">Nombre</label>
                            <asp:TextBox ID="txtName" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label for="txtDate" class="form-label">Fecha de nacimiento</label>
                            <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control" required="true"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6 mb-3">
                            <label for="txtOrigin" class="form-label">Origen (lugar)</label>
                            <asp:TextBox ID="txtOrigin" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                        </div>
                        <div class="col-md-6 mb-3">
                            <label for="txtDescription" class="form-label">Descripción</label>
                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                        </div>
                    </div>
                    <div class="d-flex gap-2">
                        <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
                    </div>
                </div>
            </div>

            <!-- Tabla de Ponentes -->
            <div class="card">
                <div class="card-body">
                    <table class="table table-bordered table-striped">
                        <thead class="table-dark">
                            <tr>
                                <th>Codigo</th>
                                <th>Nombre</th>
                                <th>Origen</th>
                                <th>Fecha de nacimiento</th>
                                <th>Descripcion</th>
                                <th>Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="UserRepeater" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td><%# Eval("id_ponente") %></td>
                                        <td><%# Eval("nombre_ponente") %></td>
                                        <td><%# Eval("origen") %></td>
                                        <td><%# Eval("fecha_nacimiento", "{0:yyyy-MM-dd}") %></td>
                                        <td><%# Eval("descripcion") %></td>
                                        <td>
                                            <div class="d-flex justify-content-center gap-2">
                                                <asp:LinkButton ID="lnkEdit" runat="server"
                                                    CssClass="btn btn-sm btn-warning"
                                                    Text="Editar"
                                                    OnClick="lnkEdit_Click"
                                                    CommandArgument='<%# Eval("id_ponente") %>' />
                                                <asp:LinkButton ID="lnkDelete" runat="server"
                                                    CssClass="btn btn-sm btn-danger"
                                                    Text="Eliminar"
                                                    OnClick="lnkDelete_Click"
                                                    CommandArgument='<%# Eval("id_ponente") %>'
                                                    OnClientClick="return confirm('¿Está seguro de que desea eliminar este ponente?');" />
                                            </div>
                                        </td>
                                    </tr>
                                </ItemTemplate>
                            </asp:Repeater>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
