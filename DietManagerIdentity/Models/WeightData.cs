using System;

namespace DietManagerIdentity.Models
{
    public class WeightData
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }

        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
    }
}