using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

[assembly: TestFramework("FinanceModuleCoreAPI.Tests.Startup", "Tests")]

namespace UnitTests
{
    public class Startup : DependencyInjectionTestFramework
    {
        public Startup(IMessageSink messageSink) : base(messageSink)
        {
        }

        protected void ConfigureServices(IServiceCollection services)
        {

        }

        protected override IHostBuilder CreateHostBuilder(AssemblyName assemblyName)
        {
            return base.CreateHostBuilder(assemblyName).ConfigureServices(ConfigureServices);
        }

    }
}
