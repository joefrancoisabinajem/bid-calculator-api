using BidCalculatorAPI.Interfaces;
using BidCalculatorAPI.Services;

namespace BidCalculatorAPI.Extensions
{
    /// <summary>
    /// Extension class to register the services.
    /// </summary>
    public static class ServiceRegistrationExtensions
    {
        /// <summary>
        /// Adds the application services.
        /// </summary>
        /// <param name="services">The services from the program file.</param>
        public static void AddApplicationServices(this IServiceCollection services)
        {
            // Register application services
            services.AddScoped<ICalculatorService, CalculatorService>();
        }
    }
}