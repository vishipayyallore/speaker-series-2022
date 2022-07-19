using Azure.Storage.Queues;
using Azure.Storage.Queues.Models;
using Microsoft.Extensions.Configuration;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

// Copy the connection string from the portal in the variable below.
string connectionString = _configuration["AzStorage:ConnectionString"];
string queueName = _configuration["AzStorage:QueueName"];

// Instantiate a QueueClient which will be used to create and manipulate the queue
QueueClient queueClient = new QueueClient(connectionString, queueName);

await CreateQueue(queueClient);

string message = $"Simple message {DateTime.Now}";
await InsertMessage(queueClient, queueName, message);

await PeekMessage(queueClient, queueName);

await UpdateMessage(queueClient, queueName);

Console.WriteLine("\n\nPress any key ...");

static async Task<bool> CreateQueue(QueueClient queueClient)
{
    try
    {
        // Create the queue
        await queueClient.CreateIfNotExistsAsync();

        if (queueClient.Exists())
        {
            Console.WriteLine($"Queue created: '{queueClient.Name}'");
            return true;
        }
        else
        {
            Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
            return false;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception: {ex.Message}\n\n");
        Console.WriteLine($"Make sure the Azurite storage emulator running and try again.");
        return false;
    }
}

static async Task InsertMessage(QueueClient queueClient, string queueName, string message)
{
    // Create the queue if it doesn't already exist
    queueClient.CreateIfNotExists();

    if (queueClient.Exists())
    {
        // Send a message to the queue
        await queueClient.SendMessageAsync(message);
    }

    Console.WriteLine($"Inserted: {message}");
}

static async Task PeekMessage(QueueClient queueClient, string queueName)
{
    if (queueClient.Exists())
    {
        // Peek at the next message
        PeekedMessage[] peekedMessage = await queueClient.PeekMessagesAsync();

        // Display the message
        Console.WriteLine($"Peeked message: '{peekedMessage[0].Body}'");
    }
}

static async Task UpdateMessage(QueueClient queueClient, string queueName)
{
    if (queueClient.Exists())
    {
        // Get the message from the queue
        QueueMessage[] message = await queueClient.ReceiveMessagesAsync();

        // Update the message contents
        await queueClient.UpdateMessageAsync(message[0].MessageId,
                message[0].PopReceipt,
                $"Updated contents :: {message[0].Body}",
                TimeSpan.FromSeconds(60.0)  // Make it invisible for another 60 seconds
            );
    }
}