using AutismAppointmentApp.Models.AppointmentModels;
using System.Collections.Generic;

namespace AutismAppointmentApp.Services.AppointmentServices
{
    public interface IAppointmentService
    {
        bool CreateAppointment(AppointmentCreate model);
        bool DeleteAppointment(int id, string userId);
        IEnumerable<AppointmentListDetail> GetAllAppointments(string userId);
        AppointmentDetail GetAppointmentById(int id, string userId);
        bool UpdateAppointment(AppointmentEdit model);
    }
}