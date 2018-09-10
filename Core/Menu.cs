using System;
using System.Collections.Generic;
using Core.Controllers;
using Core.Models;

namespace Core
{
    public class Menu
    {
        public static void MenuCrearCotizacion(ISistema sistema)
        {
        }

        public static void MenuBorrarCotizacion(ISistema sistema)
        {
        }

        public static void MenuCambiarEstadoCotizacion(ISistema sistema)
        {
        }

        public static void MenuEditarCotizacion(ISistema sistema)
        {
        }

        public static void MenuVerCotizacionAdmin(ISistema sistema)
        {
        }
        
        public static void MenuVerCotizacionProductor(ISistema sistema)
        {
        }
        
        public static void MenuVerCotizacionJefe(ISistema sistema)
        {
        }
        
        public static Servicio MenuNuevoServicio()
        {
            Console.WriteLine("Ingrese comentario acerca del servicio: ");
            string comentario = Console.ReadLine();
            
            Console.WriteLine("Ingrese costo del servicio: ");
            int costo = int.Parse(Console.ReadLine());
            
            Console.WriteLine("Ingrese la cantidad de este item: ");
            int cant = int.Parse(Console.ReadLine());
            
            return new Servicio()
            {
                Comentario = comentario,
                Cantidad = cant,
                Valor = costo,
            };
        }

        /*public static Cliente MenuNuevoCliente(ISistema sistema)
        {
            Console.WriteLine("Ingrese RUT del cliente: ");
            string rut = Console.ReadLine();

            try
            {
                Persona ClientePrevio = sistema.BuscarCliente(rut);
                Console.WriteLine("Ingrese RUT del cliente: ");
                return ClientePrevio;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }*/
        
        public static void InterfazAdmin(ISistema s, Usuario u)
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
                Console.WriteLine("#######   PARA SALIR MARQUE '0'  #######");

                a = Console.ReadLine();
                switch (a)
                {
                        case "1":
                            
                            break;    
                            
                } 
            }
        }
       
        public static void InterfazProductor(ISistema s, Usuario u)
        {
        }
        
        public static void InterfazJefe(ISistema s, Usuario u)
        {
        }
    }
    
}