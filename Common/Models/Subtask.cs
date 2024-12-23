namespace Core.Models
{
    public class Subtask
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; } = "Title";
        public bool IsCompleted { get; set; }
    }
}
