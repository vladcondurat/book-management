using Application.DTOs;
using Application.Use_Clases.Commands;
using Application.Use_Clases.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ProductsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProduct(CreateProductCommand command)
    {
        var id = await _mediator.Send(command);
        return CreatedAtAction("GetById", new { Id = id }, id);
    }
    
    [HttpGet("id")]
    public async Task<ActionResult<ProductDto>> GetById(Guid id)
    {
        return await _mediator.Send(new GetProductByIdQuery { Id = id });
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
    {
        return Ok(await _mediator.Send(new GetAllProductsQuery()));
    }
    
    [HttpDelete("id")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _mediator.Send(new DeleteProductByIdCommand { Id = id });
        return StatusCode(StatusCodes.Status204NoContent);
    }
    
    [HttpPut("id")]
    public async Task<IActionResult> Update(Guid id, UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest();
        }
        await _mediator.Send(command);
        return StatusCode(StatusCodes.Status204NoContent);
    }
}