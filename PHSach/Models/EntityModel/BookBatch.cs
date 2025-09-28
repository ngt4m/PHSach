namespace PHSach.Models.EntityModel
{
    public class BookBatch
    {
        public string BookBatchId { get; set; } = Guid.NewGuid().ToString();
        public string BatchId { get; set; }
        public string? BookCode { get; set; }
        public string Title { get; set; }
        public string UnitOfMeasure { get; set; } = "cuốn";
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        // Navigation
        public Batch Batch { get; set; }
        public ICollection<Allocation> Allocations { get; set; } = new List<Allocation>();
    }
}
