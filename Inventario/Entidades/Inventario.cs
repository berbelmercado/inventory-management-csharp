using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Producto;
using Inventario.Data;
using Microsoft.Data.SqlClient;

namespace Inventario.Entidades
{
    internal class Inventario
    {
        //Método para agregar un producto al inventario
        public void AgregarProducto(String nombre, decimal precio)
        { 
            //Objeto de coneción db
            ConexionDB objConn = new ConexionDB();

            //Query de isnersión
             string query = "INSERT INTO Productos (Nombre,Precio) VALUES  (@Nombre, @Precio)";

            //Bloque de gestión de recursos
            using (SqlConnection conn = objConn.ConexionSql())
            {   
                //Bloque de gestió de recursos
                using (SqlCommand comando = new SqlCommand(query,conn))
                {
                    try
                    {

                        comando.Parameters.AddWithValue("@Nombre", nombre);
                        comando.Parameters.AddWithValue("@Precio", precio);

                        conn.Open();
                        comando.ExecuteNonQuery();

                        Console.WriteLine("Se inserta correctamente la información");
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error al insertar nuevo producto {ex}");
                    }
                }
            }
        }

        public void OptenerProductos()
        {
            //Obj conexion
            ConexionDB objConexion = new ConexionDB();

             string query = "SELECT Id, Nombre, Precio FROM Productos";

            using (SqlConnection conexion = objConexion.ConexionSql())
            {
                using (SqlCommand comando = new SqlCommand(query,conexion))
                {
                    try
                    {
                        conexion.Open();

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (!lector.HasRows)
                            {
                                Console.WriteLine("No hay datos en el inventario");
                            }
                            else 
                            {
                                while (lector.Read())
                                {
                                    Console.WriteLine(
                                                      $"Id: {lector["Id"]} " +
                                                      $"Nombre: {lector["Nombre"]} " +
                                                      $"Precio: {lector["Precio"]}\n"
                                                      );
                                }
                            }
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error al consultar inventario {ex}");
                    }
                }
            }
        }

        public Producto.Producto OptenerProducto(int productoId)
        {

            ConexionDB objConexion = new ConexionDB();

            string query = $"SELECT Id, Nombre, Precio FROM Productos WHERE Id=@Id";

            using (SqlConnection conexion = objConexion.ConexionSql())
            {
                using (SqlCommand comando= new SqlCommand(query,conexion))
                {
                    //
                    comando.Parameters.AddWithValue("@Id",productoId);

                    try
                    {
                        conexion.Open();//Abre la conexión a la base de datos

                        using (SqlDataReader lector = comando.ExecuteReader())
                        {
                            if (!lector.HasRows)//Verifica si el lector tiene filas, lo que indica que se encontraron resultados
                            {
                                Console.WriteLine("No se encontró producto en el inventario");
                                return null;
                            }
                            else
                            {
                                lector.Read();//Lee la siguiente fila del resultado de la consulta
                                Console.WriteLine($"Id: {lector["Id"]}"+
                                                  $"Nombre: {lector["Nombre"]}"+
                                                  $"Precio: {lector["Precio"]}\n"
                                                  );

                                return new Producto.Producto { Id = (int)lector["Id"],
                                                               Nombre = (string)lector["Nombre"],
                                                               Precio = (decimal)lector["Precio"]
                                                             };
                            }
                        }
                    } 
                    catch (SqlException ex) 
                    {
                        Console.WriteLine($"Error al consultar producto en el inventario: {ex}");
                        return null;
                    }
                }
            }
        }

        public void ActualizarProducto(Producto.Producto objActProducto)
        {   Producto.Producto obj_producto;
            obj_producto = OptenerProducto(objActProducto.Id);
            if (obj_producto != null)
            {
                ConexionDB objConexion = new ConexionDB();
                 string query = "UPDATE Productos SET Nombre=@Nombre, Precio=@Precio WHERE Id=@Id";
                using (SqlConnection conexion = objConexion.ConexionSql())
                {
                    using (SqlCommand comando = new SqlCommand(query, conexion))
                    {
                        comando.Parameters.AddWithValue("@Id", objActProducto.Id);
                        comando.Parameters.AddWithValue("@Nombre", objActProducto.Nombre);
                        comando.Parameters.AddWithValue("@Precio", objActProducto.Precio);
                        try
                        {
                            conexion.Open();
                            int filasAfectadas = comando.ExecuteNonQuery();//Ejecuta la consulta y devuelve el número de filas afectadas
                            if (filasAfectadas == 0)
                            {
                                Console.WriteLine("No se encontró el producto para actualizar");
                                return;
                            }
                            else
                            {
                                Console.WriteLine("-------------Se ha actualizado el producto-----------");

                                obj_producto = OptenerProducto(objActProducto.Id);

                                Console.WriteLine($"Id: {obj_producto.Id} " +
                                                  $"Nombre: {obj_producto.Nombre}" +
                                                  $"Precio: {obj_producto.Precio}\n"
                                                  );
                            }
                        }
                        catch (SqlException ex)
                        {
                            Console.WriteLine($"Error al actualizar el producto: {ex}");
                        }
                    }
                }
            }
        }
        public void EliminarProducto(int productoId)
        {
            ConexionDB objConexion = new ConexionDB();
             string query = "DELETE FROM Productos WHERE Id=@Id";

            using (SqlConnection conexion = objConexion.ConexionSql())
            {
                using (SqlCommand comando = new SqlCommand(query, conexion))
                {
                    try
                    {
                        comando.Parameters.AddWithValue("@Id", productoId);

                        conexion.Open();

                        int filasAfectadas = comando.ExecuteNonQuery();//Ejecuta la consulta y devuelve el número de filas afectadas
                        if (filasAfectadas == 0)
                        {
                            Console.WriteLine("No se encontró el producto para eliminar");
                            return;
                        }
                        else
                        {
                            Console.WriteLine("Se ha eliminado el producto del inventario");
                        }
                    }
                    catch (SqlException ex)
                    {
                        Console.WriteLine($"Error al eliminar el producto: {ex}");
                    }
                }
            }
        }

        
    }
}
