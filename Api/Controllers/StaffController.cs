using Common.Enums;
using Common.ViewModels.Staff;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffRepository _repository;

        public StaffController(IStaffRepository repository)
        {
            _repository = repository;
        }

        // ======================
        // Get All Staff
        // ======================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var staff = await _repository.GetAllAsync();

            var result = staff.Select(x => new StaffResponseVM
            {
                StaffId = x.StaffId,
                HostelId = x.HostelId,
                HostelName = x.Hostel?.HostelName,
                Name = x.Name,
                Role = x.Role,
                Phone = x.Phone
            });

            return Ok(result);
        }

        // ======================
        // Get Staff By Id
        // ======================
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var staff = await _repository.GetByIdAsync(id);

            if (staff == null)
                return NotFound();

            var result = new StaffResponseVM
            {
                StaffId = staff.StaffId,
                HostelId = staff.HostelId,
                HostelName = staff.Hostel?.HostelName,
                Name = staff.Name,
                Role = staff.Role,
                Phone = staff.Phone
            };

            return Ok(result);
        }

        // ======================
        // Create Staff
        // ======================
        [HttpPost]
        public async Task<IActionResult> Create(CreateStaffVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new Staff
            {
                HostelId = model.HostelId,
                Name = model.Name,
                Role = model.Role,
                Phone = model.Phone
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Staff created successfully");
        }

        // ======================
        // Update Staff
        // ======================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateStaffVM model)
        {
            var staff = await _repository.GetByIdAsync(id);

            if (staff == null)
                return NotFound();

            staff.HostelId = model.HostelId;
            staff.Name = model.Name;
            staff.Role = model.Role;
            staff.Phone = model.Phone;

            _repository.Update(staff);
            await _repository.SaveAsync();

            return Ok("Staff updated successfully");
        }

        // ======================
        // Delete Staff
        // ======================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var staff = await _repository.GetByIdAsync(id);

            if (staff == null)
                return NotFound();

            _repository.Remove(staff);
            await _repository.SaveAsync();

            return Ok("Staff deleted successfully");
        }

        // ======================
        // Staff By Hostel
        // ======================
        [HttpGet("hostel/{hostelId}")]
        public async Task<IActionResult> GetByHostel(int hostelId)
        {
            var staff = await _repository.GetStaffByHostelAsync(hostelId);

            return Ok(staff);
        }

        // ======================
        // Staff By Role
        // ======================
        [HttpGet("role/{role}")]
        public async Task<IActionResult> GetByRole(StaffRole role)
        {
            var staff = await _repository.GetStaffByRoleAsync(role);

            return Ok(staff);
        }
    }
}
