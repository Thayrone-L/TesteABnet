using BackEndTesteABnet.Entities.Enums;

namespace BackEndTesteABnet.Entities
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool Overdue =>  DueDate.HasValue && DueDate.Value < DateTime.UtcNow;
    }
}
