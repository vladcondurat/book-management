using Application.Use_Clases.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ProductManagement.Controllers;

namespace ProductManagement.Application.UnitTests;

public class ProductController_CreateProductTests
{
    private readonly IMediator _mediator;
    private readonly ProductsController _controller;

    public ProductController_CreateProductTests()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new ProductsController(_mediator);
    }

    [Fact]
    public async Task CreateProduct_ShouldReturnCreatedAtActionResult_WithProductId()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var command = new CreateProductCommand();
        _mediator.Send(command, Arg.Any<CancellationToken>()).Returns(productId);

        // Act
        var result = await _controller.CreateProduct(command);

        // Assert
        result.Result.Should().BeOfType<CreatedAtActionResult>();
        var createdResult = result.Result as CreatedAtActionResult;
        createdResult?.Value.Should().Be(productId);
        createdResult?.RouteValues["Id"].Should().Be(productId);
    }
}