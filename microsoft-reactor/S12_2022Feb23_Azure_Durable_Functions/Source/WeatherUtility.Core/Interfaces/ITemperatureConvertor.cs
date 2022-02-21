namespace WeatherUtility.Core.Interfaces
{

    public interface ITemperatureConvertor
    {
        float FahrenheitToCelsius(float temperatureInFahrenheit);

        float CelsiusToFahrenheit(float temperatureInCelsius);
    }

}
