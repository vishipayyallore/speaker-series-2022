namespace Hospital.Core
{

    public class Device
    {
        public string deviceId { get; set; } = "D101";

        public string patientName { get; set; } = "No Name";

        public string heartBeat { get; set; } = "72";

        public string bloodPresure { get; set; } = "120/80";

        public DateTime sendAt => DateTime.UtcNow;
    }

}