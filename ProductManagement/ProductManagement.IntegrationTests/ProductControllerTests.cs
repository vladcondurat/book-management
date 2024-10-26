using Application.DTOs;
using FluentAssertions;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using Application.Use_Clases.Commands;
using Domain;
using Xunit;

namespace ProductManagement.IntegrationTests
{
    public class ProductsControllerTests : IClassFixture<WebApplicationFactory<Program>>, IDisposable
    {
        private readonly WebApplicationFactory<Program> factory;
        private readonly ApplicationDbContext dbContext;

        public ProductsControllerTests(WebApplicationFactory<Program> factory)
        {
            this.factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var descriptor = services.SingleOrDefault(
                        d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));

                    if (descriptor != null)
                    {
                        services.Remove(descriptor);
                    }

                    services.AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("productmanagementdb");
                    });
                });
            });

            var scope = this.factory.Services.CreateScope();
            dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllProducts_ReturnsSuccessStatusCode()
        {
            // Arrange
            var client = factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/products");

            // Assert
            response.EnsureSuccessStatusCode();
            response.Content.Headers.ContentType?.ToString().Should().Be("application/json; charset=utf-8");
        }

        [Fact]
        public async Task GetAllProducts_ReturnsListOfProducts()
        {
            // Arrange
            var client = factory.CreateClient();
            await AddTestProductAsync();

            // Act
            var response = await client.GetAsync("/api/v1/products");
            var products = await response.Content.ReadFromJsonAsync<List<ProductDto>>();

            // Assert
            products.Should().NotBeNull();
            products.Should().HaveCount(1);
            products![0].Name.Should().Be("Test Product");
        }

        [Fact]
        public async Task CreateProduct_ReturnsCreatedStatusCodeAndProductId()
        {
            // Arrange
            var client = factory.CreateClient();
            var command = new CreateProductCommand
            {
                Name = "New Test Product",
                Price = 19.99f,
                Quantity = 10,
                Category = "Test Category",
                Tva = 20,
                DiscountPercent = 5
            };

            // Act
            var response = await client.PostAsJsonAsync("/api/v1/products", command);
            var productId = await response.Content.ReadFromJsonAsync<Guid>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.Created);
            productId.Should().NotBeEmpty();
        }

        [Fact]
        public async Task CreateProduct_AddsProductToDatabase()
        {
            // Arrange
            var client = factory.CreateClient();
            var command = new CreateProductCommand
            {
                Name = "Another Test Product",
                Price = 29.99f,
                Quantity = 5,
                Category = "Another Category",
                Tva = 10,
                DiscountPercent = 15
            };

            // Act
            await client.PostAsJsonAsync("/api/v1/products", command);

            // Assert
            var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Name == command.Name);
            product.Should().NotBeNull();
            product!.Price.Should().Be(command.Price);
            product.Quantity.Should().Be(command.Quantity);
        }

        [Fact]
        public async Task GetProductById_ReturnsProduct_WhenProductExists()
        {
            // Arrange
            var client = factory.CreateClient();
            var productId = await AddTestProductAsync();

            // Act
            var response = await client.GetAsync($"/api/v1/products/id?id={productId}");
            var product = await response.Content.ReadFromJsonAsync<ProductDto>();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            product.Should().NotBeNull();
            product!.Id.Should().Be(productId);
        }

        [Fact]
        public async Task GetProductById_ReturnsNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var client = factory.CreateClient();
            var nonExistentId = Guid.NewGuid();

            // Act
            var response = await client.GetAsync($"/api/v1/products/{nonExistentId}");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        public void Dispose()
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Dispose();
        }

        private async Task<Guid> AddTestProductAsync()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product",
                Price = 10.99f,
                Quantity = 5,
                Category = "Test Category",
                Tva = 20,
                DiscountPercent = 10
            };

            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return product.Id;
        }
    }
}
