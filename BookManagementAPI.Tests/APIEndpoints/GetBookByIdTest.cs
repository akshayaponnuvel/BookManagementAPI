using System.Net.Http.Json;
using BookManagementAPI;
using BookManagementAPI.Data;
using BookManagementAPI.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace BookManagementAPI.Tests.Endpoints
{
    public class GetBookByIdTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public GetBookByIdTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Testing");
                builder.ConfigureServices(services =>
                {
                    services.AddDbContext<BookDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDatabase");
                    });

                    var serviceProvider = services.BuildServiceProvider();
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<BookDbContext>();
                        dbContext.Database.EnsureCreated();
                        SeedDatabase(dbContext);
                    }
                });
            });
        }

        private void SeedDatabase(BookDbContext context)
        {
            context.Books.Add(new Book { Id = 1, Title = "Test Book", Author = "Test Author" });
            context.SaveChanges();
        }

        [Fact]
        public async Task GetBookById_ShouldReturnBook_WhenBookExists()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/books/1");

            // Assert
            response.EnsureSuccessStatusCode();
            var book = await response.Content.ReadFromJsonAsync<Book>();
            Assert.NotNull(book);
            Assert.Equal("Test Book", book.Title);
            Assert.Equal("Test Author", book.Author);
        }

        [Fact]
        public async Task GetBookById_ShouldReturnNotFound_WhenBookDoesNotExist()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/books/99");

            // Assert
            Assert.Equal(404, (int)response.StatusCode);
        }
    }
}
