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
                            <td><%# Eval("Codigo") %></td>
                            <td><%# Eval("Name") %></td>
                            <td><%# Eval("Origin") %></td>
                            <td><%# Eval("FechaNacimiento") %></td>
                            <td><%# Eval("Description") %></td>
                            <td>
                                <button class="btn btn-sm btn-warning" data-bs-toggle="modal" data-bs-target="#editModal">Edit</button>
                                <button class="btn btn-sm btn-danger" data-bs-toggle="modal" data-bs-target="#deleteModal">Delete</button>
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

                <form id="formAgregarNuevoPonente" runat="server">
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
                </form>

            </div>
        </div>
    </div>

    <!-- Edit Modal -->
    <div class="modal fade" id="editModal" tabindex="-1" aria-labelledby="editModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <form action="/edit" method="POST">
                    <div class="modal-header">
                        <h5 class="modal-title" id="editModalLabel">Edit Entry</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <!-- Usually pre-filled with existing data -->
                        <input type="hidden" name="id" value="1">
                        <div class="mb-3">
                            <label for="editName" class="form-label">Name</label>
                            <input type="text" class="form-control" id="editName" name="name" value="Jane Doe" required>
                        </div>
                        <div class="mb-3">
                            <label for="editEmail" class="form-label">Email</label>
                            <input type="email" class="form-control" id="editEmail" name="email" value="jane@example.com" required>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-warning">Update</button>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Delete Modal -->
    <div class="modal fade" id="deleteModal" tabindex="-1" aria-labelledby="deleteModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <form action="/delete" method="POST">
                    <div class="modal-header">
                        <h5 class="modal-title" id="deleteModalLabel">Confirm Deletion</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body">
                        <p>Are you sure you want to delete this entry?</p>
                        <input type="hidden" name="id" value="1">
                    </div>
                    <div class="modal-footer">
                        <button type="submit" class="btn btn-danger">Delete</button>
                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    </div>

    <!-- Bootstrap JS (required for modals to work) -->
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>
