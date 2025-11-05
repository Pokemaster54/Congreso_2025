<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MenuAdmin.Master"
    CodeBehind="WebLanding.aspx.cs" Inherits="Congreso_2025.WebLanding" %>

<asp:Content ID="ctTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Inicio
</asp:Content>

<asp:Content ID="ctHead" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        :root {
            --azul-udeo: #1c225a;
            --burdeos: #7a0c0c;
            --amarillo: #ffc107;
        }

        /* ===== HERO PRINCIPAL ===== */
        .lp-hero {
            position: relative;
            min-height: clamp(480px, 70vh, 780px);
            display: grid;
            place-items: center;
            border-radius: 1.25rem;
            overflow: hidden;
            background: linear-gradient(160deg, var(--azul-udeo) 40%, #0e1542 100%);
            color: #fff;
            box-shadow: 0 8px 24px rgba(0, 0, 0, .3);
        }

        /* ===== Onda inferior amarilla ===== */
        .lp-wave {
            position: absolute;
            bottom: 0;
            left: 0;
            width: 200%;
            height: 160px;
            background: var(--amarillo);
            -webkit-mask: url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="1200" height="200" viewBox="0 0 1200 200"><path d="M0,100 C150,200 350,0 600,100 C850,200 1050,0 1200,100 L1200,200 L0,200 Z" fill=\'white\'/></svg>') repeat-x;
            mask: url('data:image/svg+xml;utf8,<svg xmlns="http://www.w3.org/2000/svg" width="1200" height="200" viewBox="0 0 1200 200"><path d="M0,100 C150,200 350,0 600,100 C850,200 1050,0 1200,100 L1200,200 L0,200 Z" fill=\'white\'/></svg>') repeat-x;
            -webkit-mask-size: 1200px 200px;
            mask-size: 1200px 200px;
            animation: waveFlow 10s linear infinite;
            opacity: 0.9;
        }

        @keyframes waveFlow {
            from { transform: translateX(0); }
            to { transform: translateX(-50%); }
        }

        /* ===== TARJETA CENTRAL ===== */
        .lp-card {
            position: relative;
            z-index: 2;
            background: rgba(255, 255, 255, .95);
            color: #1e293b;
            border-radius: 1rem;
            padding: clamp(22px, 5vw, 48px);
            width: min(1040px, 95%);
            border: 2px solid rgba(255, 255, 255, 0.3);
            box-shadow: 0 12px 40px rgba(0, 0, 0, 0.2);
            backdrop-filter: blur(10px);
        }

        .lp-badge {
            display: inline-flex;
            align-items: center;
            gap: .5rem;
            padding: .35rem .75rem;
            border-radius: 999px;
            background: rgba(255, 193, 7, .15);
            color: var(--burdeos);
            font-weight: 600;
        }

        .lp-title {
            font-weight: 800;
            font-size: clamp(2rem, 3.6vw, 3.2rem);
            color: var(--azul-udeo);
            letter-spacing: -0.02em;
            margin: .4rem 0 .5rem;
        }

        .lp-sub {
            color: #4b5563;
            font-size: 1.1rem;
            max-width: 720px;
        }

        /* ===== KPIs ===== */
        .lp-grid {
            display: grid;
            gap: 14px;
            grid-template-columns: repeat(auto-fit, minmax(220px, 1fr));
            margin-top: 1.5rem;
        }

        .kpi {
            border-radius: 16px;
            padding: 18px;
            background: rgba(255, 255, 255, .9);
            border-left: 6px solid var(--burdeos);
            box-shadow: 0 2px 8px rgba(0, 0, 0, .05);
        }

        .kpi .label {
            color: #6b7280;
            font-size: .85rem;
            text-transform: uppercase;
            letter-spacing: .5px;
        }

        .kpi .value {
            font-weight: 800;
            font-size: 1.8rem;
            color: var(--azul-udeo);
        }

        /* ===== PRÓXIMA ACTIVIDAD ===== */
        .next {
            margin-top: 22px;
            border-radius: 16px;
            padding: 18px;
            background: #fff;
            border: 1px solid rgba(0, 0, 0, .06);
        }

        .next .title {
            font-weight: 700;
            font-size: 1.1rem;
            color: var(--burdeos);
        }

        .meta {
            color: #475569;
        }

        /* ===== BOTÓN ===== */
        .btn-aurora {
            background: var(--burdeos);
            color: #fff;
            border-radius: 12px;
            font-weight: 600;
            padding: .8rem 1.4rem;
            border: none;
            transition: all .2s;
        }

        .btn-aurora:hover {
            background: #a31212;
            color: #fff;
        }

        @media(max-width: 768px) {
            .lp-card {
                padding: 1.5rem;
            }
        }
    </style>
