using BidCalculationApi.Models;
using BidCalculationApi.Services;
using BidCalculatorApi.Interfaces;
using Xunit;

namespace BidCalculatorApiTest
{
    public class BidCalculationServiceTests
    {
        private readonly IBidCalculationService _service;

        public BidCalculationServiceTests()
        {
            _service = new BidCalculationService();
        }

        [Theory]
        [InlineData(398.00, VehicleType.Common, 0.10, 0.10, 0.04, 0.02, 25.00, 10.00, 200.00, 50.00, 100.00, 39.80, 7.96, 5.00, 100.00, 550.76)]
        [InlineData(501.00, VehicleType.Common, 0.10, 0.10, 0.04, 0.02, 25.00, 10.00, 200.00, 50.00, 100.00, 50.00, 10.02, 10.00, 100.00, 671.02)]
        [InlineData(57.00, VehicleType.Common, 0.10, 0.10, 0.04, 0.02, 25.00, 10.00, 200.00, 50.00, 100.00, 10.00, 1.14, 5.00, 100.00, 173.14)]
        [InlineData(1800.00, VehicleType.Luxury, 0.10, 0.10, 0.04, 0.02, 25.00, 10.00, 200.00, 50.00, 100.00, 180.00, 72.00, 15.00, 100.00, 2167.00)]
        [InlineData(1100.00, VehicleType.Common, 0.10, 0.10, 0.04, 0.02, 25.00, 10.00, 200.00, 50.00, 100.00, 50.00, 22.00, 15.00, 100.00, 1287.00)]
        [InlineData(1000000.00, VehicleType.Luxury, 0.10, 0.10, 0.04, 0.02, 25.00, 10.00, 200.00, 50.00, 100.00, 200.00, 40000.00, 20.00, 100.00, 1040320.00)]
        public void CalculateBid_ReturnsExpectedTotal(
            decimal basePrice, VehicleType vehicleType,
            decimal luxuryBasicFeeRate, decimal commonBasicFeeRate,
            decimal luxurySpecialFeeRate, decimal commonSpecialFeeRate,
            decimal luxuryBasicFeeMin, decimal commonBasicFeeMin,
            decimal luxuryBasicFeeMax, decimal commonBasicFeeMax,
            decimal storageFee, decimal expectedBasicFee,
            decimal expectedSpecialFee, decimal expectedAssociationFee,
            decimal expectedStorageFee, decimal expectedTotalCost)
        {
            var request = new BidCalculationRequest
            {
                BasePrice = basePrice,
                VehicleType = vehicleType,
                LuxuryBasicFeeRate = luxuryBasicFeeRate,
                CommonBasicFeeRate = commonBasicFeeRate,
                LuxurySpecialFeeRate = luxurySpecialFeeRate,
                CommonSpecialFeeRate = commonSpecialFeeRate,
                LuxuryBasicFeeMin = luxuryBasicFeeMin,
                CommonBasicFeeMin = commonBasicFeeMin,
                LuxuryBasicFeeMax = luxuryBasicFeeMax,
                CommonBasicFeeMax = commonBasicFeeMax,
                StorageFee = storageFee
            };

            var result = _service.CalculateBid(request);

            Assert.Equal(expectedBasicFee, result.BasicFee);
            Assert.Equal(expectedSpecialFee, result.SpecialFee);
            Assert.Equal(expectedAssociationFee, result.AssociationFee);
            Assert.Equal(expectedStorageFee, result.StorageFee);
            Assert.Equal(expectedTotalCost, result.TotalCost);
        }
    }
}
