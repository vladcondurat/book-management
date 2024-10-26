using Application.Use_Clases.Commands;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Clases.CommandHandlers;

public class DeleteProductByIdCommandHandler : IRequestHandler<DeleteProductByIdCommand>
{
    private readonly IProductRepository _productRepository;
    
    public DeleteProductByIdCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    
    public async Task Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        await _productRepository.DeleteAsync(request.Id);
    }
}