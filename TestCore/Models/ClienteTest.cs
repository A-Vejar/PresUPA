using System;
using Core;
using Core.Models;
using Xunit;
using Xunit.Abstractions;

namespace TestCore.Models
{
    public class ClienteTest
    {
        /// <summary>
        /// Logger de la clase
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output"></param>
        public ClienteTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// Test del constructor
        /// </summary>
        [Fact]
        public void TestConstructor()
        {
            _output.WriteLine("Creating Cliente ..");
            Cliente cliente = new Cliente();
            {
            };
            
            // Usuario null
            Assert.Equal(Assert.Throws<ModelException>(() => cliente.Validate()).Message, "Cliente inexistente");
            cliente.Persona = new Persona();

            // Error por Telefono null
            Assert.Equal(Assert.Throws<ModelException>(() => cliente.Validate()).Message, "Telefono no puede ser null");
            cliente.Telefono = 975686616;
            
            _output.WriteLine(Utils.ToJson(cliente));
        }
    }
}