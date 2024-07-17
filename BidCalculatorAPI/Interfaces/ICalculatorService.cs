using BidCalculatorAPI.Models.DTOs.Responses;
using BidCalculatorAPI.Models.Enums;

namespace BidCalculatorAPI.Interfaces
{
    /// <summary>
    /// Contract for CalculatorService.
    /// </summary>
    public interface ICalculatorService
    {
        /// <summary>
        /// Calculates the fees and total cost for a vehicle based on its base price and type.
        /// </summary>
        /// <param name="basePrice">The base price of the vehicle.</param>
        /// <param name="vehicleType">The type of the vehicle.</param>
        /// <returns>The calculation response DTO containing the calculated fees and total cost.</returns>
        Task<CalculationResponse> CalculateFeesAndTotalAsync(decimal basePrice, VehicleType vehicleType);
    }
}