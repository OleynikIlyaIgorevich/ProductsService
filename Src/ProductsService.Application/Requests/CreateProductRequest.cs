namespace ProductsService.Application.Requests;

public record CreateProductRequest(
    string Title,
    string? Description);