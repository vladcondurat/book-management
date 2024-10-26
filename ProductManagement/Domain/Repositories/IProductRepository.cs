namespace Domain.Repositories;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task<Guid> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
    
}