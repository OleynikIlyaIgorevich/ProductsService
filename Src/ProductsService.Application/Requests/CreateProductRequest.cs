namespace ProductsService.Application.Requests;

public record CreateProductRequest(
    [property: JsonPropertyName("title")]
    string Title,
    [property: JsonPropertyName("description")]
    string? Description);