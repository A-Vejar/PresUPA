using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Core.DAO;
using Core.Models;

namespace Core.Controllers
{
    /// <summary>
    /// Implementacion de la interface ISistema.
    /// </summary>
    public sealed class Sistema : ISistema
    {
        
        // ---------------------------------------------------------------------------------------------------
        // Patron Repositorio, generalizado via Generics
        // https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/generics/
        private readonly IRepository<Persona> _repositoryPersona;
        private readonly IRepository<Usuario> _repositoryUsuario;
        
        // Nuevos repositorios
        private readonly IRepository<Cotizacion> _repositoryCotizacion;
        private readonly IRepository<Cliente> _repositoryCliente;

        // Contador de la ID de cotizacion
        private int contador;

        // USUARIO
        /// <summary>
        /// Inicializa los repositorios internos de la clase.
        /// </summary>
        public Sistema(IRepository<Persona> repositoryPersona, IRepository<Usuario> repositoryUsuario,
                IRepository<Cotizacion> repositoryCotizacion, IRepository<Cliente> repositoryCliente)
        {
            // Setter!
            _repositoryPersona = repositoryPersona ??
                                 throw new ArgumentNullException("Se requiere el repositorio de personas");
            _repositoryUsuario = repositoryUsuario ??
                                 throw new ArgumentNullException("Se requiere repositorio de usuarios");
            // Nuevos Repositorios
            _repositoryCotizacion = repositoryCotizacion ??
                                 throw new ArgumentNullException("Se requiere repositorio de cotizaciones");
            _repositoryCliente = repositoryCliente ??
                                 throw new ArgumentNullException("Se requiere repositorio de clientes");

            // Inicializacion del repositorio.
            _repositoryPersona.Initialize();
            _repositoryUsuario.Initialize();
            
            // Nuevas inicializaciones
            _repositoryCotizacion.Initialize();
            _repositoryCliente.Initialize();
          
            
           //TODO: DOCUMENTAR ACÁ
            contador = 0;
            var n = _repositoryCotizacion.GetAll();
            if (n.Count == 0)
            {
                contador = n.Count + 1;
            }
            else
            {
                contador++;
            }        
        }
        
        // --------------------------------------
        //     >> USUARIO <<
        // --------------------------------------
        /// <inheritdoc />
        public void Agregar(Persona persona)
        {
            // Verificacion si es null
            if (persona == null)
            {
                throw new ModelException("Persona es null");
            }

            // Almacena la Persona en el repositorio.
            // La validacion de los atributos ocurre en el repositorio.
            _repositoryPersona.Add(persona);
        }

        /// <inheritdoc />
        public IList<Persona> GetPersonas()
        {
            // Verificacion si es null
            if (_repositoryPersona.GetAll().Count == null)
            {
                throw new ModelException("No hay datos");
            }
            return _repositoryPersona.GetAll();
        }

        /// <inheritdoc />
        /// 
        public void AgregarUsuario(Persona persona, string password)
        {
            // Guardo o actualizo en el backend.
            _repositoryPersona.Add(persona);

            // Busco si el usuario ya existe
            Usuario usuario = _repositoryUsuario.GetAll(u => u.Persona.Equals(persona)).FirstOrDefault();
            
            // Si no existe, se crea ...
            if (usuario == null)
            {
                usuario = new Usuario()
                {
                    Persona =  persona
                };
            }
            
            // Hash del password
            usuario.Password = BCrypt.Net.BCrypt.HashPassword(password);
            
            // Almaceno en el backend
            _repositoryUsuario.Add(usuario); 
        }

        /// <inheritdoc />
        public Usuario Login(string rutEmail, string password)
        {
            Persona persona = BuscarPersona(rutEmail);
            if (persona == null)
            {
                throw new ModelException("Usuario no encontrado");
            }
            
            IList<Usuario> usuarios = _repositoryUsuario.GetAll(u => u.Persona.Equals(persona));
            if (usuarios.Count == 0)
            {
                throw new ModelException("Existe la Persona pero no tiene credenciales de acceso");
            }

            if (usuarios.Count > 1)
            {
                throw new ModelException("Mas de un usuario encontrado");
            }

            Usuario usuario = usuarios.Single();
            if (!BCrypt.Net.BCrypt.Verify(password, usuario.Password))
            {
                throw new ModelException("Password Incorrecto");
            }
            return usuario;
        }

        /// <inheritdoc />
        public Persona BuscarPersona(string rutEmail)
        {
            return _repositoryPersona.GetAll(p => p.Rut.Equals(rutEmail) || p.Email.Equals(rutEmail)).FirstOrDefault();
        }
        
