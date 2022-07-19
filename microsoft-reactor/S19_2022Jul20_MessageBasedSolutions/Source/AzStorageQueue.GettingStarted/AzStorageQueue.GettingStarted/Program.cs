using AzStorageQueue.GettingStarted;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;
using System.Text;
using static System.Console;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

// Copy the connection string from the portal in the variable below.
string connectionString = _configuration["AzStorage:ConnectionString"];
string queueName = _configuration["AzStorage:QueueName"];
bool updateMessage = true;
bool deleteMessages = true;

QueueClient queueClient = AzStorageQueueHelper.CreateQueueClient(connectionString, queueName);

Console.ForegroundColor = ConsoleColor.Blue;

await AzStorageQueueHelper.CreateQueue(queueClient);

Console.ForegroundColor = ConsoleColor.Green;
List<string> messages = new();
for (int i = 0; i < 5; i++)
{
    messages.Add($"{i + 1}. Simple message {DateTime.Now}");
}

await AzStorageQueueHelper.InsertMessages(queueClient, messages);

await AzStorageQueueHelper.PeekMessage(queueClient);

if (updateMessage)
{
    Console.ForegroundColor = ConsoleColor.Yellow;
    await AzStorageQueueHelper.UpdateMessage(queueClient);
}

if (deleteMessages)
{
    Console.ForegroundColor = ConsoleColor.Red;
    await AzStorageQueueHelper.DequeueMessages(queueClient);
}

WriteLine("\n\nPress any key ...");

ResetColor();

