namespace PHSach.Models.EntityModel
{
    public class Allocation
    {
        public string AllocationId { get; set; } = Guid.NewGuid().ToString();
        public string BatchId { get; set; }
        public string BookBatchId { get; set; }
        public string UnitBudgetId { get; set; }
        public int AllocatedQuantity { get; set; }
        public decimal AllocatedCost { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public Batch Batch { get; set; }
        public BookBatch BookBatch { get; set; }
        public UnitBudget UnitBudget { get; set; }
    }
}