</asp:Content>

<asp:Content ID="ctBreadcrumb" ContentPlaceHolderID="BreadcrumbContent" runat="server">
    Inicio
</asp:Content>

<asp:Content ID="ctMain" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container-fluid px-0 px-sm-2">
        <div class="lp-hero my-3">
            <div class="lp-card">
                <div class="mb-3">
                    <span class="lp-badge"><i class="fa-solid fa-sparkles"></i> Bienvenido</span>
                </div>
                <h2 class="lp-title">Panel de Administración — Congreso 2025</h2>
                <p class="lp-sub mb-4">Administra ponentes, actividades y catálogos de forma centralizada.</p>

                <!-- KPIs -->
                <div class="lp-grid">
                    <div class="kpi">
                        <div class="label">Actividades próximas</div>
                        <div class="value"><asp:Label ID="lblKpiProximas" runat="server" Text="0" /></div>
                    </div>
                    <div class="kpi">
                        <div class="label">Ponentes</div>
                        <div class="value"><asp:Label ID="lblKpiPonentes" runat="server" Text="0" /></div>
                    </div>
                    <div class="kpi">
                        <div class="label">Ubicaciones</div>
                        <div class="value"><asp:Label ID="lblKpiUbicaciones" runat="server" Text="0" /></div>
                    </div>
                    <div class="kpi">
                        <div class="label">Tipos de actividad</div>
                        <div class="value"><asp:Label ID="lblKpiTipos" runat="server" Text="0" /></div>
                    </div>
                </div>

                <!-- Próxima actividad -->
                <asp:Panel ID="pnlNext" runat="server" CssClass="next">
                    <div class="d-flex flex-column flex-md-row align-items-md-center justify-content-between gap-2">
                        <div>
                            <div class="title"><asp:Label ID="lblNextNombre" runat="server" Text="—" /></div>
                            <div class="meta">
                                <i class="fa-regular fa-clock me-1"></i>
                                <asp:Label ID="lblNextFecha" runat="server" Text="—" />
                                &nbsp;•&nbsp; <i class="fa-solid fa-chalkboard-user me-1"></i>
                                <asp:Label ID="lblNextPonente" runat="server" Text="—" />
                                &nbsp;•&nbsp; <i class="fa-solid fa-location-dot me-1"></i>
                                <asp:Label ID="lblNextUbicacion" runat="server" Text="—" />
                                &nbsp;•&nbsp; <i class="fa-solid fa-list-check me-1"></i>
                                <asp:Label ID="lblNextTipo" runat="server" Text="—" />
                            </div>
                        </div>
                        <a runat="server" href="~/WebActividad.aspx" class="btn btn-aurora">
                            <span><i class="fa-solid fa-calendar-check me-2"></i> Ver agenda</span>
                        </a>
                    </div>
                </asp:Panel>
            </div>

            <!-- 🌊 Onda amarilla decorativa -->
            <div class="lp-wave"></div>
        </div>
    </div>
</asp:Content>

<asp:Content ID="ctScripts" ContentPlaceHolderID="ScriptsContent" runat="server">
</asp:Content>
