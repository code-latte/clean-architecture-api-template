using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CleanArchitecture.Application.DTO.Requests;
using CleanArchitecture.Application.DTO.Responses;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Exceptions;

namespace CleanArchitecture.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("accounts")]
    public class AccountController : BaseController
    {
        private readonly IAccountUseCases _accountUseCases;

        public AccountController(IAccountUseCases accountUseCases)
        {
            _accountUseCases = accountUseCases;
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetAccountAsync()
        {
            try
            {
                AccountResponse response = await _accountUseCases.GetAccountAsync(AccountId);
                return Ok(response);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
