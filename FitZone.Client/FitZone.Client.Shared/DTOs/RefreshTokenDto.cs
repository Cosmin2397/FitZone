using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.DTOs
{
    public class RefreshTokenDto
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
