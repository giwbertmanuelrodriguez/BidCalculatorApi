using BidCalculationApi.Models;

namespace BidCalculationApi.Services
{
    public class BidCalculationService
    {
        public BidCalculationResponse CalculateBid(BidCalculationRequest request)
        {
            decimal basicFeeRate = request.VehicleType.ToLower() == "luxury" ? 0.10m : 0.10m;
            decimal specialFeeRate = request.VehicleType.ToLower() == "luxury" ? 0.04m : 0.02m;

            decimal basicFee = Math.Min(Math.Max(request.BasePrice * basicFeeRate,
                request.VehicleType.ToLower() == "luxury" ? 25m : 10m),
                request.VehicleType.ToLower() == "luxury" ? 200m : 50m);

            decimal specialFee = request.BasePrice * specialFeeRate;

            decimal associationFee = request.BasePrice switch
            {
                > 3000 => 20m,
                > 1000 => 15m,
                > 500 => 10m,
                _ => 5m,
            };

            decimal storageFee = 100m;

            decimal totalCost = request.BasePrice + basicFee + specialFee + associationFee + storageFee;

            return new BidCalculationResponse
            {
                BasePrice = request.BasePrice,
                BasicFee = basicFee,
                SpecialFee = specialFee,
                AssociationFee = associationFee,
                StorageFee = storageFee,
                TotalCost = totalCost
            };
        }
    }
}
