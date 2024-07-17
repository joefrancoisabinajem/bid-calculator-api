using BidCalculatorAPI.Models.Enums;
using System.Text.Json.Serialization;

namespace BidCalculatorAPI.Models.DTOs.Requests
{
    /// <summary>
    /// DTO model for the request data for calculating fees and total cost.
    /// </summary>
    public class CalculationRequest
    {
        /// <summary>
        /// The base price.
        /// </summary>
        public decimal BasePrice
        {
            get;
            set;
        }

        // Annotation to convert string to enum from request
        [JsonConverter(typeof(JsonStringEnumConverter))]
        /// <summary>
        /// The type of the vehicle.
        /// </summary>
        public VehicleType VehicleType
        {
            get;
            set;
        }
    }
}