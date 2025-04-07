using TodoList.Domain.Interfaces;

namespace TodoList.Domain.Repositories
{
    public class MockTodoListRepository : ITodoListRepository
    {
        private int _nextId = 1;
        private readonly List<string> _categories = new() { "Work", "Personal", "Shopping", "Urgent" };

        public int GetNextId()
        {
            return _nextId++;
        }

        public List<string> GetAllCategories()
        {
            return _categories;
        }
    }
}