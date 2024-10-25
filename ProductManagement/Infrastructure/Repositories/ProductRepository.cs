using Domain;
using Domain.Repositories;
using Infrastructure.Persistence;

namespace Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext context;
    
    public ProductRepository(ApplicationDbContext context)
    {
        this.context = context;
    }
    
    public async Task<Guid> AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
        return product.Id;
    }
}