using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace DomoNow.Communications.Presentation.Commons
{
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        private ISender? _ISender;

        protected ISender Sender => _ISender ??= HttpContext.RequestServices.GetRequiredService<ISender>();

        protected IActionResult Problem(List<string> errors)
        {
            if (errors.Count is 0)
            {
                return Problem();
            }

            HttpContext.Items[Constant.Error] = errors;

            return ValidationProblem(errors);
        }

        private IActionResult ValidationProblem(List<string> errors)
        {
            var modelStateDictionary = new ModelStateDictionary();

            foreach (var error in errors)
            {
                modelStateDictionary.AddModelError(string.Empty, error);
            }

            return ValidationProblem(modelStateDictionary);
        }
    }
}