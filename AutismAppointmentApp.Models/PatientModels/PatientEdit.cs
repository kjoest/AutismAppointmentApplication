using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Models.PatientModels
{
    public class  PatientEdit
    {
        [Display(Name = "Patient ID: ")]
        public int PatientId { get; set; }
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }
        [Display(Name = "Gender: ")]
        public string Gender { get; set; }
        [Display(Name = "Therapy Needed: ")]
        public string TherapyNeeded { get; set; }
        [Display(Name = "Address: ")]
        public string Address { get; set; }
        [Display(Name = "Has Therapy: ")]
        public bool HasTherapy { get; set; }
        public string UserId { get; set; }
        public string ImagePath { get; set; }
    }
}
