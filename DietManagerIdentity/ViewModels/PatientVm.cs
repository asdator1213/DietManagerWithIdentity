using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DietManagerIdentity.Models;

namespace DietManagerIdentity.ViewModels
{
    public class PatientVm
    {
        public int PatientID { get; set; }

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

        [Required(ErrorMessage = "This field is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int PlannedWeight { get; set; }

        public string Contraindications { get; set; }

        public int[] WagesList { get; set; }
        public DateTime[] WagesDates { get; set; }
        public int? DietID { get; set; }
        public int DieticianID { get; set; }

        public virtual Diet Diet { get; set; }
    }
}