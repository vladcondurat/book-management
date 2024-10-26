using MediatR;

namespace Application.Use_Clases.Commands;

public class DeleteProductByIdCommand : IRequest
{
    public Guid Id { get; set; }
}