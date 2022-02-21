using WeatherUtility.Core.Entities;

namespace WeatherUtility.Core.Interfaces
{

    public interface IWeatherReport
    {
        void DisplayReport(WeatherData weatherData);
    }

}
