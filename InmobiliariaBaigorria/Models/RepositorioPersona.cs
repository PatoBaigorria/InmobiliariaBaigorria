using MySql.Data.MySqlClient;

namespace test.Models;

public class RepositorioPersona
{
    protected readonly string connectionString;

    public RepositorioPersona()
    {
        connectionString = "Server=localhost;User=root;Password=;Database=test;SslMode=none";
    }
    public List<Persona> ObtenerPersonas()
    {
        var res = new List<Persona>();
        using(MySqlConnection conn = new MySqlConnection(connectionString))
        {
           var sql = "SELECT" 
        }
        return res;
    }
}
