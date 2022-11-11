using AdventureWorks.Upload.Models;
using Microsoft.Azure.Cosmos;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using static System.Console;

const string EndpointUrl = "https://polycosmos12nov2022dev.documents.azure.com:443/";
const string AuthorizationKey = "wv9Rb51Urre6KO3NnzFsj7KhBkXPNykYaz7taInxecEkpPqhqaej7mDvpKiYifj4pZ4TTjjGo4qmACDbsRO0ow==";
const string DatabaseName = "Retail";
const string ContainerName = "Online";
const string _partitionKey = "/Category";
const string JsonFilePath = "./DataStore/models.json";

int amountToInsert;
List<Model> models = new();

try
{
    using CosmosClient cosmosClient = new(EndpointUrl, AuthorizationKey, new CosmosClientOptions() { AllowBulkExecution = true });

    WriteLine($"Creating a database if not already exists...");
    Database cosmosDatabase = await cosmosClient.CreateDatabaseIfNotExistsAsync(DatabaseName);

    // Configure indexing policy to exclude all attributes to maximize RU/s usage
    WriteLine($"Creating a container if not already exists...");

    var onlineContainerResponse = await cosmosDatabase?.CreateContainerIfNotExistsAsync(ContainerName, _partitionKey)!;
    var onlineContainer = onlineContainerResponse.Container;

    using (StreamReader reader = new(File.OpenRead(JsonFilePath)))
    {
        string? json = await reader.ReadToEndAsync();
        models = JsonSerializer.Deserialize<List<Model>>(json);
        amountToInsert = models is not null ? models.Count : 0;
    }

    // Prepare items for insertion
    WriteLine($"Preparing {amountToInsert} items to insert...");

    // Create the list of Tasks
    WriteLine($"Starting...");
    Stopwatch stopwatch = Stopwatch.StartNew();

    List<Task> tasks = new(amountToInsert);
    foreach (Model model in models)
    {
        tasks.Add(onlineContainer.CreateItemAsync(model, new PartitionKey(model.Category))
            .ContinueWith(itemResponse =>
            {
                if (!itemResponse.IsCompletedSuccessfully)
                {
                    AggregateException innerExceptions = itemResponse.Exception.Flatten();
                    if (innerExceptions.InnerExceptions.FirstOrDefault(innerEx => innerEx is CosmosException) is CosmosException cosmosException)
                    {
                        Console.WriteLine($"Received {cosmosException.StatusCode} ({cosmosException.Message}).");
                    }
                    else
                    {
                        Console.WriteLine($"Exception {innerExceptions.InnerExceptions.FirstOrDefault()}.");
                    }
                }
            }));
    }

    // Wait until all are done
    await Task.WhenAll(tasks);

    stopwatch.Stop();

    WriteLine($"Finished writing {amountToInsert} items in {stopwatch.Elapsed}.");
}
catch (Exception ex)
{
    Console.WriteLine(ex);
}

