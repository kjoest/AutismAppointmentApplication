using AutismAppointmentApp.Data;
using AutismAppointmentApp.Data.AppointmentData;
using AutismAppointmentApp.Models.AppointmentModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutismAppointmentApp.Services.AppointmentServices
{
    public class AppointmentService : IAppointmentService
    {
        public bool CreateAppointment(AppointmentCreate model)
        {
            var entity = new Appointment()
            {
                OwnerId = Guid.Parse(model.UserId),
                DateOfAppointment = model.DateOfAppointment,
                DetailOfAppointment = model.DetailOfAppointment,
                TherapistId = model.TherapistId,
                PatientId = model.PatientId,
                CreatedUtc = DateTimeOffset.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Appointments.Add(entity);
                return ctx.SaveChanges() > 0;
            }
        }

        public IEnumerable<AppointmentListDetail> GetAllAppointments(string userId)
        {
            var guid = Guid.Parse(userId);

            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                    .Appointments
                    .Where(a => a.OwnerId == guid)
                    .Select(
                        a =>
                            new AppointmentListDetail
                            {
                                AppointmentId = a.AppointmentId,
                                DateOfAppointment = a.DateOfAppointment,
                                DetailOfAppointment = a.DetailOfAppointment,
                                TherapistId = a.TherapistId,
                                PatientId = a.PatientId,
                                CreatedUtc = a.CreatedUtc,
                                ModifiedUtc = a.ModifiedUtc
                            });

                return query.ToArray();
            }
        }

        public AppointmentDetail GetAppointmentById(int id, string userId)
        {
            var guid = Guid.Parse(userId);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Appointments
                    .Single(a => a.AppointmentId == id && a.OwnerId == guid);
                return new AppointmentDetail
                {
                    AppointmentId = entity.AppointmentId,
                    DateOfAppointment = entity.DateOfAppointment,
                    DetailOfAppointment = entity.DetailOfAppointment,
                    TherapistId = entity.TherapistId,
                    PatientId = entity.PatientId,
                    CreatedUtc = entity.CreatedUtc,
                    ModifiedDate = entity.ModifiedUtc
                };
            }
        }

        public bool UpdateAppointment(AppointmentEdit model)
        {
            var guid = Guid.Parse(model.UserId);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Appointments
                    .Single(a => a.AppointmentId == model.AppointmentId && a.OwnerId == guid);

                entity.AppointmentId = model.AppointmentId;
                entity.DateOfAppointment = model.DateOfAppointment;
                entity.DetailOfAppointment = model.DetailOfAppointment;
                entity.TherapistId = model.TherapistId;
                entity.ModifiedUtc = DateTimeOffset.Now;

                return ctx.SaveChanges() > 0;
            }
        }

        public bool DeleteAppointment(int id, string userId)
        {
            var guid = Guid.Parse(userId);

            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                    .Appointments
                    .Single(a => a.AppointmentId == id && a.OwnerId == guid);

                ctx.Appointments.Remove(entity);

                return ctx.SaveChanges() > 0;
            }
        }
    }
}
