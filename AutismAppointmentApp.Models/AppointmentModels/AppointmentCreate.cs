using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutismAppointmentApp.Models.AppointmentModels
{
    public class AppointmentCreate
    {
        [Required]
        [Display(Name = "Appointment Date: ")]
        public DateTime? DateOfAppointment { get; set; }
        [Required]
        [Display(Name = "Appointment Details: ")]
        public string DetailOfAppointment { get; set; }
        [Required]
        [Display(Name = "Therapist: ")]
        public int TherapistId { get; set; }
        [Required]
        [Display(Name = "Patient: ")]
        public int PatientId { get; set; }

        public IEnumerable<SelectListItem> Patients { get; set; }
        public IEnumerable<SelectListItem> Therapists { get; set; }
    }
}
