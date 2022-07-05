// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

// Copy the connection string from the portal in the variable below.
string storageConnectionString = _configuration["AzStorage:BlobStorageConnectionString"];
BlobServiceClient blobServiceClient = new(storageConnectionString);

string containerName = "demoblob" + Guid.NewGuid().ToString("N");
var containerClient = await CreateContainerAsync(blobServiceClient, containerName);

Console.WriteLine("\n\nPress any key ...");

static async Task<BlobContainerClient> CreateContainerAsync(BlobServiceClient blobServiceClient, string containerName)
{
    BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
    Console.WriteLine($"A container named '{containerName}' has been created. \nTake a minute and verify in the portal. \nNext a file will be created and uploaded to the container.");

    BlobContainerClient container = blobServiceClient.GetBlobContainerClient(containerName);
    await container.CreateIfNotExistsAsync();

    return containerClient;
}