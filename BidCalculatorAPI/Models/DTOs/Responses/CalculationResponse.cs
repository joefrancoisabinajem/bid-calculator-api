namespace BidCalculatorAPI.Models.DTOs.Responses
{
    /// <summary>
    /// DTO model for the response of the fee and total cost calculation.
    /// </summary>
    public class CalculationResponse
    {
        /// <summary>
        /// The basic buyer fee.
        /// </summary>
        public decimal BasicBuyerFee
        {
            get;
            set;
        }

        /// <summary>
        /// The special fee.
        /// </summary>
        public decimal SpecialFee
        {
            get;
            set;
        }

        /// <summary>
        /// The association fee.
        /// </summary>
        public decimal AssociationFee
        {
            get;
            set;
        }

        /// <summary>
        /// The storage fee.
        /// </summary>
        public decimal StorageFee
        {
            get;
            set;
        }

        /// <summary>
        /// The total cost including all fees and the base price.
        /// </summary>
        public decimal TotalCost
        {
            get;
            set;
        }
    }
}