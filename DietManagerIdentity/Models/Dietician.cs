using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DietManagerIdentity.Models
{
    public class Dietician
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        //public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<Patient> Patients { get; set; }
    }
}