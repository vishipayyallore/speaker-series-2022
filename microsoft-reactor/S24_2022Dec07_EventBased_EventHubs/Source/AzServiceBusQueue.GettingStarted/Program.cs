using AzServiceBusQueue.MsgSender.Data;
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
bool receiveMessages = true;

// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var serviceBusClient = new ServiceBusClient(connectionString);
ServiceBusSender serviceBusSender = serviceBusClient.CreateSender(queueName);

await SendSingleMessage(serviceBusSender);

await SendBatchMessages(serviceBusSender);

if (receiveMessages)
{
    await ReceiveSingleMessage(queueName, serviceBusClient);
}

await serviceBusSender.DisposeAsync();
WriteLine("\n\nPress any key ...");

static List<Order> GetOrders()
{
    return new()
    {
        new Order(){Quantity=100,UnitPrice=9.99F},
        new Order(){Quantity=200,UnitPrice=10.99F},
        new Order(){Quantity=300,UnitPrice=8.99F}
    };
}

static async Task SendSingleMessage(ServiceBusSender serviceBusSender)
{
    WriteLine("Sending Single String Message");
    ServiceBusMessage message = new($"Hello world! {DateTime.Now}");
    await serviceBusSender.SendMessageAsync(message);

    WriteLine("Sending Single Order Message");
    ServiceBusMessage orderMessage = new(JsonSerializer.Serialize(new Order() { Quantity = 100, UnitPrice = 9.99F }));
    await serviceBusSender.SendMessageAsync(orderMessage);
}

static async Task SendBatchMessages(ServiceBusSender serviceBusSender)
{
    List<Order> orders = GetOrders();
    int i = 0;
    string[] Importance = new string[] { "High", "Medium", "Low" };

    WriteLine("Creating Service Bus Batch Messages");
    ServiceBusMessageBatch serviceBusMessageBatch = await serviceBusSender.CreateMessageBatchAsync();

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
}

static async Task ReceiveSingleMessage(string queueName, ServiceBusClient serviceBusClient)
{
    ServiceBusReceiver receiver = serviceBusClient.CreateReceiver(queueName);

    ServiceBusReceivedMessage receivedMessage = await receiver.ReceiveMessageAsync();
    WriteLine($"Message Received: {receivedMessage.Body}");

    // dead-letter the message, thereby preventing the message from being received again without receiving from the dead letter queue.
    await receiver.DeadLetterMessageAsync(receivedMessage);

    // receive the dead lettered message with receiver scoped to the dead letter queue.
    ServiceBusReceiver dlqReceiver = serviceBusClient.CreateReceiver(queueName, new ServiceBusReceiverOptions
    {
        SubQueue = SubQueue.DeadLetter
    });
    ServiceBusReceivedMessage dlqMessage = await dlqReceiver.ReceiveMessageAsync();

    // await receiver.CompleteMessageAsync(receivedMessage);
}