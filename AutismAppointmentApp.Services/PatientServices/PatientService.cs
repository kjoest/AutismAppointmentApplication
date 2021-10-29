using AutismAppointmentApp.Data;
using AutismAppointmentApp.Data.PatientData;
using AutismAppointmentApp.Models.PatientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Services.PatientServices
{
    public class PatientService
    {
        private readonly Guid _userId;

        public PatientService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreatePatient(PatientCreate model)
        {
            var entity =
                new Patient()
                {
                    OwnerId = _userId,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    TherapyNeeded = model.TherapyNeeded,
                    DateOfBirth = model.DateOfBirth,
                    HasTherapy = model.HasTherapy,
                    CreatedUtc = DateTime.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Patients.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<PatientListDetail> GetAllPatients()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Patients
                    .Where(s => s.OwnerId == _userId)
                    .Select(
                        s =>
                            new PatientListDetail
                            {
                                PatientId = s.PatientId,
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                Gender = s.Gender,
                                TherapyNeeded = s.TherapyNeeded,
                                DateOfBirth = s.DateOfBirth,
                                HasTherapy = s.HasTherapy,
                                CreatedUtc = s.CreatedUtc,
                                ModifiedUtc = s.ModifiedUtc
                            });

                return query.ToArray();
            }
        }

        public PatientDetail GetPatientById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Patients
                    .Single(s => s.PatientId == id && s.OwnerId == _userId);
                return new PatientDetail
                {
                    PatientId = entity.PatientId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Gender = entity.Gender,
                    TherapyNeeded = entity.TherapyNeeded,
                    DateOfBirth = entity.DateOfBirth,
                    HasTherapy = entity.HasTherapy,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc
                };
            }
        }

        public bool UpdatePatient(PatientEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Patients
                    .Single(s => s.PatientId == model.PatientId && s.OwnerId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Gender = model.Gender;
                entity.TherapyNeeded = model.TherapyNeeded;
                entity.HasTherapy = model.HasTherapy;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() > 0;
            }
        }

        public bool DeletePatient(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Patients
                    .Single(s => s.PatientId == id && s.OwnerId == _userId);

                ctx.Patients.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }
    }
}
