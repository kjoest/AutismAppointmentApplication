using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AutismAppointmentApp.Models.AppointmentModels
{
    public class AppointmentEdit
    {
        [Display(Name = "Appointment ID: ")]
        public int AppointmentId { get; set; }
        [Display(Name = "Appointment Date: ")]
        public DateTime? DateOfAppointment { get; set; }
        [Display(Name = "Appointment Details: ")]
        public string DetailOfAppointment { get; set; }
        [Display(Name = "Therapist ID: ")]
        public int TherapistId { get; set; }
        public string UserId { get; set; }

        public IEnumerable<SelectListItem> Therapists { get; set; }
    }
}
