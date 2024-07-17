using BidCalculatorAPI.Interfaces;
using BidCalculatorAPI.Models.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace BidCalculatorAPI.Controllers
{
    /// <summary>
    /// Controller to handle vehicle API requests.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class VehicleController : ControllerBase
    {
        // Services
        private readonly ICalculatorService _calculatorService;

        /// <summary>
        /// Initializes the CalculatorService.
        /// </summary>
        /// <param name="calculatorService">The service used to calculate vehicle fees and total cost.</param>
        public VehicleController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        /// <summary>
        /// Calculates the fees and total cost for a vehicle based on the provided request.
        /// </summary>
        /// <param name="request">The calculation request containing the base price and vehicle type.</param>
        /// <returns>The calculation response containing the fees and the total cost.</returns>
        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] CalculationRequest request)
        {
            // Perform the fee and total cost calculation using the provided base price and vehicle type
            var result = await _calculatorService.CalculateFeesAndTotalAsync(request.BasePrice, request.VehicleType);

            return Ok(result);
        }
    }
}