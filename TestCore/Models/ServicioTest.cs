using System;
using System.Collections.Generic;
using Core;
using Core.Models;
using Xunit;
using Xunit.Abstractions;

namespace TestCore.Models
{
    public class ServicioTest
    {
        /// <summary>
        /// Logger de la clase
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output"></param>
        public ServicioTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// Test del constructor
        /// </summary>
        [Fact]
        public void TestConstructor()
        {
            _output.WriteLine("Creating Servicio ..");
            Servicio servicio = new Servicio();
            {
            };
            
            // Error por Nombre null
            Assert.Equal(Assert.Throws<ModelException>(() => servicio.Validate()).Message, "Nombre no puede ser null o vacio");
            servicio.Nombre = "Testing nombre servicio";
            
            servicio.Cantidad = -5;
            servicio.Cantidad = 0;
            // Error por Cantidad negativo o '0'
            Assert.Equal(Assert.Throws<ModelException>(() => servicio.Validate()).Message, "Cantidad no puede ser negativo o '0'");           
            servicio.Cantidad = 5;
               
            servicio.Valor = -5000;
            servicio.Valor = 0;
            // Error por Valor negativo o '0'
            Assert.Equal(Assert.Throws<ModelException>(() => servicio.Validate()).Message, "Valor no puede ser negativo o '0'");
            servicio.Valor = 5000;
            
            _output.WriteLine(Utils.ToJson(servicio));
        }
    }
}