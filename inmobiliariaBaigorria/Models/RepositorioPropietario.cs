using MySql.Data.MySqlClient;

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
                            Dni = reader.GetInt32("Dni"),
                            Nombre = reader.GetString("Nombre"),
                            Apellido = reader.GetString("Apellido"),
                            Direccion = reader.GetString("Direccion"),
                            Email = reader.GetString("Email"),
                            Telefono = reader.GetInt32("Telefono"),
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
                cmd.Parameters.AddWithValue("@Email", propietario.Email);
                cmd.Parameters.AddWithValue("@Telefono", propietario.Telefono);
                conn.Open();
                res = Convert.ToInt32(cmd.ExecuteScalar());
                propietario.Id = res;
                conn.Close();
            }
        }
        return res;
    }

}
