using _Flagsmith.Adapters;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace API.Filters
{
    public class FeatureFlagInativeRedirectAttribute : ActionFilterAttribute
    {
        private string RedirectTo { get; set; }
        private string FeatureFlag { get; set; }
        public FeatureFlagInativeRedirectAttribute(string featureFlag, string redirectTo)
        {
            RedirectTo = redirectTo;
            FeatureFlag = featureFlag;
        }

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var featureManager = (IFlagsmithAdapter)context.HttpContext.RequestServices.GetService(typeof(IFlagsmithAdapter));

            if (!await featureManager.IsEnabledAsync(FeatureFlag))
                context.HttpContext.Response.Redirect(RedirectTo);
            else
                await next();
        }
    }
}
