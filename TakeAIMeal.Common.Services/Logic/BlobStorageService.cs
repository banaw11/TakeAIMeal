using Azure.Storage.Blobs;
using TakeAIMeal.Common.Services.Interfaces;

namespace TakeAIMeal.Common.Services.Logic
{
    public class BlobStorageService : IBlobStorageService
    {
        private readonly BlobServiceClient _blobServiceClient;
        public BlobStorageService(string connectionString)
        {
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> DownloadStringContent(string containerName, string blobName)
        {
            BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            var downloadResponse = await blobClient.DownloadContentAsync();
            if(downloadResponse != null && downloadResponse.Value != null)
            {
                return downloadResponse.Value.Content.ToString();
            }

            return null;
        }

        public async Task UploadStringContent(string content, string containerName, string blobName, bool createIfNotExists = false)
        {
            if (!string.IsNullOrEmpty(content))
            {
                BlobContainerClient containerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                BlobClient blobClient = containerClient.GetBlobClient(blobName);

                await blobClient.UploadAsync(BinaryData.FromString(content), overwrite: true).ConfigureAwait(false);
            }
        }
    }
}
