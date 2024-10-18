using Application.Use_Cases.Commands;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.CommandHandlers
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
    {
        private readonly IBookRepository repository;

        public UpdateBookCommandHandler(IBookRepository repository)
        {
            this.repository = repository;
        }

        public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await repository.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {request.Id} not found.");
            }

            book.Title = request.Title;
            book.Author = request.Author;
            book.ISBN = request.ISBN;
            book.PublicationDate = request.PublicationDate;

            await repository.UpdateAsync(book);
        }
    }
}