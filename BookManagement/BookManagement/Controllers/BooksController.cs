using Application.DTOs;
using Application.Use_Cases.Commands;
using Application.Use_Cases.Queries;
using AutoMapper;
using BookManagement.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public BooksController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateBook(CreateBookCommand command)
        {
            var id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetBookById), new { Id = id }, command);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<BookDto>> GetBookById(Guid id)
        {
            return await mediator.Send(new GetBookByIdQuery { Id = id });
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDto>>> GetAllBooks()
        {
            var books = await mediator.Send(new GetAllBooksQuery());
            return Ok(books);
        }
        
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> DeleteBook(Guid id)
        {
            await mediator.Send(new DeleteBookCommand { Id = id });
            return NoContent();
        }
        
        [HttpPatch("{id:guid}")]
        public async Task<ActionResult> UpdateBook(Guid id, UpdateBookRequest request)
        {
            var command = mapper.Map<UpdateBookCommand>(request);
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
