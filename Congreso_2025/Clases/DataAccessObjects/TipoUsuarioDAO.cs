using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class TipoUsuarioDAO
    {
        private General general = new General();
        public TipoUsuarioDAO() { }

        public List<Tipo_usuario> ConsultarTiposUsuario()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Tipo_usuario
                             .OrderBy(t => t.nombre_tipo)
                             .ToList();
                }
            }
            catch
            {
                return new List<Tipo_usuario>();
            }
        }

        public bool InsertarTipoUsuario(string nombre_tipo)
        {
            string nuevoId = GenerarSiguienteCodigoTipoUsuario();
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var nuevo = new Tipo_usuario
                    {
                        id_tipo_usuario = nuevoId, // NVARCHAR(6)
                        nombre_tipo = nombre_tipo   // NVARCHAR(50)
                    };
                    db.Tipo_usuario.InsertOnSubmit(nuevo);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Tipo_usuario CargarDatosTipoUsuario(Tipo_usuario t)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Tipo_usuario.FirstOrDefault(x => x.id_tipo_usuario == t.id_tipo_usuario);
                    if (cur == null) return null;

                    return new Tipo_usuario
                    {
                        id_tipo_usuario = cur.id_tipo_usuario,
                        nombre_tipo = cur.nombre_tipo
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ActualizarTipoUsuario(Tipo_usuario t)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Tipo_usuario.FirstOrDefault(x => x.id_tipo_usuario == t.id_tipo_usuario);
                    if (cur == null) return false;

                    cur.nombre_tipo = t.nombre_tipo;
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarTipoUsuario(string idTipo)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Tipo_usuario.FirstOrDefault(x => x.id_tipo_usuario == idTipo);
                    if (cur == null) return false;

                    db.Tipo_usuario.DeleteOnSubmit(cur);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                // Si hay FK desde Usuario, aquí puede fallar
                return false;
            }
        }

        /// <summary>
        /// Genera IDs tipo "TU0001" con largo 6 (prefijo 2 + 4 dígitos), acorde a NVARCHAR(6).
        /// </summary>
        public string GenerarSiguienteCodigoTipoUsuario()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                try
                {
                    var numerosStr = db.Tipo_usuario
                        .Where(x => x.id_tipo_usuario != null && x.id_tipo_usuario.StartsWith("TU") && x.id_tipo_usuario.Length == 6)
                        .Select(x => x.id_tipo_usuario.Substring(2));

                    int ultimo = 0;
                    if (numerosStr.Any())
                    {
                        ultimo = numerosStr.AsEnumerable().Select(s => int.Parse(s)).Max();
                    }

                    int siguiente = ultimo + 1;
                    return $"TU{siguiente:D4}";
                }
                catch
                {
                    return "TU0001";
                }
            }
        }
    }
}
