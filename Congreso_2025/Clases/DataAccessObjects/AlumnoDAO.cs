using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Congreso_2025.Clases
{
    public class AlumnoDAO
    {
        private readonly General general = new General();

        public List<Alumno> Listar(string filtro = null)
        {
            using (var milinq = new MiLinQ(general.CadenaDeConexion))
            {
                var q = milinq.Alumno.AsQueryable();

                if (!string.IsNullOrWhiteSpace(filtro))
                {
                    filtro = filtro.Trim();
                    q = q.Where(a =>
                        ((a.carne ?? "").Contains(filtro)) ||
                        ((a.nombres_alumno ?? "").Contains(filtro)) ||
                        ((a.apellidos_alumno ?? "").Contains(filtro))
                    );
                }

                return q
                    .OrderBy(a => a.apellidos_alumno)
                    .ThenBy(a => a.nombres_alumno)
                    .ToList(); // devolvemos entidades Alumno de LINQ
            }
        }

        public Alumno ObtenerPorId(string id)
        {
            using (var milinq = new MiLinQ(general.CadenaDeConexion))
            {
                return milinq.Alumno.SingleOrDefault(a => a.id_alumno == id);
            }
        }

        public bool Actualizar(Alumno alumnoEditado, out string error)
        {
            error = null;
            if (alumnoEditado == null || string.IsNullOrWhiteSpace(alumnoEditado.id_alumno))
            {
                error = "Alumno inválido.";
                return false;
            }

            try
            {
                using (var milinq = new MiLinQ(general.CadenaDeConexion))
                {
                    var ent = milinq.Alumno.SingleOrDefault(a => a.id_alumno == alumnoEditado.id_alumno);
                    if (ent == null) { error = "Alumno no encontrado."; return false; }

                    // === Campos editables (ajusta según tu modelo) ===
                    ent.carne = alumnoEditado.carne;
                    ent.nombres_alumno = alumnoEditado.nombres_alumno;
                    ent.apellidos_alumno = alumnoEditado.apellidos_alumno;
                    ent.id_estado = alumnoEditado.id_estado;   // (int? en LINQ si es null en DB)
                    ent.id_usuario = alumnoEditado.id_usuario;  // (int?)
                    ent.id_pago = alumnoEditado.id_pago;     // (int?)
                    // =================================================

                    milinq.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                error = ex.Message;
                return false;
            }
        }

        public bool Eliminar(string id, out string error)
        {
            error = null;
            if (string.IsNullOrWhiteSpace(id)) { error = "Id inválido."; return false; }

            try
            {
                using (var milinq = new MiLinQ(general.CadenaDeConexion))
                {
                    var ent = milinq.Alumno.SingleOrDefault(a => a.id_alumno == id);
                    if (ent == null) { error = "Alumno no encontrado."; return false; }

                    milinq.Alumno.DeleteOnSubmit(ent);
                    milinq.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // aquí suelen salir errores de FK
                error = ex.Message;
                return false;
            }
        }
    }
}
