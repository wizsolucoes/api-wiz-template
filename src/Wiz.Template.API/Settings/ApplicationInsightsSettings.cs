namespace Wiz.Template.API.Settings
{
    public class ApplicationInsightsSettings
    {
        public ApplicationInsightsSettings() { }

        public ApplicationInsightsSettings(string instrumentationKey)
        {
            InstrumentationKey = instrumentationKey;
        }

        public string InstrumentationKey { get; }
    }
}
