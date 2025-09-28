namespace PHSach.Models.EntityModel
{
    public class Unit
    {
        public string UnitId { get; set; } = Guid.NewGuid().ToString();
        public string? UnitCode { get; set; }
        public string UnitName { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? Phone { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation
        public ICollection<UnitBudget> UnitBudgets { get; set; } = new List<UnitBudget>();
    }
}
