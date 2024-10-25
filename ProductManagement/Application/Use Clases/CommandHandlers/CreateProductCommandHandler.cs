using Application.DTOs;
using Application.Use_Clases.Commands;
using AutoMapper;
using Domain;
using Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Application.Use_Clases.CommandHandlers;

public class CreateProductCommandHandler: IRequestHandler<CreateProductCommand, Guid>
{
    private readonly IProductRepository repository;
    private readonly IMapper mapper;

    public CreateProductCommandHandler(IProductRepository repository, IMapper mapper)
    {
        this.repository = repository;
        this.mapper = mapper;
    }
    public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = mapper.Map<Product>(request);
        return await repository.AddAsync(product);
    }
}