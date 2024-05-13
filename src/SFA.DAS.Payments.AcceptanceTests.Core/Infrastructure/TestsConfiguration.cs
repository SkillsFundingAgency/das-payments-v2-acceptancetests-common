using System;
using Microsoft.Extensions.Configuration;

namespace SFA.DAS.Payments.AcceptanceTests.Core.Infrastructure
{
    public class TestsConfiguration
    {
        public static IConfigurationRoot Config;

        public static readonly string EndpointNameAppSetting = "EndpointName";
        public static readonly string StorageConnectionStringAppSetting = "StorageConnectionString";
        public static readonly string ServiceBusConnectionStringAppSetting = "ServiceBusConnectionString";
        public static readonly string DasServiceBusConnectionStringAppSetting = "DASServiceBusConnectionString";
        public static readonly string PaymentsConnectionStringAppSetting = "PaymentsConnectionString";
        public static readonly string ValidateDcAndDasServicesAppSetting = "ValidateDcAndDasServices";
        public static readonly string TimeToWaitAppSetting = "TimeToWait";
        public static readonly string TimeToWaitForUnexpectedAppSetting = "TimeToWaitForUnexpected";
        public static readonly string TimeToPauseAppSetting = "TimeToPause";
        public static readonly string DefaultMessageTimeToLiveAppSetting = "DefaultMessageTimeToLive";

        public TestsConfiguration(IConfigurationRoot config)
        {
            Config = config;
        }

        public string AcceptanceTestsEndpointName => GetAppSetting(AppSettingFormatter(EndpointNameAppSetting));
        public string StorageConnectionString => GetConnectionString(StorageConnectionStringAppSetting);
        public string ServiceBusConnectionString => GetConnectionString(ServiceBusConnectionStringAppSetting);
        public string DasServiceBusConnectionString => GetConnectionString(DasServiceBusConnectionStringAppSetting);
        public string PaymentsConnectionString => GetConnectionString(PaymentsConnectionStringAppSetting);

        public bool ValidateDcAndDasServices =>
            bool.Parse(GetAppSetting(AppSettingFormatter(ValidateDcAndDasServicesAppSetting)) ?? "false");

        public TimeSpan TimeToWait =>
            TimeSpan.Parse(GetAppSetting(AppSettingFormatter(TimeToWaitAppSetting)) ?? "00:00:30");

        public TimeSpan TimeToWaitForUnexpected =>
            TimeSpan.Parse(GetAppSetting(AppSettingFormatter(TimeToWaitForUnexpectedAppSetting)) ?? "00:00:30");

        public TimeSpan TimeToPause =>
            TimeSpan.Parse(GetAppSetting(AppSettingFormatter(TimeToPauseAppSetting)) ?? "00:00:05");

        public TimeSpan DefaultMessageTimeToLive =>
            TimeSpan.Parse(GetAppSetting(AppSettingFormatter(DefaultMessageTimeToLiveAppSetting)) ?? "00:20:00");

        private static string AppSettingFormatter(string appSettingName)
        {
            return $"AppSettings:{appSettingName}";
        }

        public string GetAppSetting(string keyName)
        {
            return Config.GetSection(keyName).Value ??
                   throw new InvalidOperationException($"{keyName} not found in app settings.");
        }

        public string GetConnectionString(string name)
        {
            return Config.GetConnectionString(name) ??
                   throw new InvalidOperationException($"{name} not found in connection strings.");
        }
    }
}