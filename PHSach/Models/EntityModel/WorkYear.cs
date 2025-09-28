namespace PHSach.Models.EntityModel
{
    public class WorkYear
    {
        public string WorkYearId { get; set; } = Guid.NewGuid().ToString();
        public int Year { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<UnitBudget> UnitBudgets { get; set; } = new List<UnitBudget>();
        public ICollection<Batch> Batches { get; set; } = new List<Batch>();
    }
}
