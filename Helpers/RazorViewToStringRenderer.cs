using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.DependencyInjection;

namespace TechyRecruit.Helpers
{
    public static class RazorViewToStringRenderer
    {
        public static async Task<string> RenderViewToStringAsync(Controller controller, string viewName, object model)
        {
            var services = controller.HttpContext.RequestServices;
            var viewEngine = services.GetRequiredService<ICompositeViewEngine>();
            var viewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary()) { Model = model };

            await using var sw = new StringWriter();
            var viewResult = viewEngine.FindView(controller.ControllerContext, viewName, false);

            if (viewResult.View == null || !viewResult.Success)
            {
                throw new ArgumentNullException($"{viewName} does not match any available view");
            }

            var viewContext = new ViewContext(
                controller.ControllerContext,
                viewResult.View,
                viewData,
                new TempDataDictionary(controller.ControllerContext.HttpContext, services.GetRequiredService<ITempDataProvider>()),
                sw,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext).ConfigureAwait(false);
            return sw.ToString();
        }
    }
}