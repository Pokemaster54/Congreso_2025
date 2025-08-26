<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuAdmin.Master"
    CodeBehind="WebTipoUsuario.aspx.cs" Inherits="Congreso_2025.WebTipoUsuario" %>

<asp:Content ID="ctTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Tipo de usuario
</asp:Content>

<asp:Content ID="ctHead" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .table-responsive thead th { position: sticky; top: 0; z-index: 1; }
        @media (max-width: 575.98px) {
            .table-stack tbody, .table-stack tr, .table-stack td, .table-stack thead { display:block; width:100%; }
            .table-stack thead { display:none; }
            .table-stack tr { border:1px solid rgba(0,0,0,.125); border-radius:.5rem; margin-bottom:.75rem; background:#fff; overflow:hidden; }
            .table-stack td { display:flex; padding:.5rem .75rem; border:none !important; border-bottom:1px solid rgba(0,0,0,.075) !important; }
            .table-stack td:last-child { border-bottom:none !important; }
            .table-stack td::before { content: attr(data-label); flex:0 0 42%; max-width:42%; font-weight:600; color:#374151; padding-right:.5rem; }
            .actions-col { justify-content:flex-start !important; }
        }
    </style>
</asp:Content>

<asp:Content ID="ctBreadcrumb" ContentPlaceHolderID="BreadcrumbContent" runat="server">
    Personas y Accesos &raquo; Tipo de usuario
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Hidden para edición -->
    <asp:HiddenField ID="hfTipoUsuarioId" runat="server" />

    <div class="container-fluid px-0 px-sm-2">
        <h2 class="mb-4">Gestión de Tipos de usuario</h2>

        <!-- Formulario -->
        <div class="card mb-4 shadow-sm">
            <div class="card-header">
                <h5 class="card-title mb-0">
                    <asp:Label ID="lblFormTitle" runat="server" Text="Añadir Nuevo Tipo de usuario"></asp:Label>
                </h5>
            </div>
            <div class="card-body">
                <div class="row g-3 row-cols-1 row-cols-md-2">
                    <div class="col">
                        <label for="txtNombreTipo" class="form-label">Nombre del tipo</label>
                        <asp:TextBox ID="txtNombreTipo" runat="server" CssClass="form-control" MaxLength="50" required="true" />
                    </div>
                </div>

                <div class="d-grid d-sm-flex gap-2 mt-3">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="btn btn-primary w-100 w-sm-auto" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-secondary w-100 w-sm-auto" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>

        <!-- Tabla -->
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-bordered table-striped table-sm align-middle table-stack">
                        <thead class="table-dark">
                            <tr>
                                <th scope="col">Código</th>
                                <th scope="col">Nombre</th>
                                <th scope="col" style="width:160px">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="UserRepeater" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td data-label="Código"><%# Eval("id_tipo_usuario") %></td>
                                        <td data-label="Nombre"><%# Eval("nombre_tipo") %></td>
                                        <td data-label="Acciones">
                                            <div class="d-flex actions-col justify-content-center gap-2 flex-wrap">
                                                <asp:LinkButton ID="lnkEdit" runat="server"
                                                    CssClass="btn btn-sm btn-warning"
                                                    Text="Editar"
                                                    OnClick="lnkEdit_Click"
                                                    CommandArgument='<%# Eval("id_tipo_usuario") %>' />
                                                <asp:LinkButton ID="lnkDelete" runat="server"
                                                    CssClass="btn btn-sm btn-danger"
                                                    Text="Eliminar"
                                                    OnClick="lnkDelete_Click"
                                                    CommandArgument='<%# Eval("id_tipo_usuario") %>'
                                                    OnClientClick="return confirm('¿Está seguro de que desea eliminar este tipo?');" />
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
    </div>
</asp:Content>

<asp:Content ID="ctScripts" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
