using FastEndpoints;
using BookManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace BookManagementAPI.APIEndpoints
{
    public class DeleteBookEndpoint : Endpoint<DeleteBookEndpoint.Request, DeleteBookEndpoint.Response>
    {
        private readonly BookDbContext _dbContext;

        public DeleteBookEndpoint(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Delete("/api/books");
            AllowAnonymous();
            Options(x => x
                .WithTags("Books")
                .Accepts<Request>("application/json")
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

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync(ct);

            var response = new Response
            {
                Message = "Book deleted successfully."
            };

            await SendAsync(response, cancellation: ct);
        }

        public class Request
        {
            public int Id { get; set; }
        }

        public new class Response
        {
            public string Message { get; set; } = string.Empty;
        }
    }
}
