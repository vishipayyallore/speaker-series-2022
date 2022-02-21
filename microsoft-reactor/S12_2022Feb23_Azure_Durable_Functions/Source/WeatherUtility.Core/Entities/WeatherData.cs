namespace WeatherUtility.Core.Entities
{

    public class WeatherData
    {
        public string Location { get; set; } = string.Empty;

        public float TemperatureCelsius { get; set; }

        public float TemperatureFahrenheit { get; set; }

        public float Humidity { get; set; }

        public float ComfortIndex { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.UtcNow;
    }

}
