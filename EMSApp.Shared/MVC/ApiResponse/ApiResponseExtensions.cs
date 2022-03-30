using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;

namespace EMSApp.Shared
{
    public static class ApiResponseExtensions
    {
        public static ActionResult<T> ToActionResult<T>(this ApiResponse<T> response, ControllerBase controller)
        {
            return controller.ToActionResult((IApiResponse)response);
        }

        public static ActionResult<T> ToActionResult<T>(this ControllerBase controller, ApiResponse<T> response)
        {
            return controller.ToActionResult((IApiResponse)response); 
        }

        internal static ActionResult ToActionResult(this ControllerBase controller, IApiResponse response)
        {
            switch (response.Status)
            {
                case ResponseStatus.Ok: return controller.Ok(response.GetValue());
                case ResponseStatus.NotFound: return controller.NotFound();
                case ResponseStatus.Unauthorized: return controller.Unauthorized();
                case ResponseStatus.Forbidden: return controller.Forbid();
                case ResponseStatus.Invalid: return BadRequest(controller, response);
                case ResponseStatus.Error: return UnprocessableEntity(controller, response);
                default:
                    throw new NotSupportedException($"Result {response.Status} conversion is not supported.");
            }
        }

        private static ActionResult BadRequest(ControllerBase controller, IApiResponse response)
        {
            foreach (var error in response.ValidationErrors)
            {
                controller.ModelState.AddModelError(error.Identifier, error.ErrorMessage);
            }

            return controller.BadRequest(controller.ModelState);
        }

        private static ActionResult UnprocessableEntity(ControllerBase controller, IApiResponse response)
        {
            var details = new StringBuilder();

            foreach (var error in response.Errors) details.Append(error).AppendLine();

            return controller.UnprocessableEntity(new ProblemDetails
            {
                Title = "Something went wrong.",
                Detail = details.ToString()
            });
        }
    }
}
