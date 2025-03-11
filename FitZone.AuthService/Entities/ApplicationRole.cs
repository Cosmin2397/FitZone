using Microsoft.AspNetCore.Identity;

namespace FitZone.AuthService.Entities
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() : base() { }

        public ApplicationRole(string roleName) : base()
        {
            Name = roleName;
            Id = Guid.NewGuid();
        }
    }

}
