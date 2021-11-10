using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Models.TherapistModels
{
    public class TherapistCreate
    {
        [Required]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Specialization: ")]
        public string TherapySpecialist { get; set; }
        [Required]
        [DefaultValue(false)]
        [Display(Name = "Certified: ")]
        public bool IsCertified { get; set; }
        public string UserId { get; set; }
    }
}
