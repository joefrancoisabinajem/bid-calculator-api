using System.Net.Http.Json;
using BidCalculatorAPI.Models.DTOs.Requests;
using BidCalculatorAPI.Models.DTOs.Responses;
using BidCalculatorAPI.Models.Enums;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BidCalculatorTests.Integration
{
    /// <summary>
    /// Integration tests class for the VehicleController.
    /// </summary>
    public class VehicleControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        /// <summary>
        /// The http client.
        /// </summary>
        private readonly HttpClient _client;

        /// <summary>
        /// Initializes the http client.
        /// </summary>
        /// <param name="factory">The web application factory to create the client.</param>
        public VehicleControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        /// <summary>
        /// Test if the Calculate endpoint returns the correct result for valid inputs.
        /// </summary>
        [Fact]
        public async Task Calculate_ReturnsCorrectResult_ForValidInputs()
        {
            // Arrange
            var request = new CalculationRequest
            {
                BasePrice = 1000,
                VehicleType = VehicleType.Common
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/vehicle/calculate", request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CalculationResponse>();

            result.Should().NotBeNull();
            result.BasicBuyerFee.Should().Be(50);
            result.SpecialFee.Should().Be(20);
            result.AssociationFee.Should().Be(10);
            result.StorageFee.Should().Be(100);
            result.TotalCost.Should().Be(1180);
        }

        /// <summary>
        /// Test if the Calculate endpoint returns the correct result for various inputs.
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
        public async Task Calculate_ReturnsCorrectResult_ForVariousInputs(
            decimal basePrice, VehicleType vehicleType, decimal expectedBasicBuyerFee, 
            decimal expectedSpecialFee, decimal expectedAssociationFee, decimal expectedStorageFee, 
            decimal expectedTotalCost)
        {
            // Arrange
            var request = new CalculationRequest
            {
                BasePrice = basePrice,
                VehicleType = vehicleType
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/vehicle/calculate", request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CalculationResponse>();

            result.Should().NotBeNull();
            result.BasicBuyerFee.Should().Be(expectedBasicBuyerFee);
            result.SpecialFee.Should().Be(expectedSpecialFee);
            result.AssociationFee.Should().Be(expectedAssociationFee);
            result.StorageFee.Should().Be(expectedStorageFee);
            result.TotalCost.Should().Be(expectedTotalCost);
        }

        /// <summary>
        /// Test if the Calculate endpoint returns zero fees and total cost for a base price of zero.
        /// </summary>
        [Fact]
        public async Task Calculate_WithZeroBasePrice_ReturnsZeroFeesAndTotal()
        {
            // Arrange
            var request = new CalculationRequest
            {
                BasePrice = 0,
                VehicleType = VehicleType.Common
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/vehicle/calculate", request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CalculationResponse>();

            result.Should().NotBeNull();
            result.BasicBuyerFee.Should().Be(0);
            result.SpecialFee.Should().Be(0);
            result.AssociationFee.Should().Be(0);
            result.StorageFee.Should().Be(0);
            result.TotalCost.Should().Be(0);
        }

        /// <summary>
        /// Test if the Calculate endpoint returns zero fees and total cost for a negative base price.
        /// </summary>
        [Fact]
        public async Task Calculate_WithNegativeBasePrice_ReturnsZeroFeesAndTotal()
        {
            // Arrange
            var request = new CalculationRequest
            {
                BasePrice = -100,
                VehicleType = VehicleType.Common
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/vehicle/calculate", request);

            // Assert
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CalculationResponse>();

            result.Should().NotBeNull();
            result.BasicBuyerFee.Should().Be(0);
            result.SpecialFee.Should().Be(0);
            result.AssociationFee.Should().Be(0);
            result.StorageFee.Should().Be(0);
            result.TotalCost.Should().Be(0);
        }
    }
}