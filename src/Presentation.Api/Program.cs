using Microsoft.AspNetCore.Http.HttpResults;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
    options.AddDefaultPolicy(
        policyBuilder => policyBuilder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Host.UseSystemd();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

var people = new List<string>
{
    "Ali",
    "Beatriz",
    "Charles",
    "Diya",
    "Eric",
    "Fatima",
    "Gabriel",
    "Hanna"
};

app.MapGet(
    "/api/people",
    Results<Ok<List<string>>, NotFound> (IConfiguration configuration)
        => TypedResults.Ok(people.OrderBy(x => Random.Shared.Next()).ToList()));

app.UseHttpsRedirection().UseCors();
app.Run();