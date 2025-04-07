using TodoList.Domain.Interfaces;

namespace TodoList.Domain.Services
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine($"[LOG] {DateTime.Now:HH:mm:ss} - {message}");
        }
    }
}