namespace ProductsService.Domain.Entities;

public class Product : BaseEntity<int>
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string AuthorAppName { get; set; }

    public Product() { }

    public Product(
        string title,
        string? description,
        string authorAppName)
    {
        Title = title;  
        Description = description;
        AuthorAppName = authorAppName;
    }
}
