<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="Congreso_2025.ResetPassword" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Restablecer contraseña</title>
  <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light">
  <form id="form1" runat="server">
    <div class="container" style="max-width:520px; margin-top:60px;">
      <div class="card shadow p-4">
        <h4 class="mb-3">Restablecer contraseña</h4>

        <div class="mb-3">
          <label for="txtUsuario" class="form-label">Usuario</label>
          <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
        </div>

        <hr class="my-3" />

        
        <h6>Nueva contraseña</h6>
        <div class="mb-2">
          <asp:TextBox ID="txtNueva" runat="server" TextMode="Password" CssClass="form-control" placeholder="Nueva contraseña" />
        </div>
        <div class="mb-3">
          <asp:TextBox ID="txtConfirmar" runat="server" TextMode="Password" CssClass="form-control" placeholder="Confirmar contraseña" />
        </div>
        <asp:Button ID="btnReset" runat="server" Text="Guardar nueva contraseña" CssClass="btn btn-primary w-100"
                    OnClick="btnReset_Click" />

        <div class="text-center my-3">o</div>

       
        <asp:Button ID="btnGenerar" runat="server" Text="Generar contraseña temporal" CssClass="btn btn-outline-secondary w-100"
                    OnClick="btnGenerar_Click" />

        <asp:Label ID="lblMsg" runat="server" CssClass="mt-3 d-block text-danger"></asp:Label>
        <asp:Label ID="lblTemporal" runat="server" CssClass="mt-3 d-block text-success"></asp:Label>

        <div class="mt-3 text-center">
          <asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="Login.aspx" Text="Volver al Login" />
        </div>
      </div>
    </div>
  </form>
</body>
</html>
