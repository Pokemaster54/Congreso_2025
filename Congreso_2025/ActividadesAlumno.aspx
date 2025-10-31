<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuAlumno.Master"
    CodeBehind="ActividadesAlumno.aspx.cs" Inherits="Congreso_2025.ActividadesAlumno" %>

<asp:Content ID="ctHead" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        h2 {
            color: #002b5b;
            font-weight: 700;
        }
        .card {
            border-radius: 1rem;
            border: 1px solid rgba(0,0,0,.08);
            box-shadow: 0 3px 10px rgba(0,0,0,.05);
        }
        .table th {
            background-color: #002b5b;
            color: white;
        }
        .filter-row select, .filter-row input {
            min-width: 180px;
        }
        .btn-export {
            background: linear-gradient(135deg,#4f46e5,#06b6d4);
            color: #fff;
            border: none;
            font-weight: 500;
        }
        .btn-export:hover {
            opacity: 0.9;
        }
        .link-perfil {
            text-decoration: none;
            color: #0d6efd;
            font-weight: 500;
        }
        .link-perfil:hover {
            text-decoration: underline;
        }
    </style>
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container-fluid px-0 px-sm-2">
        <h2 class="mb-4"><i class="fa-solid fa-calendar-days me-2"></i> Mis Actividades</h2>

        <!-- 🔹 Filtros -->
        <div class="card mb-3">
            <div class="card-body filter-row d-flex flex-wrap align-items-end gap-3">
                <div>
                    <label for="ddlPonente" class="form-label">Filtrar por Ponente:</label>
                    <asp:DropDownList ID="ddlPonente" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="Filtro_Changed">
                    </asp:DropDownList>
                </div>
                <div>
                    <label for="txtFecha" class="form-label">Filtrar por Fecha:</label>
                    <asp:TextBox ID="txtFecha" runat="server" CssClass="form-control" TextMode="Date" AutoPostBack="true" OnTextChanged="Filtro_Changed"></asp:TextBox>
                </div>
                <div class="ms-auto">
                    <asp:Button ID="btnExport" runat="server" CssClass="btn btn-export" Text="Exportar a PDF" OnClick="btnExport_Click" />
                </div>
            </div>
        </div>

        <!-- 🔹 Tabla -->
        <div class="card shadow-sm">
            <div class="card-body">
                <asp:GridView ID="gvActividades" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped align-middle">
                    <Columns>
                        <asp:BoundField DataField="Nombre_actividad" HeaderText="Actividad" />
                        <asp:TemplateField HeaderText="Ponente">
                            <ItemTemplate>
                                <a class="link-perfil" href='<%# "PerfilPonente.aspx?id=" + Eval("id_ponente") %>'>
                                    <%# Eval("nombre_ponente") %>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="nombre_tipo_actividad" HeaderText="Tipo" />
                        <asp:BoundField DataField="nombre_ubicacion" HeaderText="Ubicación" />
                        <asp:BoundField DataField="hora_inicio" HeaderText="Inicio" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        <asp:BoundField DataField="hora_fin" HeaderText="Fin" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblMensaje" runat="server" CssClass="text-muted"></asp:Label>
            </div>
        </div>
    </div>

</asp:Content>

<asp:Content ID="ctScripts" ContentPlaceHolderID="ScriptsContent" runat="server">
</asp:Content>
