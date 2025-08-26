using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class UsuarioDAO
    {
        private General general = new General();
        public UsuarioDAO() { }

        public List<Usuario> ConsultarUsuarios()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    return db.Usuario.OrderBy(u => u.nombre_usuario).ToList();
                }
            }
            catch
            {
                return new List<Usuario>();
            }
        }

        /// <summary>
        /// Devuelve usuarios con el nombre del tipo (join a Tipo_usuario).
        /// </summary>
        public List<dynamic> ConsultarUsuariosConTipo()
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    // Proyección anónima para mostrar en el Repeater (incluye nombre_tipo)
                    var q = from u in db.Usuario
                            join t in db.Tipo_usuario on u.id_tipo_usuario equals t.id_tipo_usuario
                            orderby u.nombre_usuario
                            select new
                            {
                                u.id_usuario,
                                u.nombre_usuario,
                                u.password,
                                u.id_tipo_usuario,
                                nombre_tipo = t.nombre_tipo
                            };
                    return q.ToList<dynamic>();
                }
            }
            catch
            {
                return new List<dynamic>();
            }
        }

        public bool InsertarUsuario(string nombre_usuario, string password, string id_tipo_usuario)
        {
            string nuevoId = GenerarSiguienteCodigoUsuario();
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var nuevo = new Usuario
                    {
                        id_usuario = nuevoId,            // Nvarchar(6)
                        nombre_usuario = nombre_usuario, // Nvarchar(10)
                        password = password,             // Nvarchar(10)
                        id_tipo_usuario = id_tipo_usuario
                    };
                    db.Usuario.InsertOnSubmit(nuevo);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Usuario CargarDatosUsuario(Usuario u)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var found = db.Usuario.FirstOrDefault(x => x.id_usuario == u.id_usuario);
                    if (found == null) return null;
                    // Devolver una instancia desacoplada opcionalmente (no es estrictamente necesario)
                    return new Usuario
                    {
                        id_usuario = found.id_usuario,
                        nombre_usuario = found.nombre_usuario,
                        password = found.password,
                        id_tipo_usuario = found.id_tipo_usuario
                    };
                }
            }
            catch
            {
                return null;
            }
        }

        public bool ActualizarUsuario(Usuario u)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Usuario.FirstOrDefault(x => x.id_usuario == u.id_usuario);
                    if (cur == null) return false;

                    cur.nombre_usuario = u.nombre_usuario;
                    cur.password = u.password;
                    cur.id_tipo_usuario = u.id_tipo_usuario;

                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarUsuario(string idUsuario)
        {
            try
            {
                using (var db = new MiLinQ(general.CadenaDeConexion))
                {
                    var cur = db.Usuario.FirstOrDefault(x => x.id_usuario == idUsuario);
                    if (cur == null) return false;

                    db.Usuario.DeleteOnSubmit(cur);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Genera IDs tipo "US0001" con largo 6 (prefijo 2 + 4 dígitos), acorde a Nvarchar(6).
        /// </summary>
        public string GenerarSiguienteCodigoUsuario()
        {
            using (var db = new MiLinQ(general.CadenaDeConexion))
            {
                try
                {
                    var numerosStr = db.Usuario
                        .Where(x => x.id_usuario != null && x.id_usuario.StartsWith("US") && x.id_usuario.Length == 6)
                        .Select(x => x.id_usuario.Substring(2));

                    int ultimo = 0;
                    if (numerosStr.Any())
                    {
                        ultimo = numerosStr.AsEnumerable().Select(s => int.Parse(s)).Max();
                    }

                    int siguiente = ultimo + 1;
                    return $"US{siguiente:D4}";
                }
                catch
                {
                    // Fallback simple si hay formatos inesperados
                    return "US0001";
                }
            }
        }
    }
}
