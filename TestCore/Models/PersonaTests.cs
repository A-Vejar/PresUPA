using System;
using Core;
using Core.Models;
using Xunit;
using Xunit.Abstractions;

namespace TestCore.Models
{
    /// <summary>
    /// Testing de la clase Persona.
    /// </summary>
    public class PersonaTests
    {
        /// <summary>
        /// Logger de la clase
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output"></param>
        public PersonaTests(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// Test del constructor
        /// </summary>
        [Fact]
        public void TestConstructor()
        {
            _output.WriteLine("Creating Persona ..");
            Persona persona = new Persona()
            {
            };

            /*
            // Error por rut null
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Rut no puede ser null");
            // Error por rut incorrecto
            persona.Rut = "Hola Como estas?";
            
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Rut no valido");
            persona.Rut = "130144918";
            
            // Nombre
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Nombre no puede ser null o vacio");
            persona.Nombre = "Diego";
            
            // Apellido
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Apellido Paterno no puede ser null o vacio");
            persona.Paterno = "Urrutia";
            
            // Email
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Email no puede ser null o vacio");
            persona.Email = "durrutia@ucn.cl";
            
            _output.WriteLine(Utils.ToJson(persona));
            */
            
            // Error por rut null
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Rut no puede ser null");
            
            // Error por rut incorrecto
            persona.Rut = "WsaaP Bitchess";            
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Rut no valido");
            persona.Rut = "185855740";
            
            // Nombre
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Nombre no puede ser null o vacio");
            persona.Nombre = "Ariel";
            
            // Apellido
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Apellido Paterno no puede ser null o vacio");
            persona.Paterno = "Vejar";
            
            // Email
            Assert.Equal(Assert.Throws<ModelException>(() => persona.Validate()).Message, "Email no puede ser null o vacio");
            persona.Email = "ariel.vejar@live.cl";
            
            _output.WriteLine(Utils.ToJson(persona));
        }
    }
}