namespace BidCalculationApi.Models
{
    public class BidCalculationRequest
    {
        public decimal BasePrice { get; set; }
        public VehicleType VehicleType { get; set; }
        public decimal LuxuryBasicFeeRate { get; set; }
        public decimal CommonBasicFeeRate { get; set; }
        public decimal LuxurySpecialFeeRate { get; set; }
        public decimal CommonSpecialFeeRate { get; set; }
        public decimal LuxuryBasicFeeMin { get; set; }
        public decimal CommonBasicFeeMin { get; set; }
        public decimal LuxuryBasicFeeMax { get; set; }
        public decimal CommonBasicFeeMax { get; set; }
        public decimal StorageFee { get; set; }
    }

    public enum VehicleType
    {
        Common,
        Luxury
    }
}