using Microsoft.AspNetCore.Http;
using TakeAIMeal.API.Services.Extensions;
using TakeAIMeal.API.Services.Interfaces;

namespace TakeAIMeal.API.Services.Logic
{
    public class UserIdentityService : IUserIdentityService
    {
        private readonly string _userName;
        private readonly int _userId;
        private readonly string _emailAddress;
        public UserIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;
            _userName = httpContext.User.GetUserName();
            _userId = httpContext.User.GetUserId().GetValueOrDefault();
            _emailAddress = httpContext.User.GetUserEmail();
        }

        /// <inheritdoc/>
        public string UserName => _userName;

        /// <inheritdoc/>
        public int UserId => _userId;

        /// <inheritdoc/>
        public string EmailAddress => _emailAddress;
    }
}
