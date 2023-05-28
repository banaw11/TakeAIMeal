using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TakeAIMeal.API.Services.Extensions;
using TakeAIMeal.API.Services.Interfaces;
using TakeAIMeal.API.Services.Models;
using TakeAIMeal.Common.Services.Interfaces;
using TakeAIMeal.Data.Entities;

namespace TakeAIMeal.API.Services.Logic
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailService _emailService;
        private readonly ITemplateService _templateService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IHttpContextAccessor httpContextAccessor, IEmailService emailService, ITemplateService templateService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _httpContextAccessor = httpContextAccessor;
            _emailService = emailService;
            _templateService = templateService;
        }

        /// <inheritdoc />
        public async Task<bool> ConfirmEmailAsync(string email, string code)
        {
            var normalizedEmail = _userManager.NormalizeEmail(email);
            var user = await _userManager.FindByEmailAsync(normalizedEmail);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }

            var result = await _userManager.ConfirmEmailAsync(user, code);

            return result.Succeeded;
        }

        /// <inheritdoc />
        public UserModel GetSignedUser()
        {
            var identity = _signInManager.Context.User;
            if(identity?.Identity != null && _signInManager.IsSignedIn(identity))
            {
                return new UserModel
                {
                    Email = identity.GetUserEmail(),
                    UserName = identity.GetUserName()
                };
            }

            return null;
            
        }

        /// <inheritdoc />
        public async Task<bool> RegisterAccountAsync(string email, string password, string username)
        {
            var normalizedEmail = _userManager.NormalizeEmail(email);
            var user = await _userManager.FindByEmailAsync(normalizedEmail);
            if(user != null)
            {
                throw new Exception("Account with this email address already exists");
            }

            user = new ApplicationUser { Email = email, UserName = username};

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                var emailConfirmationResult = await GenerateEmailConfirmationTokenAsync(user);
                if (!emailConfirmationResult)
                {
                    await _userManager.DeleteAsync(user);
                }

                return emailConfirmationResult;
            }

            throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));
        }

        /// <inheritdoc />
        public async Task<UserModel> SignInAsync(string email, string password)
        {
            var normalizedEmail = _userManager.NormalizeEmail(email);
            var user = await _userManager.FindByEmailAsync(normalizedEmail);
            if(user == null)
            {
                throw new Exception("Invalid credentials.");
            }

            if (!user.EmailConfirmed)
            {
                throw new Exception("Email has been not confirmed.");
            }

            var passwordResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!passwordResult.Succeeded)
            {
                throw new Exception("Invalid credentials.");
            }

            var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
            if (!signInResult.Succeeded)
            {
                throw new Exception("Invalid credentials.");
            }

            return new UserModel
            {
                Email = user.Email,
                UserName = user.UserName
            };
        }

        /// <inheritdoc />
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        /// <summary>
        /// Generates an email confirmation token for the specified user and sends an email with the confirmation link.
        /// </summary>
        /// <param name="user">The user for whom the email confirmation token is generated.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task<bool> GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var uri = new UriBuilder(request.Scheme, request.Host.Host);
            if (request.Host.Port.HasValue)
            {
                uri.Port = (int)request.Host.Port;
            }
            uri.Path = "account/email-confirmation";
            uri.Query = $"email={user.Email}&code={code}";

            var body = await _templateService.RenderTemplateAsync("Templates/EmailConfirmationTemplate", new ConfirmationEmailModel { Url = uri.ToString() });

            return await _emailService.SendEmail(user.Email, "Take ai meal - potwierdzenie adresu email", body);
        }
    }
}
