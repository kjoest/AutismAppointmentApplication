using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Models.PatientModels
{
    public class PatientListDetail
    {
        [Display(Name = "Patient ID: ")]
        public int PatientId { get; set; }
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name: ")]
        public string LastName { get; set; }
        [Display(Name = "Full Name: ")]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        [Display(Name = "Gender: ")]
        public string Gender { get; set; }
        [Display(Name = "Therapy Needed: ")]
        public string TherapyNeeded { get; set; }
        [Display(Name = "Date of Birth: ")]
        public DateTime DateOfBirth { get; set; }
        [Display(Name = "Address: ")]
        public string Address { get; set; }
        [DefaultValue(false)]
        [Display(Name = "Has Therapy: ")]
        public bool HasTherapy { get; set; }

        [Display(Name = "Created Date: ")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified Date: ")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
