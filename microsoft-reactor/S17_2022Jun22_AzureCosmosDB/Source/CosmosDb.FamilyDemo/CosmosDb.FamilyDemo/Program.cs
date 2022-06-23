using CosmosDb.FamilyDemo;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System.Net;

IConfiguration _configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddUserSecrets("42B3177E-1CA6-448F-8F9A-1294955F5337")
    .Build();

string _endpointUrl = _configuration["CosmosDbConnectionStrings:AccountEndpoint"];
string _primaryKey = _configuration["CosmosDbConnectionStrings:AccountKey"];
string _databaseId = "Persons";
string _containerId = "FamilyTree";
string _partitionKey = "/partitionKey";

using (var cosmosClient = new CosmosClient(_endpointUrl, _primaryKey, new CosmosClientOptions() { ApplicationName = "CosmosDBDotnetQuickstart" }))
{
    // Create the database if it does not exist
    var cosmosDatabaseResponse = await cosmosClient.CreateDatabaseIfNotExistsAsync(_databaseId);
    var cosmosDatabase = cosmosDatabaseResponse?.Database;
    Console.WriteLine("Created Database: {0}\n", cosmosDatabase?.Id);

    // Create the container if it does not exist.
    // Specifiy "/partitionKey" as the partition key path since we're storing family information, to ensure good distribution of requests and storage.
    var familyTreeContainerResponse = await cosmosDatabase?.CreateContainerIfNotExistsAsync(_containerId, _partitionKey)!;
    var familyTreeContainer = familyTreeContainerResponse.Container;
    Console.WriteLine("Created Container: {0}\n", familyTreeContainer.Id);

    // Add Family items to the container
    Family andersenFamily = GetAndersenFamily();
    await AddNewFamily(familyTreeContainer, andersenFamily);

    Family wakeFieldFamily = GetWakeFieldFamily();
    await AddNewFamily(familyTreeContainer, wakeFieldFamily);

    // Run a query (using Azure Cosmos DB SQL syntax) against the container
    // Including the partition key value of lastName in the WHERE filter results in a more efficient query
    await QueryItemsAsync(familyTreeContainer, "Andersen");

    await ReplaceFamilyItemAsync(familyTreeContainer, "Wakefield.7", "Wakefield");

    await QueryItemsAsync(familyTreeContainer, "Wakefield");
}

// Create a family object for the Andersen family
Family GetAndersenFamily()
{

    return new Family
    {
        Id = "Andersen.1",
        PartitionKey = "Andersen",
        LastName = "Andersen",
        Parents = new Parent[]
        {
                        new Parent { FirstName = "Thomas" },
                        new Parent { FirstName = "Mary Kay" }
        },
        Children = new Child[]
        {
                        new Child
                        {
                            FirstName = "Henriette Thaulow",
                            Gender = "female",
                            Grade = 5,
                            Pets = new Pet[]
                            {
                                new Pet { GivenName = "Fluffy" }
                            }
                        }
        },
        Address = new Address { State = "WA", County = "King", City = "Seattle" },
        IsRegistered = false
    };
}

// Create a family object for the Wakefield family
Family GetWakeFieldFamily()
{
    return new Family
    {
        Id = "Wakefield.7",
        PartitionKey = "Wakefield",
        LastName = "Wakefield",
        Parents = new Parent[]
        {
                        new Parent { FamilyName = "Wakefield", FirstName = "Robin" },
                        new Parent { FamilyName = "Miller", FirstName = "Ben" }
        },
        Children = new Child[]
        {
                        new Child
                        {
                            FamilyName = "Merriam",
                            FirstName = "Jesse",
                            Gender = "female",
                            Grade = 8,
                            Pets = new Pet[]
                            {
                                new Pet { GivenName = "Goofy" },
                                new Pet { GivenName = "Shadow" }
                            }
                        },
                        new Child
                        {
                            FamilyName = "Miller",
                            FirstName = "Lisa",
                            Gender = "female",
                            Grade = 1
                        }
        },
        Address = new Address { State = "NY", County = "Manhattan", City = "NY" },
        IsRegistered = true
    };
}

static async Task AddNewFamily(Container familyTreeContainer, Family newFamily)
{
    try
    {
        // Read the item to see if it exists.  
        ItemResponse<Family> andersenFamilyResponse = await familyTreeContainer.ReadItemAsync<Family>(newFamily.Id, new PartitionKey(newFamily.PartitionKey));
        Console.WriteLine("Item in database with id: {0} already exists\n", andersenFamilyResponse.Resource.Id);
    }
    catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
    {
        // Create an item in the container representing the Andersen family. Note we provide the value of the partition key for this item, which is "Andersen"
        ItemResponse<Family> andersenFamilyResponse = await familyTreeContainer.CreateItemAsync<Family>(newFamily, new PartitionKey(newFamily.PartitionKey));

        // Note that after creating the item, we can access the body of the item with the Resource property off the ItemResponse. We can also access the RequestCharge property to see the amount of RUs consumed on this request.
        Console.WriteLine("Created item in database with id: {0} Operation consumed {1} RUs.\n", andersenFamilyResponse.Resource.Id, andersenFamilyResponse.RequestCharge);
    }
}

static async Task QueryItemsAsync(Container familyTreeContainer, string familyKey)
{
    var sqlQueryText = $"SELECT * FROM c WHERE c.partitionKey = '{familyKey}'";

    Console.WriteLine("Running query: {0}\n", sqlQueryText);

    QueryDefinition queryDefinition = new(sqlQueryText);
    FeedIterator<Family> queryResultSetIterator = familyTreeContainer.GetItemQueryIterator<Family>(queryDefinition);

    List<Family> families = new();
    //List<Family> families1 = new List<Family>();
    //var families2 = new List<Family>();

    while (queryResultSetIterator.HasMoreResults)
    {
        FeedResponse<Family> currentResultSet = await queryResultSetIterator.ReadNextAsync();
        foreach (Family family in currentResultSet)
        {
            families.Add(family);
            Console.WriteLine("\tRead {0}\n", family);
        }
    }
}

static async Task ReplaceFamilyItemAsync(Container familyTreeContainer, string idValue, string partitionKeyValue)
{
    ItemResponse<Family> wakefieldFamilyResponse = await familyTreeContainer.ReadItemAsync<Family>(idValue, new PartitionKey(partitionKeyValue));
    var itemBody = wakefieldFamilyResponse.Resource;

    // update registration status from false to true
    itemBody.IsRegistered = false;
    // update grade of child
    itemBody.Children[0].Grade = 8;

    // replace the item with the updated content
    wakefieldFamilyResponse = await familyTreeContainer.ReplaceItemAsync<Family>(itemBody, itemBody.Id, new PartitionKey(itemBody.PartitionKey));
    Console.WriteLine("Updated Family [{0},{1}].\n \tBody is now: {2}\n", itemBody.LastName, itemBody.Id, wakefieldFamilyResponse.Resource);
}

