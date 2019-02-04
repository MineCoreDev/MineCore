using System;
using MineCore.Events;
using NUnit.Framework;

namespace MineCore.Tests
{
    [TestFixture]
    public class IMineCoreServiceProviderTests
    {
        public class TestService : IMineCoreServiceProvider
        {
            public string ServiceName => "TestService";
            public event EventHandler<ServiceProviderEventArgs> ServiceEnabled;
            public event EventHandler<ServiceProviderEventArgs> ServiceDisabled;

            internal void OnServiceEnabled(object sender, ServiceProviderEventArgs args)
            {
                ServiceEnabled?.Invoke(sender, args);
            }

            internal void OnServiceDisabled(object sender, ServiceProviderEventArgs args)
            {
                ServiceDisabled?.Invoke(sender, args);
            }
        }

        [Test]
        public void Tests()
        {
            TestService service = new TestService();
            Assert.IsTrue(service.ServiceName == "TestService");

            service.ServiceEnabled += (sender, args) => { Assert.IsTrue(args.ServiceProvider == service); };
            service.ServiceDisabled += (sender, args) => { Assert.IsTrue(args.ServiceProvider == service); };

            service.OnServiceEnabled(service, new ServiceProviderEventArgs(service));
            service.OnServiceDisabled(service, new ServiceProviderEventArgs(service));
        }
    }
}