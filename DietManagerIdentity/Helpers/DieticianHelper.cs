using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DietManagerIdentity.Models;

namespace DietManagerIdentity.Helpers
{
    public static class DieticianHelper
    {
        private static ApplicationDbContext _db = new ApplicationDbContext();

        public static int GetLoggedDieticianId(ApplicationUser dietician)
        {
            return _db.Dieticians
                .Single(p => p.ApplicationUser.Id == dietician.Id).Id;
        }
    }
}