using KDSB20230901;

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

app.UseHttpsRedirection();

var producto = new List<Producto>();

//Get
app.MapGet("/producto", () =>
{
    return producto;
});

app.MapGet("/producto/{id}", (int id) =>
{
    var product = producto.FirstOrDefault(c => c.Id == id);
    return product;
});

//Post
app.MapPost("/producto", (Producto producto1) =>
{
    producto.Add(producto1);
    return Results.Ok();

});

//put
app.MapPut("/producto/{id}", (int id, Producto product) =>
{
    var existingProduct = producto.FirstOrDefault(c => c.Id == id);
    if (existingProduct != null)
    {
        existingProduct.Name = product.Name;
        existingProduct.Price = product.Price;
        existingProduct.cantidad = product.cantidad;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }

});

app.MapDelete("/producto/{id}", (int id) =>
{
    var existingProduct = producto.FirstOrDefault(c => c.Id == id);
    if (existingProduct != null)
    {
        producto.Remove(existingProduct);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

