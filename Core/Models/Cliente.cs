using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    /// <summary>
    /// Clase que representa un cliente desde el dominio
    /// </summary>
    public class Cliente : BaseEntity
    {
        /// <summary>
        /// Persona representado por el cliente
        /// </summary>
        public Persona Persona { get; set; }

        /// <summary>
        /// Contacto del cliente
        /// </summary>
        public int Telefono { get; set; }

        public override void Validate()
        {
            if (Persona == null)
            {
                throw new ModelException("Cliente inexistente");
            }
            
            if (Telefono == null)
            {
                throw new ModelException("Error de formato. Telefono invalido");
            }
            
            if (Telefono <= 0)
            {
                throw new ModelException("Error de formato. Telefono invalido");
            }
        }
    }

    
}