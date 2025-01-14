namespace ProductsService.Domain.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Title)
            .HasMaxLength(32);
        builder
            .HasIndex(p => p.Title)
            .IsUnique();

        builder
            .Property(p => p.Description)
            .HasMaxLength(512);
    }
}
