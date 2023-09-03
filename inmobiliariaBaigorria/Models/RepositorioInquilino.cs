using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaBaigorria.Models;

public class RepositorioInquilino
{
    protected readonly string connectionString;
    public RepositorioInquilino()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliariabaigorria;";
    }

    public int Alta(Inquilino inquilino)
    {
        var res = -1;
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"INSERT INTO inquilinos(Dni,NombreCompleto,Direccion,Email,Telefono) 
            VALUES(@Dni,@NombreCompleto,@Direccion,@Email,@Telefono);
            SELECT LAST_INSERT_ID()";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Dni", inquilino.Dni);
                cmd.Parameters.AddWithValue("@NombreCompleto", inquilino.NombreCompleto);
                cmd.Parameters.AddWithValue("@Direccion", inquilino.Direccion);
                cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(inquilino.Email) ? "" : inquilino.Email);
                cmd.Parameters.AddWithValue("@Telefono", string.IsNullOrEmpty(inquilino.Telefono) ? "" : inquilino.Telefono);
                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                inquilino.Id = res;
                conn.Close();
            }
        }
        return res;
    }

    public void EliminarInquilino(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "DELETE FROM inquilinos WHERE Id=@Id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Obtiene la cantidad de filas afectadas por la eliminación
                conn.Close();

                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException($"No se encontró ningún Inquilino con el Id {id} para eliminar.");
                }
            }
        }
    }

    public void ModificarInquilino(Inquilino i)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "UPDATE inquilinos SET Dni=@Dni, NombreCompleto=@NombreCompleto, Direccion=@Direccion, Email=@Email, Telefono=@Telefono WHERE Id=@Id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", i.Id);
                cmd.Parameters.AddWithValue("@Dni", i.Dni);
                cmd.Parameters.AddWithValue("@NombreCompleto", i.NombreCompleto);
                cmd.Parameters.AddWithValue("@Direccion", i.Direccion);
                cmd.Parameters.AddWithValue("@Email", i.Email);
                cmd.Parameters.AddWithValue("@Telefono", i.Telefono);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    public List<Inquilino> ObtenerInquilinos()
    {
        var res = new List<Inquilino>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id,Dni,NombreCompleto,Direccion,Email,Telefono FROM inquilinos";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(new Inquilino
                        {
                            Id = reader.GetInt32("Id"),
                            Dni = reader.GetString("Dni"),
                            NombreCompleto = reader.GetString("NombreCompleto"),
                            Direccion = reader.GetString("Direccion"),
                            Email = reader.IsDBNull(reader.GetOrdinal("Email")) ? null : reader.GetString("Email"),
                            Telefono = reader.IsDBNull(reader.GetOrdinal("Telefono")) ? null : reader.GetString("Telefono"),
                        });
                    }
                }
                conn.Close();
            }
        }
        return res;
    }

    public Inquilino ObtenerPorId(int id)
    {
        Inquilino res = new Inquilino();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "SELECT Id,Dni,NombreCompleto,Direccion,Email,Telefono FROM inquilinos WHERE Id=@Id";
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
                        res.NombreCompleto = reader.GetString("NombreCompleto");
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
            throw new Exception("No se encontró ningún Inquilino con el ID especificado.");
        }

        return res;
    }
}