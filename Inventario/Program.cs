using Producto;
using System;
using Inventario.Vistas;
using Inventario.Entidades;
using System.Collections.Generic;

namespace Inventario
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int opcion = 0;
            //Instancia de la clase MenuGeneral para mostrar el menú y obtener opciones del usuario
            Vistas.MenuGeneral MENU = new Vistas.MenuGeneral();
            //Instancia de la clase Inventario para gestionar los productos en el inventario
            Entidades.Inventario inventario = new Entidades.Inventario();

            List<Producto.Producto> listaInventario = new List<Producto.Producto>();

            while (opcion != 99) 
            {
                MENU.MostrarMenu();
                opcion = MENU.ObtenerOpcion();

                if (opcion == 1)
                {
                    var datosProdcutos = MENU.IngresarInventario();
                    inventario.AgregarProducto(datosProdcutos.Nombre, datosProdcutos.Precio);
                }
                else if (opcion == 2)
                {
                    inventario.OptenerProductos();

                }
                else if (opcion == 3)
                {
                    inventario.OptenerProducto(MENU.OptenerProducto());
                }
                else if(opcion == 4)
                {
                    inventario.ActualizarProducto(MENU.ActualizarProducto());
                }
                else if (opcion == 5)
                {
                    inventario.EliminarProducto(MENU.OptenerProducto());
                }
                else if (opcion == 99)
                {
                    Console.WriteLine("Saliendo del programa...");
                }
                else
                {
                    Console.WriteLine("Ingrese una opción válida");
                }
            }
            Console.WriteLine("========== Fin del programa ===============");
        }
     }
}