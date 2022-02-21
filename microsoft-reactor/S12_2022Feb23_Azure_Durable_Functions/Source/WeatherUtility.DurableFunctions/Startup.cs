using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using WeatherUtility.Core.Interfaces;
using WeatherUtility.DurableFunction;
using WeatherUtility.Lib;

[assembly: FunctionsStartup(typeof(Startup))]
namespace WeatherUtility.DurableFunction
{

    public class Startup : FunctionsStartup
    {

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder?.Services.AddTransient<ITemperatureConvertor, TemperatureConvertor>();
        }

    }

}
