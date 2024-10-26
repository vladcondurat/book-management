using Application.Use_Clases.Commands;
using AutoMapper;
using Domain;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Clases.CommandHandlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    
    public UpdateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        return _repository.UpdateAsync(product);
    }
}