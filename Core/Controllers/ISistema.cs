using System;
using System.Collections.Generic;
using Core.Models;

namespace Core.Controllers
{
    /// <summary>
    /// Operaciones del sistema.
    /// </summary>
    public interface ISistema
    {
        // ------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// Operacion de sistema: Almacena una persona en el sistema.
        /// </summary>
        /// <param name="persona">Persona a guardar en el sistema.</param>
        void Save(Persona persona);

        /// <summary>
        /// Obtiene todas las personas del sistema.
        /// </summary>
        /// <returns>The IList of Persona</returns>
        IList<Persona> GetPersonas();

        /// <summary>
        /// Guarda a un usuario en el sistema
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="password"></param>
        void Save(Persona persona, string password);

        /// <summary>
        /// Obtiene el usuario desde la base de datos, verificando su login y password.
        /// </summary>
        /// <param name="rutEmail">RUT o Correo Electronico</param>
        /// <param name="password">Contrasenia de acceso al sistema</param>
        /// <returns></returns>
        Usuario Login(string rutEmail, string password);

        /// <summary>
        /// Busqueda de una persona por rut o correo electronico.
        /// </summary>
        /// <param name="rutEmail">RUT o Correo Electronico</param>
        /// <returns>La persona si existe</returns>
        Persona Find(string rutEmail);
        
        // ------------------------------------------------------------------------------------------------------
        
        /// <summary>
        /// ISistema_OS_001: Agrega una cotizacion al sistema.
        /// </summary>
        /// <param name="cotizacion"></param>
        void AgregarCotizacion(Cotizacion cotizacion);
        
        /// <summary>
        /// ISistema_OS_002: Obtiene una lista de todas las cotizaciones presentes en el sistema.
        /// </summary>
        /// <returns></returns>
        IList<Cotizacion> ListarCotizaciones();
        
        /// <summary>
        /// ISistema_OS_003: Elimina una cotizacion especifica del sistema
        /// </summary>
        /// <param name="codigoCotizacion"></param>
        Cotizacion EliminarCotizacion(string codigoCotizacion);
      
        /// <summary>
        /// ISistema_OS_004: Busca una cotizacion especifica del sistema
        /// </summary>
        /// <param name="codigoCotizacion"></param>
        Cotizacion BuscarCotizacion(string codigoCotizacion);

        /// <summary>
        /// ISistema_OS_005: Envia una cotizacion especifica del sistema
        /// </summary>
        /// <param name="codigoCotizacion"></param>
        Cotizacion EnviarCotizacion(string codigoCotizacion);
        
        /// <summary>
        /// ISistema_OS_006: Edita una cotizacion especifica del sistema
        /// </summary>
        /// <param name="codigoCotizacion"></param>
        /// <returns></returns>
        Cotizacion EditarCotizacion(string codigoCotizacion);   
        
        /// <summary>
        /// ISistema_OS_007: Establece el estado de una cotizacion
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <param name="nuevoEstado"></param>
        void SeleccionarEstadoCotizacion(string codigoCotizacion, EstadoCotizacion estado);
        
        /// <summary>
        /// ISistema_OS_008: Busqueda por codigo de una cotizacion
        /// </summary>
        /// <param name="codigo"></param>
        Cotizacion FindCotizacion(string codigo);
    }
}