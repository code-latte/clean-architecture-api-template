using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.DTO.Requests;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("signup")]
public class SignupController : BaseController
{
    private readonly IAuthUseCases _authUseCases;

    public SignupController(IAuthUseCases authUseCases)
    {
        _authUseCases = authUseCases;
    }

    [HttpPost]
    public async Task<IActionResult> SignupAsync([FromBody] SignupRequest request)
    {
        try
        {
            var user = await _authUseCases.SignupAsync(request);
            return Ok(user);
        }
        catch (EmailAlreadyInUseException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (UsernameAlreadyInUseException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
