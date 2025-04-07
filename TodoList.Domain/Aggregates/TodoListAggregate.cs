using TodoList.Domain.Interfaces;
using TodoList.Domain.Interfaces.TodoList.Domain.Interfaces;
using TodoList.Domain.Models;

namespace TodoList.Domain.Aggregates
{
    public class TodoListAggregate : ITodoList
    {
        private readonly ITodoListRepository _repository;
        private readonly Dictionary<int, TodoItem> _items = new();
        private readonly ILogger _logger;

        public TodoListAggregate(ITodoListRepository repository, ILogger logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public void AddItem(int id, string title, string description, string category)
        {
            if (!_repository.GetAllCategories().Contains(category))
                throw new ArgumentException("Invalid category", nameof(category));

            if (_items.ContainsKey(id))
                throw new ArgumentException("Item with this ID already exists.", nameof(id));

            var item = new TodoItem(id, title, description, category);
            _items.Add(id, item);
            _logger.Log($"Item added: {title} (ID: {id})");
        }

        public void UpdateItem(int id, string description)
        {
            var item = GetItemById(id);
            item.UpdateDescription(description);
            _logger.Log($"Item updated: ID {id}, new description: {description}");
        }

        public void RemoveItem(int id)
        {
            var item = GetItemById(id);
            item.Remove();
            _items.Remove(id);
            _logger.Log($"Item removed: ID {id}");
        }

        public void RegisterProgression(int id, DateTime dateTime, decimal percent)
        {
            var item = GetItemById(id);
            var progression = new Progression(dateTime, percent);
            item.AddProgression(progression);
            _logger.Log($"Progression registered: Item {id}, {percent}% on {dateTime:yyyy-MM-dd}");

            if (item.IsCompleted)
                _logger.Log($"Item {id} is now completed.");
        }

        public void PrintItems()
        {
            foreach (var item in _items.Values.OrderBy(i => i.Id))
            {
                Console.WriteLine($"{item.Id}) {item.Title} - {item.Description} ({item.Category}) Completed:{item.IsCompleted}");

                decimal accumulated = 0;
                foreach (var p in item.Progressions.OrderBy(p => p.Date))
                {
                    accumulated += p.Percent;
                    Console.WriteLine($"{p.Date.ToShortDateString()} - {accumulated}%\t {BuildProgressBar(accumulated)}");
                }
                Console.WriteLine();
            }
        }

        private TodoItem GetItemById(int id)
        {
            if (!_items.TryGetValue(id, out var item))
                throw new KeyNotFoundException($"Item with ID {id} not found.");
            return item;
        }

        private string BuildProgressBar(decimal percent)
        {
            int totalBars = 50;
            int filledBars = (int)(percent / 100 * totalBars);
            return "|" + new string('O', filledBars).PadRight(totalBars) + "|";
        }
    }
}