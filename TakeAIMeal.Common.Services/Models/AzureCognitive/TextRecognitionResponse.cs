namespace TakeAIMeal.Common.Services.Models.AzureCognitive
{
    /// <summary>
    /// Represents the response returned by the text recognition API.
    /// </summary>
    public class TextRecognitionResponse
    {
        /// <summary>
        /// Gets or sets a collection of recognition document responses returned by the API.
        /// </summary>
        public ICollection<RecognitionDocumentResponse> Documents { get; set; }
    }

    /// <summary>
    /// Represents a recognition document response returned by the text recognition API.
    /// </summary>
    public class RecognitionDocumentResponse
    {
        /// <summary>
        /// Gets or sets the ID of the recognition document.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a collection of entities recognized in the document.
        /// </summary>
        public ICollection<DocumentEntity> Entities { get; set; }
    }

    /// <summary>
    /// Represents an entity recognized by the text recognition API in a document.
    /// </summary>
    public class DocumentEntity
    {
        /// <summary>
        /// Gets or sets the text of the entity.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets the category of the entity.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the subcategory of the entity.
        /// </summary>
        public string Subcategory { get; set; }

        /// <summary>
        /// Gets or sets the offset of the entity in the document.
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Gets or sets the length of the entity in the document.
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the confidence score of the entity recognition.
        /// </summary>
        public float ConfidenceScore { get; set; }
    }

}
