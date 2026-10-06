using Common.ViewModels.Hostels;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HostelController : ControllerBase
    {
        private readonly IHostelRepository _repository;

        public HostelController(IHostelRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // Get All Hostels
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var hostels = await _repository.GetAllAsync();

            var result = hostels.Select(x => new HostelResponseVM
            {
                HostelId = x.HostelId,
                HostelName = x.HostelName,
                Address = x.Address,
                City = x.City,
                WardenName = x.WardenName,
                WardenPhone = x.WardenPhone,
                IsActive = x.IsActive
            });

            return Ok(result);
        }

        // =========================
        // Get Hostel By Id
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var hostel = await _repository.GetByIdAsync(id);

            if (hostel == null)
                return NotFound();

            var result = new HostelResponseVM
            {
                HostelId = hostel.HostelId,
                HostelName = hostel.HostelName,
                Address = hostel.Address,
                City = hostel.City,
                WardenName = hostel.WardenName,
                WardenPhone = hostel.WardenPhone,
                IsActive = hostel.IsActive
            };

            return Ok(result);
        }

        // =========================
        // Create Hostel
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(CreateHostelVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new Hostel
            {
                HostelName = model.HostelName,
                Address = model.Address,
                City = model.City,
                WardenName = model.WardenName,
                WardenPhone = model.WardenPhone,
                IsActive = model.IsActive
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Hostel created successfully");
        }

        // =========================
        // Update Hostel
        // =========================
        [HttpPut]
        public async Task<IActionResult> Update(UpdateHostelVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var hostel = await _repository.GetByIdAsync(model.HostelId);

            if (hostel == null)
                return NotFound();

            hostel.HostelName = model.HostelName;
            hostel.Address = model.Address;
            hostel.City = model.City;
            hostel.WardenName = model.WardenName;
            hostel.WardenPhone = model.WardenPhone;
            hostel.IsActive = model.IsActive;

            _repository.Update(hostel);
            await _repository.SaveAsync();

            return Ok("Hostel updated successfully");
        }

        // =========================
        // Delete Hostel
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var hostel = await _repository.GetByIdAsync(id);

            if (hostel == null)
                return NotFound();

            _repository.Remove(hostel);
            await _repository.SaveAsync();

            return Ok("Hostel deleted successfully");
        }

        // =========================
        // Hostel With Rooms
        // =========================
        [HttpGet("rooms/{hostelId}")]
        public async Task<IActionResult> GetHostelWithRooms(int hostelId)
        {
            var hostel = await _repository.GetHostelWithRoomsAsync(hostelId);

            if (hostel == null)
                return NotFound();

            var result = new HostelDetailsVM
            {
                HostelId = hostel.HostelId,
                HostelName = hostel.HostelName,
                Address = hostel.Address,
                City = hostel.City,
                WardenName = hostel.WardenName,
                WardenPhone = hostel.WardenPhone,
                IsActive = hostel.IsActive,
                TotalRooms = hostel.Rooms?.Count ?? 0
            };

            return Ok(result);
        }

        // =========================
        // Hostel With Staff
        // =========================
        [HttpGet("staff/{hostelId}")]
        public async Task<IActionResult> GetHostelWithStaff(int hostelId)
        {
            var hostel = await _repository.GetHostelWithStaffAsync(hostelId);

            if (hostel == null)
                return NotFound();

            var result = new HostelDetailsVM
            {
                HostelId = hostel.HostelId,
                HostelName = hostel.HostelName,
                Address = hostel.Address,
                City = hostel.City,
                WardenName = hostel.WardenName,
                WardenPhone = hostel.WardenPhone,
                IsActive = hostel.IsActive,
                TotalStaff = hostel.Staffs?.Count ?? 0
            };

            return Ok(result);
        }

        // =========================
        // All Hostels With Rooms
        // =========================
        [HttpGet("withrooms")]
        public async Task<IActionResult> GetAllHostelsWithRooms()
        {
            var hostels = await _repository.GetAllHostelsWithRoomsAsync();

            var result = hostels.Select(x => new HostelDetailsVM
            {
                HostelId = x.HostelId,
                HostelName = x.HostelName,
                Address = x.Address,
                City = x.City,
                WardenName = x.WardenName,
                WardenPhone = x.WardenPhone,
                IsActive = x.IsActive,
                TotalRooms = x.Rooms?.Count ?? 0
            });

            return Ok(result);
        }
    }
}
