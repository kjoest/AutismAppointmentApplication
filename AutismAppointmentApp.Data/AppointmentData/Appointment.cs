using AutismAppointmentApp.Data.PatientData;
using AutismAppointmentApp.Data.TherapistData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Data.AppointmentData
{
    public class Appointment
    {
        [Key]
        [Display(Name = "Appointment ID: ")]
        public int AppointmentId { get; set; }
        public Guid OwnerId { get; set; }
        [Required]
        [Display(Name = "Appointment Date: ")]
        public DateTime? DateOfAppointment { get; set; }
        [Required]
        [Display(Name = "Appointment Details: ")]
        public string DetailOfAppointment { get; set; }

        [ForeignKey(nameof(Therapist))]
        [Display(Name = "Therapist ID: ")]
        public int TherapistId { get; set; }
        public virtual Therapist Therapist { get; set; }
        [ForeignKey(nameof(Patient))]
        [Display(Name = "Patient ID: ")]
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        [Required]
        [Display(Name = "Created Date: ")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified Date: ")]
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
