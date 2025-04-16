using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;
using practicoInmobiliaria.Models;


namespace practicoInmobiliaria.Models
{
    public class ConexionDB
    {
        private MySqlConnection conexion;

        // Definimos la cadena de conexión como un campo de la clase
        private string _connectionString;

        // Constructor donde inicializamos la conexión
        public ConexionDB()
        {
            string servidor = "localhost";
            string baseDatos = "inmobiliaria";  // Nombre de tu base de datos
            string usuario = "root";  // Usuario por defecto en XAMPP
            string contrasena = "";   // En XAMPP no suele tener contraseña por defecto

            _connectionString = $"Server={servidor}; database={baseDatos}; UID={usuario}; password={contrasena};";
            conexion = new MySqlConnection(_connectionString);
        }



        // Método para obtener todos los propietarios desde la base de datos
        public List<Propietario> ObtenerPropietarios()
        {
            List<Propietario> propietarios = new List<Propietario>();

            try
            {
                conexion.Open();
                string query = "SELECT * FROM propietario";  // Consulta SQL para obtener los propietarios
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Propietario propietario = new Propietario
                    {
                        IdPropietario = reader.GetInt32("idPropietario"),
                        DniPropietario = reader.GetString("dniPropietario"),
                        ApellidoPropietario = reader.GetString("apellidoPropietario"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        ContactoPropietario = reader.GetString("contactoPropietario")
                    };
                    propietarios.Add(propietario);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al obtener los propietarios: " + ex.Message);  // Para depurar
            }
            finally
            {
                conexion.Close();
            }

            return propietarios;
        }



        // Método para obtener todos los inquilinos desde la base de datos
        public List<Inquilino> ObtenerInquilinos()
        {
            List<Inquilino> inquilinos = new List<Inquilino>();

            try
            {
                conexion.Open();
                string query = "SELECT * FROM inquilino";  // Consulta SQL para obtener los inquilinos
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Inquilino inquilino = new Inquilino
                    {
                        IdInquilino = reader.GetInt32("idInquilino"),
                        DniInquilino = reader.GetString("dniInquilino"),
                        ApellidoInquilino = reader.GetString("apellidoInquilino"),
                        NombreInquilino = reader.GetString("nombreInquilino"),
                        ContactoInquilino = reader.GetString("contactoInquilino")
                    };
                    inquilinos.Add(inquilino);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al obtener los inquilinos: " + ex.Message);  // Para depurar
            }
            finally
            {
                conexion.Close();
            }

            return inquilinos;
        }

        // Obtener propietario por ID
        public Propietario ObtenerPropietarioPorId(int id)
        {
            Propietario propietario = null;

            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM propietario WHERE idPropietario = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    propietario = new Propietario
                    {
                        IdPropietario = reader.GetInt32("idPropietario"),
                        DniPropietario = reader.GetString("dniPropietario"),
                        ApellidoPropietario = reader.GetString("apellidoPropietario"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        ContactoPropietario = reader.GetString("contactoPropietario")
                    };
                }
            }
            return propietario;
        }

