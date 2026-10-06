using Common.ViewModels.Girls;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GirlController : ControllerBase
    {
        private readonly IGirlRepository _repository;

        public GirlController(IGirlRepository repository)
        {
            _repository = repository;
        }

        // ==========================
        // Get All
        // ==========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var girls = await _repository.GetAllAsync();

            var result = girls.Select(x => new GirlResponseVM
            {
                GirlId = x.GirlId,
                Name = x.Name,
                Phone = x.Phone,
                GuardianName = x.GuardianName,
                GuardianPhone = x.GuardianPhone,
                RoomId = x.RoomId,
                RoomName = x.Room?.RoomNumber,
                IsActive = x.IsActive
            });

            return Ok(result);
        }

        // ==========================
        // Get By Id
        // ==========================
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var girl = await _repository.GetByIdAsync(id);

            if (girl == null)
                return NotFound();

            var result = new GirlResponseVM
            {
                GirlId = girl.GirlId,
                Name = girl.Name,
                Phone = girl.Phone,
                GuardianName = girl.GuardianName,
                GuardianPhone = girl.GuardianPhone,
                RoomId = girl.RoomId,
                RoomName = girl.Room?.RoomNumber,
                IsActive = girl.IsActive
            };

            return Ok(result);
        }

        // ==========================
        // Create
        // ==========================
        [HttpPost]
        public async Task<IActionResult> Create(CreateGirlVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new Girl
            {
                Name = model.Name,
                Phone = model.Phone,
                GuardianName = model.GuardianName,
                GuardianPhone = model.GuardianPhone,
                RoomId = model.RoomId,
                IsActive = model.IsActive
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Girl registered successfully");
        }

        // ==========================
        // Update
        // ==========================
        [HttpPut]
        public async Task<IActionResult> Update(UpdateGirlVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var girl = await _repository.GetByIdAsync(model.GirlId);

            if (girl == null)
                return NotFound();

            girl.Name = model.Name;
            girl.Phone = model.Phone;
            girl.GuardianName = model.GuardianName;
            girl.GuardianPhone = model.GuardianPhone;
            girl.RoomId = model.RoomId;
            girl.IsActive = model.IsActive;

            _repository.Update(girl);
            await _repository.SaveAsync();

            return Ok("Girl updated successfully");
        }

        // ==========================
        // Delete
        // ==========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var girl = await _repository.GetByIdAsync(id);

            if (girl == null)
                return NotFound();

            _repository.Remove(girl);
            await _repository.SaveAsync();

            return Ok("Girl deleted successfully");
        }

        // ==========================
        // Girls By Room
        // ==========================
        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetGirlsByRoom(int roomId)
        {
            var girls = await _repository.GetGirlsByRoomAsync(roomId);

            var result = girls.Select(x => new GirlResponseVM
            {
                GirlId = x.GirlId,
                Name = x.Name,
                Phone = x.Phone,
                RoomId = x.RoomId,
                IsActive = x.IsActive
            });

            return Ok(result);
        }

        // ==========================
        // Active Girls
        // ==========================
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveGirls()
        {
            var girls = await _repository.GetActiveGirlsAsync();

            var result = girls.Select(x => new GirlResponseVM
            {
                GirlId = x.GirlId,
                Name = x.Name,
                Phone = x.Phone,
                RoomId = x.RoomId,
                RoomName = x.Room?.RoomNumber,
                IsActive = x.IsActive
            });

            return Ok(result);
        }

        // ==========================
        // Girl Full Details
        // ==========================
        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetGirlDetails(int id)
        {
            var girl = await _repository.GetGirlWithDetailsAsync(id);

            if (girl == null)
                return NotFound();

            var result = new GirlDetailsVM
            {
                GirlId = girl.GirlId,
                Name = girl.Name,
                Phone = girl.Phone,
                GuardianName = girl.GuardianName,
                GuardianPhone = girl.GuardianPhone,
                RoomId = girl.RoomId,
                RoomName = girl.Room?.RoomNumber,
                IsActive = girl.IsActive,

                TotalEntryLogs = girl.EntryLogs?.Count ?? 0,
                TotalMarketVisits = girl.MarketVisits?.Count ?? 0,
                TotalFoodAttendances = girl.FoodAttendances?.Count ?? 0,
                TotalHostelAttendance = girl.HostelAttendances?.Count ?? 0
            };

            return Ok(result);
        }
    }
}
