using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.DTO.Requests;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
[Route("login")]
public class LoginController : BaseController
{
    private readonly IAuthUseCases _loginUseCases;

    public LoginController(IAuthUseCases loginUseCases)
    {
        _loginUseCases = loginUseCases;
    }

    [HttpPost]
    public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
    {
        try
        {
            var user = await _loginUseCases.LoginAsync(request);
            return Ok(user);
        }
        catch (EntityNotFoundException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (InvalidCredentialsException ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
