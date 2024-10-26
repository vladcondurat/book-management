using Application.Use_Clases.Commands;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using ProductManagement.Controllers;

namespace ProductManagement.Application.UnitTests;

public class ProductsController_DeleteTests
{
    private readonly IMediator _mediator;
    private readonly ProductsController _controller;

    public ProductsController_DeleteTests()
    {
        _mediator = Substitute.For<IMediator>();
        _controller = new ProductsController(_mediator);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent_WhenProductIsDeleted()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var deleteCommand = new DeleteProductByIdCommand { Id = productId };

        // Simulate that the product exists by confirming the deletion will succeed
        _mediator.Send(deleteCommand, Arg.Any<CancellationToken>()).Returns(Task.CompletedTask);

        // Act - Delete the product
        var result = await _controller.Delete(productId);
        var statusCodeResult = result as StatusCodeResult;
        
        // Assert
        statusCodeResult.StatusCode.Should().Be(204);
    }
}