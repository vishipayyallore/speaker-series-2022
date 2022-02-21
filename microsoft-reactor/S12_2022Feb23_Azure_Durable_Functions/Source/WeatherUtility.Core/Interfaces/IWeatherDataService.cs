using WeatherUtility.Core.Entities;

namespace WeatherUtility.Core.Interfaces
{

    public interface IWeatherDataService
    {
        IList<WeatherData> GetWeatherDataFromDatabase();
    }

}
