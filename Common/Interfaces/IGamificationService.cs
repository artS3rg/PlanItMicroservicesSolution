namespace Core.Interfaces
{
    public interface IGamificationService
    {
        Task AddPointsAsync(int userId, int points);
    }

}
