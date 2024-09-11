using Newtonsoft.Json;

namespace Comercio.Api.Errores;

public class CodeErrorException : CodeErrorResponse
{
    [JsonProperty(PropertyName = "details")]
    public string? Details { get; set; }
    public CodeErrorException(int statusCode, string[]? message = null, string? details = null) 
                            : base(statusCode, message)
    {
        Details = details;
    }
}