using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Hospital.Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddUserSecrets("dc15d825-8181-4631-9245-31ec411f4dc3")
    .Build();

var connectionString = _configuration["ConnectionString"];
var eventHubName = _configuration["EventHubName"];

foreach(var patient in DataHelper.GetDummyData())
{
    await SendPatientData(patient);
}

async Task SendPatientData(Device patientData)
{
    await using var producer = new EventHubProducerClient(connectionString, eventHubName);

    var batchOptions = new CreateBatchOptions
    {
        PartitionKey = patientData.deviceId
    };

    using EventDataBatch eventBatch = await producer.CreateBatchAsync(batchOptions);

    EventData eventData = new(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(patientData)));

    if (!eventBatch.TryAdd(eventData))
    {
        Console.WriteLine("Error has occured");
    }

    await producer.SendAsync(eventBatch);

    Console.WriteLine($"Sent data of {patientData.patientName}");
}
