using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaBaigorria.Models;

public class RepositorioPropietario
{
    protected readonly string connectionString;
    public RepositorioPropietario()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliariabaigorria;";
    }

    public List<Propietario> ObtenerPropietarios()
    {
        var res = new List<Propietario>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id,Dni,Nombre,Apellido,Direccion,Email,Telefono FROM propietarios";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(new Propietario
                        {
                            Id = reader.GetInt32("Id"),
                            Dni = reader.GetString("Dni"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Direccion = reader.GetString("Direccion"),
                            Email = reader.GetString("Email"),
                            Telefono = reader.GetString("Telefono"),
                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }

    public int Alta(Propietario propietario)
    {
        var res = -1;
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"INSERT INTO propietarios(Dni,Nombre,Apellido,Direccion,Email,Telefono) 
            VALUES(@Dni,@Nombre,@Apellido,@Direccion,@Email,@Telefono);
            SELECT LAST_INSERT_ID()";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Dni", propietario.Dni);
                cmd.Parameters.AddWithValue("@Nombre", propietario.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", propietario.Apellido);
                cmd.Parameters.AddWithValue("@Direccion", propietario.Direccion);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(propietario.Email) ? "" : propietario.Email);
                cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(propietario.Telefono) ? "" : propietario.Telefono);
                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                propietario.Id = res;
                conn.Close();
            }
        }
        return res;
    }
    public Propietario ObtenerPorId(int id)
    {
        Propietario res = new Propietario();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id,Dni,Nombre,Apellido,Direccion,Email,Telefono FROM propietarios WHERE Id=@Id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {


                        res.Id = reader.GetInt32("Id");
                        res.Dni = reader.GetString("Dni");
                        res.Nombre = reader.GetString("Nombre");
                        res.Apellido = reader.GetString("Apellido");
                        res.Direccion = reader.GetString("Direccion");
                        res.Email = reader["Email"] != DBNull.Value ? reader.GetString("Email") : "";
                        res.Telefono = reader["Telefono"] != DBNull.Value ? reader.GetString("Telefono") : "";

                    }

                }
                conn.Close();
            }
        }

        if (res == null)
        {
            throw new Exception("No se encontró ningún Propietario con el ID especificado.");
        }

        return res;
    }

    public void ModificarPropietario(Propietario p)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"UPDATE propietarios SET Dni=@Dni, Nombre=@Nombre, Apellido=@Apellido, Direccion=@Direccion, Email=@Email, Telefono=@Telefono WHERE Id=@Id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", p.Id);
                cmd.Parameters.AddWithValue("@Dni", p.Dni);
                cmd.Parameters.AddWithValue("@Nombre", p.Nombre);
                cmd.Parameters.AddWithValue("@Apellido", p.Apellido);
                cmd.Parameters.AddWithValue("@Direccion", p.Direccion);
                cmd.Parameters.AddWithValue("@Email", p.Email);
                cmd.Parameters.AddWithValue("@Telefono", p.Telefono);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    public void EliminarPropietario(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "DELETE FROM propietarios WHERE Id=@Id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Obtiene la cantidad de filas afectadas por la eliminación
                conn.Close();

                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException($"No se encontró ningún propietario con el ID {id} para eliminar.");
                }
            }
        }
    }



}
