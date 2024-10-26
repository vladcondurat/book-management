using FluentValidation;

namespace Application.Use_Clases.Commands;

public class DeleteProductByIdCommandValidator: AbstractValidator<DeleteProductByIdCommand>
{
    public DeleteProductByIdCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required");
    }
}