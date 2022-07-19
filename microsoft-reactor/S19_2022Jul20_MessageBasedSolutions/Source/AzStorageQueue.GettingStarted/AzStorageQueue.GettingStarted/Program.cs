using AzStorageQueue.GettingStarted;
using Azure.Storage.Queues;
using Microsoft.Extensions.Configuration;

using static System.Console;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

// Copy the connection string from the portal in the variable below.
string connectionString = _configuration["AzStorage:ConnectionString"];
string queueName = _configuration["AzStorage:QueueName"];

QueueClient queueClient = AzStorageQueueHelper.CreateQueueClient(connectionString, queueName);

await AzStorageQueueHelper.CreateQueue(queueClient);

string message = $"Simple message {DateTime.Now}";
await AzStorageQueueHelper.InsertMessage(queueClient, queueName, message);

await AzStorageQueueHelper.PeekMessage(queueClient, queueName);

await AzStorageQueueHelper.UpdateMessage(queueClient, queueName);

WriteLine("\n\nPress any key ...");


