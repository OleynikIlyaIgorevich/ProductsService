namespace ProductsService.Application.Responses;

public record ProductResponse(
    [property: JsonPropertyName("product_id")]
    int Id,
    [property: JsonPropertyName("title")]
    string Title,
    [property: JsonPropertyName("description")]
    string? Description,
    [property: JsonPropertyName("created_at")]
    DateTime CreatedAt,
    [property: JsonPropertyName("updated_at")]
    DateTime? UpdatedAt);