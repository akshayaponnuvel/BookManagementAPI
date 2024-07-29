using FastEndpoints;
using BookManagementAPI.Data;
using BookManagementAPI.Models;

namespace BookManagementAPI.APIEndpoints
{
    public class AddBookEndpoint : Endpoint<AddBookEndpoint.Request, AddBookEndpoint.Response>
    {
        private readonly BookDbContext _dbContext;

        public AddBookEndpoint(BookDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override void Configure()
        {
            Post("/api/books");
            AllowAnonymous();
            Options(x => x
                .WithTags("Books")
                .Produces<Response>(201)
                .ProducesProblem(400));
        }

        public override async Task HandleAsync(Request req, CancellationToken ct)
        {
            var book = new Book
            {
                Title = req.Title,
                Author = req.Author,
                PublicationYear = req.PublicationYear,
                ISBN = req.ISBN
            };

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync(ct);

            var response = new Response
            {
                Id = book.Id,
                Message = "Book added successfully."
            };

            await SendAsync(response, 201, ct);
        }

        public class Request
        {
            public required string Title { get; set; }
            public required string Author { get; set; }
            public int PublicationYear { get; set; }
            public string? ISBN { get; set; }
        }

        public new class Response
        {
            public int Id { get; set; }
            public string Message { get; set; } = string.Empty;
        }
    }
}
