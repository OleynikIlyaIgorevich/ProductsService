namespace ProductsService.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IProductRepository _productRepository;

    public ProductController(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        var productsResponse = products
            .Select(product => 
                new ProductResponse(
                    product.Id,
                    product.Title,
                    product.Description,
                    product.CreatedAt,
                    product.UpdatedAt));

        return Ok(productsResponse);
    }

    [HttpGet("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        if (productId == default) return BadRequest("Идентификатор не валиден!");

        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        if (product == null) return NotFound("Товар с данным идентификатором не найден!");

        var productResponse = new ProductResponse(
            product.Id,
            product.Title,
            product.Description,
            product.CreatedAt,
            product.UpdatedAt);

        return Ok(productResponse);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync(
        CreateProductRequest request,
        CancellationToken cancellationToken = default)
    {



        var isExistByTitle = await _productRepository.IsExistByTitleAsync(request.Title, cancellationToken);
        if (isExistByTitle) return BadRequest("Товар с данным заголовком уже существует!");

        var product = new Product(request.Title, request.Description);

        await _productRepository.AddAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(CreateAsync), new { id = product.Id }, product);
    }

    [HttpPut("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateAsync(
        int productId,
        UpdateProductRequest request,
        CancellationToken cancellationToken = default)
    {
        if (productId == default) return BadRequest("Идентификатор не валиден!");



        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        if (product == null) return NotFound("Товар с данным идентификатором не найден!");

        var isExistForUpdateByTitle = await _productRepository.IsExistForUpdateByTitleAsync(productId, request.Title, cancellationToken);
        if (isExistForUpdateByTitle) return BadRequest("Товар с данным заголовком уже существует!");

        product.Title = request.Title;
        product.Description = request.Description;
        product.UpdatedAt = DateTime.UtcNow;

        await _productRepository.UpdateAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }

    [HttpDelete("{productId:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteAsync(
        int productId,
        CancellationToken cancellationToken = default)
    {
        if (productId == default) return BadRequest("Идентификатор не валиден!");

        var product = await _productRepository.GetByIdAsync(productId, cancellationToken);
        if (product == null) return NotFound("Товар с данным идентификатором не найден!");

        await _productRepository.DeleteAsync(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
} 
