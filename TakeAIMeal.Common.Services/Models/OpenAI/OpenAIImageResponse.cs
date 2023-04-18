namespace TakeAIMeal.Common.Services.Models.OpenAI
{
    /// <summary>
    /// Represents the response returned by the OpenAI API when requesting image data.
    /// </summary>
    public class OpenAIImageResponse
    {
        /// <summary>
        /// Gets or sets the timestamp indicating when the response was created.
        /// </summary>
        public long Created { get; set; }

        /// <summary>
        /// Gets or sets a collection of image data returned by the OpenAI API.
        /// </summary>
        public ICollection<ImageData> Data { get; set; }
    }

    /// <summary>
    /// Represents image data returned by the OpenAI API.
    /// </summary>
    public class ImageData
    {
        /// <summary>
        /// Gets or sets the URL of the image.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the string of the image in base64 format.
        /// </summary>
        public string B64_json { get; set; }
    }
}
