using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FitZone.Client.Shared.DTOs.Auth
{
    public class RoleDto : IdentityRole<Guid>
    {
        public RoleDto() : base() { }

        public RoleDto(string roleName) : base()
        {
            Name = roleName;
            Id = Guid.NewGuid();
        }
    }
}
