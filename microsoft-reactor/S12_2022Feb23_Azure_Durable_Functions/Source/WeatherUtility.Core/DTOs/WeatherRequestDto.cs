namespace WeatherUtility.Core.DTOs
{

    public class WeatherRequestDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = "No Name";
    }

}
