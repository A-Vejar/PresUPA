using System;
using System.Collections.Generic;
using Core;
using Core.Models;
using Xunit;
using Xunit.Abstractions;

namespace TestCore.Models
{
    public class CotizacionTest
    {
        /// <summary>
        /// Logger de la clase
        /// </summary>
        private readonly ITestOutputHelper _output;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="output"></param>
        public CotizacionTest(ITestOutputHelper output)
        {
            _output = output ?? throw new ArgumentNullException(nameof(output));
        }

        /// <summary>
        /// Test del constructor
        /// </summary>
        [Fact]
        public void TestConstructor()
        {
            _output.WriteLine("Creating Cotizacion ..");
            Cotizacion cotizacion = new Cotizacion()
            {
            };
            
            // Error por Numero null
            Assert.Equal(Assert.Throws<ModelException>(() => cotizacion.Validate()).Message, "Numero no puede ser null o vacio");
            // Error por rut incorrecto
            cotizacion.Numero = 1;
            
            // Error por Codigo null
            Assert.Equal(Assert.Throws<ModelException>(() => cotizacion.Validate()).Message, "Codigo no puede ser null o vacio");
            cotizacion.Codigo = "CTZ_001-A";
            
            // Error por Nombre null
            Assert.Equal(Assert.Throws<ModelException>(() => cotizacion.Validate()).Message, "Nombre no puede ser null o vacio");
            cotizacion.Nombre = "Testing nombre cotizacion";
            
            // Error por Descripcion null
            Assert.Equal(Assert.Throws<ModelException>(() => cotizacion.Validate()).Message, "Descripcion no puede ser null o vacio");
            cotizacion.Descripcion = "TESTING ...";
            
            // Error por Servicios (IList<>) null
            Assert.Equal(Assert.Throws<ModelException>(() => cotizacion.Validate()).Message, "No se presentan servicios");
            cotizacion.Servicios = new List<Servicio>();
            
            cotizacion.ValorFinal = -5000;
            cotizacion.ValorFinal = 0;
            // Error por Valor negativo o '0'
            Assert.Equal(Assert.Throws<ModelException>(() => cotizacion.Validate()).Message, "Valor no puede ser negativo o '0'");
            cotizacion.ValorFinal = 5000;
            
            // Error por Cliente null -----------------------------------------------------
            Assert.Equal(Assert.Throws<ModelException>(() => cotizacion.Validate()).Message, "Cliente inexistente");
            cotizacion.Cliente = new Cliente();
            
            _output.WriteLine(Utils.ToJson(cotizacion));
        }
    }
}