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
    public class PatientService : IPatientService
    {
        public bool CreatePatient(PatientCreate model, string path)
        {
            var entity =
                new Patient()
                {
                    OwnerId = Guid.Parse(model.UserId),
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Gender = model.Gender,
                    Address = model.Address,
                    TherapyNeeded = model.TherapyNeeded,
                    DateOfBirth = model.DateOfBirth,
                    HasTherapy = model.HasTherapy,
                    CreatedUtc = DateTime.Now,
                    ImagePath = path
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Patients.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<PatientListDetail> GetAllPatients(string userId)
        {
            var guid = Guid.Parse(userId);

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Patients
                    .Where(s => s.OwnerId == guid)
                    .Select(
                        s =>
                            new PatientListDetail
                            {
                                PatientId = s.PatientId,
                                FirstName = s.FirstName,
                                LastName = s.LastName,
                                Gender = s.Gender,
                                TherapyNeeded = s.TherapyNeeded,
                                Address = s.Address,
                                DateOfBirth = s.DateOfBirth,
                                HasTherapy = s.HasTherapy,
                                CreatedUtc = s.CreatedUtc,
                                ModifiedUtc = s.ModifiedUtc
                            });

                return query.ToArray();
            }
        }

        public PatientDetail GetPatientById(int id, string userId)
        {
            var guid = Guid.Parse(userId);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Patients
                    .SingleOrDefault(s => s.PatientId == id && s.OwnerId == guid);
                return new PatientDetail
                {
                    PatientId = entity.PatientId,
                    FirstName = entity.FirstName,
                    LastName = entity.LastName,
                    Gender = entity.Gender,
                    Address = entity.Address,
                    TherapyNeeded = entity.TherapyNeeded,
                    DateOfBirth = entity.DateOfBirth,
                    HasTherapy = entity.HasTherapy,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedUtc = entity.ModifiedUtc,
                    ImagePath = entity.ImagePath,
                };
            }
        }

        public bool UpdatePatient(PatientEdit model)
        {
            var guid = Guid.Parse(model.UserId);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Patients
                    .SingleOrDefault(s => s.PatientId == model.PatientId && s.OwnerId == guid);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Gender = model.Gender;
                entity.Address = model.Address;
                entity.TherapyNeeded = model.TherapyNeeded;
                entity.HasTherapy = model.HasTherapy;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() > 0;
            }
        }

        public bool DeletePatient(int id, string userId)
        {
            var guid = Guid.Parse(userId);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Patients
                    .Single(s => s.PatientId == id && s.OwnerId == guid);

                ctx.Patients.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }
    }
}
