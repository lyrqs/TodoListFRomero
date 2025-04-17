using TodoList.Domain.Aggregates;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Interfaces.TodoList.Domain.Interfaces;
using TodoList.Domain.Repositories;
using TodoList.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Register dependencies
builder.Services.AddSingleton<ITodoListRepository, MockTodoListRepository>();
builder.Services.AddSingleton<ITodoList, TodoListAggregate>();
builder.Services.AddSingleton<TodoList.Domain.Interfaces.ILogger, ConsoleLogger>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod()
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();
