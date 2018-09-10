using System;
using System.Collections.Generic;
using Core.Controllers;
using Core.Models;

namespace Core
{
    public class Menu
    {
        public static void InterfazAdmin(Sistema s, Usuario u)
        {
            string a="...";
            while (!a.Equals("0"))
            {
                Console.WriteLine("##############  ADMINISTRACION DE COTIZACIONES   ##################");
                Console.WriteLine("1.Agregar");
                Console.WriteLine("2.Borrar");
                Console.WriteLine("3.Buscar");
                Console.WriteLine("4.Editar");
                Console.WriteLine("5.Actualizar estado");
                Console.WriteLine("6.Menu de Cotizaciones Disponibles");
                Console.WriteLine("#####   PARA SALIR, MARQUE '0'  #######");

                a = Console.ReadLine();
                
            }
        }
        
    }
}