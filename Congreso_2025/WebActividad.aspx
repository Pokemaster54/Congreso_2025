<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuAdmin.Master"
    CodeBehind="WebActividad.aspx.cs" Inherits="Congreso_2025.WebActividad" %>

<asp:Content ID="ctTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Actividad
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
    Actividades &raquo; Actividad
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Para edición -->
    <asp:HiddenField ID="hfActividadId" runat="server" />

    <div class="container-fluid px-0 px-sm-2">
        <h2 class="mb-4">Gestión de Actividades</h2>

        <!-- Formulario alta/edición -->
        <div class="card mb-4 shadow-sm">
            <div class="card-header">
                <h5 class="card-title mb-0">
                    <asp:Label ID="lblFormTitle" runat="server" Text="Añadir Nueva Actividad"></asp:Label>
                </h5>
            </div>
            <div class="card-body">
                <div class="row g-3">
                    <div class="col-12 col-md-6">
                        <label for="txtNombre" class="form-label">Nombre de la actividad</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" MaxLength="50" />
                    </div>

                    <div class="col-12 col-md-6">
                        <label for="ddlTipo" class="form-label">Tipo</label>
                        <asp:DropDownList ID="ddlTipo" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccione --" Value="" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-12 col-md-6">
                        <label for="ddlEstado" class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccione --" Value="" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-12 col-md-6">
                        <label for="ddlPonente" class="form-label">Ponente</label>
                        <asp:DropDownList ID="ddlPonente" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccione --" Value="" />
                        </asp:DropDownList>
                    </div>

                    <div class="col-12 col-md-6">
                        <label for="ddlUbicacion" class="form-label">Ubicación</label>
                        <asp:DropDownList ID="ddlUbicacion" runat="server" CssClass="form-select" AppendDataBoundItems="true">
                            <asp:ListItem Text="-- Seleccione --" Value="" />
                        </asp:DropDownList>
                    </div>

                    <!-- Inicio -->
                    <div class="col-12 col-md-6">
                        <label class="form-label">Inicio</label>
                        <div class="row g-2">
                            <div class="col-6">
                                <asp:TextBox ID="txtInicioFecha" runat="server" CssClass="form-control" TextMode="Date" />
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtInicioHora" runat="server" CssClass="form-control" TextMode="Time" />
                            </div>
                        </div>
                    </div>

                    <!-- Fin -->
                    <div class="col-12 col-md-6">
                        <label class="form-label">Fin</label>
                        <div class="row g-2">
                            <div class="col-6">
                                <asp:TextBox ID="txtFinFecha" runat="server" CssClass="form-control" TextMode="Date" />
                            </div>
                            <div class="col-6">
                                <asp:TextBox ID="txtFinHora" runat="server" CssClass="form-control" TextMode="Time" />
                            </div>
                        </div>
                    </div>

                    <div class="col-12 col-md-6">
                        <label for="txtInscritos" class="form-label">Inscritos (opcional)</label>
                        <asp:TextBox ID="txtInscritos" runat="server" CssClass="form-control" TextMode="Number" />
                    </div>
                </div>

                <div class="d-grid d-sm-flex gap-2 mt-3">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="btn btn-primary w-100 w-sm-auto" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-secondary w-100 w-sm-auto" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>

        <!-- Tabla de actividades -->
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="table-responsive">
                    <table class="table table-bordered table-striped align-middle table-stack">
                        <thead class="table-dark">
                            <tr>
                                <th>Código</th>
                                <th>Nombre</th>
                                <th>Tipo</th>
                                <th>Estado</th>
                                <th>Ponente</th>
                                <th>Ubicación</th>
                                <th>Inicio</th>
                                <th>Fin</th>
                                <th>Inscritos</th>
                                <th style="width:170px">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptActividades" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td data-label="Código"><%# Eval("id_actividad") %></td>
                                        <td data-label="Nombre"><%# Eval("Nombre_actividad") %></td>
                                        <td data-label="Tipo"><%# Eval("nombre_tipo_actividad") %></td>
                                        <td data-label="Estado"><%# Eval("nombre_estado_actividad") %></td>
                                        <td data-label="Ponente"><%# Eval("nombre_ponente") %></td>
                                        <td data-label="Ubicación"><%# Eval("nombre_ubicacion") %></td>
                                        <td data-label="Inicio"><%# Eval("hora_inicio","{0:yyyy-MM-dd HH:mm}") %></td>
                                        <td data-label="Fin"><%# Eval("hora_fin","{0:yyyy-MM-dd HH:mm}") %></td>
                                        <td data-label="Inscritos"><%# Eval("inscritos") %></td>
                                        <td data-label="Acciones">
                                            <div class="d-flex justify-content-center gap-2 flex-wrap">
                                                <asp:LinkButton ID="lnkEdit" runat="server"
                                                    CssClass="btn btn-sm btn-warning"
                                                    Text="Editar"
                                                    OnClick="lnkEdit_Click"
                                                    CommandArgument='<%# Eval("id_actividad") %>' />
                                                <asp:LinkButton ID="lnkDelete" runat="server"
                                                    CssClass="btn btn-sm btn-danger"
                                                    Text="Eliminar"
                                                    OnClick="lnkDelete_Click"
                                                    CommandArgument='<%# Eval("id_actividad") %>'
                                                    OnClientClick="return confirm('¿Eliminar esta actividad?');" />
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
