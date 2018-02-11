using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DietManagerIdentity.Models
{
    public class Diet
    {
        public int Id { get; set; }
        [Display(Name = "Diet's name")]
        public string Name { get; set; }
        public DateTime DateOfAddition { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
        public virtual ICollection<Meal> Meals { get; set; }
    }
}