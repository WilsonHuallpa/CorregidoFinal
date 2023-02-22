using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Collections;

namespace Entidades
{
    public class ADO
    {
        public delegate void ColorExistenteDelegado(object sender, EventArgs e);
        public event ColorExistenteDelegado ColorExistente;

        static string conexion;
        static SqlConnection connection;
        static SqlCommand command;

        static ADO()
        {
            conexion = @"Server=.;Database=concesionaria;Trusted_Connection=True;";
            command = new SqlCommand();
            connection = new SqlConnection(conexion);
            command.Connection = connection;
            command.CommandType = System.Data.CommandType.Text;
        }

        public bool Agregar(Auto a)
        {
            bool todoOk = false;
            try
            {
                connection.Open();

                string query = "INSERT INTO autos (marca, modelo, kms, color, patente) VALUES (@marca, @modelo, @kms, @color, @patente)";

                command.CommandText = query;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@marca", a.Marca);
                command.Parameters.AddWithValue("@modelo", a.Modelo);
                command.Parameters.AddWithValue("@kms", a.Kms);
                command.Parameters.AddWithValue("@color", a.Color);
                command.Parameters.AddWithValue("@patente", a.Patente);

                command.ExecuteNonQuery();
                this.ColorExistente(ObtenerTodos(a.Color), EventArgs.Empty);
                todoOk = true;

                /*if(ExistePatente(a.Patente, out a))
                {
                    throw new PatenteExistenteException();
                }
                else
                {
                    command.ExecuteNonQuery();
                    this.ColorExistente(ObtenerTodos(a.Color), EventArgs.Empty);
                    todoOk = true;
                }*/
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return todoOk;
        }

        public bool Modificar(Auto a)
        {
            bool todoOk = false;
            try
            {
                string query = "UPDATE autos SET marca = @marca, modelo = @modelo, kms = @kms, color = @color WHERE patente = @patente";

                connection.Open();

                command.CommandText = query;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@marca", a.Marca);
                command.Parameters.AddWithValue("@modelo", a.Modelo);
                command.Parameters.AddWithValue("@kms", a.Kms);
                command.Parameters.AddWithValue("@color", a.Color);
                command.Parameters.AddWithValue("@patente", a.Patente);

                command.ExecuteNonQuery();
                todoOk = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return todoOk;
        }

        public bool Eliminar(Auto a)
        {
            bool todoOk = false;
            try
            {
                string query = "DELETE FROM autos WHERE patente = @patente";

                connection.Open();

                command.CommandText = query;

                command.Parameters.Clear();
                command.Parameters.AddWithValue("@patente", a.Patente);

                command.ExecuteNonQuery();
                todoOk = true;
            }
            catch (Exception)
            {
                throw new Exception();
            }
            finally
            {
                if (connection != null && connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return todoOk;
        }

        public static List<Auto> ObtenerTodos()
        {
            List<Auto> lista = new List<Auto>();

            try
            {
                ADO.command = new SqlCommand();
                conexion = @"Server=.;Database=concesionaria;Trusted_Connection=True;";
                ADO.connection = new SqlConnection(conexion);

                ADO.command.CommandType = CommandType.Text;
                ADO.command.CommandText = "SELECT * FROM autos";
                ADO.command.Connection = ADO.connection;

                ADO.connection.Open();

                SqlDataReader lector = ADO.command.ExecuteReader();

                while (lector.Read())
                {
                    Auto item = new Auto(lector[3].ToString(), (int)lector["kms"], lector[0].ToString(), lector[1].ToString(), lector[4].ToString());

                    lista.Add(item);
                }

                lector.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (ADO.connection.State == ConnectionState.Open)
                {
                    ADO.connection.Close();
                }
            }

            return lista;
        }

        public static List<Auto> ObtenerTodos(string colorAuto)
        {
            List<Auto> lista = new List<Auto>();

            try
            {
                ADO.command = new SqlCommand();

                ADO.command.CommandType = CommandType.Text;
                ADO.command.CommandText = "SELECT * FROM autos WHERE color = @color";
                ADO.command.Connection = ADO.connection;

                ADO.connection.Open();

                ADO.command.Parameters.AddWithValue("@color", colorAuto);
                SqlDataReader lector = ADO.command.ExecuteReader();

                while (lector.Read())
                {
                    Auto item = new Auto(lector[3].ToString(), (int)lector["kms"], lector[0].ToString(), lector[1].ToString(), lector[4].ToString());

                    lista.Add(item);
                }

                lector.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (ADO.connection.State == ConnectionState.Open)
                {
                    ADO.connection.Close();
                }
            }

            return lista;
        }

        public bool ExistePatente(string patenteAuto, out Entidades.Auto car)
        {
            bool existe = false;
            car = null;

            try
            {
                ADO.command = new SqlCommand();

                ADO.command.CommandType = CommandType.Text;
                ADO.command.CommandText = "SELECT * FROM autos WHERE patente = @patente";
                ADO.command.Connection = ADO.connection;

                ADO.connection.Open();

                ADO.command.Parameters.AddWithValue("@patente", patenteAuto);
                SqlDataReader lector = ADO.command.ExecuteReader();

                while (lector.Read())
                {
                    car = new Auto(lector[3].ToString(), (int)lector["kms"], lector[0].ToString(), lector[1].ToString(), lector[4].ToString());
                    
                    existe = true;
                }

                lector.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            finally
            {
                if (ADO.connection.State == ConnectionState.Open)
                {
                    ADO.connection.Close();
                }
            }

            return existe;
        }
    }
}
