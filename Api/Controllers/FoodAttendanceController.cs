using Common.ViewModels.FoodAttendances;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodAttendanceController : ControllerBase
    {

        private readonly IFoodAttendanceRepository _repository;

        public FoodAttendanceController(IFoodAttendanceRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // Get All
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var records = await _repository.GetAllAsync();

            var result = records.Select(x => new FoodAttendanceResponseVM
            {
                FoodAttendanceId = x.FoodAttendanceId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                Date = x.Date,
                MealType = x.MealType
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

            var result = new FoodAttendanceResponseVM
            {
                FoodAttendanceId = record.FoodAttendanceId,
                GirlId = record.GirlId,
                GirlName = record.Girl?.Name,
                Date = record.Date,
                MealType = record.MealType
            };

            return Ok(result);
        }

        // =========================
        // Mark Meal Attendance
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(CreateFoodAttendanceVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool alreadyTaken = await _repository.HasGirlTakenMealAsync(
                model.GirlId,
                model.Date,
                model.MealType);

            if (alreadyTaken)
                return BadRequest("Meal already marked for this girl.");

            var entity = new FoodAttendance
            {
                GirlId = model.GirlId,
                Date = model.Date,
                MealType = model.MealType
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Meal attendance marked successfully");
        }

        // =========================
        // Today Attendance
        // =========================
        [HttpGet("today")]
        public async Task<IActionResult> GetTodayAttendance()
        {
            var records = await _repository.GetTodayFoodAttendanceAsync();

            var result = records.Select(x => new FoodAttendanceResponseVM
            {
                FoodAttendanceId = x.FoodAttendanceId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                Date = x.Date,
                MealType = x.MealType
            });

            return Ok(result);
        }

        // =========================
        // Monthly Report
        // =========================
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthly(int month, int year)
        {
            var records = await _repository.GetMonthlyFoodAttendanceAsync(month, year);

            var result = records.Select(x => new FoodAttendanceResponseVM
            {
                FoodAttendanceId = x.FoodAttendanceId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                Date = x.Date,
                MealType = x.MealType
            });

            return Ok(result);
        }

        // =========================
        // Girl Report
        // =========================
        [HttpGet("girl/{girlId}")]
        public async Task<IActionResult> GetGirlReport(int girlId)
        {
            var records = await _repository.GetGirlFoodAttendanceAsync(girlId);

            var result = records.Select(x => new FoodAttendanceResponseVM
            {
                FoodAttendanceId = x.FoodAttendanceId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                Date = x.Date,
                MealType = x.MealType
            });

            return Ok(result);
        }

    }
}
