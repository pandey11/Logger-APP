using App.Interfaces;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.ApplicationInsights.Extensibility;
using System;
using System.Collections.Generic;

namespace App.Managers
{
    public class Logger : ILogger
    {
        private static TelemetryClient telemetryClient = null;

        private static TelemetryClient client
        {
            get
            {
                if(telemetryClient == null)
                {
                    TelemetryConfiguration configuration = TelemetryConfiguration.CreateDefault();
                    configuration.InstrumentationKey = ConfigurationManager.Instance.Get<TelemetryClientConfiguration>().InstrumentationKey; ;
                    telemetryClient = new TelemetryClient(configuration);
                }
                return telemetryClient;
            }
        }

        public Logger()
        {
        }

        public void LogInformation(string message)
        {
            PushMessage(message, SeverityLevel.Information);
        }

        public void LogWarning(string message)
        {
            PushMessage(message, SeverityLevel.Warning);
        }

        public void LogError(string message)
        {
            PushMessage(message, SeverityLevel.Error);
        }

        private void PushMessage(string message, SeverityLevel severityLevel)
        {
            if(string.Equals(ConfigurationManager.Instance.Get<TelemetryClientConfiguration>().SinkType, SinkType.Console.ToString()))
            {
                Console.WriteLine($"Message: { message},SeverityLevel: {severityLevel.ToString()}");
            }
            else
            {
                client.TrackTrace(message, severityLevel, new Dictionary<string, string>()
                {
                    {"RootActivityId", ServiceContext.CurrentContext._correlationContext?.RootActivityId},
                    {"SessionId", ServiceContext.CurrentContext._correlationContext?.SessionId},
                    {"Application", ServiceContext.CurrentContext._correlationContext?.Application }
                });
            }
        }
    }
}
