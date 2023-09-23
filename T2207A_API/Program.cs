using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


//Add CORS policy access
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        //policy.WithOrigins("");
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//connect db
string connectionString = builder.Configuration.GetConnectionString("API");
builder.Services.AddDbContext<T2207A_API.Entities.T2207aApiContext>(
        options => options.UseSqlServer(connectionString)
    );
//end
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

