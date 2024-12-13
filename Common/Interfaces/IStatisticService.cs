namespace Core.Interfaces
{
    public interface IStatisticService
    {
        Task<int> GetCompletedTaskCount(int userId);
        Task<int> GetPendingTaskCount(int userId);
        Task<Dictionary<int, int>> GetTaskCountByPriority(int userId);
    }
}
