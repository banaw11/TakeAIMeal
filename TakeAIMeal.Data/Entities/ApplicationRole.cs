using Microsoft.AspNetCore.Identity;

namespace TakeAIMeal.Data.Entities
{
    public class ApplicationRole : IdentityRole<int>
    {
        public ApplicationRole() : base()
        {
        }

        public ApplicationRole(string roleName) : base(roleName)
        {
        }

    }
}