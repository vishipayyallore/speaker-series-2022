using AzServiceBusQueue.GettingStarted.Data;
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using static System.Console;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

string connectionString = _configuration["AzServiceBus:ConnectionString"];
string queueName = _configuration["AzServiceBus:QueueName"];
string[] Importance = new string[] { "High", "Medium", "Low" };

// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var client = new ServiceBusClient(connectionString);
ServiceBusSender serviceBusSender = client.CreateSender(queueName);

ServiceBusMessage message = new($"Hello world! {DateTime.Now}");
await serviceBusSender.SendMessageAsync(message);

ServiceBusMessage orderMessage = new(JsonSerializer.Serialize(new Order() { Quantity = 100, UnitPrice = 9.99F }));
await serviceBusSender.SendMessageAsync(orderMessage);

List<Order> orders = new()
{
    new Order(){Quantity=100,UnitPrice=9.99F},
    new Order(){Quantity=200,UnitPrice=10.99F},
    new Order(){Quantity=300,UnitPrice=8.99F}
};

ServiceBusMessageBatch serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();
int i = 0;
foreach (Order order in orders)
{
    ServiceBusMessage serviceBusMessage = new(JsonSerializer.Serialize(order))
    {
        ContentType = "application/json"
    };
    serviceBusMessage.ApplicationProperties.Add("Importance", Importance[i++]);
    if (!serviceBusMessageBatch.TryAddMessage(serviceBusMessage))
    {
        throw new Exception("Error occured");
    }
}
Console.WriteLine("Sending Batch messages");
await serviceBusSender.SendMessagesAsync(serviceBusMessageBatch);

WriteLine("\n\nPress any key ...");