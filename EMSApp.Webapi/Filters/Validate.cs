using Microsoft.AspNetCore.Mvc.Filters;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EMSApp.Webapi.Filters
{
    public class Validate : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState
                .Where(a => a.Value.Errors.Count > 0)
                .SelectMany(x => x.Value.Errors.Select(y => y.ErrorMessage))
                .ToList();
                if (errors.Any())
                {
                    var details = new StringBuilder("Next validation error(s) occured:");
                    foreach (var error in errors) details.Append("* ").Append(error).AppendLine();
                    
                    throw new ValidationException(details.ToString());
                }
            }

            await next();

            // after controller
        }
    }
}
