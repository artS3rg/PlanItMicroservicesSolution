namespace Core.DTOs
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = "Title";
        public string Description { get; set; } = "Desc";
        public int Priority { get; set; }
        public bool IsCompleted { get; set; }
    }
}
