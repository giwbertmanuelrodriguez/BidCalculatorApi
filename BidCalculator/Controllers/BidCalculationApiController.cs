using Microsoft.AspNetCore.Mvc;
using BidCalculationApi.Models;
using BidCalculationApi.Services;
using Microsoft.AspNetCore.Cors;

namespace BidCalculationApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BidCalculationController : ControllerBase
    {
        private readonly BidCalculationService _bidCalculationService;

        public BidCalculationController()
        {
            _bidCalculationService = new BidCalculationService();
        }

        [HttpPost("calculate")]
        public IActionResult CalculateBid(BidCalculationRequest request)
        {
            var result = _bidCalculationService.CalculateBid(request);
            return Ok(result);
        }
    }
}