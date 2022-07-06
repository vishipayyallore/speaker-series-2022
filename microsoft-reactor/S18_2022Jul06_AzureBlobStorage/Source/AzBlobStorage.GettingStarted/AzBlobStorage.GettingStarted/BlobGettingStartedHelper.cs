using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace AzBlobStorage.GettingStarted
{
    public static class BlobGettingStartedHelper
    {

        public static async Task<BlobContainerClient> CreateContainerAsync(BlobServiceClient blobServiceClient, string containerName)
        {
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(containerName);

            await blobContainerClient.CreateIfNotExistsAsync();

            return blobContainerClient;
        }

        public static async Task UploadBlobAsync(BlobContainerClient blobContainerClient, string localFilePath, string fileForSnapshotAndDelete, bool deleteFile)
        {
            var fileName = Path.GetFileName(localFilePath);
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            Console.WriteLine("\nUploading {0}", fileName);

            var fileUploadResults = await blobClient.UploadAsync(File.OpenRead($"{localFilePath}"), true);

            if (fileUploadResults.GetRawResponse().Status == 201)
            {
                Console.WriteLine($"{fileName} uploaded to the {blobClient.Uri} location");

                if (fileName == fileForSnapshotAndDelete)
                {
                    var blockBlobSnapshotResponse = await CreateSnapshotAsync(blobClient);

                    if (blockBlobSnapshotResponse.GetRawResponse().Status == 201 && deleteFile)
                    {
                        await blobClient.DeleteIfExistsAsync(DeleteSnapshotsOption.IncludeSnapshots);
                    }
                }

            }
        }

        public static async Task ListBlobAsync(BlobContainerClient blobContainerClient)
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

        static async Task<Response<BlobSnapshotInfo>> CreateSnapshotAsync(BlobClient blobClient)
        {
            return await blobClient.CreateSnapshotAsync();
        }

    }
}
