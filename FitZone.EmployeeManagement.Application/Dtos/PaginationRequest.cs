using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Dtos
{
    public record PaginationRequest(int PageIndex = 0, int PageSize = 10);
}
