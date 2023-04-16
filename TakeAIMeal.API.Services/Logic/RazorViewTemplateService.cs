using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using TakeAIMeal.API.Services.Interfaces;

namespace TakeAIMeal.API.Services.Logic
{
    public class RazorViewTemplateService : ITemplateService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRazorViewEngine _razorViewEngine;
        private readonly ITempDataProvider _tempDataProvider;

        public RazorViewTemplateService(IHttpContextAccessor httpContextAccessor, IRazorViewEngine razorViewEngine, ITempDataProvider tempDataProvider)
        {
            _httpContextAccessor = httpContextAccessor;
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
        }

        public async Task<string> RenderTemplateAsync<T>(string templateName, T model)
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());
            var view = _razorViewEngine.FindView(actionContext, templateName, false);

            if (!view.Success)
            {
                return string.Empty;
            }

            var dataDictionary = new ViewDataDictionary<T>(new EmptyModelMetadataProvider(), new ModelStateDictionary())
            {
                Model = model,
            };
            var tempDataDictionary = new TempDataDictionary(httpContext, _tempDataProvider);
            await using var sw = new StringWriter();

            try
            {
                var viewContext = new ViewContext(actionContext, view.View, dataDictionary, tempDataDictionary, sw, new HtmlHelperOptions());

                await view.View.RenderAsync(viewContext);
                return sw.ToString();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
