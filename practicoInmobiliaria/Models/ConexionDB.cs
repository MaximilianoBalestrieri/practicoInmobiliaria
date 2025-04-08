using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;


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

            try
            {
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
                        Piso = reader.GetInt32("piso"),
                        Dpto = reader.GetString("dpto"),
                        Localidad = reader.GetString("localidad"),
                        Provincia = reader.GetString("provincia"),
                        Uso = reader.GetString("uso"),
                        Tipo = reader.GetString("tipo"),
                        Ambientes = reader.GetInt32("ambientes"),
                        Pileta = reader.GetBoolean("pileta"),
                        Parrilla = reader.GetBoolean("parrilla"),
                        Garage = reader.GetBoolean("garage"),
                        Latitud = reader.GetDouble("latitud"),
                        Longitud = reader.GetDouble("longitud"),
                        Precio = reader.GetDouble("precio")
                    };
                    inmuebles.Add(inmueble);
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error al obtener los inmuebles: " + ex.Message);
            }
            finally
            {
                conexion.Close();
            }

            return inmuebles;
        }

        // Obtener inmueble por ID
        public Inmueble ObtenerInmueblePorId(int id)
        {
            Inmueble inmueble = null;

            try
            {
                using (MySqlConnection conexion = new MySqlConnection(_connectionString))
                {
                    string query = "SELECT * FROM inmueble WHERE idInmueble = @id";
                    MySqlCommand cmd = new MySqlCommand(query, conexion);
                    cmd.Parameters.AddWithValue("@id", id);

                    conexion.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        inmueble = new Inmueble
                        {
                            IdInmueble = reader.GetInt32("idInmueble"),
                            DniPropietario = reader.GetString("dniPropietario"),
                            Calle = reader.GetString("calle"),
                            Nro = reader.GetInt32("nro"),
                            Piso = reader.GetInt32("piso"),
                            Dpto = reader.GetString("dpto"),
                            Localidad = reader.GetString("localidad"),
                            Provincia = reader.GetString("provincia"),
                            Uso = reader.GetString("uso"),
                            Tipo = reader.GetString("tipo"),
                            Ambientes = reader.GetInt32("ambientes"),
                            Pileta = reader.GetBoolean("pileta"),
                            Parrilla = reader.GetBoolean("parrilla"),
                            Garage = reader.GetBoolean("garage"),
                            Latitud = reader.GetDouble("latitud"),
                            Longitud = reader.GetDouble("longitud"),
                            Precio = reader.GetDouble("precio")
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al obtener inmueble: " + ex.Message);
            }

            return inmueble;
        }


        // Agregar nuevo inmueble
        // Agregar nuevo inmueble
        public void AgregarInmueble(Inmueble inmueble)
        {
            using (MySqlConnection conexion = new MySqlConnection(_connectionString))
            {
                string query = @"INSERT INTO inmueble 
        (dniPropietario, calle, nro, piso, dpto, localidad, provincia, uso, tipo, ambientes, pileta, parrilla, garage, latitud, longitud, precio) 
        VALUES 
        (@dni, @calle, @nro, @piso, @dpto, @localidad, @provincia, @uso, @tipo, @ambientes, @pileta, @parrilla, @garage, @latitud, @longitud, @precio)";

                MySqlCommand cmd = new MySqlCommand(query, conexion);

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

                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }


        // Actualizar inmueble existente
        public void ActualizarInmueble(Inmueble inmueble)
        {
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
            precio = @precio
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

                conexion.Open();
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


    }
}
