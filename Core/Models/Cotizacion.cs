using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace Core.Models
{
    /// <summary>
    /// Clase que representa una cotizacion desde el dominio
    /// </summary>
    public class Cotizacion : BaseEntity
    {
        
        /// <summary>
        /// Numero de un documento de cotizacion, determina la cantidad de cotizaciones creadas
        /// </summary>
        [Required]
        public int? Numero { get; set; } // NECESARIO ?
        
        /// <summary>
        /// Identificador de la cotización
        /// </summary>
        [Required]
        public string Codigo { get; set; }
    
        /// <summary>
        /// Nombre o titulo del documento de cotizacion
        /// </summary>
        public string Nombre { get; set; }
        
        /// <summary>
        /// Descripcion general de la cotización
        /// </summary>
        public string Descripcion { get; set; }
        
        /// <summary>
        /// Fecha en la que se creo la cotizacion
        /// </summary>
        public DateTime FechaCreacion { get; set; }
        
        /// <summary>
        /// Servicios propuestos en una cotizacion dada (Lista de servicios)
        /// </summary>
        public IList<Servicio> Servicios { get; set; }
        
        /// <summary>
        /// Costo total del la cotizacion.
        /// </summary>
        public int ValorFinal { get; set; }
        
        /// <summary>
        /// Cliente al cual le es asignada esta cotizacion
        /// </summary>
        public Cliente Cliente { get; set; }
        
        /// <summary>
        /// Estado en que se encuentra el servicio(s) de la cotizacion
        /// </summary>
        public EstadoCotizacion Estado { get; set; }
        
        /// <summary>
        /// Asigna el valor final/total de una cotizacion realizada
        /// </summary>
        public int ValorFinalCotizacion()
        {
            if (Servicios != null)
            {
                foreach (Servicio s in Servicios)
                    ValorFinal += s.ValorServicio();
            }
            return ValorFinal;
        }

        /// <summary>
        /// Asigna el valor final/total de una cotizacion realizada
        /// </summary>
        public void ValidarServicio()
        {
            foreach (Servicio s in Servicios)
                s.Validate();
        }
        
        /// <inheritdoc cref="BaseEntity.Validate"/>
        public override void Validate()
        {
            if (Numero == null)
            {
                throw new ModelException("Error de formato. Numero de cotizacion nulo");
            }

            // Puede puede ser "String.IsNullOrEmpty(Codigo)" ????
            if (Codigo == null)
            {
                throw new ModelException("Error de formato. Codigo identificador nulo");
            }
            
            if (String.IsNullOrEmpty(Nombre))
            {
                throw new ModelException("Error de formato. Nombre de cotización nulo");
            }
            
            if (String.IsNullOrEmpty(Descripcion))
            {
                throw new ModelException("Error de formato. No hay descripcion de cotización asociada");
            }
            
            // Mmmm... ????
            if (FechaCreacion == null)
            {
                throw new ModelException("Error de formato. No hay fecha asociada a la cotizacion");
            }
            
            if (Servicios == null)
            {
                throw new ModelException("Error de formato. No hay servicios asociados a la cotizacion");
            }
            
            if (ValorFinal <= 0)
            {
                throw new ModelException("Error de formato. Valor de cotización invalido");
            }

            if (Cliente == null)
            {
                throw new ModelException("Error de formato. No hay cliente asociado");
            }
            
            // ... ????
            ValidarServicio();
        }
    }
    /// <summary>
    /// Enumeracion con la etapa en la que se encuentra el servicio
    /// </summary>
    public enum EstadoCotizacion
    {
        CANCELADA,
        PRE_PRODUCCION, // Presupuesto, Investigacion (Previo a grabacion)
        RODAJE, // Grabaciones en terreno (Toma de escenas)
        MONTAJE, // Ediciones (OSO: Linea de tiempo del video)
        POST_PRODUCCION, // Ediciones finales
        EXPORTACION // Termino de video (Cotizacion) y envio a cliente
    }

}