﻿namespace Core.DTOs
{
    public class SubtaskDto
    {
        public int Id { get; set; }
        public int TaskId { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
