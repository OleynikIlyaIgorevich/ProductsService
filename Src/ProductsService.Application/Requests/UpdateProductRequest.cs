using System.Text.Json.Serialization;

namespace ProductsService.Application.Requests;

public record UpdateProductRequest(
    [property: JsonPropertyName("title")]
    string Title,
    [property: JsonPropertyName("description")]
    string? Description);