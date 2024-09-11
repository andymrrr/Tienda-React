using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Comercio.Aplicacion.Modelo.Imagen;
using Comercio.Aplicacion.Servicios.Interfaz;
using Microsoft.Extensions.Options;
using System.Data;
using System.Net;

namespace Comercio.Infraestructura.Servicios.Repositorio
{
    public class GestorImagenServicio : IGestorImagenServicio
    {
        private ConfiguracionCloudinary _configuracionCloudinary { get; }
        public GestorImagenServicio(IOptions<ConfiguracionCloudinary> configurationCloudinary)
        {
            _configuracionCloudinary = configurationCloudinary.Value;
        }
        public async Task<RespuestaImagenes> SubirImagen(DatosImagenes datos)
        {
            var cuenta = new Account(_configuracionCloudinary.Nombre, _configuracionCloudinary.Llave,
                _configuracionCloudinary.Secreto);
            var cloudinary = new Cloudinary(cuenta);

            var subirImagen = new ImageUploadParams()
            {
                File = new FileDescription(datos.Nombre, datos.Imagen)
            };

            var resultadoSubida = await cloudinary.UploadAsync(subirImagen);
            if (resultadoSubida.StatusCode == HttpStatusCode.OK)
            {
                return new RespuestaImagenes
                {
                    CodigoPublco = resultadoSubida.PublicId,
                    Url = resultadoSubida.Url.ToString()
                };
            }
            throw new Exception("No se pudo Cargar la imagen");
        }
    }
}
