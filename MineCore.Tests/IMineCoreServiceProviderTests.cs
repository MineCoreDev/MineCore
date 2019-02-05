using System;
using MineCore.Events;
using MineCore.Models;
using NUnit.Framework;

namespace MineCore.Tests
{
    [TestFixture]
    public class IMineCoreServiceProviderTests
    {
        public class TestService : IMineCoreServiceProvider
        {
            public string ServiceName => "TestService";
            public Type[] Dependencies => new Type[0];

            public event EventHandler<ServiceProviderEventArgs> ServiceEnabled;
            public event EventHandler<ServiceProviderEventArgs> ServiceDisabled;

            public void OnServiceEnabled(object sender, ServiceProviderEventArgs args)
            {
                ServiceEnabled?.Invoke(sender, args);
            }

            public void OnServiceDisabled(object sender, ServiceProviderEventArgs args)
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