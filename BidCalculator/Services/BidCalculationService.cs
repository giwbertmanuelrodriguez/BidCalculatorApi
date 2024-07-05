using BidCalculationApi.Models;
using BidCalculatorApi.Interfaces;

namespace BidCalculationApi.Services
{
    public class BidCalculationService : IBidCalculationService
    {
        public BidCalculationResponse CalculateBid(BidCalculationRequest request)
        {
            var basicFee = CalculateBasicFee(request.BasePrice, request.VehicleType, request);
            var specialFee = CalculateSpecialFee(request.BasePrice, request.VehicleType, request);
            var associationFee = CalculateAssociationFee(request.BasePrice);
            var storageFee = request.StorageFee;

            var totalCost = request.BasePrice + basicFee + specialFee + associationFee + storageFee;

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

        private decimal CalculateBasicFee(decimal basePrice, VehicleType vehicleType, BidCalculationRequest request)
        {
            decimal basicFeeRate;
            decimal basicFeeMin;
            decimal basicFeeMax;

            switch (vehicleType)
            {
                case VehicleType.Luxury:
                    basicFeeRate = request.LuxuryBasicFeeRate;
                    basicFeeMin = request.LuxuryBasicFeeMin;
                    basicFeeMax = request.LuxuryBasicFeeMax;
                    break;
                default:
                    basicFeeRate = request.CommonBasicFeeRate;
                    basicFeeMin = request.CommonBasicFeeMin;
                    basicFeeMax = request.CommonBasicFeeMax;
                    break;
            }

            var basicFee = basePrice * basicFeeRate;
            return Math.Min(Math.Max(basicFee, basicFeeMin), basicFeeMax);
        }

        private decimal CalculateSpecialFee(decimal basePrice, VehicleType vehicleType, BidCalculationRequest request)
        {
            var specialFeeRate = vehicleType == VehicleType.Luxury
                ? request.LuxurySpecialFeeRate
                : request.CommonSpecialFeeRate;

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
