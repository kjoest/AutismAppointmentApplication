using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Models.TherapistModels
{
    public class  TherapistEdit
    {
        [Display(Name = "Therapist ID: ")]
        public int TherapistId { get; set; }
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }
        [Display(Name = "Specialization: ")]
        public string TherapySpecialist { get; set; }
        [DefaultValue(false)]
        [Display(Name = "Certified: ")]
        public bool IsCertified { get; set; }
    }
}
