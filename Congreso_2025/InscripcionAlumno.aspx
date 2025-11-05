<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InscripcionAlumno.aspx.cs" Inherits="Congreso_2025.InscripcionAlumno" %>

<!DOCTYPE html>
<html lang="es">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>Inscripción de Alumno - Congreso 2025</title>

    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <script src="https://kit.fontawesome.com/a2d9c6a66b.js" crossorigin="anonymous"></script>

    <style>
        body {
            background-color: #f8fafc;
            font-family: 'Segoe UI', sans-serif;
        }
        .container {
            max-width: 960px;
            padding-bottom: 80px;
        }
        .card {
            border-radius: 1rem;
            box-shadow: 0 4px 15px rgba(0,0,0,.08);
        }
        h2 {
            color: #002b5b;
            font-weight: 700;
            text-align: center;
        }
        .btn-primary {
            background: linear-gradient(135deg,#4f46e5,#06b6d4);
            border: none;
        }
        .btn-primary:hover {
            opacity: .9;
        }
        .table th {
            background-color: #002b5b;
            color: white;
        }
        .top-bar {
            background-color: #002b5b;
            color: white;
            padding: 10px 16px;
            display: flex;
            align-items: center;
            justify-content: space-between;
            flex-wrap: wrap;
        }
        .top-bar .title {
            font-weight: 600;
            font-size: 1rem;
        }
        .logout-btn {
            background: transparent;
            border: 1px solid white;
            color: white;
            padding: 5px 12px;
            border-radius: 8px;
            transition: background .3s;
            font-size: 0.9rem;
        }
        .logout-btn:hover {
            background: white;
            color: #002b5b;
        }

        /* 📱 Ajustes responsive */
        @media (max-width: 768px) {
            .top-bar {
                flex-direction: column;
                align-items: flex-start;
                gap: 8px;
            }
            .logout-btn {
                align-self: flex-end;
            }
            .card {
                padding: 1rem !important;
            }
            .calendar-wrapper {
                overflow-x: auto;
                border-radius: 8px;
            }
        }

        @media (max-width: 576px) {
            h2 {
                font-size: 1.4rem;
            }
            label {
                font-size: 0.9rem;
            }
            .btn {
                width: 100%;
                margin-bottom: 8px;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- 🔹 Barra superior -->
        <div class="top-bar mb-4">
            <span class="title"><i class="fa-solid fa-building-columns me-2"></i>Congreso 2025 — Inscripciones</span>
            <asp:Button ID="btnLogout" runat="server" CssClass="logout-btn" Text="Cerrar sesión" OnClick="btnLogout_Click" />
        </div>

        <div class="container my-4">
            <h2 class="mb-4"><i class="fa-solid fa-user-plus me-2"></i>Inscripción de Alumno</h2>

            <!-- 🔹 Datos de alumno -->
            <div class="card p-4 mb-4">
                <div class="row g-3">
                    <div class="col-md-4 col-sm-6">
                        <label class="form-label">Carne:</label>
                        <asp:TextBox ID="txtCarne" runat="server" CssClass="form-control" placeholder="Ej. 2025001001" />
                    </div>
                    <div class="col-md-8 col-sm-6">
                        <label class="form-label">Nombres:</label>
                        <asp:TextBox ID="txtNombres" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <label class="form-label">Apellidos:</label>
                        <asp:TextBox ID="txtApellidos" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <label class="form-label">Carrera:</label>
                        <asp:DropDownList ID="ddlCarrera" runat="server" CssClass="form-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCarrera_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-md-6 col-sm-6">
                        <label class="form-label">Número de boleta:</label>
                        <asp:TextBox ID="txtBoleta" runat="server" CssClass="form-control" />
                    </div>
                    <div class="col-md-6 col-sm-12 calendar-wrapper">
                        <label class="form-label">Fecha de pago:</label>
                        <asp:Calendar ID="calFechaPago" runat="server" CssClass="border rounded p-2 w-100"></asp:Calendar>
                    </div>
                </div>
            </div>

            <!-- 🔹 Actividades -->
            <div class="card p-3 mb-4">
                <h5 class="mb-3"><i class="fa-solid fa-calendar-days me-2"></i>Actividades de la carrera seleccionada</h5>
                <div class="table-responsive">
                    <asp:GridView ID="gvActividades" runat="server" CssClass="table table-bordered table-sm" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField DataField="Nombre_actividad" HeaderText="Actividad" />
                            <asp:BoundField DataField="nombre_tipo_actividad" HeaderText="Tipo" />
                            <asp:BoundField DataField="nombre_ponente" HeaderText="Ponente" />
                            <asp:BoundField DataField="hora_inicio" HeaderText="Inicio" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <!-- 🔹 Resumen antes de guardar -->
            <asp:Panel ID="pnlResumen" runat="server" Visible="false" CssClass="card p-4 mb-4 bg-light">
                <h5><i class="fa-solid fa-circle-info me-2"></i>Resumen del nuevo usuario:</h5>
                <p><strong>Usuario:</strong> <asp:Label ID="lblUser" runat="server" /></p>
                <p><strong>Contraseña:</strong> <asp:Label ID="lblPass" runat="server" /></p>
                <p><strong>Nombre completo:</strong> <asp:Label ID="lblNombre" runat="server" /></p>
                <p><strong>Carrera:</strong> <asp:Label ID="lblCarrera" runat="server" /></p>
                <p><strong>Boleta:</strong> <asp:Label ID="lblBoleta" runat="server" /></p>
            </asp:Panel>

            <!-- 🔹 Botones -->
            <div class="d-flex flex-wrap justify-content-center gap-2">
                <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-secondary" Text="Previsualizar" OnClick="btnPreview_Click" />
                <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-primary" Text="Guardar Inscripción" OnClick="btnGuardar_Click" />
            </div>
        </div>
    </form>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
