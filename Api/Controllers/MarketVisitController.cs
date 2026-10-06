using Common.ViewModels.MarketVisits;
using DataAccess.Entities;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketVisitController : ControllerBase
    {
        private readonly IMarketVisitRepository _repository;

        public MarketVisitController(IMarketVisitRepository repository)
        {
            _repository = repository;
        }

        // =========================
        // Get All Visits
        // =========================
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var visits = await _repository.GetAllAsync();

            var result = visits.Select(x => new MarketVisitResponseVM
            {
                MarketVisitId = x.MarketVisitId,
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
            var visit = await _repository.GetByIdAsync(id);

            if (visit == null)
                return NotFound();

            var result = new MarketVisitResponseVM
            {
                MarketVisitId = visit.MarketVisitId,
                GirlId = visit.GirlId,
                GirlName = visit.Girl?.Name,
                OutTime = visit.OutTime,
                InTime = visit.InTime,
                Reason = visit.Reason
            };

            return Ok(result);
        }

        // =========================
        // Create Market Visit
        // =========================
        [HttpPost]
        public async Task<IActionResult> Create(CreateMarketVisitVM model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var entity = new MarketVisit
            {
                GirlId = model.GirlId,
                OutTime = model.OutTime,
                Reason = model.Reason
            };

            await _repository.AddAsync(entity);
            await _repository.SaveAsync();

            return Ok("Market visit recorded successfully");
        }

        // =========================
        // Delete Visit
        // =========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var visit = await _repository.GetByIdAsync(id);

            if (visit == null)
                return NotFound();

            _repository.Remove(visit);
            await _repository.SaveAsync();

            return Ok("Market visit deleted successfully");
        }

        // =========================
        // Today Visits
        // =========================
        [HttpGet("today")]
        public async Task<IActionResult> GetTodayVisits()
        {
            var visits = await _repository.GetTodayMarketVisitsAsync();

            var result = visits.Select(x => new MarketVisitResponseVM
            {
                MarketVisitId = x.MarketVisitId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                OutTime = x.OutTime,
                InTime = x.InTime,
                Reason = x.Reason
            });

            return Ok(result);
        }

        // =========================
        // Monthly Report
        // =========================
        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthly(int month, int year)
        {
            var visits = await _repository.GetMonthlyMarketVisitsAsync(month, year);

            var result = visits.Select(x => new MarketVisitResponseVM
            {
                MarketVisitId = x.MarketVisitId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                OutTime = x.OutTime,
                InTime = x.InTime,
                Reason = x.Reason
            });

            return Ok(result);
        }

        // =========================
        // Girl Specific Visits
        // =========================
        [HttpGet("girl/{girlId}")]
        public async Task<IActionResult> GetGirlVisits(int girlId)
        {
            var visits = await _repository.GetGirlMarketVisitsAsync(girlId);

            var result = visits.Select(x => new MarketVisitResponseVM
            {
                MarketVisitId = x.MarketVisitId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                OutTime = x.OutTime,
                InTime = x.InTime,
                Reason = x.Reason
            });

            return Ok(result);
        }

        // =========================
        // Girls Currently in Market
        // =========================
        [HttpGet("active")]
        public async Task<IActionResult> GetGirlsCurrentlyInMarket()
        {
            var visits = await _repository.GetGirlsCurrentlyInMarketAsync();

            var result = visits.Select(x => new MarketVisitResponseVM
            {
                MarketVisitId = x.MarketVisitId,
                GirlId = x.GirlId,
                GirlName = x.Girl?.Name,
                OutTime = x.OutTime,
                InTime = x.InTime,
                Reason = x.Reason
            });

            return Ok(result);
        }

        // =========================
        // Mark Return
        // =========================
        [HttpPut("return/{id}")]
        public async Task<IActionResult> MarkReturn(int id)
        {
            await _repository.MarkReturnAsync(id);

            return Ok("Girl marked as returned from market");
        }
    }
}
