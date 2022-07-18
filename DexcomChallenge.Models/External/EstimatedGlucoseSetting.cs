namespace DexcomChallenge.Models.External
{
    public class EstimatedGlucoseSetting
    {
        internal EstimatedGlucoseSetting(string measurementUnits, decimal multiplier, decimal conversion)
        {
            MeasurementUnits = measurementUnits;
            Multiplier = multiplier;
            Conversion = conversion;
        }

        internal string MeasurementUnits { get; }

        internal decimal Multiplier { get; }

        internal decimal Conversion { get;  }

        public decimal CalculateGmi(decimal meanGlucose)
        {
            return Math.Round( Multiplier + (Conversion * meanGlucose), 1) ;
        }
    }
}