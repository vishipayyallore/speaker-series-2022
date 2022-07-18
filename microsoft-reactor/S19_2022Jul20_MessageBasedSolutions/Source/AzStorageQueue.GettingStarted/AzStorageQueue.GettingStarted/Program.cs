using Azure.Storage.Queues;
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

await CreateQueue(queueClient, queueName);

Console.WriteLine("\n\nPress any key ...");

static async Task<bool> CreateQueue(QueueClient queueClient, string queueName)
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