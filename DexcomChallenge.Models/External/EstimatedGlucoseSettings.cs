namespace DexcomChallenge.Models.External
{
    using System.Linq;

    public static class EstimatedGlucoseSettings
    {
        private static EstimatedGlucoseSetting[] settings = new EstimatedGlucoseSetting[] { new EstimatedGlucoseSetting(@"mg/dl", 3.31M , 0.02392M),
                                                                                            new EstimatedGlucoseSetting(@"mmol/L", 12.71M , 4.70587M) };

        public static EstimatedGlucoseSetting GetEstimatedGlucoseSetting(string unitOfMeasurement)
        {
            return settings.Single(x => x.MeasurementUnits == unitOfMeasurement);
        }
    }
}