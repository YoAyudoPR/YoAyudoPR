using Microsoft.AspNetCore.Mvc.ModelBinding;
using YoAyudoPR.Web.Models.Response;

namespace YoAyudoPR.Web.Extensions
{
    public static class ModelStateExtensions
    {
        public static IEnumerable<ModelStateError> GetModelStateErrors(this ModelStateDictionary modelState)
        {
            var errors = modelState.Keys.Select(key => new ModelStateError()
            {
                Key = key,
                Errors = modelState[key].Errors.Select(err => err.ErrorMessage).ToArray()
            }).Where(model => model.Errors.Any());

            return errors;
        }
    }
}
