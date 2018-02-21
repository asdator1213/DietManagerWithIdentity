using System.Linq;
using DietManagerIdentity.Models;
using DietManagerIdentity.ViewModels;

namespace DietManagerIdentity.Helpers
{
    public static class PatientHelper
    {
        public static PatientVm CreatePatientViewModel(Patient patient)
        {
            return new PatientVm()
            {
                PatientID = patient.Id,
                FullName = patient.FullName,
                DateOfAddition = patient.DateOfAddition,
                Height = patient.Height,
                Weight = patient.Weight,
                PatientAge = patient.PatientAge,
                Dislikes = patient.Dislikes,
                Contraindications = patient.Contraindications,
                Allergy = patient.Allergy,
                NumberOfConsultation = patient.NumberOfConsultation,
                PlannedWeight = patient.PlannedWeight,
                DietLength = patient.DietLength,
                WagesList = patient.WeightDatas.Select(p => p.Weight).ToArray(),
                WagesDates = patient.WeightDatas.Select(p => p.Date).ToArray(),
                DieticianID = patient.DieticianId,
                DietID = patient.DietId,
                Diet = patient.Diet
            };
        }

        public static Patient FillPatientFromViewModel(PatientVm patient)
        {
            return new Patient()
            {
                Id = patient.PatientID,
                FullName = patient.FullName,
                DateOfAddition = patient.DateOfAddition,
                Height = patient.Height,
                Weight = patient.Weight,
                PatientAge = patient.PatientAge,
                Dislikes = patient.Dislikes,
                Contraindications = patient.Contraindications,
                Allergy = patient.Allergy,
                NumberOfConsultation = patient.NumberOfConsultation,
                PlannedWeight = patient.PlannedWeight,
                DietLength = patient.DietLength,

                DieticianId = patient.DieticianID,
                DietId = patient.DietID
            };
        }
    }
}