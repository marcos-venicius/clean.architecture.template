using Core.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiExceptionFilter]
[ApiController]
public abstract class ApiControllerBase : ControllerBase
{
    private ISender? _mediator;

    private protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}