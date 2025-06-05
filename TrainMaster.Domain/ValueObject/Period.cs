namespace TrainMaster.Domain.ValueObject
{
    public class Period
    {
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }

        // Construtor vazio necessário para o model binder
        public Period() { }

        public Period(DateTime startDate, DateTime dueDate)
        {
            //if (dueDate < startDate)
            //    throw new ArgumentException("Due date must be after start date");

            StartDate = startDate;
            DueDate = dueDate;
        }
    }
}