<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuCatedratico.Master"
    CodeBehind="PonentePerfil.aspx.cs" Inherits="Congreso_2025.PonentePerfil" %>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">
    <style>
        .profile-card {
            background: #fff;
            border: 1px solid rgba(0,0,0,.08);
            border-radius: 16px;
            padding: 24px;
            box-shadow: 0 6px 16px rgba(0,0,0,.06);
        }
        .badge-act {
            background: rgba(0,43,91,.1);
            color: #002b5b;
            border-radius: 8px;
            padding: 5px 8px;
            margin: 2px;
            display: inline-block;
        }
    </style>

    <div class="container">
        <h3 class="fw-bold mb-3"><i class="fa-solid fa-user-tie me-2"></i>Perfil del Ponente</h3>

        <asp:Panel ID="pnlPerfil" runat="server" Visible="false" CssClass="profile-card">
            <h4 class="fw-bold text-primary"><asp:Label ID="lblNombre" runat="server" /></h4>
            <p><strong>Origen:</strong> <asp:Label ID="lblOrigen" runat="server" /></p>
            <p><strong>Fecha de nacimiento:</strong> <asp:Label ID="lblNacimiento" runat="server" /></p>
            <p><strong>Descripción:</strong></p>
            <p class="text-secondary"><asp:Label ID="lblDescripcion" runat="server" /></p>

            <hr />
            <h5 class="fw-bold mb-2"><i class="fa-solid fa-calendar-days me-1"></i> Actividades asignadas</h5>
            <asp:Literal ID="litActividades" runat="server" />
        </asp:Panel>

        <asp:Label ID="lblMensaje" runat="server" CssClass="text-muted fst-italic" />
    </div>
</asp:Content>
