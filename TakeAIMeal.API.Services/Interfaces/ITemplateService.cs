namespace TakeAIMeal.API.Services.Interfaces
{
    /// <summary>
    /// Defines an interface for rendering templates with a given model.
    /// </summary>
    public interface ITemplateService
    {
        /// <summary>
        /// Renders a template with the specified name using the provided model.
        /// </summary>
        /// <typeparam name="T">The type of the model.</typeparam>
        /// <param name="templateName">The name of the template to render.</param>
        /// <param name="model">The model to use when rendering the template.</param>
        /// <returns>A task that represents the asynchronous operation of rendering the template.</returns>
        Task<string> RenderTemplateAsync<T>(string templateName, T model);
    }
}
