namespace TodoList.Server.Models
{
    public class AddProgressionRequest
    {
        /// <example>1</example>
        public int Id { get; set; }

        /// <example>2025-04-01T00:00:00</example>
        public DateTime Date { get; set; }

        /// <example>50</example>
        public decimal Percent { get; set; }
    }
}
