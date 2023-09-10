using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace inmobiliariaBaigorria.Models;

public class RepositorioInmueble
{
    protected readonly string connectionString;
    public RepositorioInmueble()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=inmobiliariabaigorria;";
    }

    public int Alta(Inmueble inmueble)
    {
        var res = -1;
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"INSERT INTO inmuebles(Tipo,Uso,Ambientes,Longitud,Latitud,Precio,Estado,PropietarioId) 
            VALUES(@Tipo,@Uso,@Ambientes,@Longitud,@Latitud,@Precio,@Estado,@PropietarioId);
            SELECT LAST_INSERT_ID()";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Tipo", inmueble.Tipo);
                cmd.Parameters.AddWithValue("@Uso", inmueble.Uso);
                cmd.Parameters.AddWithValue("@Ambientes", inmueble.Ambientes);
                cmd.Parameters.AddWithValue("@Longitud", inmueble.Longitud.HasValue ? inmueble.Longitud.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Latitud", inmueble.Latitud.HasValue ? inmueble.Latitud.Value : (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
                cmd.Parameters.AddWithValue("@Estado", inmueble.Estado);
                cmd.Parameters.AddWithValue("@PropietarioId", inmueble.PropietarioId);
                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                inmueble.Id = res;
                conn.Close();
            }
        }
        return res;
    }

    public void EliminarInmueble(int id)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "DELETE FROM inmuebles WHERE Id=@Id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", id);
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery(); // Obtiene la cantidad de filas afectadas por la eliminación
                conn.Close();

                if (rowsAffected == 0)
                {
                    throw new InvalidOperationException($"No se encontró ningún Inmueble con el ID {id} para eliminar.");
                }
            }
        }
    }

    public void ModificarInmueble(Inmueble i)
    {
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = "UPDATE inmuebles SET Tipo=@Tipo, Uso=@Uso, Ambientes=@Ambientes, Longitud=@Longitud, Latitud=@Latitud, Precio=@Precio, Estado=@Estado, PropietarioId=@PropietarioId WHERE Id=@Id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", i.Id);
                cmd.Parameters.AddWithValue("@Tipo", i.Tipo);
                cmd.Parameters.AddWithValue("@Uso", i.Uso);
                cmd.Parameters.AddWithValue("@Ambientes", i.Ambientes);
                cmd.Parameters.AddWithValue("@Longitud", i.Longitud);
                cmd.Parameters.AddWithValue("@Latitud", i.Latitud);
                cmd.Parameters.AddWithValue("@Precio", i.Precio);
                cmd.Parameters.AddWithValue("@Estado", i.Estado);
                cmd.Parameters.AddWithValue("@PropietarioId", i.PropietarioId);
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }
    }

    public List<Inmueble> ObtenerTodosLosInmuebles()
    {
        List<Inmueble> inmuebles = new List<Inmueble>();

        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT i.Id, Tipo, Uso, Ambientes, Longitud, Latitud, Precio, Estado, p.Id as PropietarioId, p.Nombre as PropietarioNombre, p.Apellido as PropietarioApellido
        FROM inmuebles i INNER JOIN propietarios p ON i.PropietarioId = p.Id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inmueble inmueble = new Inmueble
                        {
                            Id = reader.GetInt32("Id"),
                            Tipo = reader.GetString("Tipo"),
                            Uso = reader.GetString("Uso"),
                            Ambientes = reader.GetInt32("Ambientes"),
                            Longitud = reader.GetDecimal("Longitud"),
                            Latitud = reader.GetDecimal("Latitud"),
                            Precio = reader.GetDecimal("Precio"),
                            Estado = reader.GetBoolean("Estado"),
                            //PropietarioId = reader.GetInt32("PropietarioId"),
                            Duenio = new Propietario
                            {
                                Id = reader.GetInt32("PropietarioId"),
                                Nombre = reader.GetString("PropietarioNombre"),
                                Apellido = reader.GetString("PropietarioApellido"),
                            }
                        };
                        inmuebles.Add(inmueble);
                    }
                }
                conn.Close();
            }
        }

        return inmuebles;
    }

    public List<Inmueble> ObtenerInmueblesPorPropietario(int id)
    {
        List<Inmueble> inmuebles = new List<Inmueble>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT  i.Id as InmuebleId, Tipo, Uso, Ambientes, Longitud, Latitud, Precio, Estado, PropietarioId, p.Nombre, p.Apellido  
        FROM inmuebles i JOIN Propietarios p ON i.PropietarioId = p.Id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {


                cmd.CommandType = CommandType.Text;
                conn.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Inmueble inmueble = new Inmueble
                        {
                            Id = reader.GetInt32("InmuebleId"),
                            Tipo = reader.GetString("Tipo"),
                            Uso = reader.GetString("Uso"),
                            Ambientes = reader.GetInt32("Ambientes"),
                            Longitud = reader.GetDecimal("Longitud"),
                            Latitud = reader.GetDecimal("Latitud"),
                            Precio = reader.GetDecimal("Precio"),
                            Estado = reader.GetBoolean("Estado"),
                            PropietarioId = reader.GetInt32("PropietarioId"),
                            Duenio = new Propietario
                            {
                                Id = reader.GetInt32("PropietarioId"),
                                Nombre = reader.GetString("Nombre"),
                                Apellido = reader.GetString("Apellido"),
                            }
                        };
                        inmuebles.Add(inmueble);
                    }
                }
                conn.Close();
            }
        }

        return inmuebles;
    }


}