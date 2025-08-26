<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuAdmin.Master"
    CodeBehind="WebCarrera.aspx.cs" Inherits="Congreso_2025.WebCarrera" %>

<asp:Content ID="ctTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Carrera
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
    Catálogos &raquo; Carrera
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="container-fluid px-0 px-sm-2">
        <h2 class="mb-4">Gestión de Carreras</h2>

        <!-- Formulario de alta -->
        <div class="card mb-4 shadow-sm">
            <div class="card-header">
                <h5 class="card-title mb-0">Añadir Nueva Carrera</h5>
            </div>
            <div class="card-body">
                <div class="row g-3 row-cols-1 row-cols-md-2">
                    <div class="col">
                        <label for="txtNombreCarrera" class="form-label">Nombre de la carrera</label>
                        <asp:TextBox ID="txtNombreCarrera" runat="server" CssClass="form-control" MaxLength="100" />
                    </div>
                </div>
                <div class="d-grid d-sm-flex gap-2 mt-3">
                    <asp:Button ID="btnAdd" runat="server" Text="Guardar" CssClass="btn btn-primary w-100 w-sm-auto" OnClick="btnAdd_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="Cancelar" CssClass="btn btn-secondary w-100 w-sm-auto" OnClick="btnClear_Click" />
                </div>
            </div>
        </div>

        <!-- Tabla con edición en línea -->
        <div class="card shadow-sm">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:GridView ID="gvCarreras" runat="server"
                        CssClass="table table-bordered table-striped table-sm align-middle table-stack"
                        AutoGenerateColumns="False" DataKeyNames="id_carrera" GridLines="None"
                        OnRowEditing="gvCarreras_RowEditing"
                        OnRowUpdating="gvCarreras_RowUpdating"
                        OnRowCancelingEdit="gvCarreras_RowCancelingEdit"
                        OnRowDeleting="gvCarreras_RowDeleting"
                        OnRowDataBound="gvCarreras_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="id_carrera" HeaderText="Código" ReadOnly="True" />
                            <asp:TemplateField HeaderText="Nombre">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("nombre_carrera") %>' />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEditNombre" runat="server" CssClass="form-control"
                                                 Text='<%# Bind("nombre_carrera") %>' MaxLength="100" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones">
                                <ItemTemplate>
                                    <div class="d-flex actions-col justify-content-center gap-2 flex-wrap">
                                        <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit"
                                            CssClass="btn btn-sm btn-warning" Text="Editar" />
                                        <asp:LinkButton ID="lnkDelete" runat="server" CommandName="Delete"
                                            CssClass="btn btn-sm btn-danger" Text="Eliminar"
                                            OnClientClick="return confirm('¿Eliminar esta carrera?');" />
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <div class="d-flex actions-col justify-content-center gap-2 flex-wrap">
                                        <asp:LinkButton ID="lnkUpdate" runat="server" CommandName="Update"
                                            CssClass="btn btn-sm btn-primary" Text="Guardar" />
                                        <asp:LinkButton ID="lnkCancel" runat="server" CommandName="Cancel"
                                            CssClass="btn btn-sm btn-secondary" Text="Cancelar" />
                                    </div>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="table-dark" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ctScripts" ContentPlaceHolderID="ScriptsContent" runat="server">
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</asp:Content>
