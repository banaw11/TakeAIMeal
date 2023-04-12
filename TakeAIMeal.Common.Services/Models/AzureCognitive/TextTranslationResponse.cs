namespace TakeAIMeal.Common.Services.Models.AzureCognitive
{
    /// <summary>
    /// Represents a response to a text translation request.
    /// </summary>
    public class TextTranslationResponse
    {
        /// <summary>
        /// Gets or sets the detected language of the input text.
        /// </summary>
        public DetectedLanguage DetectedLanguage { get; set; }

        /// <summary>
        /// Gets or sets the collection of translation results for the input text.
        /// </summary>
        public ICollection<TextTranslationResult> Translations { get; set; }
    }

    /// <summary>
    /// Represents a detected language in a text translation response.
    /// </summary>
    public class DetectedLanguage
    {
        /// <summary>
        /// Gets or sets the language code of the detected language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the confidence score of the detected language.
        /// </summary>
        public float Score { get; set; }
    }

    /// <summary>
    /// Represents a translation result in a text translation response.
    /// </summary>
    public class TextTranslationResult
    {
        /// <summary>
        /// Gets or sets the target language code of the translation.
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Gets or sets the translated text.
        /// </summary>
        public string Text { get; set; }
    }
}
