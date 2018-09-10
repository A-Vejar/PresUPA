using System;

namespace Core.Models
{
    public class Servicio : BaseEntity
    {
        /// <summary>
        /// Nombre del servicio 
        /// </summary>
        public string Nombre { get; set; }
        
        /// <summary>
        /// Comentario del servicio, es opcional (Puede ser null)
        /// </summary>
        public string Comentario { get; set; }
        
        /// <summary>
        /// Cantidad servicios necesitados por el cliente
        /// </summary>
        public int Cantidad { get; set; }
        
        /// <summary>
        /// Valor dependiendo del servicio a entregar
        /// </summary>
        public int Valor { get; set; }

        /// <summary>
        /// Patron GRASP -> Experto en Informacion (Responsabilidad limitada)
        /// Valor del servicio
        /// </summary>
        public int ValorServicio()
        {
            return this.Cantidad * this.Valor;
        }

        // Validaciones de servicios
        public override void Validate()
        {
            if (String.IsNullOrEmpty(Nombre))
            {
                throw new ModelException("Nombre no puede ser null o vacio");
            }

            if (Cantidad <= 0)
            {
                throw new ModelException("Cantidad no puede ser negativo o '0'");
            }
            
            if (Valor <= 0)
            {
                throw new ModelException("Valor no puede ser negativo o '0'");
            }
        }
    }
}