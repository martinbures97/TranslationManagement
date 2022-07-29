using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace TranslationManagement.Api.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected ISender Sender => (ISender)HttpContext.RequestServices.GetRequiredService(typeof(ISender));
}