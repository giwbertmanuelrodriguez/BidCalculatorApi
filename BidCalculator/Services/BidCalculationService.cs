using BidCalculationApi.Models;
using BidCalculatorApi.Interfaces;

namespace BidCalculationApi.Services
{
    public class BidCalculationService : IBidCalculationService
    {
        private const decimal LuxuryBasicFeeRate = 0.10m;
        private const decimal CommonBasicFeeRate = 0.10m;
        private const decimal LuxurySpecialFeeRate = 0.04m;
        private const decimal CommonSpecialFeeRate = 0.02m;
        private const decimal LuxuryBasicFeeMin = 25m;
        private const decimal CommonBasicFeeMin = 10m;
        private const decimal LuxuryBasicFeeMax = 200m;
        private const decimal CommonBasicFeeMax = 50m;
        private const decimal StorageFee = 100m;

        public BidCalculationResponse CalculateBid(BidCalculationRequest request)
        {
            var basicFee = CalculateBasicFee(request.BasePrice, request.VehicleType);
            var specialFee = CalculateSpecialFee(request.BasePrice, request.VehicleType);
            var associationFee = CalculateAssociationFee(request.BasePrice);

            var totalCost = request.BasePrice + basicFee + specialFee + associationFee + StorageFee;

            return new BidCalculationResponse
            {
                BasePrice = request.BasePrice,
                BasicFee = basicFee,
                SpecialFee = specialFee,
                AssociationFee = associationFee,
                StorageFee = StorageFee,
                TotalCost = totalCost
            };
        }

        private decimal CalculateBasicFee(decimal basePrice, string vehicleType)
        {
            var basicFeeRate = vehicleType.ToLower() == "luxury" ? LuxuryBasicFeeRate : CommonBasicFeeRate;
            var basicFeeMin = vehicleType.ToLower() == "luxury" ? LuxuryBasicFeeMin : CommonBasicFeeMin;
            var basicFeeMax = vehicleType.ToLower() == "luxury" ? LuxuryBasicFeeMax : CommonBasicFeeMax;

            var basicFee = basePrice * basicFeeRate;
            return Math.Min(Math.Max(basicFee, basicFeeMin), basicFeeMax);
        }

        private decimal CalculateSpecialFee(decimal basePrice, string vehicleType)
        {
            var specialFeeRate = vehicleType.ToLower() == "luxury" ? LuxurySpecialFeeRate : CommonSpecialFeeRate;
            return basePrice * specialFeeRate;
        }

        private decimal CalculateAssociationFee(decimal basePrice)
        {
            return basePrice switch
            {
                > 3000 => 20m,
                > 1000 => 15m,
                > 500 => 10m,
                _ => 5m,
            };
        }
    }
}
