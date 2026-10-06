using Common.ViewModels.HostelAttendances;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelAttendanceController : ControllerBase
    {
        private readonly IHostelAttendanceRepository _repository;

        public HostelAttendanceController(IHostelAttendanceRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // Get All Attendance
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _repository.GetAllAsync();

            var result = records.Select(x => new HostelAttendanceResponseVM
            {
                HostelAttendanceId = x.HostelAttendanceId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                Date = x.Date,
                IsPresent = x.IsPresent
            });

            return Ok(result);
        }

        // =========================
        // Get By Id
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var record = await _repository.GetByIdAsync(id);

            if (record == null)
                return NotFound();

            var result = new HostelAttendanceResponseVM
            {
                HostelAttendanceId = record.HostelAttendanceId,
                GirlId = record.GirlId,
                GirlName = record.Girl?.Name,
                Date = record.Date,
                IsPresent = record.IsPresent
            };

            return Ok(result);
        }

        // =========================
        // Mark Attendance
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(CreateHostelAttendanceVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool alreadyMarked = await _repository.HasGirlMarkedAttendanceAsync(
                model.GirlId,
                model.Date);

            if (alreadyMarked)
                return BadRequest("Attendance already marked for this girl.");

            var entity = new HostelAttendance
            {
                GirlId = model.GirlId,
                Date = model.Date,
                IsPresent = model.IsPresent
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Attendance marked successfully");
        }

        // =========================
        // Today Attendance
        // =========================
        [HttpGet("today")]
        public async Task<IActionResult> GetTodayAttendance()
        {
            var records = await _repository.GetTodayAttendanceAsync();

            var result = records.Select(x => new HostelAttendanceResponseVM
            {
                HostelAttendanceId = x.HostelAttendanceId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                Date = x.Date,
                IsPresent = x.IsPresent
            });

            return Ok(result);
        }

        // =========================
        // Monthly Attendance
        // =========================
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthly(int month, int year)
        {
            var records = await _repository.GetMonthlyAttendanceAsync(month, year);

            var result = records.Select(x => new HostelAttendanceResponseVM
            {
                HostelAttendanceId = x.HostelAttendanceId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                Date = x.Date,
                IsPresent = x.IsPresent
            });

            return Ok(result);
        }

        // =========================
        // Girl Attendance Report
        // =========================
        [HttpGet("girl/{girlId}")]
        public async Task<IActionResult> GetGirlAttendance(int girlId)
        {
            var records = await _repository.GetGirlAttendanceAsync(girlId);

            var result = records.Select(x => new HostelAttendanceResponseVM
            {
                HostelAttendanceId = x.HostelAttendanceId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                Date = x.Date,
                IsPresent = x.IsPresent
            });

            return Ok(result);
        }

        // =========================
        // Absent Girls
        // =========================
        [HttpGet("absent")]
        public async Task<IActionResult> GetAbsentGirls(DateTime date)
        {
            var records = await _repository.GetAbsentGirlsAsync(date);

            var result = records.Select(x => new HostelAttendanceResponseVM
            {
                HostelAttendanceId = x.HostelAttendanceId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                Date = x.Date,
                IsPresent = x.IsPresent
            });

            return Ok(result);
        }
    }
}
