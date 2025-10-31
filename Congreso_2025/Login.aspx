<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Congreso_2025.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ingreso al Congreso</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body class="bg-light d-flex align-items-center justify-content-center" style="height: 100vh;">
    <form id="form1" runat="server" class="w-25">
        <div class="card shadow-lg p-4">
            <h3 class="text-center mb-4">Inicio de Sesión</h3>

            <div class="mb-3">
                <label for="txtUsuario" class="form-label">Usuario</label>
                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" />
            </div>

            <div class="mb-3">
                <label for="txtPassword" class="form-label">Contraseña</label>
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" />l
            </div>

            <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger d-block mb-3"></asp:Label>

            <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click"
                CssClass="btn btn-primary w-100" />

            <div
                class="mt-3">
                <asp:HyperLink ID="lnkOlvido" runat="server"
                    NavigateUrl="ResetPassword.aspx"
                    Text="¿Olvidaste tu contraseña?"
                    CssClass="link-secondary" />
            </div>

        </div>

    </form>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

</body>
</html>
