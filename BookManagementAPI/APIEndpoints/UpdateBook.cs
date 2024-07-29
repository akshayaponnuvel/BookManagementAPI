using FastEndpoints;
using BookManagementAPI.Data;
using BookManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.APIEndpoints
{
    public class UpdateBookEndpoint : Endpoint<UpdateBookEndpoint.Request, UpdateBookEndpoint.Response>
    {
        private readonly BookDbContext _dbContext;

        public UpdateBookEndpoint(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Put("/api/books/{id:int}");
            AllowAnonymous();
            Options(x => x
                .WithTags("Books")
                .Produces<Response>()
                .ProducesProblem(404));
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var book = await _dbContext.Books.FindAsync(new object[] { req.Id }, ct);

            if (book == null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            book.Title = req.Title;
            book.Author = req.Author;
            book.PublicationYear = req.PublicationYear;
            book.ISBN = req.ISBN;

            await _dbContext.SaveChangesAsync(ct);

            var response = new Response
            {
                Message = "Book updated successfully."
            };

            await SendAsync(response, cancellation: ct);
        }

        public class Request
        {
            public int Id { get; set; }
            public required string Title { get; set; }
            public required string Author { get; set; }
            public int PublicationYear { get; set; }
            public string? ISBN { get; set; }
        }

        public new class Response
        {
            public string Message { get; set; } = string.Empty;
        }
    }
}
