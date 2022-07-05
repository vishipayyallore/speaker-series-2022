// See https://aka.ms/new-console-template for more information
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.Extensions.Configuration;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

// Copy the connection string from the portal in the variable below.
string storageConnectionString = _configuration["AzStorage:BlobStorageConnectionString"];
BlobServiceClient blobServiceClient = new(storageConnectionString);

string containerName = _configuration["AzStorage:BlobContainerName"];
var containerClient = await CreateContainerAsync(blobServiceClient, containerName);

string fileForSnapshotAndDelete = _configuration["AzStorage:FileForSnapshotAndDelete"];
foreach (var fileEntry in Directory.GetFiles(_configuration["AzStorage:FilesLocation"]))
{
    await UploadBlobAsync(containerClient, fileEntry, fileForSnapshotAndDelete);
}

await ListBlobAsync(containerClient);

Console.WriteLine("\n\nPress any key ...");

static async Task<BlobContainerClient> CreateContainerAsync(BlobServiceClient blobServiceClient, string containerName)
{
    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
    await containerClient.CreateIfNotExistsAsync();

    return containerClient;
}

static async Task UploadBlobAsync(BlobContainerClient containerClient, string localFilePath, string fileForSnapshotAndDelete)
{
    var fileName = Path.GetFileName(localFilePath);
    BlobClient blobClient = containerClient.GetBlobClient(fileName);

    Console.WriteLine("\nUploading {0}", fileName);

    var fileUploadResults = await blobClient.UploadAsync(File.OpenRead($"{localFilePath}"), true);

    if (fileUploadResults.GetRawResponse().Status == 201)
    {
        Console.WriteLine($"{fileName} uploaded to the {blobClient.Uri} location");

        if (fileName == fileForSnapshotAndDelete)
        {
            var blockBlobSnapshot = await blobClient.CreateSnapshotAsync();
        }
    }
}

static async Task ListBlobAsync(BlobContainerClient blobContainerClient)
{
    Console.WriteLine("\n\nDisplaying the Blobs");

    var counter = 1;
    await foreach (var blob in blobContainerClient.GetBlobsAsync())
    {
        Console.WriteLine("{2}- {0} (type: {1})", blob.Name, blob.GetType(), (counter++));

        await DownloadBlobAsync(blobContainerClient, blob);
    }
}

static async Task DownloadBlobAsync(BlobContainerClient containerClient, BlobItem blob)
{
    Console.WriteLine("\tDownloading {0}", blob.Name);

    BlobClient blobClient = containerClient.GetBlobClient(blob.Name);
    await blobClient.DownloadToAsync(string.Format("./Downloads/CopyOf{0}", blob.Name));

    Console.WriteLine("\tDownloaded {0}", blob.Name);
}