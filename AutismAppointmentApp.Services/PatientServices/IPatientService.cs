using AutismAppointmentApp.Models.PatientModels;
using System.Collections.Generic;

namespace AutismAppointmentApp.Services.PatientServices
{
    public interface IPatientService
    {
        bool CreatePatient(PatientCreate model, string path);
        bool DeletePatient(int id, string userId);
        IEnumerable<PatientListDetail> GetAllPatients(string userId);
        PatientDetail GetPatientById(int id, string userId);
        bool UpdatePatient(PatientEdit model);
    }
}