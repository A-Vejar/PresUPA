using System;
using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    /// <summary>
    /// Usuario del sistema
    /// </summary>
    public class Usuario : BaseEntity
    {
        /// <summary>
        /// Persona que representa a este usuario
        /// </summary>
        [Required]
        public Persona Persona { get; set; }

        /// <summary>
        /// Contrasenia de acceso de la Persona
        /// </summary>
        [Required]
        public string Password { get; set; }
        
        /// <summary>
        /// Tipo de usuario a entrar al sistema (Permisos de usuario)
        /// </summary>
        [Required]
        public TipoUsuario TipoUsuario { get; set; }

        /// <inheritdoc cref="BaseEntity.Validate"/>
        public override void Validate()
        {
            if (Persona == null)
            {
                throw new ModelException("Usuario inexistente");
            }

            if (String.IsNullOrEmpty(Password))
            {
                throw new ModelException("Se requiere el Password");
            }
        }
    }

    /// <summary>
    /// Tipo de usuarios presentes en el sistema (Clase Enum)
    /// </summary>
    public enum TipoUsuario
    {
        ADMINISTRADOR,
        JEFE,
        PRODUCTOR
    }
}