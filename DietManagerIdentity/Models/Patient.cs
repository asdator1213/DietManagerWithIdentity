using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DietManagerIdentity.Models
{
    public class Patient
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        public DateTime DateOfAddition { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int DietLength { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int NumberOfConsultation { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Weight { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int Height { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        [Display(Name = "Patient's age")]
        public int PatientAge { get; set; }

        public string Dislikes { get; set; }

        public string Allergy { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int PlannedWeight { get; set; }

        public string Contraindications { get; set; }

        public int? DietId { get; set; }
        public virtual Diet Diet { get; set; }

        public int DieticianId { get; set; }
        public virtual Dietician Dietician { get; set; }

        public virtual ICollection<WeightData> WeightDatas { get; set; }
    }
}