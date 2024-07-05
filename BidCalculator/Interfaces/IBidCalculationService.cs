using BidCalculationApi.Models;

namespace BidCalculatorApi.Interfaces
{
    public interface IBidCalculationService
    {
        BidCalculationResponse CalculateBid(BidCalculationRequest request);
    }
}
