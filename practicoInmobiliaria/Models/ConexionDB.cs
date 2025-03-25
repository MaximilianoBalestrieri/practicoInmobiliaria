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
    }
}
