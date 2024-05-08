using Microsoft.Extensions.Configuration;
using System;
using System.Configuration;

namespace SFA.DAS.Payments.AcceptanceTests.Core.Infrastructure
{
    public class TestsConfiguration
    {
        public static IConfigurationRoot Config;
        public bool IsJsonConfig;
        public string ConfigSettingsName { get; set; }

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

        public TestsConfiguration()
        {
            
        }
        public TestsConfiguration(IConfigurationRoot config, string? configSettingsName = "AppSettings")
        {
            Config = config;
            IsJsonConfig = true;
            ConfigSettingsName = configSettingsName;
        }

        private string AppSettingFormatter(string appSettingName) => IsJsonConfig ? $"{ConfigSettingsName}:{appSettingName}" : appSettingName;
        public string AcceptanceTestsEndpointName => GetAppSetting(AppSettingFormatter(EndpointNameAppSetting));
        public string StorageConnectionString => GetConnectionString(AppSettingFormatter(StorageConnectionStringAppSetting));
        public string ServiceBusConnectionString => GetConnectionString(AppSettingFormatter(ServiceBusConnectionStringAppSetting));
        public string DasServiceBusConnectionString => GetConnectionString(AppSettingFormatter(DasServiceBusConnectionStringAppSetting));
        public string PaymentsConnectionString => GetConnectionString(AppSettingFormatter(PaymentsConnectionStringAppSetting));
        public bool ValidateDcAndDasServices => bool.Parse(GetAppSetting(AppSettingFormatter(ValidateDcAndDasServicesAppSetting)) ?? "false");
        public TimeSpan TimeToWait => TimeSpan.Parse(GetAppSetting(AppSettingFormatter(TimeToWaitAppSetting)) ?? "00:00:30");
        public TimeSpan TimeToWaitForUnexpected => TimeSpan.Parse(GetAppSetting(AppSettingFormatter(TimeToWaitForUnexpectedAppSetting)) ?? "00:00:30");
        public TimeSpan TimeToPause => TimeSpan.Parse(GetAppSetting(AppSettingFormatter(TimeToPauseAppSetting)) ?? "00:00:05");
        public TimeSpan DefaultMessageTimeToLive => TimeSpan.Parse(GetAppSetting(AppSettingFormatter(DefaultMessageTimeToLiveAppSetting)) ?? "00:20:00");
        public string GetAppSetting(string keyName) => IsJsonConfig? Config.GetSection(keyName).Value : ConfigurationManager.AppSettings[keyName] ?? throw new InvalidOperationException($"{keyName} not found in app settings.");
        public string GetConnectionString(string name) => IsJsonConfig ? Config.GetConnectionString(name) : ConfigurationManager.ConnectionStrings[name].ConnectionString ?? throw new InvalidOperationException($"{name} not found in connection strings.");
    }
}