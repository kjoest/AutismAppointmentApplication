using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Models.AppointmentModels
{
    public class AppointmentDetail
    {
        [Display(Name = "Appointment ID: ")]
        public int AppointmentId { get; set; }
        [Display(Name = "Appointment Date: ")]
        public DateTime? DateOfAppointment { get; set; }
        [Display(Name = "Appointment Details: ")]
        public string DetailOfAppointment { get; set; }
        [Display(Name = "Therapist ID: ")]
        public int TherapistId { get; set; }
        [Display(Name = "Patient ID: ")]
        public int PatientId { get; set; }

        [Display(Name = "Created Date: ")]
        public DateTimeOffset CreatedUtc { get; set; }
        [Display(Name = "Modified Date: ")]
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
