using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Producto;


namespace Inventario.Vistas
{
    internal class MenuGeneral
    {
        private int productoId = 0;
        private string nombreProducto = "";
        private decimal precioProducto = 0;

        public MenuGeneral() { }

        public void MostrarMenu()
        {
            Console.WriteLine("=== Menú General ===");
            Console.WriteLine("1. Agregar Producto a inventario");
            Console.WriteLine("2. Ver Inventario");
            Console.WriteLine("3. Consultar producto por id");
            Console.WriteLine("4. Actualizar producto");
            Console.WriteLine("5. Eliminar producto por id");

            Console.WriteLine("99. Salir");
        }

        public int ObtenerOpcion()
        {
            int opcion;
            bool esValida = false;
            while (!esValida)
            {
                Console.Write("Seleccione una opción: ");

                if (int.TryParse(Console.ReadLine(), out opcion))
                {
                    esValida = true;
                    Console.WriteLine("\n");
                    return opcion;
                }
                else
                {
                    Console.WriteLine("Opción no válida. Por favor, ingrese un número.\n");
                }
            }
            return -1;
        }

        public Producto.Producto IngresarInventario() 
        {
            Console.WriteLine("Ingrese Nombre Producto");
            nombreProducto = Console.ReadLine();

            Console.WriteLine("Ingrese Precio Producto");
            precioProducto = decimal.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            return new Producto.Producto
            {
                Nombre = nombreProducto,
                Precio = precioProducto
            };
        }
        public Producto.Producto ActualizarProducto()
        {
            Console.Write("Ingrese Ide de producto a actualizar: ");
            productoId = int.Parse(Console.ReadLine());

            Console.Write("Ingrese nuevo Nombre de producto: ");
            nombreProducto = Console.ReadLine();

            Console.Write("Ingrese nuevo precio de producto:");
            precioProducto = decimal.Parse(Console.ReadLine());

            return new Producto.Producto 
            {
                Id = productoId,
                Nombre =nombreProducto,
                Precio = precioProducto,
            };
        }
        public int OptenerProducto()
        {
            Console.Write("Ingrese id producto: ");
            productoId = int.Parse(Console.ReadLine());

            return productoId;
        }
    }
}