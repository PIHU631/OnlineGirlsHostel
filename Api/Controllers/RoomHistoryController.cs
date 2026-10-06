using Common.ViewModels.RoomHistory;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomHistoryController : ControllerBase
    {
        private readonly IRoomHistoryRepository _repository;

        public RoomHistoryController(IRoomHistoryRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // Get All
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var histories = await _repository.GetAllAsync();

            var result = histories.Select(x => new RoomHistoryResponseVM
            {
                RoomHistoryId = x.RoomHistoryId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                RoomId = x.RoomId,
                RoomNumber = x.Room?.RoomNumber,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            });

            return Ok(result);
        }

        // =========================
        // Get By Id
        // =========================
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var history = await _repository.GetByIdAsync(id);

            if (history == null)
                return NotFound();

            var result = new RoomHistoryResponseVM
            {
                RoomHistoryId = history.RoomHistoryId,
                GirlId = history.GirlId,
                GirlName = history.Girl?.Name,
                RoomId = history.RoomId,
                RoomNumber = history.Room?.RoomNumber,
                StartDate = history.StartDate,
                EndDate = history.EndDate
            };

            return Ok(result);
        }

        // =========================
        // Assign Room
        // =========================
        [HttpPost]
        public async Task<IActionResult> AssignRoom(CreateRoomHistoryVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // End previous room if exists
            await _repository.EndCurrentRoomAsync(model.GirlId);

            var entity = new RoomHistory
            {
                GirlId = model.GirlId,
                RoomId = model.RoomId,
                StartDate = model.StartDate
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Room assigned successfully");
        }

        // =========================
        // Delete History
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var history = await _repository.GetByIdAsync(id);

            if (history == null)
                return NotFound();

            _repository.Remove(history);
            await _repository.SaveAsync();

            return Ok("Room history deleted");
        }

        // =========================
        // Girl Room History
        // =========================
        [HttpGet("girl/{girlId}")]
        public async Task<IActionResult> GetGirlRoomHistory(int girlId)
        {
            var histories = await _repository.GetGirlRoomHistoryAsync(girlId);

            var result = histories.Select(x => new RoomHistoryResponseVM
            {
                RoomHistoryId = x.RoomHistoryId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                RoomId = x.RoomId,
                RoomNumber = x.Room?.RoomNumber,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            });

            return Ok(result);
        }

        // =========================
        // Current Room of Girl
        // =========================
        [HttpGet("current/{girlId}")]
        public async Task<IActionResult> GetCurrentRoom(int girlId)
        {
            var room = await _repository.GetCurrentRoomAsync(girlId);

            if (room == null)
                return Ok(null);

            var result = new RoomHistoryResponseVM
            {
                RoomHistoryId = room.RoomHistoryId,
                GirlId = room.GirlId,
                GirlName = room.Girl?.Name,
                RoomId = room.RoomId,
                RoomNumber = room.Room?.RoomNumber,
                StartDate = room.StartDate,
                EndDate = room.EndDate
            };

            return Ok(result);
        }

        // =========================
        // Room Occupancy History
        // =========================
        [HttpGet("room/{roomId}")]
        public async Task<IActionResult> GetRoomHistory(int roomId)
        {
            var histories = await _repository.GetRoomHistoryByRoomAsync(roomId);

            var result = histories.Select(x => new RoomHistoryResponseVM
            {
                RoomHistoryId = x.RoomHistoryId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                RoomId = x.RoomId,
                RoomNumber = x.Room?.RoomNumber,
                StartDate = x.StartDate,
                EndDate = x.EndDate
            });

            return Ok(result);
        }

        // =========================
        // End Current Room
        // =========================
        [HttpPut("end/{girlId}")]
        public async Task<IActionResult> EndRoom(int girlId)
        {
            await _repository.EndCurrentRoomAsync(girlId);

            return Ok("Room ended successfully");
        }
    }
}
