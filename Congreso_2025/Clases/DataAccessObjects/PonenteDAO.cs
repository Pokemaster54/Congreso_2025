using Congreso_2025.Clases.DataClasses;
using Congreso_2025.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using PonenteDC = Congreso_2025.Clases.DataClasses.PonenteDC;

namespace Congreso_2025.Clases.DataAccessObjects
{
    public class PonenteDAO
    {
        private General general = new General();
        public PonenteDAO() { }

        public List<Ponente> ConsultarPonentes()
        {
            try
            {
                using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                {
                    return miLinQ.Ponente.ToList();
                }
            }
            catch
            {
                return null;
            }

            
        }

        public bool InsertarPonente(PonenteDC ponente)
        {
            string siguienteCodigo = GenerarSiguienteCodigoPonente();
            try
            {
                using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                {
                    Ponente nuevoPonente = new Ponente
                    {
                        id_ponente = siguienteCodigo,
                        nombre_ponente = ponente.Nombre,
                        fecha_nacimiento = ponente.FechaNacimiento,
                        descripcion = ponente.Descripcion,
                        Origen = ponente.Origen
                    };
                    miLinQ.Ponente.InsertOnSubmit(nuevoPonente);
                    miLinQ.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                Console.WriteLine($"Error inserting ponente: {ex.Message}");
                return false;

                {
                }
            }

        }

        public string GenerarSiguienteCodigoPonente()
        {
            using (MiLinQ db = new MiLinQ(general.CadenaDeConexion)) // Instancia tu DataContext
            {
                try
                {
                    // Paso 1: Obtener las cadenas de código que cumplen el formato desde la base de datos.
                    // Esta parte de la consulta es traducible a SQL.
                    var codigosNumericosComoStrings = db.Ponente
                                                        .Where(p => p.id_ponente != null && p.id_ponente.StartsWith("PO") && p.id_ponente.Length == 6)
                                                        .Select(p => p.id_ponente.Substring(2)); // Obtenemos "0001", "0042", etc.

                    int ultimoNumero = 0; // Valor por defecto si no hay códigos que cumplan el formato

                    // Paso 2: Ejecutar la consulta hasta aquí (traer los strings a memoria)
                    // y luego procesar el resto en el cliente.
                    // Usamos .Any() para verificar si hay resultados antes de llamar a .Max()
                    if (codigosNumericosComoStrings.Any())
                    {
                        // Ahora que los strings están en memoria, podemos parsearlos a int y obtener el máximo.
                        ultimoNumero = codigosNumericosComoStrings.AsEnumerable() // Fuerza que lo siguiente se procese en memoria
                                                                  .Select(s => int.Parse(s)) // Convierte los strings a int
                                                                  .Max(); // Encuentra el número más alto
                    }

                    // Paso 3: Incrementar el número para obtener el siguiente
                    int siguienteNumero = ultimoNumero + 1;

                    // Paso 4: Formatear el nuevo código con ceros iniciales y el prefijo "PO"
                    string nuevoCodigo = $"PO{siguienteNumero:D4}";

                    return nuevoCodigo;
                }
                catch (FormatException fex)
                {
                    // Captura errores si alguna cadena no tiene el formato numérico esperado (ej. "POABCD")
                    Console.WriteLine($"Error de formato al generar código: Un código existente no tiene el formato numérico esperado (ej. 'PO0001').");
                    Console.WriteLine($"Detalle: {fex.Message}");
                    throw new ApplicationException("Error al generar el código del ponente debido a un formato inesperado en los códigos existentes.", fex);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error general al generar el siguiente código de ponente: {ex.Message}");
                    Console.WriteLine($"Detalle: {ex.ToString()}"); // Para ver la pila de llamadas completa
                    throw; // Relanza la excepción para que sea manejada por el código llamador
                }
            }

        }

        public Ponente CargarDatosPonente(Ponente ponente)
        {
            try
            {
                using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                {

                    var consultaPonente = miLinQ.Ponente
                        .Where(p => p.id_ponente == ponente.id_ponente)
                        .FirstOrDefault();
                    if (ponente != null)
                    {
                        Ponente nuevo = new Ponente()
                        {
                            id_ponente = consultaPonente.id_ponente,
                            nombre_ponente = consultaPonente.nombre_ponente,
                            fecha_nacimiento = consultaPonente.fecha_nacimiento,
                            Origen = consultaPonente.Origen,
                            descripcion = consultaPonente.descripcion
                        };
                        return nuevo;

                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool ActualizarPonente(Ponente ponente)
        {
            try
            {
                using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                {
                    // Cargar el ponente directamente desde el contexto actual
                    var consultaPonente = miLinQ.Ponente
                        .FirstOrDefault(p => p.id_ponente == ponente.id_ponente);

                    if (consultaPonente != null)
                    {
                        // Actualizar los campos
                        consultaPonente.nombre_ponente = ponente.nombre_ponente;
                        consultaPonente.fecha_nacimiento = ponente.fecha_nacimiento;
                        consultaPonente.Origen = ponente.Origen;
                        consultaPonente.descripcion = ponente.descripcion;

                        // Guardar cambios
                        miLinQ.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating ponente: {ex.Message}");
                return false;
            }
        }

        public bool EliminarPonente(string ponenteId)
        {
            try
            {
                using (MiLinQ miLinQ = new MiLinQ(general.CadenaDeConexion))
                {
                    var ponente = miLinQ.Ponente
                        .FirstOrDefault(p => p.id_ponente == ponenteId);

                    if (ponente != null)
                    {
                        miLinQ.Ponente.DeleteOnSubmit(ponente);
                        miLinQ.SubmitChanges();
                        return true;
                    }
                    return false; 
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando ponente: {ex.Message}");
                return false;
            }
        }
    }


}