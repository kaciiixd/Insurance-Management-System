using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models.ViewModels
{
    public class AddInsuranceRequest
    {
        [Required]
        public string InsuranceType { get; set; }
        [Required]
        public int Sum { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public DateTime ValidFrom { get; set; }
        [Required]
        public DateTime ValidUntil { get; set; }


        public Guid SelectedClientId { get; set; }
        public IEnumerable<SelectListItem> Clients { get; set; }
    }
}
