using BidCalculatorAPI.Models.DTOs.Responses;

namespace BidCalculatorAPI.Helpers
{
    /// <summary>
    /// Data helper static class.
    /// </summary>
    public static class DataHelpers
    {
        /// <summary>
        /// Creates a 0 fee calculation response with the correct 0 data.
        /// </summary>
        /// <returns>The calculation response DTO.</returns>
        public static CalculationResponse CreateZeroFeeResponse()
        {
            return new CalculationResponse
            {
                BasicBuyerFee = 0,
                SpecialFee = 0,
                AssociationFee = 0,
                StorageFee = 0,
                TotalCost = 0
            };
        }
    }
}