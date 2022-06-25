using Amazon.S3;
using static System.Console;

namespace AWSS3.BasicsDemo
{

    internal class S3BucketHelper
    {
        public static async Task CreateBucketAsync(string _bucketName, IAmazonS3 client)
        {
            var success = await S3BucketUtility.CreateBucketAsync(client, _bucketName);
            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                WriteLine($"Successfully created bucket: {_bucketName}.\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteLine($"Could not create bucket: {_bucketName}.\n");
            }
        }

        public static async Task<string> UploadFileAsync(string _bucketName, string _filePath, IAmazonS3 client)
        {
            string _keyName = Path.GetFileName(_filePath);

            var success = await S3BucketUtility.UploadFileAsync(client, _bucketName, _keyName, _filePath);

            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                WriteLine($"Successfully uploaded {_keyName} from {_filePath} to {_bucketName}.\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteLine($"Could not upload {_keyName}.\n");
            }

            return _keyName;
        }

        public static async Task DownloadObjectFromBucketAsync(string _bucketName, string _filePath, string _keyName, IAmazonS3 client)
        {
            var success = await S3BucketUtility.DownloadObjectFromBucketAsync(client, _bucketName, _keyName, _filePath);

            if (success)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                WriteLine($"Successfully downloaded {_keyName}.\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                WriteLine($"Sorry, could not download {_keyName}.\n");
            }
        }
    }
}
