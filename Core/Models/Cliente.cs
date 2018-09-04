using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    /// <summary>
    /// Clase que representa un cliente desde el dominio
    /// </summary>
    public class Cliente : BaseEntity
    {
        public Persona Persona { get; set; }
        
        public override void Validate()
        {
            if (Persona == null)
            {
                throw new ModelException("Error de autenticación");
            }

        }
    }

    
}