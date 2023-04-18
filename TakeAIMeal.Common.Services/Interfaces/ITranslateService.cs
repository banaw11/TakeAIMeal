namespace TakeAIMeal.Common.Services.Interfaces
{
    /// <summary>
    /// Represents a service for translating text.
    /// </summary>
    public interface ITranslateService
    {
        /// <summary>
        /// Translates a single string of text from the specified origin language to the specified target language.
        /// </summary>
        /// <param name="origin">The text to be translated.</param>
        /// <param name="to">The target language code.</param>
        /// <param name="from">The origin language code. If not specified, the service will attempt to auto-detect the language.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the translated text.</returns>
        Task<string> TranslateText(string origin, string to, string from = null);

        /// <summary>
        /// Translates a collection of strings from the specified origin language to the specified target language.
        /// </summary>
        /// <param name="origin">The collection of strings to be translated.</param>
        /// <param name="to">The target language code.</param>
        /// <param name="from">The origin language code. If not specified, the service will attempt to auto-detect the language.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a collection of translated strings.</returns>
        Task<ICollection<string>> TranslateTexts(ICollection<string> origin, string to, string from = null);
    }
}
