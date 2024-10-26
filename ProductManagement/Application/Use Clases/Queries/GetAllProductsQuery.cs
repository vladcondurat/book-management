using Application.DTOs;
using MediatR;

namespace Application.Use_Clases.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
{
    
}