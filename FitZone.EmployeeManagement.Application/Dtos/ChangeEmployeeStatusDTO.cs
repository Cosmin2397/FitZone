﻿using FitZone.EmployeeManagement.Domain.Enums;
using FitZone.EmployeeManagement.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.EmployeeManagement.Application.Dtos
{
    public record ChangeEmployeeStatusDTO(Guid id,  string status);
}
