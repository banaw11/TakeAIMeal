namespace TakeAIMeal.Common.Services.Interfaces
{
    /// <summary>
    /// Represents a service for storing and retrieving string content in Azure Blob Storage.
    /// </summary>
    public interface IBlobStorageService
    {
        /// <summary>
        /// Uploads the specified string content to the specified container and blob name in Azure Blob Storage.
        /// </summary>
        /// <param name="content">The string content to upload.</param>
        /// <param name="containerName">The name of the container to upload to.</param>
        /// <param name="blobName">The name of the blob to upload to.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task UploadStringContent(string content, string containerName, string blobName);

        /// <summary>
        /// Downloads the string content from the specified container and blob name in Azure Blob Storage.
        /// </summary>
        /// <param name="containerName">The name of the container to download from.</param>
        /// <param name="blobName">The name of the blob to download from.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the downloaded string content.</returns>
        Task<string> DownloadStringContent(string containerName, string blobName);
    }
}
