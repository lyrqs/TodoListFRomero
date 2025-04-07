namespace TodoList.Domain.Models
{
    public class Progression
    {
        public DateTime Date { get; }
        public decimal Percent { get; }

        public Progression(DateTime date, decimal percent)
        {
            if (percent <= 0 || percent >= 100)
                throw new ArgumentOutOfRangeException(nameof(percent), "Percent must be greater than 0 and less than 100.");

            Date = date;
            Percent = percent;
        }
    }
}