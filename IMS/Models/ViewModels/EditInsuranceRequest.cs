using System;
using System.ComponentModel.DataAnnotations;

namespace IMS.Models.ViewModels
{
    public class EditInsuranceRequest
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Insurance Type is required")]
        public string InsuranceType { get; set; }

        [Required(ErrorMessage = "Insured Sum is required")]
        public int Sum { get; set; }

        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Valid From date is required")]
        public DateTime ValidFrom { get; set; }

        [Required(ErrorMessage = "Valid Until date is required")]
        public DateTime ValidUntil { get; set; }
    }
}
