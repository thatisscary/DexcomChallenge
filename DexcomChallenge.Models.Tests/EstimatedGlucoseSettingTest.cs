namespace DexcomChallenge.Models.Tests
{
    using DexcomChallenge.Models.External;

    public class EstimatedGlucoseSettingTest
    {
        [Theory]
        [InlineData(@"mg/dl", 3.31, 0.02392,  22.2, 3.8)]
        [InlineData(@"mmol/L", 12.71, 4.70587,  22.2, 117.2)]
        public void Test1(string unit, decimal multiplier, decimal conversion, decimal average, decimal expectedOutput)
        {
            EstimatedGlucoseSetting setting = new EstimatedGlucoseSetting(unit, multiplier, conversion);

            var output = setting.CalculateGmi(average);

            Assert.Equal(expectedOutput, output);
        }
    }
}