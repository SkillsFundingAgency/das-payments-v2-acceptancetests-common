﻿using System;
using System.Threading;
using Autofac;
using NServiceBus;
using NUnit.Framework;
using SFA.DAS.Payments.AcceptanceTests.Core.Automation;
using SFA.DAS.Payments.AcceptanceTests.Core.Data;
using SFA.DAS.Payments.AcceptanceTests.Core.Infrastructure;
using TechTalk.SpecFlow;

namespace SFA.DAS.Payments.AcceptanceTests.Core
{
    [Binding]
    public abstract class StepsBase
    {
        public ScenarioContext ScenarioCtx { get; }
        public static ContainerBuilder Builder { get; protected set; }
        public static IContainer Container { get; protected set; }
        public static IMessageSession MessageSession { get; protected set; }
        public TestsConfiguration Config => Container.Resolve<TestsConfiguration>();
        public TestSession TestSession { get => Get<TestSession>(); set => Set(value); }
        public string Environment => Config.GetAppSetting("Environment");
        protected string CollectionYear { get => Get<string>("collection_year"); set => Set(value, "collection_year"); }
        protected byte CollectionPeriod { get => Get<byte>("collection_period"); set => Set(value, "collection_period"); }
        public bool IsDevEnvironment => (Environment?.Equals("DEVELOPMENT", StringComparison.OrdinalIgnoreCase) ?? false) ||
                                        (Environment?.Equals("LOCAL", StringComparison.OrdinalIgnoreCase) ?? false);
        protected decimal SfaContributionPercentage { get => Get<decimal>("sfa_contribution_percentage"); set => Set(value, "sfa_contribution_percentage"); }
        protected byte ContractType { get => Get<byte>("contract_type"); set => Set(value, "contract_type"); }

        protected StepsBase(ScenarioContext scenarioContext)
        {
            ScenarioCtx = scenarioContext;
        }

        public T Get<T>(string key = null)// where T : class
        {
            return key == null ? ScenarioCtx.Get<T>() : ScenarioCtx.Get<T>(key);
        }

        public void Set<T>(T item, string key = null)
        {
            if (key == null)
                ScenarioCtx.Set(item);
            else
                ScenarioCtx.Set(item, key);
        }

        protected void WaitForIt(Func<bool> lookForIt, string failText)
        {
            var endTime = DateTime.Now.Add(Config.TimeToWait);
            while (DateTime.Now < endTime)
            {
                if (lookForIt())
                    return;
                Thread.Sleep(Config.TimeToPause);
            }
            Assert.Fail(failText);
        }

        protected void WaitForIt(Func<Tuple<bool, string>> lookForIt, string failText)
        {
            var endTime = DateTime.Now.Add(Config.TimeToWait);
            var reason = "";
            var pass = false;
            while (DateTime.Now < endTime)
            {
                (pass, reason) = lookForIt();
                if (pass)
                    return;
                Thread.Sleep(Config.TimeToPause);
            }
            Assert.Fail(failText + " - " + reason);
        }

        protected bool WaitForIt(Func<bool> lookForIt)
        {
            var endTime = DateTime.Now.Add(Config.TimeToWait);
            while (DateTime.Now < endTime)
            {
                if (lookForIt())
                    return true;
                Thread.Sleep(Config.TimeToPause);
            }
            return false;
        }
    }
}
