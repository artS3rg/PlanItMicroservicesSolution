using Core.DTOs;

namespace Core.Interfaces
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasksAsync();
        Task<TaskDto> GetTaskByIdAsync(int taskId);
        Task AddTaskAsync(CreateTaskDto taskDto);
        Task UpdateTaskAsync(UpdateTaskDto taskDto);
        Task DeleteTaskAsync(int taskId);
        Task CompleteTaskAsync(int taskId);
    }
}
