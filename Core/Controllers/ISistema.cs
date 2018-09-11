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
        // --------------------------------------
        //     >> USUARIO <<
        // --------------------------------------
        /// <summary>
        /// ISistema_OS_001: Almacena una persona en el sistema.
        /// </summary>
        /// <param name="persona">Persona a guardar en el sistema.</param>
        void Agregar(Persona persona);

        /// <summary>
        /// ISistema_OS_002: Obtiene todas las personas del sistema.
        /// </summary>
        /// <returns>The IList of Persona</returns>
        IList<Persona> GetPersonas();

        /// <summary>
        /// ISistema_OS_003: Guarda a un usuario en el sistema
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="password"></param>
        void AgregarUsuario(Persona persona, string password);

        /// <summary>
        /// ISistema_OS_004: Obtiene el usuario desde la base de datos, verificando su login y password.
        /// </summary>
        /// <param name="rutEmail">RUT o Correo Electronico</param>
        /// <param name="password">Contrasenia de acceso al sistema</param>
        /// <returns></returns>
        Usuario Login(string rutEmail, string password);

        /// <summary>
        /// ISistema_OS_005: Busqueda de una persona por rut o correo electronico.
        /// </summary>
        /// <param name="rutEmail">RUT o Correo Electronico</param>
        /// <returns>La persona si existe</returns>
        Persona BuscarPersona(string rutEmail);
        
        // --------------------------------------
        //    >> COTIZACION <<
        // --------------------------------------
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
        void EliminarCotizacion(string codigoCotizacion);
      
        /// <summary>
        /// ISistema_OS_004: Busca una cotizacion especifica del sistema
        /// </summary>
        /// <param name="codigoCotizacion"></param>
        Cotizacion BuscarCotizacion(string codigoCotizacion);

        /// <summary>
        /// ISistema_OS_005: Envia una cotizacion especifica del sistema
        /// </summary>
        /// <param name="codigoCotizacion"></param>
        bool EnviarCotizacion(string codigoCotizacion, string emailTo, string emailFrom, string msj, string password);
        
        /// <summary>
        /// ISistema_OS_006: Edita una cotizacion especifica del sistema
        /// </summary>
        /// <param name="codigoCotizacion"></param>
        /// <returns></returns>
        void EditarCotizacion(Cotizacion cotizacion);   
        
        /// <summary>
        /// ISistema_OS_007: Establece el estado de una cotizacion
        /// </summary>
        /// <param name="idCotizacion"></param>
        /// <param name="nuevoEstado"></param>
        void SeleccionarEstadoCotizacion(string codigoCotizacion, EstadoCotizacion estado);
        
        // --------------------------------------
        //    >> SERVICIO DE COTIZACION <<
        // --------------------------------------
        
        void AgregarServicio(Servicio servicio, string codigoCotizacion);

        void EditarServicio(Servicio servicio);

        void BorrarServicio(int index, string codigoCotizacion);        

        IList<Servicio> GetServicios(string codigoCotizacion);
        
        // --------------------------------------
        //    >> CLIENTE <<
        // --------------------------------------
        /// <summary>
        /// ISistema_OS_002: Obtiene todas los clientes del sistema.
        /// </summary>
        /// <returns>The IList of clientes</returns>
        IList<Cliente> GetClientes();

        /// <summary>
        /// ISistema_OS_003: Guarda a un cliente en el sistema
        /// </summary>
        /// <param name="persona"></param>
        /// <param name="telefono"></param>
        void AgregarCliente(Persona persona, int telefono);

        /// <summary>
        /// ISistema_OS_004: Busqueda de un cliente por rut o correo electronico.
        /// </summary>
        /// <param name="rutEmail">RUT o Correo Electronico</param>
        /// <returns>La persona si existe</returns>
        Cliente BuscarCliente(string rutEmail);
    }
}