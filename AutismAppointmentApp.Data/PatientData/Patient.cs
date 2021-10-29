using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Data.PatientData
{
    public class Patient
    {
        [Key]
        [Display(Name = "Patient ID: ")]
        public int PatientId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        [Display(Name = "First Name: ")]
        public string FirstName { get; set; }
        [Required]
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

        [Required]
        [Display(Name = "Created Date: ")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified Date: ")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
