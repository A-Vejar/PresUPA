using System;
using System.Collections.Generic;
using Core.Controllers;
using Core.Models;

namespace Core
{
    public class Menu
    {
        
        /// <summary>
        /// Despliegue en consola para crear cotizacion
        /// </summary>
        /// <param name="sistema"></param>
        /// <exception cref="ModelException"></exception>
        public static void MenuCrearCotizacion(ISistema sistema)
        {
            Console.WriteLine("Ingrese nombre de cotizacion: ");
            string NombreCotizacion = Console.ReadLine();
            
            Console.WriteLine("Asociar a Cliente: ");

            Cliente ClienteCotiz = MenuNuevoCliente(sistema);
            if (ClienteCotiz ==null)
                throw new ModelException("Error ingreso de cliente");
            
            List<Servicio> serviciosCotiz = new List<Servicio>();
            
            Console.WriteLine("Ingrese servicios solicitados: ");
            
            while (true)
            {
                string alt = ".";
                Servicio servicio = MenuNuevoServicio();
                serviciosCotiz.Add(servicio);

                Console.WriteLine("1. Agregar un nuevo servicio");
                Console.WriteLine("[Para terminar pulse otra tecla]");

                alt = Console.ReadLine();

                if (alt != null && alt.Equals("1"))
                {
                    continue;
                }
                break;
            }
            
            Console.WriteLine("Ingrese una descripcion acerca de la cotizacion: ");
            string descripcionCotiz = Console.ReadLine();


            sistema.AgregarCotizacion(new Cotizacion()
            {
                Nombre = NombreCotizacion,
                Descripcion = descripcionCotiz,
                Cliente = ClienteCotiz,
                Servicios = serviciosCotiz});
            
                Console.WriteLine("Cotizacion creada");

        }

