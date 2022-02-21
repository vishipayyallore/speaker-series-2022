namespace WeatherUtility.Core.Interfaces
{

    public interface ITemperatureUtility
    {
        float GetComfortIndex(float temperatureFahrenheit, float humidityPercent);
    }

}
