﻿namespace Core.DTOs
{
    public class TaskDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsCompleted { get; set; }
        public int Priority { get; set; }
        public List<SubtaskDto> Subtasks { get; set; }
    }
}
