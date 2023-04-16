using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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

        public async Task RegisterAccountAsync(string email, string password, string username)
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
                await GenerateEmailConfirmationTokenAsync(user);
            }

            throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description)));
        }

        public async Task SignInAsync(string email, string password)
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

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, normalizedEmail));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            var principal = new ClaimsPrincipal(identity);
            await _httpContextAccessor.HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties
                {
                    IsPersistent = false,
                    AllowRefresh = true,
                    ExpiresUtc = DateTime.Now.AddMinutes(60)
                });
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();

            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Generates an email confirmation token for the specified user and sends an email with the confirmation link.
        /// </summary>
        /// <param name="user">The user for whom the email confirmation token is generated.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task GenerateEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var request = _httpContextAccessor.HttpContext.Request;
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var uri = new UriBuilder(request.Scheme, request.Host.Host);
            if (request.Host.Port.HasValue)
            {
                uri.Port = (int)request.Host.Port;
            }
            uri.Path = "account/email-confirmation";
            uri.Query = $"email={user.Email}&code={System.Web.HttpUtility.UrlEncode(code)}";

            var body = await _templateService.RenderTemplateAsync("Templates/EmailConfirmationTemplate", new ConfirmationEmailModel { Url = uri.ToString() });

            await _emailService.SendEmail(user.Email, "Take ai meal - potwierdzenie adresu email", body);
        }
    }
}
