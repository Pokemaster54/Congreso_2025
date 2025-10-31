<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuCatedratico.Master"
    CodeBehind="Alumnos.aspx.cs" Inherits="Congreso_2025.Alumnos" %>

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
        .table thead {
            background-color: #002b5b;
            color: white;
        }
    </style>

    <div class="container-fluid">
        <h3 class="fw-bold mb-3"><i class="fa-solid fa-users me-2"></i>Listado de Alumnos</h3>

        <!-- 🔹 Filtros -->
        <div class="filter-box">
            <div class="row g-3 align-items-end">
                <div class="col-md-4">
                    <label for="ddlCarrera" class="form-label fw-semibold">Carrera</label>
                    <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="form-select" AutoPostBack="true"
                        OnSelectedIndexChanged="FiltroChanged" />
                </div>

                <div class="col-md-4">
                    <label for="ddlActividad" class="form-label fw-semibold">Actividad</label>
                    <asp:DropDownList ID="ddlActividad" runat="server" CssClass="form-select" AutoPostBack="true"
                        OnSelectedIndexChanged="FiltroChanged" />
                </div>

                <div class="col-md-4 text-md-end">
                    <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar filtros"
                        CssClass="btn btn-outline-secondary me-2" OnClick="btnLimpiar_Click" />
                    <asp:Button ID="btnExportar" runat="server" Text="Exportar a PDF"
                        CssClass="btn btn-danger" OnClick="btnExportar_Click" />
                </div>
            </div>
        </div>

        <!-- 🔹 Tabla de alumnos -->
        <asp:GridView ID="gvAlumnos" runat="server" CssClass="table table-striped table-hover"
            AutoGenerateColumns="False" GridLines="None">
            <Columns>
                <asp:BoundField DataField="carne" HeaderText="Carne" />
                <asp:BoundField DataField="nombres_alumno" HeaderText="Nombres" />
                <asp:BoundField DataField="apellidos_alumno" HeaderText="Apellidos" />
                <asp:BoundField DataField="Carrera" HeaderText="Carrera" />
                <asp:BoundField DataField="Actividad" HeaderText="Actividad inscrita" />
                <asp:BoundField DataField="EstadoPago" HeaderText="Estado de Pago" />
            </Columns>
        </asp:GridView>

        <asp:Label ID="lblResultado" runat="server" CssClass="text-muted fst-italic" />
    </div>
</asp:Content>
