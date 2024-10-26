using Application.DTOs;
using Application.Use_Clases.Queries;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ProductManagement.Controllers;

namespace ProductManagement.Application.UnitTests;

public class ProductsController_GetByIdTests
{
    private readonly IMediator _mediator;
    private readonly ProductsController _controller;

    public ProductsController_GetByIdTests()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new ProductsController(_mediator);
    }

    [Fact]
    public async Task GetById_ShouldReturnProductDto_WhenProductExists()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var productDto = new ProductDto 
        { 
            Id = productId, 
            Name = "Test Product",
            Price = 19.99f,
            Quantity = 10,
            Category = "Electronics",
            Tva = 20,
            DiscountPercent = 10
        };
        _mediator.Send(Arg.Any<GetProductByIdQuery>(), Arg.Any<CancellationToken>()).Returns(productDto);

        // Act
        var result = await _controller.GetById(productId);

        // Assert
        result.Value.Should().BeEquivalentTo(productDto);
    }
}