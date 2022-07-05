// See https://aka.ms/new-console-template for more information
using AzBlobStorage.GettingStarted;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

// To Show Case Delete Blob with Snapshots
var deleteFile = false;
var deleteBlobContainer = false;

// Copy the connection string from the portal in the variable below.
string storageConnectionString = _configuration["AzStorage:BlobStorageConnectionString"];
BlobServiceClient blobServiceClient = new(storageConnectionString);

string containerName = _configuration["AzStorage:BlobContainerName"];
var blobContainerClient = await BlobGettingStartedHelper.CreateContainerAsync(blobServiceClient, containerName);

string fileForSnapshotAndDelete = _configuration["AzStorage:FileForSnapshotAndDelete"];
foreach (var fileEntry in Directory.GetFiles(_configuration["AzStorage:FilesLocation"]))
{
    await BlobGettingStartedHelper.UploadBlobAsync(blobContainerClient, fileEntry, fileForSnapshotAndDelete, deleteFile);
}

await BlobGettingStartedHelper.ListBlobAsync(blobContainerClient);

if (deleteBlobContainer)
{
    await blobContainerClient.DeleteIfExistsAsync();
}

Console.WriteLine("\n\nPress any key ...");


