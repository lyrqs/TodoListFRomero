namespace TodoList.Domain.Models
{
    /// Represents a todo item in the system.
    public class TodoItem
    {
        public int Id { get; }
        public string Title { get; }
        public string Description { get; private set; }
        public string Category { get; }
        private readonly List<Progression> _progressions = new();
        public IReadOnlyList<Progression> Progressions => _progressions.AsReadOnly();

        public bool IsCompleted => GetTotalProgress() == 100;

        public TodoItem(int id, string title, string description, string category)
        {
            Id = id;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Category = category ?? throw new ArgumentNullException(nameof(category));
        }

        public void UpdateDescription(string newDescription)
        {
            if (GetTotalProgress() > 50)
                throw new InvalidOperationException("Cannot update item with more than 50% progress.");

            Description = newDescription ?? throw new ArgumentNullException(nameof(newDescription));
        }

        public void AddProgression(Progression progression)
        {
            if (_progressions.Any() && progression.Date <= _progressions.Max(p => p.Date))
                throw new InvalidOperationException("Progression date must be greater than existing ones.");

            var totalAfterAdding = GetTotalProgress() + progression.Percent;
            if (totalAfterAdding > 100)
                throw new InvalidOperationException("Total progress cannot exceed 100%.");

            _progressions.Add(progression);
        }

        public void Remove()
        {
            if (GetTotalProgress() > 50)
                throw new InvalidOperationException("Cannot remove item with more than 50% progress.");
        }

        public decimal GetTotalProgress()
        {
            return _progressions.Sum(p => p.Percent);
        }
    }
}