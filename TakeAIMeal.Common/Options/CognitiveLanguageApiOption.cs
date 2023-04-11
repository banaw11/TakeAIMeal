namespace TakeAIMeal.Common.Options
{
    /// <summary>
    /// Represents the options that can be used to configure the Azure Cognitive Services Language API client.
    /// </summary>
    public class CognitiveLanguageApiOption
    {
        /// <summary>
        /// Gets or sets the API key used to authenticate the client with the Azure Cognitive Services Language API.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the API url used to coomunicate the client with the Azure Cognitive Services Language API.
        /// </summary>
        public string ApiUrl { get; set; }
    }
}
