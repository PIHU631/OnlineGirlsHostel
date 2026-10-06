using Common.ViewModels.EntryLogs;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntryLogController : ControllerBase
    {
        private readonly IEntryLogRepository _repository;

        public EntryLogController(IEntryLogRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // Get All Logs
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var logs = await _repository.GetAllAsync();

            var result = logs.Select(x => new EntryLogResponseVM
            {
                EntryLogId = x.EntryLogId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                OutTime = x.OutTime,
                InTime = x.InTime,
                Reason = x.Reason
            });

            return Ok(result);
        }

        // =========================
        // Get By Id
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var log = await _repository.GetByIdAsync(id);

            if (log == null)
                return NotFound();

            var result = new EntryLogResponseVM
            {
                EntryLogId = log.EntryLogId,
                GirlId = log.GirlId,
                GirlName = log.Girl?.Name,
                OutTime = log.OutTime,
                InTime = log.InTime,
                Reason = log.Reason
            };

            return Ok(result);
        }

        // =========================
        // Girl Exit (Create Log)
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(CreateEntryLogVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new EntryLog
            {
                GirlId = model.GirlId,
                OutTime = model.OutTime,
                Reason = model.Reason
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Girl exit recorded");
        }

        // =========================
        // Mark Return
        // =========================
        [HttpPut("return/{id}")]
        public async Task<IActionResult> MarkReturn(int id)
        {
            await _repository.MarkGirlReturnAsync(id);

            return Ok("Girl return recorded");
        }

        // =========================
        // Girls Currently Outside
        // =========================
        [HttpGet("outside")]
        public async Task<IActionResult> GetGirlsOutside()
        {
            var logs = await _repository.GetGirlsCurrentlyOutsideAsync();

            return Ok(logs);
        }

        // =========================
        // Today Logs
        // =========================
        [HttpGet("today")]
        public async Task<IActionResult> GetTodayLogs()
        {
            var logs = await _repository.GetTodayEntryLogsAsync();

            return Ok(logs);
        }

        // =========================
        // Monthly Report
        // =========================
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthly(int month, int year)
        {
            var logs = await _repository.GetMonthlyEntryLogsAsync(month, year);

            return Ok(logs);
        }
    }
}
