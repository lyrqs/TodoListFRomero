using TodoList.Domain.Aggregates;
using TodoList.Domain.Interfaces;
using TodoList.Domain.Repositories;
using TodoList.Domain.Services;

// Setup
ITodoListRepository repository = new MockTodoListRepository();
ILogger logger = new ConsoleLogger();
var todoList = new TodoListAggregate(repository,logger);

// Add item
int id = repository.GetNextId();
todoList.AddItem(id, "Complete Project Report", "Finish the final report for the project", "Work");

// Register progressions
todoList.RegisterProgression(id, new DateTime(2025, 3, 18), 30);
todoList.RegisterProgression(id, new DateTime(2025, 3, 19), 50);
todoList.RegisterProgression(id, new DateTime(2025, 3, 20), 20);

// Print result
todoList.PrintItems();