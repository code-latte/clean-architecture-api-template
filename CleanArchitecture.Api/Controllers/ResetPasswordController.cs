using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.DTO.Requests;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Api.Controllers;

[ApiController]
public class ResetPasswordController : BaseController
{
    private readonly IAuthUseCases _authUseCases;

    public ResetPasswordController(IAuthUseCases authUseCases)
    {
        _authUseCases = authUseCases;
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> RequestResetPasswordAsync(
        [FromBody] PasswordResetRequest request
    )
    {
        try
        {
            await _authUseCases.RequestPasswordResetAsync(request);
            return Ok();
        }
        catch (EntityNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(
        [FromBody] ConfirmPasswordResetRequest request
    )
    {
        try
        {
            await _authUseCases.ConfirmPasswordResetAsync(request);
            return Ok();
        }
        catch (EntityNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (TokenNotValidException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
