using System;
using Core;
using Core.Models;
using Xunit;
using Xunit.Abstractions;

namespace TestCore.Models
{
    public class UsuarioTest
    {
        /// <summary>
        /// Logger de la clase
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output"></param>
        public UsuarioTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// Test del constructor
        /// </summary>
        [Fact]
        public void TestConstructor()
        {
            _output.WriteLine("Creating Usuario ..");
            Usuario usuario = new Usuario()
            {
            };
            
            // Usuario null
            Assert.Equal(Assert.Throws<ModelException>(() => usuario.Validate()).Message, "Usuario inexistente");
            usuario.Persona = new Persona();

            // Error por Password null
            Assert.Equal(Assert.Throws<ModelException>(() => usuario.Validate()).Message, "Se requiere el Password");
            usuario.Password = BCrypt.Net.BCrypt.HashPassword("TestingPass123");
            
            // Checking de Password (Encriptada)
            Assert.True(BCrypt.Net.BCrypt.Verify("TestingPass123", usuario.Password));
            
            _output.WriteLine(Utils.ToJson(usuario));
        }       
    }
}