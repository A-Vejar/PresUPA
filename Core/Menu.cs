using System;
using System.Collections.Generic;
using Core.Controllers;
using Core.Models;


namespace Core
{
    public class Menu
    {
        
        // -----------------------------------------------------------------------------
        //    >> COTIZACION <<
        // -----------------------------------------------------------------------------
        
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

        /// <summary>
        /// Despliegue en consola para editar una cotizacion especifica
        /// </summary>
        /// <param name="sistema"></param>
        public static void MenuEditarCotizacion(ISistema sistema)
        {
            Cotizacion cotizacionInicial;
            Cotizacion cotizacionSecundaria;
            Console.WriteLine("Ingrese codigo de la cotizacion");
            string codigoCotiz = Console.ReadLine();

            if (String.IsNullOrEmpty(codigoCotiz))
            {
                throw new ModelException("Valor ingresado vacio");
                return;
            }

            try
            {
                cotizacionInicial = sistema.BuscarCotizacion(codigoCotiz);
            }
            catch (ModelException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            cotizacionSecundaria = new Cotizacion()
            {
                Numero = cotizacionInicial.Numero,
                Codigo = cotizacionInicial.Codigo,
                Nombre = cotizacionInicial.Nombre,
                Descripcion = cotizacionInicial.Descripcion,
                Servicios = cotizacionInicial.Servicios,
                ValorFinal = cotizacionInicial.ValorFinal,
                Cliente = cotizacionInicial.Cliente
            };
            
            string a="...";
            while (!a.Equals("0"))
            {
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("##############  EDITAR COTIZACION   ##################");
                Console.WriteLine("--------------------------------------------------------------------");
                
                Console.WriteLine("1.Nombre");
                Console.WriteLine("2.Descripcion");
                Console.WriteLine("3.Servicios");
                Console.WriteLine("4.Cliente");
                Console.WriteLine("0.SALIR");

                a = Console.ReadLine();
                switch (a)
                {
                    case "1":
                    {
                        Console.WriteLine("Ingrese nombre de la cotizacion");
                        cotizacionSecundaria.Nombre = Console.ReadLine();
                        break;
                    }
                    case "2":
                    {
                        Console.WriteLine("Ingrese descripcion de la cotizacion");
                        cotizacionSecundaria.Descripcion = Console.ReadLine();
                        break;
                    }
                    case "3":
                    {
                        int cont = 1;
                        Console.WriteLine("Servicios ...");
                        foreach (Servicio s in cotizacionSecundaria.Servicios)
                        {
                            Console.WriteLine(cont + "." + s.ToString());
                            cont++;
                        }
                        
                        string b = "...";
                        while (!b.Equals("0"))
                        {
                            Console.WriteLine("--------------------------------------------------------------------");
                            Console.WriteLine("############## EDITAR SERVICIO ##################");
                            Console.WriteLine("--------------------------------------------------------------------");

                            Console.WriteLine("Seleccione el numero del servicio que desea editar");
                            Console.WriteLine("0.Salir");

                            b = Console.ReadLine();
                            
                            int num;
                            try
                            {
                                num = Int32.Parse(b);

                            }
                            catch (FormatException e)
                            {
                                Console.WriteLine(e.Message);
                                continue;
                            }

                            if (num >= 1 && num <= cotizacionSecundaria.Servicios.Count)
                            {
                                Console.WriteLine("--------------------------------------------------------------------");
                                Console.WriteLine("############## EDITAR SERVICIO ##################");
                                Console.WriteLine("--------------------------------------------------------------------");
                                
                                Console.WriteLine("1.Cambiar");
                                Console.WriteLine("2.Eliminar");
                                Console.Write("0.Salir");

                                b = Console.ReadLine();
                                
                                switch (b)
                                {
                                    case "1":
                                    {
                                        cotizacionSecundaria.Servicios[num] = MenuNuevoServicio();
                                        Console.WriteLine("Done.");
                                        break;
                                    }
                                    case "2":
                                    {
                                        cotizacionSecundaria.Servicios.RemoveAt(num);
                                        Console.WriteLine("Done.");
                                        break;
                                    }
                                    case "0":
                                        MenuEditarCotizacion(sistema);
                                        break;
                                }
                            }
                        }
                        break;
                    }
                    
                    case "4":
                    {
                        Console.WriteLine("Ingrese datos del cliente");
                        Cliente clienteSecundario = MenuNuevoCliente(sistema);
                        if (clienteSecundario == null)
                        {
                            throw new ModelException("Error al ingresar datos de cliente");
                            return;
                        }
                        else
                        {
                            cotizacionSecundaria.Cliente = clienteSecundario;
                            Console.WriteLine("Done.");
                        }
                        break;
                    }
                    case "0":
                        break;
                    default:
                        continue;
                }
            }

        }
        
        /// <summary>
        /// Metodo que despliega una busqueda de cotizacion en el sistema
        /// </summary>
        /// <param name="sistema"></param>
        public static void MenuBuscarCotizacion(ISistema sistema)
        {
            Console.WriteLine("Ingrese informacion de busqueda");
            string busqueda = Console.ReadLine();

            try
            {
                IList<Cotizacion> despliegue = sistema.BusquedaCotizaciones(busqueda);
                if (despliegue.Count ==0)
                {
                    Console.WriteLine("Sin resultados.");
                }
                
                foreach (Cotizacion enlistada in despliegue)
                {
                    Console.WriteLine(enlistada.ToString());
                } 
                
            }
            catch (ModelException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        
        /// <summary>
        /// Metodo por el cual se envia una cotizacion por correo en el sistema
        /// </summary>
        /// <param name="sistema"></param>
        public static void MenuEnviarCotizacion(ISistema sistema, Usuario usuario)
        {
            Cotizacion cotizacion;
            Console.WriteLine("Ingrese codigo de la cotizacion");
            string codigo = Console.ReadLine();

            if (codigo == null)
            {
                throw new ModelException("Error en ingreso de codigo");
            }

            try
            {
                cotizacion = sistema.BuscarCotizacion(codigo);
            }
            catch (ModelException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            string emailFrom = "";
            string emailTo = "";
            string msj = "";
            string password = "";
            string msjServ = "";
            
            Console.WriteLine("Email: " + emailFrom);
            
            Console.WriteLine("Desea cambiar el email ?");
            Console.WriteLine("1.SI");
            Console.WriteLine("2.NO");
            
            string b = Console.ReadLine();
            while (!b.Equals("2"))
            {
                switch (b)
                {
                    case "1":
                    {
                        Console.WriteLine("Email ");
                        emailFrom = Console.ReadLine();
                        break;
                    }
                    case "2":
                    {
                        emailFrom = usuario.Persona.Email;
                        break;
                    }
                }
            }
            
            Console.WriteLine("Email: " + emailFrom);
            Console.WriteLine("Password ");
            password = Console.ReadLine();
            

            //Seleccion del remitente:
            string remitente = null;
            string input = "...";

            emailTo = cotizacion.Cliente.Persona.Email;
            
            //Servicios de la cotizacion
            string servicios = null;
            foreach (Servicio servicio in cotizacion.Servicios)
            {
                msjServ = servicios;
            }

            //Creacion del Email:
            msj = "Numero Cotizacion: " + cotizacion.Numero +
                  "Codigo: " + cotizacion.Codigo +
                  "Titulo/Nombre: " + cotizacion.Nombre +
                  "Descripcion: " + cotizacion.Descripcion +
                  "Servicios: " + msjServ +
                  "Valor Final: " + cotizacion.ValorFinalCotizacion();
            Console.WriteLine();

            try
            {
                sistema.EnviarCotizacion(codigo, emailTo, emailFrom, msj, password);
                Console.WriteLine("Done.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
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
        
        // -----------------------------------------------------------------------------
        //    >> SERVICIO <<
        // -----------------------------------------------------------------------------
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
                Valor = costo
            };
        }
        
        // -----------------------------------------------------------------------------
        //    >> CLIENTE <<
        // -----------------------------------------------------------------------------
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
        
        // -----------------------------------------------------------------------------
        //    >> LOGIN ADMINISTRADOR <<
        // -----------------------------------------------------------------------------
        public static void InterfazAdmin(ISistema sistema, Usuario u)
        {
            string a="...";
            while (!a.Equals("0"))
            {
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("##############  ADMINISTRACION DE COTIZACIONES   ##################");
                Console.WriteLine("--------------------------------------------------------------------");
                
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
                        MenuEditarCotizacion(sistema);
                        break;
                    }
                    case "5":
                        MenuCambiarEstadoCotizacion(sistema);
                        break;
                    
                    case "6":
                    {
                        MenuVerCotizacion(sistema);
                        break;
                    }
                    case "7":
                    {
                        MenuEnviarCotizacion(sistema, u);
                        break;
                    }
                    case "0":
                        return;
                    default:
                        continue;         
                } 
            }
        }
       
        // -----------------------------------------------------------------------------
        //    >> LOGIN PRODUCTOR <<
        // -----------------------------------------------------------------------------
        public static void InterfazProductor(ISistema sistema, Usuario u)
        {
            string a = "...";
            while (a != null && !a.Equals("0"))
            {

                Console.WriteLine("1. Editar estado de cotizacion");
                Console.WriteLine("2. Ver cotizaciones disponibles");
                Console.WriteLine("0. SALIR");

                a = Console.ReadLine();
                
                switch (a)
                {
                    case null:
                        continue;
                    case "1":
                        MenuCambiarEstadoCotizacion(sistema);
                        break;
                    case "2":
                        MenuVerCotizacion(sistema);
                        break;
                    default:
                        continue;
                }
            } 
        }
        
        // -----------------------------------------------------------------------------
        //    >> LOGIN JEFE <<
        // -----------------------------------------------------------------------------
        public static void InterfazJefe(ISistema sistema, Usuario u)
        {
            string a = "...";
            while (a != null && !a.Equals("0"))
            {
                
                Console.WriteLine("1. Ver Cotizaciones disponibles");
                Console.WriteLine("0. SALIR");

                a = Console.ReadLine();

                switch (a)
                {
                    case null:
                        continue;
                    case "1":
                        MenuVerCotizacion(sistema);
                        break;
                    default:
                        continue;
                }
            }
        }
    }
    
}