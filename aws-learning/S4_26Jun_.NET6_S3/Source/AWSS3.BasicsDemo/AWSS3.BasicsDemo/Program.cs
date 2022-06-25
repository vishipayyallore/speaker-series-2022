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
string _filePath = _configuration["AWSS3:UploadsFilePath"];
string _keyName = _configuration["AWSS3:KeyName"];


using (IAmazonS3 client = new AmazonS3Client())
{

    Console.ResetColor();
    await CreateBucketAsync(_bucketName, client);
    Console.ResetColor();

    _keyName = await UploadFileAsync(_bucketName, _filePath, client);
    Console.ResetColor();

    _filePath = _configuration["AWSS3:DownFilePath"];
    await DownloadObjectFromBucketAsync(_bucketName, _filePath, _keyName, client);
    Console.ResetColor();
}

WriteLine("\n\nPress any key ...");

static async Task CreateBucketAsync(string _bucketName, IAmazonS3 client)
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

static async Task<string> UploadFileAsync(string _bucketName, string _filePath, IAmazonS3 client)
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

static async Task DownloadObjectFromBucketAsync(string _bucketName, string _filePath, string _keyName, IAmazonS3 client)
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