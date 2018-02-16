using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DietManagerIdentity.Models;

namespace DietManagerIdentity.Controllers
{
    public class DefaultController : Controller
    {
        public List<Meal> Meals = new List<Meal>
        {
            new Meal{ Id = 1, Name = "Kopytka"},
            new Meal{ Id = 2, Name = "Kluski"},
            new Meal{ Id = 3, Name = "Makaron z pestou"}
        };

        public JsonResult GetMealList()
        {
            return Json(Meals, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMealById(int id)
        {
            var meal = Meals.Single(x => x.Id == id);
            return Json(meal, JsonRequestBehavior.AllowGet);
        }


        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
    }
}