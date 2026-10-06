using Common.ViewModels.Rooms;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {

        private readonly IRoomRepository _repository;

        public RoomController(IRoomRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // Get All Rooms
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _repository.GetAllAsync();

            var result = rooms.Select(x => new RoomResponseVM
            {
                RoomId = x.RoomId,
                HostelId = x.HostelId,
                HostelName = x.Hostel?.HostelName,
                RoomNumber = x.RoomNumber,
                Floor = x.Floor,
                Capacity = x.Capacity,
                IsActive = x.IsActive,
                Occupancy = x.Girls?.Count ?? 0
            });

            return Ok(result);
        }

        // =========================
        // Get Room By Id
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var room = await _repository.GetRoomWithGirlsAsync(id);

            if (room == null)
                return NotFound();

            var result = new RoomResponseVM
            {
                RoomId = room.RoomId,
                HostelId = room.HostelId,
                HostelName = room.Hostel?.HostelName,
                RoomNumber = room.RoomNumber,
                Floor = room.Floor,
                Capacity = room.Capacity,
                IsActive = room.IsActive,
                Occupancy = room.Girls?.Count ?? 0
            };

            return Ok(result);
        }

        // =========================
        // Create Room
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomVM model)
        {
            var entity = new Room
            {
                HostelId = model.HostelId,
                RoomNumber = model.RoomNumber,
                Floor = model.Floor,
                Capacity = model.Capacity,
                IsActive = model.IsActive
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Room created successfully");
        }

        // =========================
        // Update Room
        // =========================
        [HttpPut]
        public async Task<IActionResult> Update(UpdateRoomVM model)
        {
            var room = await _repository.GetByIdAsync(model.RoomId);

            if (room == null)
                return NotFound();

            room.HostelId = model.HostelId;
            room.RoomNumber = model.RoomNumber;
            room.Floor = model.Floor;
            room.Capacity = model.Capacity;
            room.IsActive = model.IsActive;

            _repository.Update(room);

            await _repository.SaveAsync();

            return Ok("Room updated successfully");
        }

        // =========================
        // Delete Room
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _repository.GetByIdAsync(id);

            if (room == null)
                return NotFound();

            _repository.Remove(room);

            await _repository.SaveAsync();

            return Ok("Room deleted successfully");
        }

        // =========================
        // Rooms by Hostel
        // =========================
        [HttpGet("hostel/{hostelId}")]
        public async Task<IActionResult> GetRoomsByHostel(int hostelId)
        {
            var rooms = await _repository.GetRoomsByHostelAsync(hostelId);

            return Ok(rooms);
        }

        // =========================
        // Available Rooms
        // =========================
        [HttpGet("available/{hostelId}")]
        public async Task<IActionResult> GetAvailableRooms(int hostelId)
        {
            var rooms = await _repository.GetAvailableRoomsAsync(hostelId);

            return Ok(rooms);
        }

        // =========================
        // Room Occupancy
        // =========================
        [HttpGet("occupancy/{roomId}")]
        public async Task<IActionResult> GetOccupancy(int roomId)
        {
            var count = await _repository.GetRoomOccupancyAsync(roomId);

            return Ok(count);
        }
    }
}
