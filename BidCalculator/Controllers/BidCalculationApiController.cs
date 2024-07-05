using Microsoft.AspNetCore.Mvc;
using BidCalculationApi.Models;
using BidCalculatorApi.Interfaces;

namespace BidCalculationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidCalculationController : ControllerBase
    {
        private readonly IBidCalculationService _bidCalculationService;

        public BidCalculationController(IBidCalculationService bidCalculationService)
        {
            _bidCalculationService = bidCalculationService;
        }

        [HttpPost("calculate")]
        public IActionResult CalculateBid(BidCalculationRequest request)
        {
            var result = _bidCalculationService.CalculateBid(request);
            return Ok(result);
        }
    }
}