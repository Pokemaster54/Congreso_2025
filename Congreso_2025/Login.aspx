<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Congreso_2025.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" charset="utf-8" name="viewport" content="width=device-width, initial-scale=1" />
    <title>Ingreso | Congreso</title>

    <!-- Bootstrap 5 -->
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" />

    <!-- Tipografía (Helvetica first) -->
    <style>
        :root {
            --brand: #1f6feb;
            --ink: #0b1220;
        }

        body {
            font-family: Helvetica, Arial, sans-serif;
        }
    </style>

    <!-- Tu CSS externo -->
    <link href="CSS/EstilosChidos.css" rel="stylesheet" />
    <meta />
</head>
<body class="login-bg">

    <form id="form1" runat="server" class="h-100">
        <div class="container h-100">
            <div class="row align-items-center justify-content-center h-100">
                <div class="col-11 col-sm-9 col-md-7 col-lg-5">
                    <div class="card login-card shadow-lg border-0 glassy fade-up">
                        <div class="card-body p-4 p-md-5">

                            <!-- Marca / título -->
                            <div class="text-center mb-4">
                                <div class="logo-spin mb-2">
                                    <!-- Placeholder logo: cámbialo por una imagen si quieres -->
                                    <svg width="42" height="42" viewBox="0 0 24 24" fill="none">
                                        <circle cx="12" cy="12" r="10" stroke="currentColor" stroke-width="1.5" />
                                        <path d="M12 6v6l4 2" stroke="currentColor" stroke-width="1.5" stroke-linecap="round" />
                                    </svg>
                                </div>
                                <h1 class="h4 mb-1 fw-semibold">Ingreso al sistema</h1>
                                <p class="text-secondary small mb-0">Congreso Académico</p>
                            </div>

                            <!-- Inputs -->
                            <div class="mb-3 form-floating">
                                <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control control-elevate" placeholder="Usuario"></asp:TextBox>
                                <label for="txtUsuario" class="form-label">Usuario</label>
                            </div>

                            <div class="mb-2 form-floating">
                                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control control-elevate" placeholder="Contraseña"></asp:TextBox>
                                <label for="txtPassword" class="form-label">Contraseña</label>
                            </div>

                            <!-- Mensaje -->
                            <div class="min-h-message">
                                <asp:Label ID="lblMsg" runat="server" CssClass="text-danger small"></asp:Label>
                            </div>

                            <!-- Botón -->
                            <asp:Button ID="btnIngresar" runat="server"
                                Text="Ingresar"
                                OnClick="btnIngresar_Click"
                                CssClass="btn btn-primary w-100 btn-lift mt-2" />

                            <!-- Footer -->
                            <div class="text-center mt-4">
                                <small class="text-secondary">© <%= DateTime.Now.Year %> Congreso 2025</small>
                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <!-- Bootstrap JS (opcional para algunos componentes) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
