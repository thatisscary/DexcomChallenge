namespace DexcomChallenge.Models.External
{
    using System;
    using System.Text.Json.Serialization;

    public class CollectedEstimatedGlucoseValues
    {
        public CollectedEstimatedGlucoseValues()
        {
            Unit = String.Empty;
            RateUnit = String.Empty;
            EstimatedGlucoseValues = new EstimatedGlucoseValue[0];
        }

        public string Unit { get; set; }
        public string RateUnit { get; set; }

        [JsonPropertyName("egvs")]
        public EstimatedGlucoseValue[] EstimatedGlucoseValues { get; set; }
    }
}