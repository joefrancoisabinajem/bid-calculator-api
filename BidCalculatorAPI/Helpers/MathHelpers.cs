namespace BidCalculatorAPI.Helpers
{
    /// <summary>
    /// Math helper static class.
    /// </summary>
    public static class MathHelpers
    {
        /// <summary>
        /// Rounds a decimal to 2 decimal places.
        /// </summary>
        /// <param name="value">The decimal value to round.</param>
        /// <returns>The decimal value rounded.</returns>
        public static decimal RoundToTwoDecimalPlaces(decimal value)
        {
            return Math.Round(value, 2);
        }
    }
}