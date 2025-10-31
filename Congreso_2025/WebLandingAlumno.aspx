<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuAlumno.Master"
    CodeBehind="WebLandingAlumno.aspx.cs" Inherits="Congreso_2025.WebLandingAlumno" %>

<asp:Content ID="ctTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Inicio
</asp:Content>

<asp:Content ID="ctHead" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        /* ===== Hero con líneas animadas ===== */
        .lp-hero {
            position: relative;
            min-height: clamp(460px, 70vh, 780px);
            display: grid;
            place-items: center;
            border-radius: 1.25rem;
            overflow: hidden;
            background: radial-gradient(1200px 600px at 10% 10%, #f8fafc 0%, #ffffff 30%, #f1f5f9 100%);
            box-shadow: 0 10px 40px rgba(0, 0, 0, .06);
        }
        .lp-lines {
            position: absolute;
            inset: 0;
            opacity: .9;
            pointer-events: none;
        }
        .lp-lines svg {
            width: 100%;
            height: 100%;
            display: block;
        }
        .line {
            fill: none;
            stroke-width: 2.2;
            stroke-linecap: round;
            stroke-dasharray: 180 22;
            animation: dash 18s linear infinite;
            filter: drop-shadow(0 0 6px rgba(99,102,241,.25));
        }
        .line.slow { animation-duration: 24s; stroke-dasharray: 220 26; }
        .line.fast { animation-duration: 12s; stroke-dasharray: 140 18; }
        .line:nth-child(1) { stroke: url(#gradIndigoCyan); }
        .line:nth-child(2) { stroke: url(#gradPinkViolet); }
        .line:nth-child(3) { stroke: url(#gradLimeSky); }
        @keyframes dash { to { stroke-dashoffset: -1600; } }

        /* ===== Glass card ===== */
        .lp-card {
            position: relative;
            width: min(1040px, 96%);
            z-index: 1;
            padding: clamp(22px, 5vw, 48px);
            border-radius: 24px;
            background: rgba(255,255,255,.65);
            -webkit-backdrop-filter: saturate(160%) blur(10px);
            backdrop-filter: saturate(160%) blur(10px);
            border: 1px solid rgba(255,255,255,.6);
            box-shadow: 0 12px 44px rgba(15,23,42,.08);
        }
        .lp-badge {
            display: inline-flex;
            align-items: center;
            gap: .5rem;
            padding: .35rem .75rem;
            border-radius: 999px;
            background: rgba(99,102,241,.12);
            color: #4f46e5;
            font-weight: 600;
        }
        .lp-title {
            font-weight: 800;
            line-height: 1.1;
            font-size: clamp(2rem, 3.6vw, 3.2rem);
            letter-spacing: -.02em;
            margin: .35rem 0 .5rem;
        }
        .lp-sub { color: #4b5563; font-size: clamp(1rem, 1.6vw, 1.125rem); }

        /* ===== KPIs ===== */
        .lp-grid { display: grid; gap: 14px; grid-template-columns: repeat(12,1fr); }
        .lp-kpi { grid-column: span 12; }
        @media (min-width:768px) { .lp-kpi { grid-column: span 3; } }
        .kpi {
            border-radius: 16px;
            padding: 18px;
            background: rgba(255,255,255,.82);
            border: 1px solid rgba(0,0,0,.06);
        }
        .kpi .label { color: #6b7280; font-size: .85rem; }
        .kpi .value { font-weight: 800; font-size: 1.7rem; }

        /* ===== Próxima actividad ===== */
        .next {
            margin-top: 18px;
            border-radius: 16px;
            padding: 18px;
            background: rgba(255,255,255,.9);
            border: 1px solid rgba(0,0,0,.06);
        }
        .next .title { font-weight: 700; font-size: 1.1rem; }
        .meta { color: #475569; }

        /* ===== CTA ===== */
        .btn-aurora {
            position: relative;
            overflow: hidden;
            border-radius: 14px;
            padding: .85rem 1.2rem;
            font-weight: 600;
            border: 0;
            background: linear-gradient(135deg,#4f46e5,#06b6d4);
            color: #fff;
        }
        .btn-aurora::before {
            content: "";
            position: absolute;
            inset: -40%;
            background: conic-gradient(from 0deg, transparent, rgba(255,255,255,.35), transparent 30%);
            animation: spin 3.5s linear infinite;
            filter: blur(12px);
        }
        .btn-aurora > span { position: relative; z-index: 1; }
        @keyframes spin { to { transform: rotate(360deg) } }
    </style>
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-0 px-sm-2">
        <div class="lp-hero my-2 my-md-3">
            <!-- Líneas animadas -->
            <div class="lp-lines" aria-hidden="true">
                <svg viewBox="0 0 1200 600" preserveAspectRatio="none">
                    <defs>
                        <linearGradient id="gradIndigoCyan" x1="0%" y1="0%" x2="100%" y2="0%">
                            <stop offset="0%" stop-color="#6366F1" />
                            <stop offset="100%" stop-color="#06B6D4" />
                        </linearGradient>
                        <linearGradient id="gradPinkViolet" x1="0%" y1="0%" x2="100%" y2="0%">
                            <stop offset="0%" stop-color="#F472B6" />
                            <stop offset="100%" stop-color="#8B5CF6" />
                        </linearGradient>
                        <linearGradient id="gradLimeSky" x1="0%" y1="0%" x2="100%" y2="0%">
                            <stop offset="0%" stop-color="#84CC16" />
                            <stop offset="100%" stop-color="#0EA5E9" />
                        </linearGradient>
                    </defs>
                    <path class="line slow" d="M -50,120 C 250,20 450,220 650,140 C 850,60 980,120 1250,40" />
                    <path class="line" d="M -50,300 C 220,240 430,360 680,300 C 900,250 1050,320 1250,260" />
                    <path class="line fast" d="M -50,520 C 260,420 460,600 760,520 C 960,480 1100,560 1250,520" />
                </svg>
            </div>

            <!-- Tarjeta principal -->
            <div class="lp-card">
                <div class="mb-3">
                    <span class="lp-badge"><i class="fa-solid fa-user-graduate"></i> Bienvenido alumno</span>
                </div>
                <h2 class="lp-title">Panel del Alumno — Congreso 2025</h2>
                <p class="lp-sub mb-4">
                    Consulta tus actividades inscritas, próximas sesiones y tus ponentes asignados.
                </p>

                <!-- KPIs -->
                <div class="lp-grid">
                    <div class="lp-kpi">
                        <div class="kpi">
                            <div class="label">Próximas actividades</div>
                            <div class="value"><asp:Label ID="lblKpiProximas" runat="server" Text="0" /></div>
                        </div>
                    </div>
                    <div class="lp-kpi">
                        <div class="kpi">
                            <div class="label">Ponentes asignados</div>
                            <div class="value"><asp:Label ID="lblKpiPonentes" runat="server" Text="0" /></div>
                        </div>
                    </div>
                    <div class="lp-kpi">
                        <div class="kpi">
                            <div class="label">Ubicaciones</div>
                            <div class="value"><asp:Label ID="lblKpiUbicaciones" runat="server" Text="0" /></div>
                        </div>
                    </div>
                    <div class="lp-kpi">
                        <div class="kpi">
                            <div class="label">Tipos de actividad</div>
                            <div class="value"><asp:Label ID="lblKpiTipos" runat="server" Text="0" /></div>
                        </div>
                    </div>
                </div>

                <!-- Próxima actividad -->
                <asp:Panel ID="pnlNext" runat="server" CssClass="next">
                    <div class="d-flex flex-column flex-md-row align-items-md-center justify-content-between gap-2">
                        <div>
                            <div class="title"><asp:Label ID="lblNextNombre" runat="server" Text="—" /></div>
                            <div class="meta">
                                <i class="fa-regular fa-clock me-1"></i> <asp:Label ID="lblNextFecha" runat="server" Text="—" />
                                &nbsp;•&nbsp; <i class="fa-solid fa-chalkboard-user me-1"></i> <asp:Label ID="lblNextPonente" runat="server" Text="—" />
                                &nbsp;•&nbsp; <i class="fa-solid fa-location-dot me-1"></i> <asp:Label ID="lblNextUbicacion" runat="server" Text="—" />
                                &nbsp;•&nbsp; <i class="fa-solid fa-list-check me-1"></i> <asp:Label ID="lblNextTipo" runat="server" Text="—" />
                            </div>
                        </div>
                        <a runat="server" href="~/ActividadesAlumno.aspx" class="btn btn-aurora">
                            <span><i class="fa-solid fa-calendar-check me-2"></i> Ver mis actividades</span>
                        </a>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
</asp:Content>
