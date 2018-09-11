using System;
using Core.Controllers;
using Core.Models;

namespace Core
{
    /// <summary>
    /// 
    /// </summary>
    public class App
    {
        /// <summary>
        /// Punto de entrada de la aplicacion.
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="ModelException"></exception>
        private static void Main(string[] args)
        {
            Console.WriteLine("INICIANDO . . . .");
            ISistema sistema = Startup.BuildSistema();
            {
                //Prueba de creacion de usuario
                
                Persona persona = new Persona()
                {
                    Rut = "176288043",
                    Nombre = "Fernando",
                    Paterno = "Caimanque",
                    Materno = "Maulen",
                    Email = "fernando.caimanque@alumnos.ucn.cl"
                };
                
                Usuario usuario = new Usuario()
                {
                    Persona = persona,
                    Password = "00000",
                    TipoUsuario = TipoUsuario.ADMINISTRADOR
                };

                Console.WriteLine(persona);
                Console.WriteLine(Utils.ToJson(persona));

                // Save in the repository
                sistema.Agregar(persona);
                
                try
                {
                    sistema.AgregarUsuario(persona,usuario.Password);
                }
                catch (ModelException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
                
                // Ingreso con una maximo de 3 intentos 
                int cont = 3;
                while (cont != 0)
                {
                    Console.WriteLine("LOGIN: ");
                    string credencial = Console.ReadLine();
                
                    Console.WriteLine("PASSWORD: ");
                    string password = Console.ReadLine();

                    if (credencial == "" || password == "")
                    {
                        Console.WriteLine("Error de ingreso");
                        cont--;
                        return;
                    }

                    Usuario user = null;
            
                    try
                    {
                        user = sistema.Login(credencial, password);          
                    }
                    catch (ModelException e)
                    {
                        Console.WriteLine(e.Message);
                        cont--;
                        return;
                    }

                    cont = 3;
                
                    switch (user.TipoUsuario)
                    {
                        case TipoUsuario.ADMINISTRADOR:
                            Console.WriteLine("Bienvenido " + persona.Nombre + " " + persona.Paterno);
                            Menu.InterfazAdmin(sistema,user);
                            break;
                        case TipoUsuario.PRODUCTOR:
                            Console.WriteLine("Bienvenido " + persona.Nombre + " " + persona.Paterno);
                            Menu.InterfazProductor(sistema,user);
                            break;
                        case TipoUsuario.JEFE:
                            Console.WriteLine("Bienvenido " + persona.Nombre + " " + persona.Paterno);
                            Menu.InterfazJefe(sistema,user);
                            break;
                        default:
                            throw new ModelException("Usuario no valido");
                            break;
                    }
                } // End While
                
                Console.WriteLine("Program Done.");
            } // End ISistema
        } // End Main
    } // End App
}