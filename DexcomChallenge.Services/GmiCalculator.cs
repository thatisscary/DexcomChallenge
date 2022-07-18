
namespace DexcomChallenge.Services
{
    using DexcomChallenge.Models.External;
    using System.Linq;
    public class GmiCalculator 
    {
        private static string[] users = { "SandboxUser5", "SandboxUser6" };

        


        public Decimal CalculateGmi(CollectedEstimatedGlucoseValues valueCollection )
        {
            var settings =  EstimatedGlucoseSettings.GetEstimatedGlucoseSetting(valueCollection.Unit);
            return settings.CalculateGmi(valueCollection.EstimatedGlucoseValues.Average(x => x.Value));

        }
    }
}