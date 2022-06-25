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
    ResetColor();
    await S3BucketHelper.CreateBucketAsync(_bucketName, client);
    ResetColor();

    _keyName = await S3BucketHelper.UploadFileAsync(_bucketName, _filePath, client);
    ResetColor();

    _filePath = _configuration["AWSS3:DownFilePath"];
    await S3BucketHelper.DownloadObjectFromBucketAsync(_bucketName, _filePath, _keyName, client);
    ResetColor();
}

WriteLine("\n\nPress any key ...");

