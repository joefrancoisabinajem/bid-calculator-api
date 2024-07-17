using BidCalculatorAPI.Models.Enums;
using BidCalculatorAPI.Services;
using FluentAssertions;

namespace BidCalculatorTests.Unit
{
    /// <summary>
    /// Unit tests class for the CalculatorService.
    /// </summary>
    public class CalculatorServiceTests
    {
        /// <summary>
        /// The calculation service.
        /// </summary>
        private readonly CalculatorService _service;

        /// <summary>
        /// Initializes the calculation service.
        /// </summary>
        public CalculatorServiceTests()
        {
            _service = new CalculatorService();
        }

        /// <summary>
        /// Test if the CalculateFeesAndTotal returns zero fees and total for a base price of zero.
        /// </summary>
        [Fact]
        public async Task CalculateFees_WithZeroBasePrice_ReturnsZeroFeesAndTotal()
        {
            // Arrange
            decimal basePrice = 0;
            VehicleType vehicleType = VehicleType.Common;

            // Act
            var result = await _service.CalculateFeesAndTotalAsync(basePrice, vehicleType);

            // Assert
            result.Should().NotBeNull();
            result.BasicBuyerFee.Should().Be(0);
            result.SpecialFee.Should().Be(0);
            result.AssociationFee.Should().Be(0);
            result.StorageFee.Should().Be(0);
            result.TotalCost.Should().Be(0);
        }

        /// <summary>
        /// Test if the CalculateFeesAndTotal returns zero fees and total for a negative base price.
        /// </summary>
        [Fact]
        public async Task CalculateFees_WithNegativeBasePrice_ReturnsZeroFeesAndTotal()
        {
            // Arrange
            decimal basePrice = -100;
            VehicleType vehicleType = VehicleType.Common;

            // Act
            var result = await _service.CalculateFeesAndTotalAsync(basePrice, vehicleType);

            // Assert
            result.Should().NotBeNull();
            result.BasicBuyerFee.Should().Be(0);
            result.SpecialFee.Should().Be(0);
            result.AssociationFee.Should().Be(0);
            result.StorageFee.Should().Be(0);
            result.TotalCost.Should().Be(0);
        }

        /// <summary>
        /// Test if the CalculateFeesAndTotal returns the correct values for valid inputs.
        /// </summary>
        [Fact]
        public async Task CalculateFees_WithValidInputs_ReturnsCorrectValues()
        {
            // Arrange
            decimal basePrice = 1000;
            VehicleType vehicleType = VehicleType.Common;

            // Act
            var result = await _service.CalculateFeesAndTotalAsync(basePrice, vehicleType);

            // Assert
            result.Should().NotBeNull();
            result.BasicBuyerFee.Should().Be(50);
            result.SpecialFee.Should().Be(20);
            result.AssociationFee.Should().Be(10);
            result.StorageFee.Should().Be(100);
            result.TotalCost.Should().Be(1180);
        }

        /// <summary>
        /// Test if the CalculateFeesAndTotal returns the correct values for various inputs.
        /// </summary>
        /// <param name="basePrice">The base price of the vehicle.</param>
        /// <param name="vehicleType">The type of the vehicle.</param>
        /// <param name="expectedBasicBuyerFee">The expected basic buyer fee.</param>
        /// <param name="expectedSpecialFee">The expected special fee.</param>
        /// <param name="expectedAssociationFee">The expected association fee.</param>
        /// <param name="expectedStorageFee">The expected storage fee.</param>
        /// <param name="expectedTotalCost">The expected total cost.</param>
        [Theory]
        [InlineData(398.00, VehicleType.Common, 39.80, 7.96, 5, 100, 550.76)]
        [InlineData(501.00, VehicleType.Common, 50.00, 10.02, 10, 100, 671.02)]
        [InlineData(57.00, VehicleType.Common, 10, 1.14, 5, 100, 173.14)]
        [InlineData(1800.00, VehicleType.Luxury, 180.00, 72.00, 15, 100, 2167.00)]
        [InlineData(1100.00, VehicleType.Common, 50, 22.00, 15, 100, 1287.00)]
        [InlineData(1000000.00, VehicleType.Luxury, 200.00, 40000.00, 20, 100, 1040320.00)]
        public async Task CalculateFees_WithVariousInputs_ReturnsCorrectValues(
            decimal basePrice, VehicleType vehicleType, decimal expectedBasicBuyerFee, 
            decimal expectedSpecialFee, decimal expectedAssociationFee, decimal expectedStorageFee, 
            decimal expectedTotalCost)
        {
            // Arrange
            // Inputs are already provided via InlineData

            // Act
            var result = await _service.CalculateFeesAndTotalAsync(basePrice, vehicleType);

            // Assert
            result.Should().NotBeNull();
            result.BasicBuyerFee.Should().Be(expectedBasicBuyerFee);
            result.SpecialFee.Should().Be(expectedSpecialFee);
            result.AssociationFee.Should().Be(expectedAssociationFee);
            result.StorageFee.Should().Be(expectedStorageFee);
            result.TotalCost.Should().Be(expectedTotalCost);
        }
    }
}