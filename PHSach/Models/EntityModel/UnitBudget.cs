namespace PHSach.Models.EntityModel
{
    public class UnitBudget
    {
        public string UnitBudgetId { get; set; } = Guid.NewGuid().ToString();
        public string UnitId { get; set; }
        public string WorkYearId { get; set; }
        public decimal InitialBudget { get; set; }
        public decimal RemainingBudget { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Unit Unit { get; set; }
        public WorkYear WorkYear { get; set; }
        public ICollection<Allocation> Allocations { get; set; } = new List<Allocation>();
    }
}
