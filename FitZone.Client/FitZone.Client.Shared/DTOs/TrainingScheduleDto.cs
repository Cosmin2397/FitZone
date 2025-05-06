using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitZone.Client.Shared.DTOs
{
    public class TrainingScheduleDto
    {
        public Guid Id { get; set; }

        public Guid ClientId { get; set; }

        public string ClientName { get; set; }

        public string ScheduleStatus { get; set; }
    }
}
