using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Consumer;
using Azure.Messaging.EventHubs.Producer;
using Hospital.Core;
using Microsoft.Azure.Amqp;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddUserSecrets("dc15d825-8181-4631-9245-31ec411f4dc3")
    .Build();

var connectionString = _configuration["ConnectionString"];
var eventHubName = _configuration["EventHubName"];
var consumerGroup = EventHubConsumerClient.DefaultConsumerGroupName;

await GetPartitionIds();
await ReadEvents();

async Task GetPartitionIds()
{
    await using EventHubConsumerClient eventHubConsumerClient = new(consumerGroup, connectionString);

    string[] partitionIds = await eventHubConsumerClient.GetPartitionIdsAsync();

    foreach (string partitionId in partitionIds)
    {
        Console.WriteLine("Partition Id {0}", partitionId);
    }
}

async Task ReadEvents()
{
    await using var consumer = new EventHubConsumerClient(consumerGroup, connectionString, eventHubName);

    using CancellationTokenSource cancellationSource = new();
    cancellationSource.CancelAfter(TimeSpan.FromSeconds(145));

    await foreach (PartitionEvent _event in consumer.ReadEventsAsync(cancellationSource.Token))
    {
        Console.WriteLine($"Partition ID {_event.Partition.PartitionId}");
        Console.WriteLine($"Data Offset {_event.Data.Offset}");
        Console.WriteLine($"Sequence Number {_event.Data.SequenceNumber}");
        Console.WriteLine($"Partition Key {_event.Data.PartitionKey}");
        Console.WriteLine(Encoding.UTF8.GetString(_event.Data.EventBody));
    }
}
