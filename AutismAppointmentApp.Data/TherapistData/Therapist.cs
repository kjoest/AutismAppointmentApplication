using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Data.TherapistData
{
    public class Therapist
    {
        [Key]
        [Display(Name = "Therapist ID: ")]
        public int TherapistId { get; set; }
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
        [Display(Name = "Specialization: ")]
        public string TherapySpecialist { get; set; }
        [Required]
        [DefaultValue(false)]
        [Display(Name = "Certified: ")]
        public bool IsCertified { get; set; }

        [Required]
        [Display(Name = "Created Date: ")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified Date: ")]
        public DateTimeOffset? ModifiedUtc { get; set; }
        public string ImagePath { get; set; }
    }
}
