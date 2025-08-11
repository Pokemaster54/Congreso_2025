<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DashboardAlumno.aspx.cs" Inherits="Congreso_2025.DashboardAlumno" %>

<!DOCTYPE html>
<html lang="es">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Panel de Administrador - Dashboard</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.3/font/bootstrap-icons.min.css" rel="stylesheet">
    <style>
        body {
            font-family: 'Segoe UI', sans-serif;
            background-color: #f8f9fa;
        }
        .sidebar {
            position: fixed;
            top: 0;
            left: 0;
            bottom: 0;
            width: 260px;
            padding: 20px;
            background-color: #0d6efd;
            color: white;
            z-index: 1000;
        }
        .sidebar .nav-link {
            color: rgba(255, 255, 255, 0.8);
            font-weight: 500;
            padding: 10px 15px;
            border-radius: 8px;
            transition: background-color 0.2s, color 0.2s;
        }
        .sidebar .nav-link:hover, .sidebar .nav-link.active {
            background-color: #ff7b00;
            color: white;
        }
        .sidebar .nav-link .bi {
            margin-right: 12px;
            font-size: 1.2rem;
        }
        .sidebar .brand {
            font-weight: 800;
            font-size: 1.5rem;
            color: white;
            text-align: center;
            margin-bottom: 2rem;
        }
        .main-content {
            margin-left: 260px;
            padding: 2rem;
        }
        .card {
            border: none;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0,0,0,0.08);
            transition: transform 0.2s, box-shadow 0.2s;
        }
        .card:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 25px rgba(0,0,0,0.12);
        }
        .btn-primary {
            background-color: #ff7b00;
            border-color: #ff7b00;
        }
        .btn-primary:hover {
            background-color: #e76a00;
            border-color: #e76a00;
        }
        .card-img-overlay {
            background: linear-gradient(to top, rgba(0,0,0,0.8) 0%, rgba(0,0,0,0) 100%);
            color: white;
            display: flex;
            flex-direction: column;
            justify-content: flex-end;
            padding: 1.5rem;
        }
        .card-img-overlay .badge {
            position: absolute;
            top: 1rem;
            right: 1rem;
            font-size: 0.9rem;
        }
        .card-title {
            font-weight: 700;
        }
    </style>
</head>
<body>

<div class="sidebar">
    <h3 class="brand">🎯 Admin Congreso</h3>
    <ul class="nav flex-column">
        <li class="nav-item">
            <a class="nav-link active" href="#"><i class="bi bi-grid-fill"></i>Dashboard</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="#"><i class="bi bi-calendar-event-fill"></i>Gestión de Actividades</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="#"><i class="bi bi-people-fill"></i>Gestión de Alumnos</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="#"><i class="bi bi-mic-fill"></i>Gestión de Ponentes</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="#"><i class="bi bi-person-badge-fill"></i>Gestión de Usuarios</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="#"><i class="bi bi-book-fill"></i>Gestión de Carreras</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="#"><i class="bi bi-geo-alt-fill"></i>Gestión de Ubicaciones</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="#"><i class="bi bi-cash-stack"></i>Gestión de Pagos</a>
        </li>
        <li class="nav-item mt-3">
            <small class="text-white-50 px-3">CATÁLOGOS</small>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="#"><i class="bi bi-tags-fill"></i>Tipos de Actividad</a>
        </li>
        <li class="nav-item">
            <a class="nav-link" href="#"><i class="bi bi-check-circle-fill"></i>Estados (Alumno/Pago)</a>
        </li>
    </ul>
</div>

<main class="main-content">
    <div class="container-fluid">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1 class="h2 fw-bold">Dashboard de Actividades</h1>
        </div>

        <div class="row row-cols-1 row-cols-md-2 row-cols-lg-3 g-4">
            <div class="col">
                <div class="card text-white overflow-hidden">
                    <img src="https://www.womenintech.co.uk/wp-content/uploads/2021/11/Tech-skills-2022-1.png" class="card-img" alt="Mockup de conferencia sobre IA">
                    <div class="card-img-overlay">
                        <span class="badge bg-success">Activo</span>
                        <h5 class="card-title">Inteligencia Artificial en el Desarrollo Web</h5>
                        <p class="card-text mb-0"><small><i class="bi bi-mic-fill me-1"></i>Dra. Ana Torres</small></p>
                        <p class="card-text mb-0"><small><i class="bi bi-geo-alt-fill me-1"></i>Auditorio Principal</small></p>
                        <p class="card-text mb-0"><small><i class="bi bi-calendar-event me-1"></i>09:00 - 11:00</small></p>
                        <p class="card-text"><small><i class="bi bi-people-fill me-1"></i>120 inscritos</small></p>
                    </div>
                </div>
            </div>
            
            <div class="col">
                <div class="card text-white overflow-hidden">
                    <img src="https://via.placeholder.com/600x400.png?text=Taller+React+y+Vite" class="card-img" alt="Mockup de taller de React">
                    <div class="card-img-overlay">
                        <span class="badge bg-success">Activo</span>
                        <h5 class="card-title">Taller Práctico de React y Vite</h5>
                        <p class="card-text mb-0"><small><i class="bi bi-mic-fill me-1"></i>Ing. Carlos Vega</small></p>
                        <p class="card-text mb-0"><small><i class="bi bi-geo-alt-fill me-1"></i>Laboratorio C-301</small></p>
                        <p class="card-text mb-0"><small><i class="bi bi-calendar-event me-1"></i>14:00 - 17:00</small></p>
                        <p class="card-text"><small><i class="bi bi-people-fill me-1"></i>35 inscritos</small></p>
                    </div>
                </div>
            </div>

            <div class="col">
                <div class="card text-white overflow-hidden">
                    <img src="https://via.placeholder.com/600x400.png?text=Seguridad+en+Apps+Moviles" class="card-img" alt="Mockup de seguridad móvil">
                    <div class="card-img-overlay">
                        <span class="badge bg-danger">Cancelado</span>
                        <h5 class="card-title">Seguridad en Aplicaciones Móviles</h5>
                        <p class="card-text mb-0"><small><i class="bi bi-mic-fill me-1"></i>Marcos Aguilar</small></p>
                        <p class="card-text mb-0"><small><i class="bi bi-geo-alt-fill me-1"></i>Salón B-105</small></p>
                        <p class="card-text mb-0"><small><i class="bi bi-calendar-event me-1"></i>11:30 - 13:00</small></p>
                        <p class="card-text"><small><i class="bi bi-people-fill me-1"></i>85 inscritos</small></p>
                    </div>
                </div>
            </div>
            
            <div class="col">
                <div class="card text-white overflow-hidden">
                    <img src="https://via.placeholder.com/600x400.png?text=UX+y+UI+Design" class="card-img" alt="Mockup de diseño UX/UI">
                    <div class="card-img-overlay">
                        <span class="badge bg-secondary">Pendiente</span>
                        <h5 class="card-title">Fundamentos de Diseño UX/UI</h5>
                        <p class="card-text mb-0"><small><i class="bi bi-mic-fill me-1"></i>Lic. Laura Gómez</small></p>
                        <p class="card-text mb-0"><small><i class="bi bi-geo-alt-fill me-1"></i>Laboratorio B-205</small></p>
                        <p class="card-text mb-0"><small><i class="bi bi-calendar-event me-1"></i>10:00 - 12:00</small></p>
                        <p class="card-text"><small><i class="bi bi-people-fill me-1"></i>72 inscritos</small></p>
                    </div>
                </div>
            </div>

        </div>
    </div>
</main>

<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
