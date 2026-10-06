using Common.ViewModels.Visitors;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VisitorController : ControllerBase
    {
        private readonly IVisitorRepository _repository;

        public VisitorController(IVisitorRepository repository)
        {
            _repository = repository;
        }

        // ======================
        // Get All Visitors
        // ======================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var visitors = await _repository.GetAllAsync();

            var result = visitors.Select(x => new VisitorResponseVM
            {
                VisitorId = x.VisitorId,
                Name = x.Name,
                Phone = x.Phone,
                Relation = x.Relation,
                IdProofNumber = x.IdProofNumber
            });

            return Ok(result);
        }

        // ======================
        // Get Visitor By Id
        // ======================
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var visitor = await _repository.GetByIdAsync(id);

            if (visitor == null)
                return NotFound();

            var result = new VisitorResponseVM
            {
                VisitorId = visitor.VisitorId,
                Name = visitor.Name,
                Phone = visitor.Phone,
                Relation = visitor.Relation,
                IdProofNumber = visitor.IdProofNumber
            };

            return Ok(result);
        }

        // ======================
        // Create Visitor
        // ======================
        [HttpPost]
        public async Task<IActionResult> Create(CreateVisitorVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new Visitor
            {
                Name = model.Name,
                Phone = model.Phone,
                Relation = model.Relation,
                IdProofNumber = model.IdProofNumber
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Visitor created successfully");
        }

        // ======================
        // Update Visitor
        // ======================
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CreateVisitorVM model)
        {
            var visitor = await _repository.GetByIdAsync(id);

            if (visitor == null)
                return NotFound();

            visitor.Name = model.Name;
            visitor.Phone = model.Phone;
            visitor.Relation = model.Relation;
            visitor.IdProofNumber = model.IdProofNumber;

            _repository.Update(visitor);
            await _repository.SaveAsync();

            return Ok("Visitor updated successfully");
        }

        // ======================
        // Delete Visitor
        // ======================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var visitor = await _repository.GetByIdAsync(id);

            if (visitor == null)
                return NotFound();

            _repository.Remove(visitor);
            await _repository.SaveAsync();

            return Ok("Visitor deleted successfully");
        }

        // ======================
        // Search Visitor
        // ======================
        [HttpGet("search")]
        public async Task<IActionResult> Search(string keyword)
        {
            var visitors = await _repository.SearchVisitorsAsync(keyword);

            return Ok(visitors);
        }

        // ======================
        // Visitor By Phone
        // ======================
        [HttpGet("phone/{phone}")]
        public async Task<IActionResult> GetByPhone(string phone)
        {
            var visitor = await _repository.GetVisitorByPhoneAsync(phone);

            if (visitor == null)
                return NotFound();

            return Ok(visitor);
        }

        // ======================
        // Visitor with Logs
        // ======================
        [HttpGet("logs/{visitorId}")]
        public async Task<IActionResult> GetVisitorWithLogs(int visitorId)
        {
            var visitor = await _repository.GetVisitorWithLogsAsync(visitorId);

            if (visitor == null)
                return NotFound();

            return Ok(visitor);
        }
    }
}
