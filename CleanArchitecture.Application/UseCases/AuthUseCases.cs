using CleanArchitecture.Application.DTO.Requests;
using CleanArchitecture.Application.DTO.Responses;
using CleanArchitecture.Application.Interfaces;
using CleanArchitecture.Domain.Constants;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Exceptions;
using CleanArchitecture.Domain.Interfaces.Data;
using CleanArchitecture.Domain.Interfaces.Mail;
using CleanArchitecture.Domain.Interfaces.Security;

namespace CleanArchitecture.Application.UseCases
{
    public class AuthUseCases : IAuthUseCases
    {
        private readonly IAccountCustomQueries _accountCustomQueries;
        private readonly IAuthorizationTokenCustomQueries _authorizationTokenCustomQueries;
        private readonly ITokenGenerator _tokenService;
        private readonly IRepository<Account, string> _accountRepository;
        private readonly IRepository<AuthorizationToken, int> _authorizationTokenRepository;
        private readonly ICryptoUtils _cryptoUtils;
        private readonly IMailTemplateRepository _mailTemplateRepository;
        private readonly IMailSender _mailSender;

        public AuthUseCases(
            IAccountCustomQueries accountCustomQueries,
            IAuthorizationTokenCustomQueries authorizationTokenCustomQueries,
            ITokenGenerator tokenService,
            IRepository<Account, string> accountRepository,
            IRepository<AuthorizationToken, int> authorizationTokenRepository,
            ICryptoUtils cryptoUtils,
            IMailTemplateRepository mailTemplateRepository,
            IMailSender mailSender
        )
        {
            _accountCustomQueries = accountCustomQueries;
            _authorizationTokenCustomQueries = authorizationTokenCustomQueries;
            _tokenService = tokenService;
            _accountRepository = accountRepository;
            _authorizationTokenRepository = authorizationTokenRepository;
            _cryptoUtils = cryptoUtils;
            _mailTemplateRepository = mailTemplateRepository;
            _mailSender = mailSender;
        }

        public async Task ConfirmPasswordResetAsync(ConfirmPasswordResetRequest request)
        {
            Account account = await _accountCustomQueries.GetByUsernameOrEmailAsync(
                request.UsernameOrEmail
            );

            if (account == null)
            {
                throw new EntityNotFoundException("Account", request.UsernameOrEmail);
            }

            // search token in database
            AuthorizationToken authorizationToken =
                await _authorizationTokenCustomQueries.GetByTokenAndAccountIdAsync(
                    request.Token,
                    account.Id
                );

            if (authorizationToken == null)
            {
                throw new TokenNotValidException(request.Token);
            }

            // check if token is expired
            if (authorizationToken.CreatedAt.AddMinutes(15) < DateTime.UtcNow)
            {
                throw new TokenNotValidException(request.Token);
            }

            // update password
            string passwordHash = _cryptoUtils.HashPassword(request.NewPassword);
            account.PasswordHash = passwordHash;
            await _accountRepository.UpdateAsync(account);

            // set token as used
            authorizationToken.UsedAt = DateTime.UtcNow;
            await _authorizationTokenRepository.UpdateAsync(authorizationToken);
        }

        public async Task DeleteAccountAsync(string accountId)
        {
            Account accountToBeRemoved = await _accountRepository.GetByIdAsync(accountId);
            if (accountToBeRemoved == null)
            {
                throw new EntityNotFoundException("Account", accountId);
            }

            await _accountRepository.DeleteAsync(accountToBeRemoved);
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            Account account = await _accountCustomQueries.GetByUsernameOrEmailAsync(
                request.UsernameOrEmail
            );

            if (account == null)
            {
                throw new EntityNotFoundException("Account", request.UsernameOrEmail);
            }

            if (!_cryptoUtils.VerifyPassword(request.Password, account.PasswordHash))
            {
                throw new InvalidCredentialsException(request.UsernameOrEmail);
            }

            string token = _tokenService.GenerateJWTToken(account.Id);

            account.LastLoginAt = DateTime.UtcNow;

            await _accountRepository.UpdateAsync(account);

            return new AuthResponse
            {
                AccountId = account.Id,
                Token = token,
                Email = account.Email,
                Username = account.Username
            };
        }

        public async Task RequestPasswordResetAsync(PasswordResetRequest request)
        {
            Account account = await _accountCustomQueries.GetByUsernameOrEmailAsync(
                request.UsernameOrEmail
            );

            if (account == null)
            {
                throw new EntityNotFoundException("Account", request.UsernameOrEmail);
            }

            // Create a password reset token
            string token = _tokenService.GenerateAuthorizationToken();

            AuthorizationToken newAuthorizationToken = new AuthorizationToken
            {
                AccountId = account.Id,
                Token = token,
                CreatedAt = DateTime.UtcNow
            };

            // Save the token in the database
            await _authorizationTokenRepository.AddAsync(newAuthorizationToken);

            // TODO: Send email with the token
            try
            {
                string confirmationCodeEmail = await _mailTemplateRepository.ComposeEmailAsync(
                    EmailTemplates.CONFIRMATION_CODE,
                    new Dictionary<string, string> { { "ConfirmationCode", token } }
                );

                await _mailSender.SendAsync(
                    account.Username,
                    account.Email,
                    "Rivalus: Código de confirmación",
                    confirmationCodeEmail
                );
            }
            catch
            {
                // TODO: Handle error
            }
        }

        public async Task<AuthResponse> SignupAsync(SignupRequest request)
        {
            Account emailCheckAccount = await _accountCustomQueries.GetByEmailAsync(request.Email);
            if (emailCheckAccount != null)
            {
                throw new EmailAlreadyInUseException(request.Email);
            }

            Account usernameCheckAccount = await _accountCustomQueries.GetByUsernameAsync(
                request.Username
            );
            if (usernameCheckAccount != null)
            {
                throw new UsernameAlreadyInUseException(request.Username);
            }

            // At this point, we know that the email and username are not in use.

            string passwordHash = _cryptoUtils.HashPassword(request.Password);

            Account account = new Account
            {
                Id = Guid.NewGuid().ToString(),
                Email = request.Email,
                Username = request.Username,
                PasswordHash = passwordHash,
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null
            };

            await _accountRepository.AddAsync(account);

            string token = _tokenService.GenerateJWTToken(account.Id);

            return new AuthResponse
            {
                AccountId = account.Id,
                Token = token,
                Email = account.Email,
                Username = account.Username
            };
        }
    }
}
