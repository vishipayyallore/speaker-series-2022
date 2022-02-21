using WeatherUtility.Core.Interfaces;

namespace WeatherUtility.Lib
{
    public class TemperatureConvertor : ITemperatureConvertor
    {

        // (32°F − 32) × 5/9 = 0°C
        public float FahrenheitToCelsius(float temperatureInFahrenheit)
        {
            return (temperatureInFahrenheit - 32) / 1.8f;
        }

        // (32°C × 9/5) + 32 = 89.6°F
        public float CelsiusToFahrenheit(float temperatureInCelsius)
        {
            return (temperatureInCelsius * 1.8f) + 32;
        }

    }

}