using Comercio.Aplicacion.Modelo.Imagen;

namespace Comercio.Aplicacion.Servicios.Interfaz
{
    public interface IGestorImagenServicio
    {
        Task<RespuestaImagenes> SubirImagen(DatosImagenes datos);
    }
}
