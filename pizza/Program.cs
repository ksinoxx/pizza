using Microsoft.AspNetCore.Mvc;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();

List<Pizza> repo = [];

app.MapGet("/", () => repo);
app.MapPost("/", (Pizza dto) => 
{ 
    dto.Id = Guid.NewGuid();
    repo.Add(dto);
});
app.MapPut("/", ([FromQuery] Guid id, UpdatePizzaDTO dto) =>
{
Pizza buffer = repo.Find(p => p.Id == id);
if (buffer == null)
{ return Results.NotFound(); }
buffer.Name = dto.name;
buffer.Sausepizza = dto.sausepizza;
buffer.Cheese = dto.cheeze;
buffer.Firstingredient = dto.firstingredient;
buffer.Secondingredient = dto.secondingredient;
buffer.Thirdingredien = dto.thirdingredien;
buffer.Prise = dto.prise;
return Results.Json(buffer);
});
app.MapDelete("/", ([FromQuery] Guid id) =>
{
Pizza buffer = repo.Find(p => p.Id == id);
repo.Remove(buffer);
});
app.Run();


public class Pizza
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string? Sausepizza { get; set; } = "null";
    public string? Cheese { get; set; } = "null";
    public string? Firstingredient { get; set; } = "null";
    public string? Secondingredient { get; set; } = "null";
    public string? Thirdingredien { get; set; } = "null";
    public int Prise { get; set; }
    public DateOnly Datenow { get; set; }
};

record class UpdatePizzaDTO(string name, string sausepizza, string cheeze, string firstingredient, string secondingredient, string thirdingredien, int prise);
