using AutismAppointmentApp.Models.TherapistModels;
using System.Collections.Generic;

namespace AutismAppointmentApp.Services.TherapistServices
{
    public interface ITherapistService
    {
        bool CreateTherapist(TherapistCreate model);
        bool DeleteTherapist(int id, string userId);
        IEnumerable<TherapistListDetail> GetAllTherapists(string userId);
        TherapistDetail GetTherapistById(int id, string userId);
        bool UpdateTherapist(TherapistEdit model);
    }
}