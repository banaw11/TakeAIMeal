namespace TakeAIMeal.Common.Services.Models.OpenAI
{
    /// <summary>
    /// Represents the response from the OpenAI Completion API, which contains the generated text
    /// based on the prompt provided to the API.
    /// </summary>
    public class OpenAICompletionResponse
    {
        /// <summary>
        /// The identifier of the request for which this response corresponds.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// The object type returned by the OpenAI API.
        /// </summary>
        public string Object { get; set; }

        /// <summary>
        /// The name of the model used to generate the response.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The collection of CompletitionChoice objects representing the generated text for the given prompt.
        /// </summary>
        public ICollection<CompletionChoice> Choices { get; set; }
    }

    /// <summary>
    /// Represents a single completion choice returned by the OpenAI API, containing the generated text and its index.
    /// </summary>
    public class CompletionChoice
    {
        /// <summary>
        /// The generated text for the given prompt.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The index of the completion choice, indicating the order in which it was generated.
        /// </summary>
        public int Index { get; set; }
    }
}
