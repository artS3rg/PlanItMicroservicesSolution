using Core.DTOs;

namespace Core.Interfaces
{
    public interface ISubtaskService
    {
        Task<SubtaskDto> GetSubtaskByIdAsync(int subtaskId);
        Task<IEnumerable<SubtaskDto>> GetAllSubtasksAsync(int taskId);
        Task AddSubtaskAsync(SubtaskDto subtaskDto);
        Task UpdateSubtaskAsync(SubtaskDto subtaskDto);
        Task DeleteSubtaskAsync(int subtaskId);
        Task CompleteSubtaskAsync(int subtaskId);
    }
}
