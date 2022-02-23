using WeatherUtility.Core.Entities;

namespace WeatherUtility.Core.DTOs
{

    public class WeatherResponseDto
    {
        public bool Success { get; set; } = true;

        public string Message { get; set; } = "All Is Well";

        public IList<WeatherData>? Data { get; set; }
    }

}
