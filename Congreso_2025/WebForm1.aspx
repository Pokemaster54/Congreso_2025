<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="Congreso_2025.WebForm1" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <meta charset="UTF-8">
    <title>Ponente</title>
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet">
</head>
<body class="bg-light py-5">
    <form runat="server">


    <div class="container">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2>Panel Ponente</h2>
            <!-- Trigger for modal -->
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#addModal">
                Añadir
            </button>
        </div>

        <!-- Table -->
        <table class="table table-bordered table-striped">
            <thead class="table-dark">
                <tr>
                    <th>Codigo</th>
                    <th>Nombre</th>
                    <th>Origen</th>
                    <th>Fecha de nacimiento</th>
                    <th>Descripcion</th>
                    <th>Acciones</th>
                </tr>
            </thead>
            <tbody>
                <asp:Repeater ID="UserRepeater" runat="server">
                    <ItemTemplate>
                        <tr>
                            <%-- ¡ATENCIÓN A LOS NOMBRES DE LAS PROPIEDADES! --%>
                            <td><%# Eval("id_ponente") %></td>
                            <td><%# Eval("nombre_ponente") %></td>
                            <td><%# Eval("origen") %></td>
                            <td><%# Eval("fecha_nacimiento", "{0:yyyy-MM-dd}") %></td>
                            <%-- Formato de fecha --%>
                            <td><%# Eval("descripcion") %></td>
                            <td>
                                <div class="d-flex justify-content-center">

                                    <button class="btn btn-sm btn-warning" data-bs-toggle="modal" data-bs-target="#editModal">Edit</button>
                                    <button class="btn btn-sm btn-danger" data-bs-toggle="modal" data-bs-target="#deleteModal">Delete</button>
                                </div>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:Repeater>
            </tbody>
        </table>

    </div>


    <!-- Add Modal -->
    <div class="modal fade" id="addModal" tabindex="-1" aria-labelledby="addModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                    <div class="modal-header">
                        <h5 class="modal-title" id="addModalLabel">Añadir nuevo ponente</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <div class="mb-3">
                            <label for="addName" class="form-label">Nombre</label>
                            <asp:TextBox ID="txtAddName" runat="server" CssClass="form-control" required="true"> </asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="addDate" class="form-label">Fecha de nacimiento</label>
                            <asp:TextBox ID="txtDate" runat="server" TextMode="Date" CssClass="form-control" required="true"> </asp:TextBox>

                        </div>
                        <div class="mb-3">
                            <label for="addOrigin" class="form-label">Origen (lugar)</label>
                            <asp:TextBox ID="txtAddOrigin" runat="server" CssClass="form-control" required="true"> </asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="addName" class="form-label">Descripción</label>
                            <asp:TextBox ID="txtAddDescription" runat="server" CssClass="form-control" TextMode="MultiLine"> </asp:TextBox>
                        </div>
                        <!-- Add more fields here if needed -->
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="btnSave" runat="server" Text="Añadir" CssClass="btn btn-success" OnClick="btnSave_Click" />
                        <div style="width: 1vw"></div>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    </div>

            </div>
        </div>
    </div>
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <%-- Formulario para la edición --%>
                    <div class="modal-header">
                        <h5 class="modal-title" id="editModalLabel">Editar Ponente</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <%-- Campo oculto para guardar el ID del ponente que se está editando --%>
                        <asp:HiddenField ID="HiddenField1" runat="server" />

                        <div class="mb-3">
                            <label for="txtEditNombre" class="form-label">Nombre</label>
                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtEditOrigen" class="form-label">Origen</label>
                            <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtEditFechaNacimiento" class="form-label">Fecha de Nacimiento</label>
                            <asp:TextBox ID="TextBox3" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="mb-3">
                            <label for="txtEditDescripcion" class="form-label">Descripción</label>
                            <asp:TextBox ID="TextBox4" runat="server" TextMode="MultiLine" Rows="3" CssClass="form-control"></asp:TextBox>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Button1" runat="server" Text="Guardar Cambios" CssClass="btn btn-primary" OnClick="btnGuardarEdicion_Click" />
                        <div style="width: 1vw"></div>
                        <%-- Pequeño espacio --%>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    </div>

            </div>
        </div>
    </div>

    <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">

                <%-- Formulario para la eliminación --%>
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteModalLabel">Confirmar Eliminación</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p>¿Estás seguro de que quieres eliminar al ponente <strong id="Strong1" runat="server"></strong>?</p>
                        <%-- Campo oculto para guardar el ID del ponente que se va a eliminar --%>
                        <asp:HiddenField ID="HiddenField2" runat="server" />
                    </div>
                    <div class="modal-footer">
                        <asp:Button ID="Button2" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmarEliminacion_Click" />
                        <div style="width: 1vw"></div>
                        <%-- Pequeño espacio --%>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    </div>

            </div>
        </div>
    </div>

        </form>
    <!-- Bootstrap JS (required for modals to work) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
