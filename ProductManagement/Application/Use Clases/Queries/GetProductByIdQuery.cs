using Application.DTOs;
using MediatR;

namespace Application.Use_Clases.Queries;

public class GetProductByIdQuery : IRequest<ProductDto>
{
    public Guid Id { get; set; }
}