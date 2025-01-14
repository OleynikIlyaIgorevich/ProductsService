namespace ProductsService.Domain.Entities.Base;

public class BaseEntity<TId>
{
    public TId Id { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
}
