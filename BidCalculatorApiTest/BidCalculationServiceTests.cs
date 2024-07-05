using BidCalculationApi.Models;
using BidCalculationApi.Services;
using Xunit;

namespace BidCalculatorApiTest
{
    public class BidCalculationServiceTests
    {
        private readonly BidCalculationService _service;

        public BidCalculationServiceTests()
        {
            _service = new BidCalculationService();
        }

        [Theory]
        [InlineData(398.00, "Common", 39.80, 7.96, 5.00, 100.00, 550.76)]
        [InlineData(501.00, "Common", 50.00, 10.02, 10.00, 100.00, 671.02)]
        [InlineData(57.00, "Common", 10.00, 1.14, 5.00, 100.00, 173.14)]
        [InlineData(1800.00, "Luxury", 180.00, 72.00, 15.00, 100.00, 2167.00)]
        [InlineData(1100.00, "Common", 50.00, 22.00, 15.00, 100.00, 1287.00)]
        [InlineData(1000000.00, "Luxury", 200.00, 40000.00, 20.00, 100.00, 1040320.00)]
        public void CalculateBid_ReturnsExpectedTotal(
            decimal basePrice, string vehicleType,
            decimal expectedBasicFee, decimal expectedSpecialFee,
            decimal expectedAssociationFee, decimal expectedStorageFee,
            decimal expectedTotalCost)
        {
            var request = new BidCalculationRequest
            {
                BasePrice = basePrice,
                VehicleType = vehicleType
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
