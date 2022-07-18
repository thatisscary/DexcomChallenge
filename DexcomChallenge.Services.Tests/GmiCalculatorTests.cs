namespace DexcomChallenge.Services.Tests
{
    using DexcomChallenge.Models.External;

    public class GmiCalculatorTests
    {
        [Theory]
        [InlineData(@"mg/dl", 3.8)]
        [InlineData(@"mmol/L", 106.8)]
        public void GivenACollectionOfEstimatedValues_WhenSummarizingValues_AverageIsCalculatedCorrectly(string units, decimal expectedOutput)
        {
            EstimatedGlucoseValue[] glucoseValues = new EstimatedGlucoseValue[]
            {
                 new EstimatedGlucoseValue { Value=10,  displayTime= new DateTime()  , systemTime= new DateTime()} ,
                 new EstimatedGlucoseValue { Value=20,  displayTime= new DateTime()  , systemTime= new DateTime()} ,
                 new EstimatedGlucoseValue { Value=30,  displayTime= new DateTime()  , systemTime= new DateTime()} ,
            };
            CollectedEstimatedGlucoseValues collection = new CollectedEstimatedGlucoseValues
            {
                Unit = units,
                EstimatedGlucoseValues = glucoseValues,
            };

            GmiCalculator gmiCalculator = new GmiCalculator();
            var gmiOutput = gmiCalculator.CalculateGmi(collection);

            Assert.Equal(expectedOutput, gmiOutput);
        }
    }
}