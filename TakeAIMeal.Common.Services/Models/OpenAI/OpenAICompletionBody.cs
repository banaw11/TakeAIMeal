namespace TakeAIMeal.Common.Services.Models.OpenAI
{
    /// <summary>
    /// Represents the body of the request sent to the OpenAI Completion API, containing the prompt and
    /// various parameters for generating the completion text.
    /// </summary>
    public class OpenAICompletionBody
    {
        /// <summary>
        /// The prompt for which completion text is to be generated.
        /// </summary>
        public string Prompt { get; set; }

        /// <summary>
        /// The name of the model to be used for generating the completion text.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// The maximum number of tokens in the generated text.
        /// </summary>
        public int Max_tokens { get; set; }

        /// <summary>
        /// The temperature of the sampling distribution used for generating the text.
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// The number of completions to generate for the given prompt.
        /// </summary>
        public int N { get; set; }
    }
}
