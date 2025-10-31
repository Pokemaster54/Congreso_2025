<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuCatedratico.Master"
    CodeBehind="Ponentes.aspx.cs" Inherits="Congreso_2025.Ponentes" %>

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
        .card-ponente {
            border: 1px solid rgba(0,0,0,.08);
            border-radius: 12px;
            background: #fff;
            box-shadow: 0 6px 16px rgba(0,0,0,.05);
            padding: 18px;
            transition: all .2s ease;
        }
        .card-ponente:hover {
            transform: translateY(-3px);
            box-shadow: 0 10px 24px rgba(0,0,0,.08);
        }
        .badge-actividad {
            background: rgba(0,43,91,.1);
            color: #002b5b;
            border-radius: 8px;
            padding: 4px 8px;
            margin: 2px;
            font-size: 0.85rem;
        }
    </style>

    <div class="container-fluid">
        <h3 class="fw-bold mb-3"><i class="fa-solid fa-chalkboard-user me-2"></i>Ponentes</h3>

        <!-- 🔹 Filtro por carrera -->
        <div class="filter-box mb-4">
            <div class="row align-items-end">
                <div class="col-md-6">
                    <label class="form-label fw-semibold">Filtrar por carrera</label>
                    <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="form-select" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlCarrera_SelectedIndexChanged" />
                </div>
                <div class="col-md-6 text-md-end mt-3 mt-md-0">
                    <asp:Button ID="btnLimpiar" runat="server" Text="Mostrar todos" CssClass="btn btn-outline-secondary"
                        OnClick="btnLimpiar_Click" />
                </div>
            </div>
        </div>

        <!-- 🔹 Lista de ponentes -->
        <div class="row g-3">
            <asp:Repeater ID="rptPonentes" runat="server">
                <ItemTemplate>
                    <div class="col-12 col-md-6 col-lg-4">
                        <div class="card-ponente h-100">
                            <h5 class="fw-bold">
                                <a href='<%# Eval("id_ponente", "PonentePerfil.aspx?id={0}") %>' class="text-decoration-none text-primary">
                                    <%# Eval("nombre_ponente") %>
                                </a>
                            </h5>
                            <p class="text-muted mb-1"><i class="fa-solid fa-earth-americas me-1"></i><%# Eval("Origen") %></p>
                            <p class="mb-2"><small><strong>Actividades:</strong></small></p>
                            <div>
                                <%# Eval("ActividadesHTML") %>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>

        <asp:Label ID="lblResultado" runat="server" CssClass="text-muted fst-italic d-block mt-3" />
    </div>
</asp:Content>
