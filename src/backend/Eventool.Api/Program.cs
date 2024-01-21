using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddSingleton(new MongoClient("").GetDatabase(""));

var app = builder.Build();
app.Run();
