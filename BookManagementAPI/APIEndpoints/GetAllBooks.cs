using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Data;
using BookManagementAPI.Models;

namespace BookManagementAPI.APIEndpoints
{
    public class GetAllBooksEndpoint : EndpointWithoutRequest<List<Book>>
    {
        private readonly BookDbContext _dbContext;

        public GetAllBooksEndpoint(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/api/books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var books = await _dbContext.Books.ToListAsync(ct);
            await SendAsync(books);
        }
    }
}
