using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Azure Blob Storage exercise\n");

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

// Run the examples asynchronously, wait for the results before proceeding
ProcessAsync(configuration).GetAwaiter().GetResult();

Console.WriteLine("Press enter to exit the sample application.");
Console.ReadLine();

static async Task ProcessAsync(IConfigurationRoot configuration)
{
    // Copy the connection string from the portal in the variable below.
    string storageConnectionString = configuration.GetConnectionString("BlobStorageCS");

    // Create a client that can authenticate with a connection string
    BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);

    // COPY EXAMPLE CODE BELOW HERE

}