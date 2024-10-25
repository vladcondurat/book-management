namespace Domain.Repositories;

public interface IProductRepository
{
    Task<Guid> AddAsync(Product product);
}