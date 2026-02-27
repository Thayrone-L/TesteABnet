using BackEndTesteABnet.Entities.Enums;

namespace BackEndTesteABnet.Entities
{
    public class Assignment
    {
        int id { get; set; }
        string title { get; set; }
        string description { get; set; }
        Status status { get; set; }
        Priority priority{ get; set; }
        DateTime dueDate { get; set; }
        DateTime createdAt { get; set; }
        DateTime updatedAt { get; set; }
    }
}
