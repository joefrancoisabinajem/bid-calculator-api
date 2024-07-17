using BidCalculatorAPI.Helpers;
using BidCalculatorAPI.Interfaces;
using BidCalculatorAPI.Models.DTOs.Responses;
using BidCalculatorAPI.Models.Enums;

namespace BidCalculatorAPI.Services
{
    /// <summary>
    /// Service to calculate fees and total cost for a vehicle based on its base price and type.
    /// </summary>
    public class CalculatorService : ICalculatorService
    {
        // Constants
        private const decimal BasicBuyerFeePercentage = 0.1m;
		private const decimal CommonBuyerFeeMin = 10m;
		private const decimal CommonBuyerFeeMax = 50m;
		private const decimal LuxuryBuyerFeeMin = 25m;
		private const decimal LuxuryBuyerFeeMax = 200m;
		private const decimal CommonSpecialFeePercentage = 0.02m;
		private const decimal LuxurySpecialFeePercentage = 0.04m;
		private const decimal StorageFee = 100m;
		private const decimal AssociationFeeThreshold1 = 500m;
		private const decimal AssociationFeeThreshold2 = 1000m;
		private const decimal AssociationFeeThreshold3 = 3000m;
		private const decimal AssociationFee1 = 5m;
		private const decimal AssociationFee2 = 10m;
		private const decimal AssociationFee3 = 15m;
		private const decimal AssociationFee4 = 20m;

		/// <summary>
		/// Calculates the fees and total cost for a vehicle based on its base price and type.
		/// </summary>
		/// <param name="basePrice">The base price of the vehicle.</param>
		/// <param name="vehicleType">The type of the vehicle.</param>
		/// <returns>The calculation response DTO containing the calculated fees and total cost.</returns>
		public async Task<CalculationResponse> CalculateFeesAndTotalAsync(decimal basePrice, VehicleType vehicleType)
        {
            if (basePrice < 1)
            {
                // Return a response with zero fees if the base price is less than 1
                return DataHelpers.CreateZeroFeeResponse();
            }

            // Calculate all fees and total cost
            var basicBuyerFee = await CalculateBasicBuyerFeeAsync(basePrice, vehicleType);
            var specialFee = await CalculateSpecialFeeAsync(basePrice, vehicleType);
            var associationFee = await CalculateAssociationFeeAsync(basePrice);
            var totalCost = basePrice + basicBuyerFee + specialFee + associationFee + StorageFee;

            // Return the calculated fees and total cost, rounding to two decimal places
            return new CalculationResponse
            {
                BasicBuyerFee = MathHelpers.RoundToTwoDecimalPlaces(basicBuyerFee),
                SpecialFee = MathHelpers.RoundToTwoDecimalPlaces(specialFee),
                AssociationFee = MathHelpers.RoundToTwoDecimalPlaces(associationFee),
                StorageFee = MathHelpers.RoundToTwoDecimalPlaces(StorageFee),
                TotalCost = MathHelpers.RoundToTwoDecimalPlaces(totalCost)
            };
        }

        /// <summary>
        /// Calculates the basic buyer fee based on the base price and vehicle type.
        /// </summary>
        /// <param name="basePrice">The base price of the vehicle.</param>
        /// <param name="vehicleType">The type of the vehicle.</param>
        /// <returns>The calculated basic buyer fee.</returns>
        private async Task<decimal> CalculateBasicBuyerFeeAsync(decimal basePrice, VehicleType vehicleType)
        {
            var fee = basePrice * BasicBuyerFeePercentage;

            // Clamp the fee based on vehicle type.
            return vehicleType == VehicleType.Common ?
                Math.Clamp(fee, CommonBuyerFeeMin, CommonBuyerFeeMax) :
                Math.Clamp(fee, LuxuryBuyerFeeMin, LuxuryBuyerFeeMax);
        }

        /// <summary>
        /// Calculates the special fee based on the base price and vehicle type.
        /// </summary>
        /// <param name="basePrice">The base price of the vehicle.</param>
        /// <param name="vehicleType">The type of the vehicle.</param>
        /// <returns>The calculated special fee.</returns>
        private async Task<decimal> CalculateSpecialFeeAsync(decimal basePrice, VehicleType vehicleType)
        {
            return vehicleType == VehicleType.Common ?
                basePrice * CommonSpecialFeePercentage :
                basePrice * LuxurySpecialFeePercentage;
        }

        /// <summary>
        /// Calculates the association fee based on the base price.
        /// </summary>
        /// <param name="basePrice">The base price of the vehicle.</param>
        /// <returns>The calculated association fee.</returns>
        private async Task<decimal> CalculateAssociationFeeAsync(decimal basePrice)
        {
            if (basePrice <= AssociationFeeThreshold1)
            {
                return AssociationFee1;
            }
            if (basePrice <= AssociationFeeThreshold2)
            {
                return AssociationFee2;
            }
            if (basePrice <= AssociationFeeThreshold3)
            {
                return AssociationFee3;
            }
            return AssociationFee4;
        }
    }
}