namespace TakeAIMeal.API.Services.Models
{
    /// <summary>
    /// Represents a key-value pair that can store a dynamic value.
    /// </summary>
    public class DictionaryItem
    {
        /// <summary>
        /// Gets or sets the name of the dictionary item.
        /// </summary>
        /// <value>The name of the dictionary item.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the value of the dictionary item.
        /// </summary>
        /// <value>The value of the dictionary item.</value>
        public dynamic Value { get; set; }
    }
}
