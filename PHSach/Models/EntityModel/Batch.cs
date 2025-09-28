namespace PHSach.Models.EntityModel
{
    public class Batch
    {
        public string BatchId { get; set; } = Guid.NewGuid().ToString();
        public string WorkYearId { get; set; }
        public string BatchName { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation
        public WorkYear WorkYear { get; set; }
        public ICollection<BookBatch> BookBatches { get; set; } = new List<BookBatch>();
        public ICollection<Allocation> Allocations { get; set; } = new List<Allocation>();
    }
}
