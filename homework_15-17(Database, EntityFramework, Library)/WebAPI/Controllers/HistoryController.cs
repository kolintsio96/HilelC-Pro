using Common.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using AccessToDB;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryController : ControllerBase
    {
        private readonly IHistoryRepository _historyRepository;
        private readonly ILogger<HistoryController> _logger;

        public HistoryController(IHistoryRepository historyRepository, ILogger<HistoryController> logger)
        {
            this._historyRepository = historyRepository;
            this._logger = logger;
        }

        [AllowAnonymous]
        [HttpPost("add")]
        public async Task<IActionResult> Add(HistoryDto historyDto)
        {
            _logger.LogInformation($"Adding history begin process with history dto - {historyDto}");
            var history = new History()
            {
                BookId = historyDto.BookId,
                ReaderId = historyDto.ReaderId,
                TakeDate = historyDto.TakeDate,
                BookingTime = historyDto.BookingTime,
                ReturnDate = historyDto.ReturnDate,
            };

            await _historyRepository.AddHistory(history);
            return Ok(history);
        }

        [AllowAnonymous]
        [HttpGet("list")]
        public async Task<IActionResult> GetList([FromQuery] int? readerId = null)
        {
            _logger.LogInformation($"Get list of histories");
            List<History> histories;
            if (readerId != null)
            {
                histories = await _historyRepository.GetHistoryByReader(readerId);
            } else
            {
                histories = await _historyRepository.GetHistories();
            }
            if (histories.Count > 0)
                return Ok(histories);

            return NotFound("History empty!");
        }
    }
}
