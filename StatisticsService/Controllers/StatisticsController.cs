using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Statistics.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatisticsController : ControllerBase
    {
        private readonly IStatisticService _statisticService;

        public StatisticsController(IStatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserInfo(int userId)
        {
            var completedTasks = await _statisticService.GetCompletedTaskCount(userId);
            var pendingTasks = await _statisticService.GetPendingTaskCount(userId);
            var tasksByPriority = await _statisticService.GetTaskCountByPriority(userId);

            var response = new
            {
                Statistics = new
                {
                    CompletedTasks = completedTasks,
                    PendingTasks = pendingTasks,
                    TasksByPriority = tasksByPriority
                }
            };

            return Ok(response);
        }
    }
}
