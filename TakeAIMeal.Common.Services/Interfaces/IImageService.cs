namespace TakeAIMeal.Common.Services.Interfaces
{
    /// <summary>
    /// Represents a service for generating images in base64 format.
    /// </summary>
    public interface IImageService
    {
        /// <summary>
        /// Generates an image in base64 format based on the given prompt.
        /// </summary>
        /// <param name="prompt">The prompt used to generate the image.</param>
        /// <param name="size">The size which the generated image should has. Default 256 x 256.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the generated image in base64 format.</returns>
        Task<string> GenerateImageToBase64(string prompt, string size = "256x256");
    }
}
