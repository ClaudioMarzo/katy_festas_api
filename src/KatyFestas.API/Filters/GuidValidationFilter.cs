using KatyFestas.API.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace KatyFestas.API.Filters;

/// <summary>
/// Filtro que rejeita automaticamente parâmetros Guid com valor Guid.Empty.
/// Aplica-se a todos os parâmetros do tipo Guid em qualquer controller.
/// </summary>
public class GuidValidationFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var argument in context.ActionArguments)
        {
            if (argument.Value is Guid guidValue && guidValue == Guid.Empty)
            {
                var correlationId = context.HttpContext.Items["X-Correlation-Id"]?.ToString();

                context.Result = new BadRequestObjectResult(
                    new ErrorResponse(
                        message: $"O parâmetro '{argument.Key}' não pode ser vazio.",
                        statusCode: 400,
                        correlationId: correlationId,
                        errors: [$"'{argument.Key}' possui um valor inválido (Guid vazio)."]
                    )
                );
                return;
            }
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
