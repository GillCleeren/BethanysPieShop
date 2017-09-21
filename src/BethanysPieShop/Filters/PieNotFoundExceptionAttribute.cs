using BethanysPieShop.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BethanysPieShop.Filters
{
    public class PieNotFoundExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {

            if (context.Exception is PieNotFoundException)
            {
                context.Result = new ViewResult
                {
                    ViewName = "PieNotFound",
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                    {
                        Model = "An error occured while searching the requested pie"
                    }
                };
            }
        }
    }
}