        // Obtener inquilino por ID
        public Inquilino ObtenerInquilinoPorId(int id)
        {
            Inquilino inquilino = null;

            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM inquilino WHERE idInquilino = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    inquilino = new Inquilino
                    {
                        IdInquilino = reader.GetInt32("idInquilino"),
                        DniInquilino = reader.GetString("dniInquilino"),
                        ApellidoInquilino = reader.GetString("apellidoInquilino"),
                        NombreInquilino = reader.GetString("nombreInquilino"),
                        ContactoInquilino = reader.GetString("contactoInquilino")
                    };
                }
            }
            return inquilino;
        }

        // Agregar un nuevo propietario
        public void AgregarPropietario(Propietario propietario)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO propietario (dniPropietario, apellidoPropietario, nombrePropietario, contactoPropietario) VALUES (@dni, @apellido, @nombre, @contacto)";
                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@dni", propietario.DniPropietario);
                cmd.Parameters.AddWithValue("@apellido", propietario.ApellidoPropietario);
                cmd.Parameters.AddWithValue("@nombre", propietario.NombrePropietario);
                cmd.Parameters.AddWithValue("@contacto", propietario.ContactoPropietario);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Agregar un nuevo inquilino
        public void AgregarInquilino(Inquilino inquilino)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO inquilino (dniInquilino, apellidoInquilino, nombreInquilino, contactoInquilino) VALUES (@dni, @apellido, @nombre, @contacto)";
                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@dni", inquilino.DniInquilino);
                cmd.Parameters.AddWithValue("@apellido", inquilino.ApellidoInquilino);
                cmd.Parameters.AddWithValue("@nombre", inquilino.NombreInquilino);
                cmd.Parameters.AddWithValue("@contacto", inquilino.ContactoInquilino);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar un propietario existente
        public void ActualizarPropietario(Propietario propietario)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE propietario SET dniPropietario = @dni, apellidoPropietario = @apellido, nombrePropietario = @nombre, contactoPropietario = @contacto WHERE idPropietario = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@id", propietario.IdPropietario);
                cmd.Parameters.AddWithValue("@dni", propietario.DniPropietario);
                cmd.Parameters.AddWithValue("@apellido", propietario.ApellidoPropietario);
                cmd.Parameters.AddWithValue("@nombre", propietario.NombrePropietario);
                cmd.Parameters.AddWithValue("@contacto", propietario.ContactoPropietario);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Actualizar un inquilino existente
        public void ActualizarInquilino(Inquilino inquilino)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "UPDATE inquilino SET dniInquilino = @dni, apellidoInquilino = @apellido, nombreInquilino = @nombre, contactoInquilino = @contacto WHERE idInquilino = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@id", inquilino.IdInquilino);
                cmd.Parameters.AddWithValue("@dni", inquilino.DniInquilino);
                cmd.Parameters.AddWithValue("@apellido", inquilino.ApellidoInquilino);
                cmd.Parameters.AddWithValue("@nombre", inquilino.NombreInquilino);
                cmd.Parameters.AddWithValue("@contacto", inquilino.ContactoInquilino);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar un propietario
        public void EliminarPropietario(int id)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM propietario WHERE idPropietario = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }

        // Eliminar un inquilino
        public void EliminarInquilino(int id)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM inquilino WHERE idInquilino = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
        // private string connectionString = "server=localhost;database=inmobiliaria;uid=root;pwd=;";

        // Obtener todos los inmuebles
        public List<Inmueble> ObtenerInmuebles()
        {
            List<Inmueble> inmuebles = new List<Inmueble>();

            conexion.Open();
            string query = "SELECT * FROM inmueble";
            MySqlCommand cmd = new MySqlCommand(query, conexion);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Inmueble inmueble = new Inmueble
                {
                    IdInmueble = reader.GetInt32("idInmueble"),
                    DniPropietario = reader.GetString("dniPropietario"),
                    Calle = reader.GetString("calle"),
                    Nro = reader.GetInt32("nro"),
                    Piso = reader.IsDBNull(reader.GetOrdinal("piso")) ? 0 : reader.GetInt32("piso"),
                    Dpto = reader.IsDBNull(reader.GetOrdinal("dpto")) ? null : reader.GetString("dpto"),
                    Localidad = reader.GetString("localidad"),
                    Provincia = reader.GetString("provincia"),
                    Uso = reader.GetString("uso"),
                    Tipo = reader.GetString("tipo"),
                    Ambientes = reader.GetInt32("ambientes"),
                    Pileta = reader["pileta"].ToString() == "1",
                    Parrilla = reader["parrilla"].ToString() == "1",
                    Garage = reader["garage"].ToString() == "1",
                    Latitud = Convert.ToDouble(reader["latitud"]),
                    Longitud = Convert.ToDouble(reader["longitud"]),
                    Precio = Convert.ToDouble(reader["precio"]),
                    ImagenPortada = reader["ImagenPortada"] != DBNull.Value ? reader["ImagenPortada"].ToString() : null,
                    FotosCarruselLista = new List<string>()
                };

                inmuebles.Add(inmueble);
            }

            reader.Close();
            conexion.Close();

            return inmuebles;
        }

        public List<InmuebleFotoCarrusel> ObtenerFotosCarruselPorInmueble(int id)
        {
            var fotosCarrusel = new List<InmuebleFotoCarrusel>();

            using (var conexion = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM InmuebleFotoCarrusel WHERE IdInmueble = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        fotosCarrusel.Add(new InmuebleFotoCarrusel
                        {
                            Id = reader.GetInt32("Id"),
                            IdInmueble = reader.GetInt32("IdInmueble"),
                            RutaFoto = reader.GetString("RutaFoto")
                        });
                    }
                }
            }

            return fotosCarrusel;
        }




        public void InsertarFotoCarrusel(int idInmueble, string rutaFoto)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "INSERT INTO InmuebleFotoCarrusel (IdInmueble, RutaFoto) VALUES (@idInmueble, @ruta)";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@idInmueble", idInmueble);
                cmd.Parameters.AddWithValue("@ruta", rutaFoto);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public void AgregarFotoCarrusel(int idInmueble, string rutaFoto)
        {
            using (var conn = ObtenerConexion())
            {
                var cmd = new MySqlCommand("INSERT INTO InmuebleFotoCarrusel (IdInmueble, RutaFoto) VALUES (@id, @ruta)", conn);
                cmd.Parameters.AddWithValue("@id", idInmueble);
                cmd.Parameters.AddWithValue("@ruta", rutaFoto);
                cmd.ExecuteNonQuery();
            }
        }

        public Inmueble ObtenerInmueblePorId(int id)
        {
            Inmueble inmueble = null;

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(_connectionString))
                {
                    conexion.Open();

                    // 1. Obtener datos del inmueble
                    string query = "SELECT * FROM inmueble WHERE idInmueble = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@id", id);

                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            inmueble = new Inmueble
                            {
                                IdInmueble = reader.GetInt32("idInmueble"),
                                DniPropietario = reader.GetString("dniPropietario"),
                                Calle = reader.GetString("calle"),
                                Nro = reader.GetInt32("nro"),
                                Piso = reader.IsDBNull(reader.GetOrdinal("piso")) ? 0 : reader.GetInt32("piso"),
                                Dpto = reader["dpto"] != DBNull.Value ? reader["dpto"].ToString() : "",
                                Localidad = reader.GetString("localidad"),
                                Provincia = reader.GetString("provincia"),
                                Uso = reader.GetString("uso"),
                                Tipo = reader.GetString("tipo"),
                                Ambientes = reader.GetInt32("ambientes"),
                                Pileta = reader["pileta"].ToString() == "1",
                                Parrilla = reader["parrilla"].ToString() == "1",
                                Garage = reader["garage"].ToString() == "1",
                                Latitud = Convert.ToDouble(reader["latitud"]),
                                Longitud = Convert.ToDouble(reader["longitud"]),
                                Precio = Convert.ToDouble(reader["precio"]),
                                ImagenPortada = reader["ImagenPortada"] != DBNull.Value ? reader["ImagenPortada"].ToString() : null,
                                FotosCarruselLista = new List<string>()
                            };
                        }
                    }

                    // 2. Obtener fotos del carrusel (misma conexión, aún abierta)
                    if (inmueble != null)
                    {
                        string fotosQuery = "SELECT RutaFoto FROM InmuebleFotoCarrusel WHERE IdInmueble = @id";
                        MySqlCommand cmdFotos = new MySqlCommand(fotosQuery, conexion);
                        cmdFotos.Parameters.AddWithValue("@id", id);

                        using (var readerFotos = cmdFotos.ExecuteReader())
                        {
                            while (readerFotos.Read())
                            {
                                inmueble.FotosCarruselLista.Add(readerFotos["RutaFoto"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener inmueble: " + ex.Message);
            }

            return inmueble;
        }

        public List<practicoInmobiliaria.Models.FotoCarrusel> ObtenerFotosCarrusel(int idInmueble)
        {
            var lista = new List<practicoInmobiliaria.Models.FotoCarrusel>();

            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                var cmd = new MySqlCommand("SELECT Id, RutaFoto FROM InmuebleFotoCarrusel WHERE IdInmueble = @id", conexion);
                cmd.Parameters.AddWithValue("@id", idInmueble);
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new practicoInmobiliaria.Models.FotoCarrusel
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        RutaFoto = reader["RutaFoto"].ToString()
                    });
                }
            }

            return lista;
        }


        public string ObtenerRutaFotoCarrusel(int idFoto)
        {
            string ruta = "";
            using (var conn = ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand("SELECT RutaFoto FROM InmuebleFotoCarrusel WHERE Id = @id", conn);
                cmd.Parameters.AddWithValue("@id", idFoto);
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    ruta = reader["RutaFoto"].ToString();
                }
            }
            return ruta;
        }


        public void EliminarFotoCarrusel(int id)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM InmuebleFotoCarrusel WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public InmuebleFotoCarrusel ObtenerFotoCarruselPorId(int id)
        {
            InmuebleFotoCarrusel foto = null;
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "SELECT * FROM InmuebleFotoCarrusel WHERE Id = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);
                conexion.Open();
                Console.WriteLine("Foto encontrada: " + foto?.RutaFoto);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        foto = new InmuebleFotoCarrusel
                        {
                            Id = reader.GetInt32("Id"),
                            IdInmueble = reader.GetInt32("IdInmueble"),
                            RutaFoto = reader.GetString("RutaFoto")
                        };
                    }
                }
            }
            return foto;
        }





        public class InmuebleViewModel
        {
            // Otros campos del inmueble...
            public List<InmuebleFotoCarrusel> ImagenesCarrusel { get; set; }
        }


        // Agregar nuevo inmueble
        public int AgregarInmueble(Inmueble inmueble)
        {
            int idInmueble = 0;

            using (var conn = ObtenerConexion())
            {
                conn.Open();
                var cmd = new MySqlCommand(@"
            INSERT INTO Inmueble 
                (DniPropietario, Calle, Nro, Piso, Dpto, Localidad, Provincia, Uso, Tipo, Ambientes, Precio, Latitud, Longitud, Pileta, Parrilla, Garage, ImagenPortada)
            VALUES 
                (@DniPropietario, @Calle, @Nro, @Piso, @Dpto, @Localidad, @Provincia, @Uso, @Tipo, @Ambientes, @Precio, @Latitud, @Longitud, @Pileta, @Parrilla, @Garage, @ImagenPortada);
            SELECT LAST_INSERT_ID();", conn);

                cmd.Parameters.AddWithValue("@DniPropietario", inmueble.DniPropietario);
                cmd.Parameters.AddWithValue("@Calle", inmueble.Calle);
                cmd.Parameters.AddWithValue("@Nro", inmueble.Nro);
                cmd.Parameters.AddWithValue("@Piso", inmueble.Piso);
                cmd.Parameters.AddWithValue("@Dpto", inmueble.Dpto);
                cmd.Parameters.AddWithValue("@Localidad", inmueble.Localidad);
                cmd.Parameters.AddWithValue("@Provincia", inmueble.Provincia);
                cmd.Parameters.AddWithValue("@Uso", inmueble.Uso);
                cmd.Parameters.AddWithValue("@Tipo", inmueble.Tipo);
                cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
                cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
                cmd.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
                cmd.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
                cmd.Parameters.AddWithValue("@Pileta", inmueble.Pileta);
                cmd.Parameters.AddWithValue("@Parrilla", inmueble.Parrilla);
                cmd.Parameters.AddWithValue("@Garage", inmueble.Garage);
                cmd.Parameters.AddWithValue("@ImagenPortada", inmueble.ImagenPortada);

                idInmueble = Convert.ToInt32(cmd.ExecuteScalar());

                // Guardar fotos del carrusel si hay
                if (!string.IsNullOrEmpty(inmueble.FotosCarrusel))
                {
                    var rutas = inmueble.FotosCarrusel.Split(';');

                    foreach (var ruta in rutas)
                    {
                        if (!string.IsNullOrWhiteSpace(ruta))
                        {
                            var cmdCarrusel = new MySqlCommand(
                                "INSERT INTO InmuebleFotoCarrusel (IdInmueble, RutaFoto) VALUES (@IdInmueble, @RutaFoto)", conn);
                            cmdCarrusel.Parameters.AddWithValue("@IdInmueble", idInmueble);
                            cmdCarrusel.Parameters.AddWithValue("@RutaFoto", ruta);
                            cmdCarrusel.ExecuteNonQuery();
                        }
                    }
                }
            }

            return idInmueble;
        }




        // Actualizar inmueble existente
        public void ActualizarInmueble(Inmueble inmueble)
        {
            System.Diagnostics.Debug.WriteLine("📌 Entrando a ActualizarInmueble: " + inmueble.IdInmueble);
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = @"UPDATE inmueble SET 
            dniPropietario = @dni,
            calle = @calle,
            nro = @nro,
            piso = @piso,
            dpto = @dpto,
            localidad = @localidad,
            provincia = @provincia,
            uso = @uso,
            tipo = @tipo,
            ambientes = @ambientes,
            pileta = @pileta,
            parrilla = @parrilla,
            garage = @garage,
            latitud = @latitud,
            longitud = @longitud,
            precio = @precio,
            ImagenPortada=@ImagenPortada
        WHERE idInmueble = @id";

                MySqlCommand cmd = new MySqlCommand(query, conexion);

                cmd.Parameters.AddWithValue("@id", inmueble.IdInmueble);
                cmd.Parameters.AddWithValue("@dni", inmueble.DniPropietario);
                cmd.Parameters.AddWithValue("@calle", inmueble.Calle);
                cmd.Parameters.AddWithValue("@nro", inmueble.Nro);
                cmd.Parameters.AddWithValue("@piso", inmueble.Piso);
                cmd.Parameters.AddWithValue("@dpto", inmueble.Dpto);
                cmd.Parameters.AddWithValue("@localidad", inmueble.Localidad);
                cmd.Parameters.AddWithValue("@provincia", inmueble.Provincia);
                cmd.Parameters.AddWithValue("@uso", inmueble.Uso);
                cmd.Parameters.AddWithValue("@tipo", inmueble.Tipo);
                cmd.Parameters.AddWithValue("@ambientes", inmueble.Ambientes);
                cmd.Parameters.AddWithValue("@pileta", inmueble.Pileta);
                cmd.Parameters.AddWithValue("@parrilla", inmueble.Parrilla);
                cmd.Parameters.AddWithValue("@garage", inmueble.Garage);
                cmd.Parameters.AddWithValue("@latitud", inmueble.Latitud);
                cmd.Parameters.AddWithValue("@longitud", inmueble.Longitud);
                cmd.Parameters.AddWithValue("@precio", inmueble.Precio);
                cmd.Parameters.AddWithValue("@ImagenPortada", inmueble.ImagenPortada);
                conexion.Open();
                System.Diagnostics.Debug.WriteLine("Actualizando inmueble con ruta: " + inmueble.ImagenPortada);

                cmd.ExecuteNonQuery();
            }
        }

        public void EliminarInmueble(int id)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = "DELETE FROM inmueble WHERE idInmueble = @id";
                MySqlCommand cmd = new MySqlCommand(query, conexion);
                cmd.Parameters.AddWithValue("@id", id);

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }


        public Propietario ObtenerPropietarioPorDni(int dni)
        {
            Propietario propietario = null;

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM propietario WHERE dniPropietario = @dni";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@dni", dni);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        propietario = new Propietario
                        {
                            DniPropietario = reader.GetString("DniPropietario"),
                            ApellidoPropietario = reader.GetString("ApellidoPropietario"),
                            NombrePropietario = reader.GetString("NombrePropietario"),
                            ContactoPropietario = reader.GetString("ContactoPropietario")

                        };
                    }
                }
            }

            return propietario;
        }

        public MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(_connectionString);
        }

        public List<Propietario> BuscarPropietariosPorNombreODni(string filtro)
        {
            List<Propietario> lista = new List<Propietario>();

            using (var connection = ObtenerConexion())
            {
                connection.Open();
                string query = @"SELECT *
                         FROM Propietario 
                         WHERE dniPropietario LIKE @filtro OR 
                               apellidoPropietario LIKE @filtro OR 
                               nombrePropietario LIKE @filtro";

                using (var cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Propietario
                            {
                                DniPropietario = reader.GetString("DniPropietario"),
                                ApellidoPropietario = reader.GetString("ApellidoPropietario"),
                                NombrePropietario = reader.GetString("NombrePropietario"),
                                ContactoPropietario = reader.GetString("ContactoPropietario")
                            });
                        }
                    }
                }
            }

            return lista;
        }

        public List<Propietario> BuscarPropietarios(string termino)
        {
            List<Propietario> lista = new List<Propietario>();

            using (MySqlConnection connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = @"SELECT * FROM Propietario 
                         WHERE dniPropietario LIKE @termino OR 
                               nombrePropietario LIKE @termino OR 
                               apellidoPropietario LIKE @termino";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@termino", "%" + termino + "%");

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Propietario
                            {
                                DniPropietario = reader.GetString("dniPropietario"),
                                ApellidoPropietario = reader.GetString("apellidoPropietario"),
                                NombrePropietario = reader.GetString("nombrePropietario"),
                                ContactoPropietario = reader.GetString("contactoPropietario")
                            });
                        }
                    }
                }
            }

            return lista;
        }


        public List<Propietario> ListarPropietarios()
        {
            List<Propietario> lista = new List<Propietario>();
            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                var comando = new MySqlCommand("SELECT * FROM propietario", conexion);
                var reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    lista.Add(new Propietario
                    {
                        DniPropietario = reader.GetString("dniPropietario"),
                        ApellidoPropietario = reader.GetString("apellidoPropietario"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        ContactoPropietario = reader.GetString("contactoPropietario")
                    });
                }
            }
            return lista;
        }







        public void AgregarFotosCarrusel(int idInmueble, List<string> rutas)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                foreach (var ruta in rutas)
                {
                    var command = new MySqlCommand("INSERT INTO InmuebleFotoCarrusel (IdInmueble, RutaFoto) VALUES (@id, @ruta)", connection);
                    command.Parameters.AddWithValue("@id", idInmueble);
                    command.Parameters.AddWithValue("@ruta", ruta);
                    command.ExecuteNonQuery();
                }
            }
        }


        public List<Contrato> ObtenerTodosLosContratos()
        {
            var contratos = new List<Contrato>();

            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = "SELECT * FROM Contrato"; // Asegúrate de que la tabla se llama Contratos en tu base de datos
                var comando = new MySqlCommand(query, conexion);
                var reader = comando.ExecuteReader();

                while (reader.Read())
                {
                    contratos.Add(new Contrato
                    {
                        IdContrato = reader.GetInt32("idContrato"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        NombreInquilino = reader.GetString("nombreInquilino"),
                        Direccion = reader.GetString("direccion"),
                        FechaInicio = reader.GetDateTime("fechaInicio"),
                        FechaFinal = reader.GetDateTime("fechaFinal"),
                        Monto = reader.GetDecimal("monto"),
                        Vigente = reader.GetBoolean("vigente")
                    });
                }
            }

            return contratos;
        }

        public Contrato ObtenerContratoPorId(int id)
        {
            Contrato contrato = null;

            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = "SELECT * FROM Contrato WHERE idContrato = @id";
                var comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);
                var reader = comando.ExecuteReader();

                if (reader.Read())
                {
                    contrato = new Contrato
                    {
                        IdContrato = reader.GetInt32("idContrato"),
                        NombrePropietario = reader.GetString("nombrePropietario"),
                        NombreInquilino = reader.GetString("nombreInquilino"),
                        Direccion = reader.GetString("direccion"),
                        FechaInicio = reader.GetDateTime("fechaInicio"),
                        FechaFinal = reader.GetDateTime("fechaFinal"),
                        Monto = reader.GetDecimal("monto"),
                        Vigente = reader.GetBoolean("vigente")
                    };
                }
            }

            return contrato;
        }

        // Método para agregar un nuevo contrato
        public bool AgregarContrato(Contrato contrato)
        {
            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = "INSERT INTO Contrato (nombrePropietario, nombreInquilino, direccion, fechaInicio, fechaFinal, monto, vigente) " +
                            "VALUES (@nombrePropietario, @nombreInquilino, @direccion, @fechaInicio, @fechaFinal, @monto, @vigente)";
                var comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombrePropietario", contrato.NombrePropietario);
                comando.Parameters.AddWithValue("@nombreInquilino", contrato.NombreInquilino);
                comando.Parameters.AddWithValue("@direccion", contrato.Direccion);
                comando.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                comando.Parameters.AddWithValue("@fechaFinal", contrato.FechaFinal);
                comando.Parameters.AddWithValue("@monto", contrato.Monto);
                comando.Parameters.AddWithValue("@vigente", contrato.Vigente);

                var filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        // Método para actualizar un contrato existente
        public bool ActualizarContrato(Contrato contrato)
        {
            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = "UPDATE Contrato SET nombrePropietario = @nombrePropietario, nombreInquilino = @nombreInquilino, direccion = @direccion, " +
                            "fechaInicio = @fechaInicio, fechaFinal = @fechaFinal, monto = @monto, vigente = @vigente WHERE idContrato = @idContrato";
                var comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@nombrePropietario", contrato.NombrePropietario);
                comando.Parameters.AddWithValue("@nombreInquilino", contrato.NombreInquilino);
                comando.Parameters.AddWithValue("@direccion", contrato.Direccion);
                comando.Parameters.AddWithValue("@fechaInicio", contrato.FechaInicio);
                comando.Parameters.AddWithValue("@fechaFinal", contrato.FechaFinal);
                comando.Parameters.AddWithValue("@monto", contrato.Monto);
                comando.Parameters.AddWithValue("@vigente", contrato.Vigente);
                comando.Parameters.AddWithValue("@idContrato", contrato.IdContrato);

                var filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }

        // Método para eliminar un contrato por ID
        public bool EliminarContrato(int id)
        {
            using (var conexion = new MySqlConnection(_connectionString))
            {
                conexion.Open();
                var query = "DELETE FROM Contrato WHERE idContrato = @id";
                var comando = new MySqlCommand(query, conexion);
                comando.Parameters.AddWithValue("@id", id);

                var filasAfectadas = comando.ExecuteNonQuery();
                return filasAfectadas > 0;
            }
        }



        // Método para buscar inquilinos
        public List<Inquilino> BuscarInquilinos(string termino)
        {
            var lista = new List<Inquilino>();

            using (var conexion = ObtenerConexion())
            {
                conexion.Open();
                string query = @"SELECT * FROM inquilino 
                         WHERE dniInquilino LIKE @termino OR nombreInquilino LIKE @termino OR apellidoInquilino LIKE @termino";

                using (var cmd = new MySqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@termino", "%" + termino + "%");

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Inquilino
                            {
                                DniInquilino = reader["dniInquilino"].ToString(),
                                NombreInquilino = reader["nombreInquilino"].ToString(),
                                ApellidoInquilino = reader["apellidoInquilino"].ToString()

                            });
                        }
                    }
                }
            }

            return lista;
        }


        public List<Inmueble> ObtenerInmueblesPorDni(string dni)
        {
            List<Inmueble> lista = new List<Inmueble>();
            string query = "SELECT * FROM Inmueble WHERE dniPropietario = @dni";

            using (var con = ObtenerConexion())
            {
                con.Open();
                using (var cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@dni", dni);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Inmueble
                            {
                                IdInmueble = Convert.ToInt32(reader["idInmueble"]),
                                Calle = reader["calle"].ToString(),
                                Nro = Convert.ToInt32(reader["nro"]),
                                Piso = reader["piso"] != DBNull.Value ? Convert.ToInt32(reader["piso"]) : 0,
                                Dpto = reader["dpto"] != DBNull.Value ? reader["dpto"].ToString() : "",
                                Localidad = reader["localidad"].ToString()
                            });
                        }
                    }
                }
            }

            return lista;
        }


    }

}

