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
            var sql = @"INSERT INTO inmuebles(Tipo,Uso,CantidadDeAmbientes,Longitud,Latitud,Precio,PropietarioId) 
            VALUES(@Tipo,@Uso,@CantidadDeAmbientes,@Longitud,@Latitud,@Precio,@PropietarioId);
            SELECT LAST_INSERT_ID()";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Tipo", inmueble.Tipo);
                cmd.Parameters.AddWithValue("@Uso", inmueble.Uso);
                cmd.Parameters.AddWithValue("@CantidadDeAmbientes", inmueble.CantidadDeAmbientes);
                cmd.Parameters.AddWithValue("@Longitud", inmueble.Longitud);
                cmd.Parameters.AddWithValue("@Latitud", inmueble.Latitud);
                cmd.Parameters.AddWithValue("@Precio", inmueble.Precio);
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
            var sql = "UPDATE inmuebles SET Tipo=@Tipo, Uso=@Uso, CantidadDeAmbientes=@CantidadDeAmbientes, Longitud=@Longitud, Latitud=@Latitud, Precio=@Precio, PropietarioId=@PropietarioId WHERE Id=@Id";
            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@Id", i.Id);
                cmd.Parameters.AddWithValue("@Dni", i.Tipo);
                cmd.Parameters.AddWithValue("@Nombre", i.Uso);
                cmd.Parameters.AddWithValue("@Direccion", i.CantidadDeAmbientes);
                cmd.Parameters.AddWithValue("@Email", i.Longitud);
                cmd.Parameters.AddWithValue("@Telefono", i.Latitud);
                cmd.Parameters.AddWithValue("@Telefono", i.Precio);
                cmd.Parameters.AddWithValue("@Telefono", i.PropietarioId);
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
            var sql = @"SELECT Id, Tipo, Uso, CantidadDeAmbientes, Longitud, Latitud, Precio, PropietarioId, p.Nombre, p.Apellido 
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
                            CantidadDeAmbientes = reader.GetInt32("CantidadDeAmbientes"),
                            Longitud = reader.GetDecimal("Longitud"),
                            Latitud = reader.GetDecimal("Latitud"),
                            Precio = reader.GetDecimal("Precio"),
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

    public List<Inmueble> ObtenerInmueblesPorPropietario(int id)
    {
        List<Inmueble> inmuebles = new List<Inmueble>();
        using (MySqlConnection conn = new MySqlConnection(connectionString))
        {
            var sql = @"SELECT {nameof(Inmueble.Id)}, Tipo, Uso, CantidadDeAmbientes, Longitud, Latitud, Precio, PropietarioId, p.Nombre, p.Apellido  
        FROM inmuebles i JOIN Propietarios p ON i.PropietarioId = id 
        WHERE PropietarioId = @Id";

            using (MySqlCommand cmd = new MySqlCommand(sql, conn))
            {

                //cmd.Parameters.Add("@Id", MySqlDbType.Int).Value = Id;  //VERRR
                cmd.CommandType = CommandType.Text;  //VERRR
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
                            CantidadDeAmbientes = reader.GetInt32("CantidadDeAmbientes"),
                            Longitud = reader.GetDecimal("Longitud"),
                            Latitud = reader.GetDecimal("Latitud"),
                            Precio = reader.GetDecimal("Precio"),
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