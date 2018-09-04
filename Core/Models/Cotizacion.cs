using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    /// <summary>
    /// Clase que representa una cotizacion desde el dominio
    /// </summary>
    public class Cotizacion : BaseEntity
    {
    
        /// <summary>
        /// Nombre del documento de cotizacion
        /// </summary>
        [Required]
        public string Nombre { get; set; }

         /// <summary>
         /// Identificador de la cotización
         /// </summary>
         [Required]
         public int? Codigo { get; set; }
        
        /// <summary>
        /// Fecha en la que se creo la cotizacion
        /// </summary>
        [Required]
        public DateTime FechaCreacion { get; set; }

        /// <summary>
        /// Etapa en la que se encuentra el servicio de la cotizacion
        /// </summary>
        [Required]
        public EstadoCotiz Estado { get; set; }
        
        /// <summary>
        /// Cliente al cual le es asignada esta cotizacion
        /// </summary>
        [Required]
        public Cliente Cliente { get; set; }
        
        /// <summary>
        /// Nombre del servicio asignado a la cotizacion
        /// </summary>
        [Required]
        public string Servicio { get; set; }
        
        /// <summary>
        /// Costo total del la cotizacion.
        /// </summary>
        public int Valor { get; set; }
        

        /// <inheritdoc cref="BaseEntity.Validate"/>
        public override void Validate()
        {
            if (Codigo == null)
            {
                throw new ModelException("Error de formato. Identificador Nulo");
            }
            
            if (String.IsNullOrEmpty(Nombre))
            {
                throw new ModelException("Error de formato. Nombre de cotización Nulo");
            }
   
            if (Cliente == null)
            {
                throw new ModelException("Error de formato. No hay Cliente asociado");
            }

            if (String.IsNullOrEmpty(Servicio))
            {
                throw new ModelException("Error de formato. Nombre de cotización Nulo");
            }

            if (Valor <= 0)
            {
                throw new ModelException("Error de formato. Valor de cotización no válido");
            }
        }
    }
    /// <summary>
    /// Enumeracion con la etapa en la que se encuentra el servicio
    /// </summary>
    public enum EstadoCotiz
    {
        PRE_PRODUCCION,
        RODAJE,
        MONTAJE,
        POST_PRODUCCION,
        TERMINADA
    }

}