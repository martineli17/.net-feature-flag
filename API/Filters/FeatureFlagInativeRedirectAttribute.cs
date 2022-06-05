using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.FeatureManagement;
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
            var featureManager = (IFeatureManager)context.HttpContext.RequestServices.GetService(typeof(IFeatureManager));

            if (!await featureManager.IsEnabledAsync(FeatureFlag))
                context.HttpContext.Response.Redirect(RedirectTo);
            else
                await next();
        }
    }
}