        // --------------------------------------
        //    >> COTIZACION <<
        // --------------------------------------
        public void Agregar(Cotizacion cotizacion)
        {
            // Verificacion de nulidad
            if (cotizacion == null)
            {
                throw new ModelException("No hay datos");
            }

            if (cotizacion.Numero == null)
            {
                cotizacion.Numero = contador;
            }
            
            cotizacion.ValorFinalCotizacion();
            cotizacion.Estado = EstadoCotizacion.PRE_PRODUCCION;
            _repositoryCotizacion.Add(cotizacion);
        }

        public IList<Cotizacion> ListarCotizaciones()
        {
            if (_repositoryCotizacion.GetAll().Count == null)
            {
                throw new ModelException("No hay datos");
            }
                
            return _repositoryCotizacion.GetAll();
        }

        public void EliminarCotizacion(string codigoCotizacion)
        {
            // Busqueda de codigo (Cotizacion)
            Cotizacion cotizacion = _repositoryCotizacion.GetAll(c => c.Codigo.Equals(codigoCotizacion)).FirstOrDefault();
            
            if (cotizacion == null)
            {
                throw new ModelException("Codigo no encontrado");
            }
            
            // Elimino del backend
            _repositoryCotizacion.Remove(cotizacion);

        }

        public Cotizacion BuscarCotizacion(string codigoCotizacion)
        {
            // Busqueda de codigo (Cotizacion)
            Cotizacion cotizacion = _repositoryCotizacion.GetAll(c => c.Codigo.Equals(codigoCotizacion)).FirstOrDefault();
            
            if (cotizacion == null)
            {
                throw new ModelException("Codigo no encontrado");
            }
            else
            {
                return cotizacion;
            }
        }

        public Cotizacion EnviarCotizacion(string codigoCotizacion, string email)
        {
            throw new NotImplementedException();
        }

        public void Editar(Cotizacion cotizacion)
        {
            if (cotizacion != null)
            {
                Cotizacion Comparable = BuscarCotizacion(cotizacion.Codigo) ?? throw new ArgumentNullException("BuscarCotizacion(Comparable.Codigo)");
                if (Validate.CambiosCotizacion(Comparable, cotizacion))
                {
                    Console.WriteLine("Sin modificaciones");
                }
            }

            Console.WriteLine("Actualizada");
            Agregar(cotizacion);
        }

        public void SeleccionarEstadoCotizacion(string codigoCotizacion, EstadoCotizacion estado)
        {
            Cotizacion cotizacion = BuscarCotizacion(codigoCotizacion);
            cotizacion.Estado = estado;
            _repositoryCotizacion.Add(cotizacion);
        }
        
        // --------------------------------------
        //    >> SERVICIO DE COTIZACION <<
        // --------------------------------------
        public void AgregarServicio(Servicio servicio, string idCotizacion)
        {
            throw new NotImplementedException();
        }

        public void EditarServicio(Servicio servicio)
        {
            throw new NotImplementedException();
        }

        public void BorrarServicio(int index, string idCotizacion)
        {
            throw new NotImplementedException();
        }

        public IList<Servicio> GetServicios(string idCotizacion)
        {
            throw new NotImplementedException();
        }

        // --------------------------------------
        //    >> CLIENTE <<
        // --------------------------------------
        // TODO: REVISAR SI HAY QUE AGREGARLO SI NO EXISTE
        public void AgregarPersonaCliente(Persona persona)
        {
            // Verificacion si es null
            if (persona == null)
            {
                throw new ModelException("Persona es null");
            }

            // Almacena la Persona en el repositorio.
            // La validacion de los atributos ocurre en el repositorio.
            _repositoryPersona.Add(persona);
        }

        public IList<Cliente> GetClientes()
        {
            // Verificacion si es null
            if (_repositoryCliente.GetAll().Count == null)
            {
                throw new ModelException("No hay datos");
            }
            return _repositoryCliente.GetAll();
        }

        // NOT SURE IF IT'S WORTH IT ...
        //todo: revisar si es que es necesario
        public void AgregarCliente(Persona persona, int telefono)
        {
            // Guardo o actualizo en el backend.
            _repositoryPersona.Add(persona);

            // Busco si el usuario ya existe
            Cliente cliente = _repositoryCliente.GetAll(c => c.Persona.Equals(persona)).FirstOrDefault();
            
            // Si no existe, se crea ...
            if (cliente == null)
            {
                cliente = new Cliente()
                {
                    Persona =  persona
                };
            }

            cliente.Telefono = telefono;
            
            // Almaceno en el backend
            _repositoryCliente.Add(cliente);
        }

        public Persona BuscarCliente(string rutEmail)
        {
            return _repositoryPersona.GetAll(p => p.Rut.Equals(rutEmail) || p.Email.Equals(rutEmail)).FirstOrDefault();
        }
    }
}