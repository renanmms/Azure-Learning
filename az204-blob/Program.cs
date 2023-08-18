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
    string blobContainerName = configuration.GetSection("BlobContainerName").Value;

    // Create a client that can authenticate with a connection string
    BlobServiceClient blobServiceClient = new BlobServiceClient(storageConnectionString);

    // Get the container and return a container client object
    BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(blobContainerName);
    Console.WriteLine("A container named '" + containerClient.Name + "' has been founded. " +
        "\nNext a file will be created and uploaded to the container.");
    
    // await UploadTextFileToBlob(containerClient, "hello-az.txt");
    Console.WriteLine("\nThe file was uploaded. We'll verify by listing" + 
            " the blobs next.");

    await ListAllBlobs(containerClient);
    
    Console.ReadLine();
}

static async Task UploadTextFileToBlob(BlobContainerClient containerClient, string fileName){
    // Create a local file in the ./data/ directory for uploading and downloading
    string localPath = "./data/";
    string localFilePath = Path.Combine(localPath, fileName);

    // Get a reference to the blob
    BlobClient blobClient = containerClient.GetBlobClient(fileName);

    Console.WriteLine("Uploading to Blob storage as blob:\n\t {0}\n", blobClient.Uri);

    // Open the file and upload its data
    using (FileStream uploadFileStream = File.OpenRead(localFilePath))
    {
        await blobClient.UploadAsync(uploadFileStream);
        uploadFileStream.Close();
    }

    Console.WriteLine("Press 'Enter' to continue.");
}

static async Task ListAllBlobs(BlobContainerClient containerClient)
{
    var blobs = containerClient.GetBlobsAsync();
    await foreach(var blob in blobs)
    {
        Console.WriteLine($"{blob.Name}");
    }
}