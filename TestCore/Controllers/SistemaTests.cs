using System;
using Core;
using Core.Controllers;
using Core.DAO;
using Core.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Xunit.Abstractions;

namespace TestCore.Controllers
{
    /// <summary>
    /// Test del sistema
    /// </summary>
    public class SistemaTests
    {
        
        /// <summary>
        /// Logger de la clase
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output"></param>
        public SistemaTests(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// Test principal de la clase.
        /// </summary>
        [Fact]
        public void AllMethodsTest()
        {
            _output.WriteLine("Starting Sistema test ...");
            ISistema sistema = Startup.BuildSistema();
            
            // Insert null
            {
                Assert.Throws<ModelException>(() => sistema.Agregar((Persona) null));
            }
            
            // Insert persona
            {
                _output.WriteLine("Testing insert ..");
                Persona persona = new Persona()
                {
                    Rut = "130144918",
                    Nombre = "Diego",
                    Paterno = "Urrutia",
                    Materno = "Astorga",
                    Email = "durrutia@ucn.cl"
                };

                sistema.Agregar(persona);
            }
            
            // GetPersonas
            {
                _output.WriteLine("Testing getPersonas ..");
                Assert.NotEmpty(sistema.GetPersonas());
            }
            
            // Buscar persona
            {
                _output.WriteLine("Testing Find ..");
                Assert.NotNull(sistema.BuscarPersona("durrutia@ucn.cl"));
                Assert.NotNull(sistema.BuscarPersona("130144918"));
            }
            
            // Busqueda de usuario
            {
                Exception usuarioNoExiste =
                    Assert.Throws<ModelException>(() => sistema.Login("notfound@ucn.cl", "durrutia123"));
                Assert.Equal("Usuario no encontrado", usuarioNoExiste.Message);
                
                Exception usuarioNoExistePersonaSi =
                    Assert.Throws<ModelException>(() => sistema.Login("durrutia@ucn.cl", "durrutia123"));
                Assert.Equal("Existe la Persona pero no tiene credenciales de acceso", usuarioNoExistePersonaSi.Message);                
            }
            
            // Insertar usuario
            {
                Persona persona = sistema.BuscarPersona("durrutia@ucn.cl");
                Assert.NotNull(persona);
                _output.WriteLine("Persona: {0}", Utils.ToJson(persona));
                
                sistema.AgregarUsuario(persona, "durrutia123");
            }

            // Busqueda de usuario
            {
                Exception usuarioExisteWrongPassword =
                    Assert.Throws<ModelException>(() => sistema.Login("durrutia@ucn.cl", "este no es mi password"));
                Assert.Equal("Password no coincide", usuarioExisteWrongPassword.Message);

                Usuario usuario = sistema.Login("durrutia@ucn.cl", "durrutia123");
                Assert.NotNull(usuario);
                _output.WriteLine("Usuario: {0}", Utils.ToJson(usuario));

            }

        }
        

    }
}