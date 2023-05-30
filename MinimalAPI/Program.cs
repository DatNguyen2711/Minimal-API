var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var books = new List<Book>
{
     new Book { Id=1,Title="co cai db",Author="cc"},
    new Book { Id = 2, Title = "co cai dbasdsad", Author = "dacc" },
    new Book { Id = 3, Title = "co casdasdai db", Author = "cc" }
};
app.UseHttpsRedirection();
app.MapGet("/book", () =>
{
    return books;

});
app.MapGet("/book/{id}", (int id) =>
{
    var book = books.Find(x => x.Id == id);
    if (book is null)
    {
        return Results.NotFound("Dell co dau");
    }
    return Results.Ok(book);

});
app.MapPost("/book", (Book book) =>
{
    books.Add(book);
    return books;
});

app.MapPut("/book/{id}", (Book updatebook,int id) =>
{
   var book = books.Find(b=> b.Id == id);
    if(book is null)
    {
        return Results.NotFound("dell co");
    }

    book.Title=updatebook.Title;
    book.Author=updatebook.Author;

    return Results.Ok(books);
});

app.MapDelete("/book/{id}", ( int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
    {
        return Results.NotFound("dell co");
    }

    books.Remove(book);

    return Results.Ok(books);
});

app.Run();


class Book
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Author { get; set; }
}
