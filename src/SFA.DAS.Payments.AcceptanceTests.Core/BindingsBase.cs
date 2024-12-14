using Autofac;
using NServiceBus;
using SFA.DAS.Payments.AcceptanceTests.Core.Infrastructure;

namespace SFA.DAS.Payments.AcceptanceTests.Core
{
    public abstract class BindingsBase
    {
        public static ContainerBuilder Builder { get; protected set; } // -1
        public static IContainer Container { get; protected set; } // 50
        public static IMessageSession MessageSession { get; protected set; }
        public static ITestsConfiguration Config => Container.Resolve<ITestsConfiguration>();

        public static string Environment => Config.GetAppSetting("Environment");
    }
}