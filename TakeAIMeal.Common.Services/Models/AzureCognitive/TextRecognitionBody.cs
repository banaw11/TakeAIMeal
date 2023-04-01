namespace TakeAIMeal.Common.Services.Models.AzureCognitive
{
    /// <summary>
    /// Represents the request body sent to the text recognition API.
    /// </summary>
    public class TextRecognitionBody
    {
        /// <summary>
        /// Gets or sets a collection of recognition documents to be processed by the API.
        /// </summary>
        public ICollection<RecognitionDocument> Documents { get; set; }
    }

    /// <summary>
    /// Represents a document to be processed by the text recognition API.
    /// </summary>
    public class RecognitionDocument
    {
        /// <summary>
        /// Gets or sets the language of the document to be recognized.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the ID of the document to be recognized.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the text of the document to be recognized.
        /// </summary>
        public string Text { get; set; }
    }
}
