<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuCatedratico.Master"
    CodeBehind="Actividades.aspx.cs" Inherits="Congreso_2025.Actividades" %>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .filter-box {
            background: #fff;
            border: 1px solid rgba(0,0,0,.08);
            border-radius: 12px;
            padding: 16px;
            margin-bottom: 20px;
            box-shadow: 0 4px 12px rgba(0,0,0,.04);
        }
        .card-actividad {
            border: 1px solid rgba(0,0,0,.08);
            border-radius: 12px;
            background: #fff;
            box-shadow: 0 6px 16px rgba(0,0,0,.05);
            padding: 18px;
            transition: all .2s ease;
        }
        .card-actividad:hover {
            transform: translateY(-3px);
            box-shadow: 0 10px 24px rgba(0,0,0,.08);
        }
        .badge-carrera {
            background: rgba(0,43,91,.1);
            color: #002b5b;
            border-radius: 8px;
            padding: 4px 8px;
            margin: 2px;
            font-size: 0.85rem;
        }
    </style>

    <div class="container-fluid">
        <h3 class="fw-bold mb-3"><i class="fa-solid fa-calendar-days me-2"></i>Actividades</h3>

        <!-- 🔹 Filtros -->
        <div class="filter-box mb-4">
            <div class="row align-items-end">
                <div class="col-md-4">
                    <label class="form-label fw-semibold">Filtrar por carrera</label>
                    <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="Filtros_Changed" />
                </div>
                <div class="col-md-4">
                    <label class="form-label fw-semibold">Filtrar por ponente</label>
                    <asp:DropDownList ID="ddlPonente" runat="server" CssClass="form-select"
                        AutoPostBack="true" OnSelectedIndexChanged="Filtros_Changed" />
                </div>
                <div class="col-md-4 text-md-end mt-3 mt-md-0">
                    <asp:Button ID="btnLimpiar" runat="server" Text="Mostrar todo" CssClass="btn btn-outline-secondary"
                        OnClick="btnLimpiar_Click" />
                </div>
            </div>
        </div>

        <!-- 🔹 Lista de actividades -->
        <div class="row g-3">
            <asp:Repeater ID="rptActividades" runat="server">
                <ItemTemplate>
                    <div class="col-12 col-md-6 col-lg-4">
                        <div class="card-actividad h-100">
                            <h5 class="fw-bold"><%# Eval("Nombre_actividad") %></h5>
                            <p class="text-muted mb-1"><i class="fa-regular fa-clock me-1"></i> <%# Eval("Horario") %></p>
                            <p><strong>Ponente:</strong> 
                                <a href='<%# Eval("id_ponente", "PonentePerfil.aspx?id={0}") %>' class="text-decoration-none text-primary">
                                    <%# Eval("nombre_ponente") %>
                                </a>
                            </p>
                            <p><strong>Ubicación:</strong> <%# Eval("nombre_ubicacion") %></p>
                            <p><strong>Carreras:</strong></p>
                            <div><%# Eval("CarrerasHTML") %></div>
                            <hr />
                            <p><strong>Inscritos:</strong> <%# Eval("Inscritos") %></p>
                            <asp:Button ID="btnVerInscritos" runat="server" CssClass="btn btn-outline-primary btn-sm"
                                Text="Ver inscritos" CommandArgument='<%# Eval("id_actividad") %>'
                                OnCommand="btnVerInscritos_Command" />
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <asp:Label ID="lblResultado" runat="server" CssClass="text-muted fst-italic d-block mt-3" />
    </div>

    <!-- 🔹 Modal inscritos -->
    <asp:Panel ID="pnlInscritos" runat="server" Visible="false" CssClass="modal fade show d-block"
        Style="background: rgba(0,0,0,0.5); position: fixed; inset: 0; z-index: 1050;">
        <div class="modal-dialog modal-dialog-centered modal-lg">
            <div class="modal-content">
                <div class="modal-header bg-primary text-white">
                    <h5 class="modal-title">Alumnos inscritos</h5>
                    <asp:LinkButton ID="btnCerrarModal" runat="server" CssClass="btn-close btn-close-white" OnClick="btnCerrarModal_Click" />
                </div>
                <div class="modal-body">
                    <asp:Repeater ID="rptInscritos" runat="server">
                        <HeaderTemplate>
                            <table class="table table-bordered table-striped mb-0">
                                <thead><tr><th>Carné</th><th>Nombre</th><th>Carrera</th></tr></thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td><%# Eval("carne") %></td>
                                <td><%# Eval("nombre") %></td>
                                <td><%# Eval("carrera") %></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                                </tbody></table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </asp:Panel>
</asp:Content>
