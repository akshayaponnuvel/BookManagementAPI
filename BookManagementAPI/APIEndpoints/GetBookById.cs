using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using BookManagementAPI.Data;
using BookManagementAPI.Models;

namespace BookManagementAPI.APIEndpoints
{
    public class GetBookByIdEndpoint : Endpoint<GetBookByIdEndpoint.Request, Book>
    {
        private readonly BookDbContext _dbContext;

        public GetBookByIdEndpoint(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Get("/api/books/{id:int}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var book = await _dbContext.Books.FindAsync(new object[] { req.Id }, ct);

            if (book == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendAsync(book);
        }

        public class Request
        {
            public int Id { get; set; }
        }
    }
}
