namespace ProductsService.Infra.Services.Base;

public abstract class BaseService
{
    protected IUnitOfWork _unitOfWork;

    public BaseService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
}