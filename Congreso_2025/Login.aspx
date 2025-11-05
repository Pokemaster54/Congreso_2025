<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Congreso_2025.Login" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Ingreso al Congreso</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://kit.fontawesome.com/a2d9c6a66b.js" crossorigin="anonymous"></script>

    <style>
        :root {
            --burdeos: #7a0c0c;
            --azul-udeo: #1c225a;
            --amarillo: #ffc107;
        }

        body {
            margin: 0;
            height: 100vh;
            display: flex;
            align-items: center;
            justify-content: center;
            flex-direction: column;
            background: var(--azul-udeo);
            color: white;
            font-family: 'Segoe UI', sans-serif;
            overflow: hidden;
            position: relative;
        }

        /* === LOGIN CARD === */
        .login-card {
            width: 100%;
            max-width: 400px;
            background-color: #fff;
            color: #000;
            border-radius: 1rem;
            box-shadow: 0 6px 20px rgba(0, 0, 0, .25);
            z-index: 2;
        }

        .login-header {
            background-color: var(--burdeos);
            color: white;
            text-align: center;
            padding: 1rem;
            border-top-left-radius: 1rem;
            border-top-right-radius: 1rem;
        }

        .login-header h3 {
            font-weight: 700;
            margin: 0;
        }

        .login-body {
            padding: 2rem 1.5rem;
        }

        .form-label {
            font-weight: 600;
            color: #1f2937;
        }

        .form-control {
            border-radius: 10px;
            border: 1px solid #d1d5db;
            padding: .6rem .75rem;
        }

        .form-control:focus {
            border-color: var(--burdeos);
            box-shadow: 0 0 0 0.2rem rgba(122, 12, 12, 0.25);
        }

        .btn-login {
            background-color: var(--burdeos);
            color: white;
            border: none;
            border-radius: 10px;
            font-weight: 600;
            padding: .75rem;
            transition: all .2s;
        }

        .btn-login:hover {
            background-color: #a31212;
        }

        .link-secondary {
            color: var(--azul-udeo) !important;
            font-weight: 500;
        }

        .footer {
            text-align: center;
            font-size: .85rem;
            color: #f8fafc;
            z-index: 2;
            position: relative;
        }

        /* === ONDA SENOIDAL REAL === */
        .wave-container {
            position: absolute;
            bottom: 0;
            left: 0;
            width: 100%;
            height: 180px;
            overflow: hidden;
            z-index: 1;
            pointer-events: none;
        }

        canvas#waveCanvas {
            width: 100%;
            height: 100%;
            display: block;
        }

        @media (max-width: 576px) {
            .login-card {
                margin: 1rem;
                box-shadow: none;
            }
        }
    </style>
</head>

<body>
    <form id="form1" runat="server" class="w-100 d-flex justify-content-center">
        <div class="login-card">
            <div class="login-header">
                <h3><i class="fa-solid fa-building-columns me-2"></i>Congreso 2025</h3>
                <p class="mb-0 small">Sistema Académico</p>
            </div>

            <div class="login-body">
                <div class="mb-3">
                    <label for="txtUsuario" class="form-label">Usuario</label>
                    <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control" placeholder="Ingrese su usuario" />
                </div>

                <div class="mb-3">
                    <label for="txtPassword" class="form-label">Contraseña</label>
                    <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Ingrese su contraseña" />
                </div>

                <asp:Label ID="lblMensaje" runat="server" CssClass="text-danger fw-semibold d-block mb-3 text-center"></asp:Label>

                <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click" CssClass="btn btn-login w-100 mb-2" />

                <div class="text-center mt-3">
                    <asp:HyperLink ID="lnkOlvido" runat="server"
                        NavigateUrl="ResetPassword.aspx"
                        Text=""
                        CssClass="link-secondary" />
                </div>
            </div>

            <div class="footer pb-3">
                © 2025 Congreso Académico — Universidad de Occidente
            </div>
        </div>
    </form>

    <div class="wave-container">
        <canvas id="waveCanvas"></canvas>
    </div>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>

    <script>
        const canvas = document.getElementById("waveCanvas");
        const ctx = canvas.getContext("2d");

        let width, height, t = 0;
        function resize() {
            width = canvas.width = window.innerWidth;
            height = canvas.height = 180;
        }
        window.addEventListener("resize", resize);
        resize();

        function drawWave() {
            ctx.clearRect(0, 0, width, height);

            const amplitude = 25;     
            const wavelength = 250;   
            const speed = 0.015;      
            const baseline = height / 2;

            ctx.beginPath();
            ctx.moveTo(0, baseline);

            for (let x = 0; x < width; x++) {
                const y = baseline + Math.sin((x / wavelength + t)) * amplitude;
                ctx.lineTo(x, y);
            }

            ctx.lineTo(width, height);
            ctx.lineTo(0, height);
            ctx.closePath();

            ctx.fillStyle = "#ffc107";
            ctx.fill();

            t += speed;
            requestAnimationFrame(drawWave);
        }

        drawWave();
    </script>
</body>
</html>
