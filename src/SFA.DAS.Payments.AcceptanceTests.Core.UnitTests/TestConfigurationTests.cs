using FluentAssertions;
using Microsoft.Extensions.Configuration;
using SFA.DAS.Payments.AcceptanceTests.Core.Infrastructure;

namespace SFA.DAS.Payments.AcceptanceTests.Core.UnitTests;

[TestFixture]
public class TestsConfigurationTests
{
    private TestsConfiguration jsonTestsConfiguration;

    [SetUp]
    public void Setup()
    {
        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string>
            {
                { "AppSettings:EndpointName", "TestEndpoint" },
                { "ConnectionStrings:StorageConnectionString", "TestStorageConnectionString" },
                { "ConnectionStrings:ServiceBusConnectionString", "TestServiceBusConnectionString" },
                { "ConnectionStrings:DASServiceBusConnectionString", "TestDASServiceBusConnectionString" },
                { "ConnectionStrings:PaymentsConnectionString", "TestPaymentsConnectionString" },
                { "AppSettings:ValidateDcAndDasServices", "true" },
                { "AppSettings:TimeToWait", "00:00:30" },
                { "AppSettings:TimeToWaitForUnexpected", "00:00:30" },
                { "AppSettings:TimeToPause", "00:00:05" },
                { "AppSettings:DefaultMessageTimeToLive", "00:20:00" }
            }!)
            .Build();

        jsonTestsConfiguration = new TestsConfiguration(config);
    }

    [Test]
    public void JsonTestConfiguration_ReturnsCorrectValue()
    {
        jsonTestsConfiguration.AcceptanceTestsEndpointName.Should().Be("TestEndpoint");
        jsonTestsConfiguration.StorageConnectionString.Should().Be("TestStorageConnectionString");
        jsonTestsConfiguration.ServiceBusConnectionString.Should().Be("TestServiceBusConnectionString");
        jsonTestsConfiguration.DasServiceBusConnectionString.Should().Be("TestDASServiceBusConnectionString");
        jsonTestsConfiguration.PaymentsConnectionString.Should().Be("TestPaymentsConnectionString");
        jsonTestsConfiguration.ValidateDcAndDasServices.Should().BeTrue();
        jsonTestsConfiguration.TimeToWait.Should().Be(TimeSpan.FromSeconds(30));
        jsonTestsConfiguration.TimeToWaitForUnexpected.Should().Be(TimeSpan.FromSeconds(30));
        jsonTestsConfiguration.TimeToPause.Should().Be(TimeSpan.FromSeconds(5));
        jsonTestsConfiguration.DefaultMessageTimeToLive.Should().Be(TimeSpan.FromMinutes(20));
    }
}