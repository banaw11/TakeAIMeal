namespace TakeAIMeal.Common.Services.Interfaces
{
    /// <summary>
    /// Represents a service for generating text using the OpenAI API.
    /// </summary>
    public interface ITextGeneratorService
    {
        /// <summary>
        /// Generates text using the OpenAI API, based on the specified prompt and generation parameters.
        /// </summary>
        /// <param name="prompt">The input text prompt to generate text from.</param>
        /// <param name="completionCount">The number of text completions to generate (default is 1).</param>
        /// <param name="temperature">The sampling temperature to use for generating text (default is 0.2f).</param>
        /// <param name="maxTokens">The maximum number of tokens (words or subwords) to generate in each text completion (default is 1024).</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a string collection that represents the generated text.</returns>
        Task<ICollection<string>> GenerateText(string prompt, int completionCount = 1, float temperature = 0.2f, int maxTokens = 1024);
    }
}
