using System;
using System.Net.Mail;
using System.Reflection.Metadata;

namespace Core.Models
{
    /// <summary>
    /// Clase que contiene funciones para validacion del modelo.
    /// </summary>
    public sealed class Validate
    {
        /// <summary>
        /// Metodo que valida un rut
        /// </summary>
        /// <param name="rut">Rut a validar</param>
        /// <exception cref="ModelException">Exception en caso de no ser valido</exception>
        public static void ValidarRut(string rut)
        {
            if (String.IsNullOrEmpty(rut))
            {
                throw new ModelException("Rut invalido");
            }

            try
            {
                int rutNumber = Convert.ToInt32(rut.Substring(0, rut.Length - 1));
                char dv = Convert.ToChar(rut.Substring(rut.Length - 1, 1));

                int m = 0;
                int s = 1;
                for (; rutNumber != 0; rutNumber /= 10)
                {
                    s = (s + rutNumber % 10 * (9 - m++ % 6)) % 11;
                }

                if (dv != Convert.ToChar(s != 0 ? s + 47 : 75))
                {
                    throw new ModelException("Rut no valido");
                }
            }
            catch (FormatException)
            {
                throw new ModelException("Rut no valido");
            }
        }
        
        /// <summary>
        /// Metodo para validar una direccion de correo
        /// </summary>
        /// <param name="email">Email a validar</param>
        /// <exception cref="ModelException">Exception en caso de no ser valido para el sistema</exception>
        public static void ValidarEmail(string email)
        {
            if (String.IsNullOrEmpty(email))
            {
                throw new ModelException("Email invalido");
            }
            try
            {
                MailAddress validarEmail = new MailAddress(email);
            }
            catch (FormatException)
            {
                throw new ModelException("Formato de email invalido");
            }
        }

        public static bool CambiosCotizacion(Cotizacion c1, Cotizacion c2)
        {
            if (!c1.Nombre.Equals(c2.Nombre) || !c1.Cliente.Equals(c2.Cliente) || !c1.Numero.Equals(c2.Numero) ||
                !c1.Descripcion.Equals(c1.Descripcion) || !c1.Servicios.Equals(c2.Servicios)) return false;
            return true;
        }
    }
}