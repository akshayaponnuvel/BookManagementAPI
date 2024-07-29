# BookManagementAPI

Setup Instructions

Prerequisites

.NET 8.0 or higher
Visual Studio or Visual Studio Code
Git (to clone the repository)

Repository link - https://github.com/akshayaponnuvel/BookManagementAPI.git

To install all the packages - dotnet restore
To build the project locally - dotnet build 
TO run the project - dotnet run
To run tests - dotnet test 

API Documentation

Base URL to run in Postman- https://localhost:7227/api

Endpoints 
1.GetAllBooks

Method - GET
URL - https://localhost:7227/api/books
Response body

[
    {
        "id": 1,
        "title": "The Great Gatsby",
        "author": "F. Scott Fitzgerald",
        "publicationYear": 1925,
        "isbn": "9780743273565"
    },
    {
        "id": 4,
        "title": "The Kite Runner",
        "author": "Khaled Hosseini",
        "publicationYear": 2003,
        "isbn": "978-2-74329-895-3"
    },
    {
        "id": 5,
        "title": "Thousand splendid suns",
        "author": "Khaled Hosseini",
        "publicationYear": 2007,
        "isbn": "978-2-74329-8975-3"
    },
    {
        "id": 6,
        "title": "Ikigai",
        "author": "Hector and Francesc",
        "publicationYear": 2007,
        "isbn": "978-1-786-33089-5"
    }
]
2.GetBookById

Method - GET
URL - https://localhost:7227/api/books/{id}
URL for this response - https://localhost:7227/api/books/4

Response body

{
    "id": 4,
    "title": "The Kite Runner",
    "author": "Khaled Hosseini",
    "publicationYear": 2003,
    "isbn": "978-2-74329-895-3"
}

3.AddBook

Method - POST
URL - https://localhost:7227/api/books
Request Body 
{
    "Title": "Ikigai",
    "Author": "Hector and Francesc",
    "PublicationYear": 2007,
    "ISBN": "978-1-786-33089-5"
}

Response Body

{
    "id": 6,
    "message": "Book added successfully."
}

4.UpdateBook

Method - PUT
URL - https://localhost:7227/api/books/{id}
Request body

{
    "Id" : 6,
    "Title": "Ikigai",
    "Author": "Hector and Francesc",
    "PublicationYear": 2016,
    "ISBN": "978-1-786-33089-5"
}

Response Body

{
    "message": "Book updated successfully."
}

5.DeleteBook

Method - Delete
URL - https://localhost:7227/api/books

Request body

{
    "Id" : 6
}

Response Body

{
    "message": "Book deleted successfully."
}

Assumptions and Design Decisions

RESTful API: The API follows RESTful conventions for managing resources.

Unit Testing : Test does not run.Encountering errors in unit testing. 

No Authentication: The current implementation does not include authentication or authorization. 

