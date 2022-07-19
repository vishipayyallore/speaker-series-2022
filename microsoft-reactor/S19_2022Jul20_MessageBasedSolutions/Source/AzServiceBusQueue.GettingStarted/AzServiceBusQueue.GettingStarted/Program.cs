using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;

using static System.Console;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

string connectionString = _configuration["AzServiceBus:ConnectionString"];
string queueName = _configuration["AzServiceBus:QueueName"];

// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var client = new ServiceBusClient(connectionString);
ServiceBusSender sender = client.CreateSender(queueName);

ServiceBusMessage message = new($"Hello world! {DateTime.Now}");
await sender.SendMessageAsync(message);

WriteLine("\n\nPress any key ...");