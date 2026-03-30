using Microsoft.Data.SqlClient; //Conexion sql
using Microsoft.Extensions.Configuration;


namespace Inventario.Data
{
    internal class ConexionDB
    {   
        private readonly string connectionString;

        //Constructor
        public ConexionDB() 
        {
            var config = new ConfigurationBuilder()
             .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
             .AddJsonFile("appsettings.json")
             .Build();

            connectionString = config.GetConnectionString("InventarioDB");
        }

        public SqlConnection ConexionSql()
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("String de conexion mal configurado");
            }
            return new SqlConnection(connectionString);
        }
    }
}
