using System;
using System.Collections.Generic;
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
                
                //Ingreso
                
                Console.WriteLine("LOGIN: ");
                
                string credencial = Console.ReadLine();
                
                Console.WriteLine("PASSWORD: ");
               
                string password = Console.ReadLine();

                Usuario u = null;
            
                try
                {
                    u = sistema.Login(credencial, password);          
                }
                catch (ModelException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }

                switch (u.TipoUsuario)
                {
                    case TipoUsuario.ADMINISTRADOR:
                        Menu.InterfazAdmin(sistema,u);
                        break;
                    case TipoUsuario.PRODUCTOR:
                        Console.WriteLine("Case 2");
                        break;
                    case TipoUsuario.JEFE:
                        Console.WriteLine("Case 2");
                        break;
                    default:
                        throw new ModelException("Usuario no valido");
                        break;
                        
                }
                
            }

            }
        }
    }