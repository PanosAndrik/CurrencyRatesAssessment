var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(); // API controllers
builder.Services.AddEndpointsApiExplorer(); // For Swagger
builder.Services.AddSwaggerGen(); // Swagger support
builder.Services.AddDbContext<AppDbContext>(); // database

var app = builder.Build();

// database
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Console.WriteLine("Ensuring database schema is created...");
    context.Database.EnsureDeleted(); 
    context.Database.EnsureCreated(); 
    Console.WriteLine("Database schema created successfully.");
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // Disabled HTTPS redirection

app.UseAuthorization();

app.MapControllers(); 

app.Run();
