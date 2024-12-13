using Core.Interfaces;
using Core.DataBaseContext;

namespace Game.Repositories
{
    public class GamificationService : IGamificationService
    {
        private readonly AppDbContext _context;

        public GamificationService(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddPointsAsync(int userId, int points)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.Score += points;
                await _context.SaveChangesAsync();
            }
        }
    }
}
