namespace App
{
    public class TelemetryClientConfiguration
    {
        public string InstrumentationKey { get; set; }

        public string SinkType { get; set; }
    }

    public enum SinkType
    {
        Console,
        AppInsight
    }
}
