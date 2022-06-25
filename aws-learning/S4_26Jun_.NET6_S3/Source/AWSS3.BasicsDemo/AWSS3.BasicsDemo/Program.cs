/*
 * Reference: https://github.com/awsdocs/aws-doc-sdk-examples/tree/main/dotnetv3/S3/S3_Basics
 */

using Amazon.S3;
using AWSS3.BasicsDemo;
using Microsoft.Extensions.Configuration;
using static System.Console;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

string _bucketName = _configuration["AWSS3:BucketName"];
string _filePath = _configuration["AWSS3:FilePath"];
string _keyName = _configuration["AWSS3:KeyName"];


using (IAmazonS3 client = new AmazonS3Client())
{

    Console.ResetColor();
    await CreateBucketAsync(_bucketName, client);
    Console.ResetColor();

    await UploadFileAsync(_bucketName, _filePath, client);
    Console.ResetColor();
}

WriteLine("\n\nPress any key ...");

static async Task CreateBucketAsync(string _bucketName, IAmazonS3 client)
{
    var success = await S3BucketHelper.CreateBucketAsync(client, _bucketName);
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

static async Task UploadFileAsync(string _bucketName, string _filePath, IAmazonS3 client)
{
    string _keyName = Path.GetFileName(_filePath);

    var success = await S3BucketHelper.UploadFileAsync(client, _bucketName, _keyName, _filePath);

    if (success)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"Successfully uploaded {_keyName} from {_filePath} to {_bucketName}.\n");
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Could not upload {_keyName}.\n");
    }
}