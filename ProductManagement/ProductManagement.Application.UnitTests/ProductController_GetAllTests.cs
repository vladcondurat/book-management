using Application.DTOs;
using Application.Use_Clases.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ProductManagement.Controllers;

namespace ProductManagement.Application.UnitTests;

public class ProductsController_GetAllTests
{
    private readonly IMediator _mediator;
    private readonly ProductsController _controller;

    public ProductsController_GetAllTests()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new ProductsController(_mediator);
    }

    [Fact]
    public async Task GetAll_ShouldReturnListOfProductDto()
    {
        // Arrange
        var products = new List<ProductDto>
        {
            new ProductDto 
            { 
                Id = Guid.NewGuid(), 
                Name = "Product 1", 
                Price = 29.99f, 
                Quantity = 5, 
                Category = "Books", 
                Tva = 10, 
                DiscountPercent = 5 
            },
            new ProductDto 
            { 
                Id = Guid.NewGuid(), 
                Name = "Product 2", 
                Price = 49.99f, 
                Quantity = 8, 
                Category = "Games", 
                Tva = 20, 
                DiscountPercent = 15 
            }
        };
            
        _mediator.Send(Arg.Any<GetAllProductsQuery>(), Arg.Any<CancellationToken>()).Returns(products);

        // Act
        var result = await _controller.GetAll();

        // Assert
        var okResult = result.Result as OkObjectResult;
        okResult.Should().NotBeNull();
        okResult.Value.Should().BeEquivalentTo(products);
    }
}