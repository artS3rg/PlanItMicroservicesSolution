namespace Core.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = "Title";
        public string Description { get; set; } = "Desc";
        public bool IsCompleted { get; set; }
        public int Priority { get; set; }
        public List<Subtask> Subtasks { get; set; } = new List<Subtask>();
    }
}
