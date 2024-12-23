namespace Core.DTOs
{
    public class CreateTaskDto
    {
        public int UserId { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public int Priority { get; set; }
    }
}
