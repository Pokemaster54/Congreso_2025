<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuAdmin.Master"
    CodeBehind="WebPonente.aspx.cs" Inherits="Congreso_2025.WebForm1" %>

<asp:Content ID="ctTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Ponente
</asp:Content>

<asp:Content ID="ctHead" ContentPlaceHolderID="HeadContent" runat="server">
    <!-- Librerías específicas de esta página -->
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Ajustes responsive sin cambiar funcionalidad -->
    <style>
        /* Hace que el thead quede fijo al hacer scroll horizontal en móviles */
        .table-responsive thead th {
            position: sticky;
            top: 0;
            z-index: 1;
        }
        /* Tabla apilada en pantallas muy pequeñas */
        @media (max-width: 575.98px) {
            .table-stack tbody,
            .table-stack tr,
            .table-stack td,
            .table-stack thead {
                display: block;
                width: 100%;
            }

            .table-stack thead {
                display: none;
            }

            .table-stack tr {
                border: 1px solid rgba(0,0,0,.125);
                border-radius: .5rem;
                margin-bottom: .75rem;
                background: #fff;
                overflow: hidden;
            }

            .table-stack td {
                display: flex;
                padding: .5rem .75rem;
                border: none !important;
                border-bottom: 1px solid rgba(0,0,0,.075) !important;
            }

                .table-stack td:last-child {
                    border-bottom: none !important;
                }

                .table-stack td::before {
                    content: attr(data-label);
                    flex: 0 0 42%;
                    max-width: 42%;
                    font-weight: 600;
                    color: #374151;
                    padding-right: .5rem;
                }

            .actions-col {
                justify-content: flex-start !important;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="ctBreadcrumb" ContentPlaceHolderID="BreadcrumbContent" runat="server">
    Personas y Accesos &raquo; Ponente
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">
    <!-- Debe vivir dentro del <form> de la master page -->
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Hidden field para almacenar el ID del ponente cuando se está editando -->
    <asp:HiddenField ID="hfPonenteId" runat="server" />

    <div class="container-fluid px-0 px-sm-2">
        <h2 class="mb-4">Panel Ponente</h2>

        <!-- Formulario para Añadir/Editar -->
        <div class="card mb-4 shadow-sm">
            <div class="card-header">
                <h5 class="card-title mb-0">
                    <asp:Label ID="lblFormTitle" runat="server" Text="Añadir Nuevo Ponente"></asp:Label>
                </h5>
            </div>
            <div class="card-body">


                <div class="row g-3 row-cols-1 row-cols-md-2">
                    <div class="col col-12 col-md-6">
                        <label for="txtName" class="form-label">Nombre</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                    </div>
                    <div class="col col-12 col-md-6">
                        <label for="txtDate" class="form-label">Fecha de nacimiento</label>
                        <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control" required="true"></asp:TextBox>
                    </div>
                    <div class="col col-12 col-md-6">
                        <label for="txtOrigin" class="form-label">Origen (lugar)</label>
                        <asp:TextBox ID="txtOrigin" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                    </div>
                    <div class="col col-12 col-md-6">
                        <label for="txtDescription" class="form-label">Descripción</label>
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </div>
                </div>

                <!-- Botones: en móvil son a ancho completo, en >=sm se muestran en fila -->
                <div class="d-grid d-sm-flex gap-2 mt-3">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="btn btn-primary w-100 w-sm-auto" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-secondary w-100 w-sm-auto" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>

        <!-- Tabla de Ponentes -->
        <div class="card shadow-sm">
            <div class="card-body">
                <asp:Button ID="btnExportarPDF" runat="server"
                    Text="Exportar a PDF"
                    CssClass="btn btn-danger mb-3"
                    OnClick="btnExportarPDF_Click"
                    CausesValidation="false" />
                <div class="table-responsive">
                    <table class="table table-bordered table-striped table-sm align-middle table-stack">
                        <thead class="table-dark">
                            <tr>
                                <th scope="col">Código</th>
                                <th scope="col">Nombre</th>
                                <th scope="col">Origen</th>
                                <th scope="col">Fecha de nacimiento</th>
                                <th scope="col">Descripción</th>
                                <th scope="col" style="width: 160px">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="UserRepeater" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td data-label="Código"><%# Eval("id_ponente") %></td>
                                        <td data-label="Nombre"><%# Eval("nombre_ponente") %></td>
                                        <td data-label="Origen"><%# Eval("origen") %></td>
                                        <td data-label="Fecha de nacimiento"><%# Eval("fecha_nacimiento", "{0:yyyy-MM-dd}") %></td>
                                        <td data-label="Descripción"><%# Eval("descripcion") %></td>
                                        <td data-label="Acciones">
                                            <div class="d-flex actions-col justify-content-center gap-2 flex-wrap">
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
    </div>
</asp:Content>

<asp:Content ID="ctScripts" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
