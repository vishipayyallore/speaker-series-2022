using WeatherUtility.Core.Interfaces;

namespace WeatherUtility.Lib
{
    public class TemperatureUtility : ITemperatureUtility
    {
        public float GetComfortIndex(float temperatureFahrenheit, float humidityPercent)
        {
            var comfortIndex = (temperatureFahrenheit + humidityPercent) / 4;

            return comfortIndex;
        }
    }
}