        /// <summary>
        /// Despliegue en consola para eliminar cotizacion
        /// </summary>
        /// <param name="sistema"></param>
        public static void MenuBorrarCotizacion(ISistema sistema)
        {
            Console.WriteLine("Ingrese codigo de la cotizacion");
            string codigoCotiz = Console.ReadLine();

            try
            {
                sistema.EliminarCotizacion(codigoCotiz);
                Console.WriteLine("La cotizacion  "+codigoCotiz+"  ha sido eliminada.");
            }
            catch (ModelException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Despliegue en consola para cambiar el estado de una cotizacion
        /// </summary>
        /// <param name="sistema"></param>
        public static void MenuCambiarEstadoCotizacion(ISistema sistema)
        {
            Console.WriteLine("Ingrese codigo de la cotizacion");
            string codigoCotiz = Console.ReadLine();

            EstadoCotizacion anterior;

            try
            {
                anterior = sistema.BuscarCotizacion(codigoCotiz).Estado;
            }
            catch (ModelException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            while (true)
            {
               
                Console.WriteLine("Elija nuevo estado de cotizacion    (Para Cancelar [0])");
                Console.WriteLine("1. PRE_PRODUCCION");
                Console.WriteLine("2. CANCELADA");
                Console.WriteLine("3. EXPORTACION");
                Console.WriteLine("4. MONTAJE");
                Console.WriteLine("5. POST_PRODUCCION");
                Console.WriteLine("6. RODAJE");

                string a = Console.ReadLine();

                EstadoCotizacion actual;

                switch (a)
                {
                  case "1":
                      actual = EstadoCotizacion.PRE_PRODUCCION;
                      break;
                  case "2":
                      actual = EstadoCotizacion.CANCELADA;
                      break;
                  case"3":
                      actual = EstadoCotizacion.EXPORTACION;
                      break;
                  case "4":
                      actual = EstadoCotizacion.MONTAJE;
                      break;
                  case "5":
                      actual = EstadoCotizacion.POST_PRODUCCION;
                      break;
                  case "6":
                      actual = EstadoCotizacion.RODAJE;
                      break;
                      
                  default:
                      continue;
                    
                }
                if (a.Equals("0"))
                    break;
                sistema.SeleccionarEstadoCotizacion(codigoCotiz,actual);
                Console.WriteLine("Estado actualizado");
            }
        }

        
        //TODO: IMPLEMENTAR
        public static void MenuEditarCotizacion(ISistema sistema)
        {
            Console.WriteLine("Ingrese codigo de la cotizacion");
            string codigoCotiz = Console.ReadLine();

            
        }
        
        //TODO: IMPLEMENTAR
        public static void MenuBuscarCotizacion(ISistema sistema)
        {
            Console.WriteLine("Ingrese info");
            string busqueda = Console.ReadLine();

            
        }
        
        //TODO: IMPLEMENTAR
        public static void MenuEnviarCotizacion(ISistema sistema)
        {
            Console.WriteLine("Ingrese codigo de la cotizacion");
            string codigo = Console.ReadLine();

            
        }
        
        /// <summary>
        /// Metodo que despliega las cotizaciones en el sistema
        /// </summary>
        /// <param name="sistema"></param>
        public static void MenuVerCotizacion(ISistema sistema)
        {
            Console.WriteLine("########    Cotizaciones    ########");
            try
            {
                IList<Cotizacion> cotizaciones = sistema.ListarCotizaciones();
                foreach (Cotizacion cotizacion in cotizaciones) 
                    Console.WriteLine(Utils.ToJson(cotizacion));
            }
            catch (NullReferenceException e)
            {
                Console.WriteLine(e.Message);
            } 
            
        }
        
        
        /// <summary>
        /// Metodo que retorna un servicio nuevo
        /// </summary>
        /// <returns></returns>
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
        
        
        /// <summary>
        /// Metodo que retorna un cliente nuevo.
        /// </summary>
        /// <param name="sistema"></param>
        /// <returns></returns>
        public static Cliente MenuNuevoCliente(ISistema sistema)
        {
            Console.WriteLine("Ingrese RUT del cliente: ");
            string rut = Console.ReadLine();

            try
            {
                Cliente ClientePrevio = sistema.BuscarCliente(rut);
                Console.WriteLine("Ingrese RUT del cliente: ");
                return ClientePrevio;
            }
            catch (ModelException)
            {
            }
            Console.WriteLine("Ingrese nombre del cliente: ");
            string nombre = Console.ReadLine();

            Console.WriteLine("Ingrese apellido paterno: ");
            string apellidoP = Console.ReadLine();

            Console.WriteLine("Ingrese apellido materno: ");
            string apellidoM = Console.ReadLine();

            Console.WriteLine("Ingrese mail: ");
            string correo = Console.ReadLine();
            
            Console.WriteLine("Ingrese telefono de contacto: ");
            int fono = Int32.Parse(Console.ReadLine());
            
            Persona personaCreada = new Persona()
            {
                Rut = rut,
                Nombre = nombre,
                Paterno = apellidoP,
                Materno = apellidoM,
                Email = correo
            };
            
            sistema.AgregarCliente(personaCreada,fono);
            Console.WriteLine("Nuevo Cliente creado");
            return sistema.BuscarCliente(personaCreada.Rut);
        }
        
        
        public static void InterfazAdmin(ISistema sistema, Usuario u)
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
                Console.WriteLine("7.Enviar");
                Console.WriteLine("0.SALIR");

                a = Console.ReadLine();
                switch (a)
                {
                    case "1":
                    {
                        MenuCrearCotizacion(sistema);
                        break;
                    }
                    case "2":
                    {
                        MenuBorrarCotizacion(sistema);
                        break;
                    }
                    case "3":
                    {
                        MenuBuscarCotizacion(sistema);
                        break;
                    }
                    case "4":
                    {
                        //TODO
                        MenuEditarCotizacion(sistema);
                        break;
                    }
                    case "5":
                       //TODO
                        MenuCambiarEstadoCotizacion(sistema);
                        break;
                    
                    case "6":
                    {
                        MenuVerCotizacion(sistema);
                        break;
                    }
                    case "7":
                    {
                        //TODO
                        MenuEnviarCotizacion(sistema);
                        break;
                    }
                    case "0":
                        return;
                    default:
                        continue;   
                            
                } 
            }
        }
       
        public static void InterfazProductor(ISistema sistema, Usuario u)
        {
            string input = "...";
            while (input != null && !input.Equals("0"))
            {

                Console.WriteLine("1. Administrar Servicios");
                Console.WriteLine("0. SALIR");

                input = Console.ReadLine();
                
                switch (input)
                {
                    case null:
                        continue;
                    case "1":
                        //TODO
                        break;
                    default:
                        continue;
                }
            } 
        }
        
        public static void InterfazJefe(ISistema ssistema, Usuario u)
        {
            string input = "...";
            while (input != null && !input.Equals("0"))
            {
                
                Console.WriteLine("1. Ver Cotizaciones");
                Console.WriteLine("0. SALIR");

                input = Console.ReadLine();

                switch (input)
                {
                    case null:
                        continue;
                    case "1":
                        //TODO
                        break;
                    default:
                        continue;
                }
            }
        }
    }
    
}