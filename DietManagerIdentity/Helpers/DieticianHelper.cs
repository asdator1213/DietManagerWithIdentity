using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DietManagerIdentity.Models;

namespace DietManagerIdentity.Helpers
{
    public static class DieticianHelper
    {
        private static readonly ApplicationDbContext Db = new ApplicationDbContext();

        public static int GetLoggedDieticianId(ApplicationUser dietician)
        {
            return Db.Dieticians
                .Single(p => p.ApplicationUser.Id == dietician.Id).Id;
        }
    }
}