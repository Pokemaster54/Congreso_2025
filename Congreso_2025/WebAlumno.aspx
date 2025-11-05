<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuAdmin.Master"
    CodeBehind="WebAlumno.aspx.cs" Inherits="Congreso_2025.WebAlumno" %>

<asp:Content ID="ctTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Alumno
</asp:Content>

<asp:Content ID="ctHead" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <style>
        .table-responsive thead th {
            position: sticky;
            top: 0;
            z-index: 1;
        }

        @media (max-width: 575.98px) {
            .table-stack tbody, .table-stack tr, .table-stack td, .table-stack thead {
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
    Personas y Accesos &raquo; Alumno
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfAlumnoId" runat="server" />

    <div class="container-fluid px-0 px-sm-2">
        <h2 class="mb-4">Bienvenido, 
    <asp:Label ID="lblNombreAlumno" runat="server" Text="Alumno"></asp:Label>
        </h2>

        <div class="card mb-4 shadow-sm">
            <div class="card-header">
                <h5 class="card-title mb-0">
                    <asp:Label ID="lblFormTitle" runat="server" Text="Añadir Nuevo Alumno"></asp:Label>
                </h5>
            </div>
            <div class="card-body">

                <div class="row g-3 row-cols-1 row-cols-md-2">
                    <div class="col">
                        <label for="txtCarne" class="form-label">Carné</label>
                        <asp:TextBox ID="txtCarne" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label for="txtNombre" class="form-label">Nombres</label>
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label for="txtApellido" class="form-label">Apellidos</label>
                        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" required="true"></asp:TextBox>
                    </div>
                    <div class="col">
                        <label for="ddlCarrera" class="form-label">Carrera</label>
                        <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                    <div class="col">
                        <label for="ddlEstado" class="form-label">Estado</label>
                        <asp:DropDownList ID="ddlEstado" runat="server" CssClass="form-select"></asp:DropDownList>
                    </div>
                </div>

                <div class="d-grid d-sm-flex gap-2 mt-3">
                    <asp:Button ID="btnSave" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnSave_Click" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
                </div>
            </div>
        </div>

        <div class="card shadow-sm">
            <div class="card-body">
                <asp:Button ID="btnExportarPDF" runat="server"
                    Text="Exportar a PDF"
                    CssClass="btn btn-danger mb-3"
                    OnClick="btnExportarPDF_Click" 
                    CausesValidation ="false"
                    />
                <div class="table-responsive">
                    <table class="table table-bordered table-striped table-sm align-middle table-stack">
                        <thead class="table-dark">
                            <tr>
                                <th>ID</th>
                                <th>Carné</th>
                                <th>Nombre completo</th>
                                <th>Carrera</th>
                                <th>Estado</th>
                                <th style="width: 160px">Acciones</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:Repeater ID="rptAlumnos" runat="server">
                                <ItemTemplate>
                                    <tr>
                                        <td data-label="ID"><%# Eval("id_alumno") %></td>
                                        <td data-label="Carné"><%# Eval("carne") %></td>
                                        <td data-label="Nombre completo"><%# Eval("nombreCompleto") %></td>
                                        <td data-label="Carrera"><%# Eval("nombre_carrera") %></td>
                                        <td data-label="Estado"><%# Eval("nombre_estado") %></td>
                                        <td data-label="Acciones">
                                            <div class="d-flex actions-col justify-content-center gap-2 flex-wrap">
                                                <asp:LinkButton ID="lnkEdit" runat="server"
                                                    CssClass="btn btn-sm btn-warning"
                                                    Text="Editar"
                                                    CommandArgument='<%# Eval("id_alumno") %>'
                                                    OnClick="lnkEdit_Click" />
                                                <asp:LinkButton ID="lnkDelete" runat="server"
                                                    CssClass="btn btn-sm btn-danger"
                                                    Text="Eliminar"
                                                    CommandArgument='<%# Eval("id_alumno") %>'
                                                    OnClientClick="return confirm('¿Desea eliminar este alumno?');"
                                                    OnClick="lnkDelete_Click" />
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
