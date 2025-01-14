namespace ProductsService.Application.Responses;

public record ProductResponse(
    int Id,
    string Title,
    string? Description,
    string Author);