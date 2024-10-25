using Application.Use_Clases.Commands;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProductManagement.Controllers;
[Route("api/v1/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IMediator mediator;
    private readonly IMapper mapper;
    
    public ProductsController(IMediator mediator, IMapper mapper)
    {
        this.mediator = mediator;
        this.mapper = mapper;
    }
    
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateProduct(CreateProductCommand command)
    {
        var id = await mediator.Send(command);
        return Created($"/products/{id}", id);
    }
}