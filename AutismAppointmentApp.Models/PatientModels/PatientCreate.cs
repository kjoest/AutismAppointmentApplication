using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Models.PatientModels
{
    public class PatientCreate
    {
        [Required]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "Gender: ")]
        public string Gender { get; set; }
        [Required]
        [Display(Name = "Therapy Needed: ")]
        public string TherapyNeeded { get; set; }
        [Required]
        [Display(Name = "Date of Birth: ")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [DefaultValue(false)]
        [Display(Name = "Has Therapy: ")]
        public bool HasTherapy { get; set; }
        public string UserId { get; set; }
    }
}
