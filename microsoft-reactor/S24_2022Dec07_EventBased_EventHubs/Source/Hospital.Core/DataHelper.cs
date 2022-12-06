namespace Hospital.Core
{
    public static class DataHelper
    {

        public static IEnumerable<Device> GetDummyData()
        {
            return new List<Device>()
            {
                new Device { deviceId = "P101", patientName = "One", heartBeat = "67", bloodPresure = "120/80" },
                new Device { deviceId = "P102", patientName = "Two", heartBeat = "74", bloodPresure = "120/80" },
                new Device { deviceId = "P103", patientName = "Three", heartBeat = "99", bloodPresure = "120/80" },
                new Device { deviceId = "P104", patientName = "Four", heartBeat = "82", bloodPresure = "120/80" }
            };
        }

    }
}
