using FitZone.SubscriptionValidationService.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitZone.SubscriptionValidationService.Models
{
    public class ClientsAccess
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid GymId { get; set; }

        public Guid ClientId { get; set; }

        public Guid SubscriptionId { get;set; }

        public Role Role { get; set; }

        public ValidationType ValidationType { get; set; }

        public DateTime DataValidare { get; set; }
    }
}
