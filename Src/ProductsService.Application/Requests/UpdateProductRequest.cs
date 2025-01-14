namespace ProductsService.Application.Requests;

public record UpdateProductRequest(
    string Title,
    string? Description);