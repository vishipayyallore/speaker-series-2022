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

// since ServiceBusClient implements IAsyncDisposable we create it with "await using"
await using var serviceBusClient = new ServiceBusClient(connectionString);

var options = new ServiceBusProcessorOptions
{
    AutoCompleteMessages = false,
    MaxConcurrentCalls = 2
};

// create a processor that we can use to process the messages
ServiceBusProcessor processor = serviceBusClient.CreateProcessor(queueName, options);

// configure the message and error handler to use
processor.ProcessMessageAsync += MessageHandler;
processor.ProcessErrorAsync += ErrorHandler;

// start processing
await processor.StartProcessingAsync();

WriteLine("\n\nPress any key to STOP ...");

// since the processing happens in the background, we add a Console.ReadKey to allow the processing to continue until a key is pressed.
Console.ReadKey();


// handle received messages
static async Task MessageHandler(ProcessMessageEventArgs args)
{
    string body = args.Message.Body.ToString();
    Console.WriteLine($"Received: {body}");

    // complete the message. messages is deleted from the queue. 
    await args.CompleteMessageAsync(args.Message);
}

// handle any errors when receiving messages
static Task ErrorHandler(ProcessErrorEventArgs args)
{
    // the error source tells me at what point in the processing an error occurred
    Console.WriteLine(args.ErrorSource);
    // the fully qualified namespace is available
    Console.WriteLine(args.FullyQualifiedNamespace);
    // as well as the entity path
    Console.WriteLine(args.EntityPath);
    Console.WriteLine(args.Exception.ToString());

    return Task.CompletedTask;
}