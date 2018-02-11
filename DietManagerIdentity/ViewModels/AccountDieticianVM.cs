using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DietManagerIdentity.ViewModels
{
    public class AccountDieticianVM
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }
        public string Login { get; set; }
    }
}