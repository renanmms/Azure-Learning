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
    string storageConnectionString = configuration.GetConnectionString("StorageAccountCS");

    // Create a client that can authenticate with a connection string
    BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);

    //Create a unique name for the container
    string containerName = "wtblob" + Guid.NewGuid().ToString();

    // Create the container and return a container client object
    BlobContainerClient containerClient = await blobServiceClient.CreateBlobContainerAsync(containerName);
    Console.WriteLine("A container named '" + containerName + "' has been created. " +
        "\nTake a minute and verify in the portal." + 
        "\nNext a file will be created and uploaded to the container.");
    Console.WriteLine("Press 'Enter' to continue.");
    Console.ReadLine();

}