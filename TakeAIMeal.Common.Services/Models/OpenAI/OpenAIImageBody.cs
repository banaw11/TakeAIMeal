namespace TakeAIMeal.Common.Services.Models.OpenAI
{
    /// <summary>
    /// Represents the body of the request sent to the OpenAI Dall-E 2 API for generating images based on a given prompt.
    /// </summary>
    public class OpenAIImageBody
    {
        /// <summary>
        /// The prompt based on which the image is to be generated.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// The number of images to be generated for the given prompt.
        /// </summary>
        public int N { get; set; }

        /// <summary>
        /// The size of the generated image, specified as a string in the format "width,height".
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// The response of the generated image, specified as a string (url, b64_json).
        /// </summary>
        public string Response_format { get; set; }
    }
}
