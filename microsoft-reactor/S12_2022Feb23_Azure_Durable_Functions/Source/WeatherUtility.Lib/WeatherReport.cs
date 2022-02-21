using WeatherUtility.Core.Entities;
using WeatherUtility.Core.Interfaces;

using static System.Console;

namespace WeatherUtility.Lib
{

    public class WeatherReport : IWeatherReport
    {
        private readonly ITemperatureConvertor _temperatureConvertor;
        private readonly ITemperatureUtility _temperatureUtility;

        public WeatherReport(ITemperatureConvertor temperatureConvertor, ITemperatureUtility temperatureUtility)
        {
            _temperatureConvertor = temperatureConvertor ?? throw new ArgumentNullException(nameof(temperatureConvertor));

            _temperatureUtility = temperatureUtility ?? throw new ArgumentNullException(nameof(temperatureUtility));
        }

        public void DisplayReport(WeatherData weatherData)
        {
            var temperatureFahrenheit = _temperatureConvertor.CelsiusToFahrenheit(weatherData.TemperatureCelsius);

            WriteLine($"Comfort Index for {weatherData.Location} : {_temperatureUtility.GetComfortIndex(temperatureFahrenheit, weatherData.Humidity)}");
        }

    }

}
