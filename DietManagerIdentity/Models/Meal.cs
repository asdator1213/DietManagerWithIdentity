using System;
using System.Collections.Generic;

namespace DietManagerIdentity.Models
{
    public enum MealType
    {
        Breakfast,
        SecondBreakfast,
        Dinner,
        AfternoonSnack,
        Supper
    }
    public class Meal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfAddition { get; set; }
        public MealType MealType { get; set; }
        public string ShoppingList { get; set; }

        public virtual ICollection<Diet> Diets { get; set; }
    }
}