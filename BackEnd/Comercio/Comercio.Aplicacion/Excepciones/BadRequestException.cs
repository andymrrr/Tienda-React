namespace Comercio.Aplicacion.Excepciones
{
    public class BadRequestException : ApplicationException
    {

        public BadRequestException(string message) : base(message)
        {
        }

    }
}